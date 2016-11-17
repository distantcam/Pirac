using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

public class ModuleWeaver
{
    public ModuleDefinition ModuleDefinition { get; set; }

    public Action<string> LogInfo { get; set; }

    public void Execute()
    {
        if (ModuleDefinition.Name != "Pirac.dll")
            return;

        AddGuardsToPiracRunner();
        AddGuardsToPiracContext();
    }

    private void AddGuardsToPiracRunner()
    {
        var piracRunnerType = ModuleDefinition.Types.First(t => t.Name == "PiracRunner");
        var contextSetField = piracRunnerType.Fields.First(t => t.Name == "contextSet");
        var exceptionCtor = ModuleDefinition.ImportReference(ModuleDefinition.ImportReference(typeof(InvalidOperationException)).Resolve().Methods.First(m => m.IsConstructor && m.Parameters.Count == 1));

        foreach (var method in piracRunnerType.Methods)
        {
            if (!method.HasBody) continue;

            if (!method.IsPublic && !method.IsAssembly) continue;

            if (method.Name == "Start" ||
                method.Name == "get_IsContextSet" ||
                method.Name == "get_MainScheduler" ||
                method.Name == "get_BackgroundScheduler" ||
                method.Name == "get_Logger" ||
                method.Name == "GetLogger" ||
                method.Name == "SetContext") continue;

            LogInfo("Adding Guard to " + method.Name);

            method.Body.SimplifyMacros();
            var il = method.Body.GetILProcessor();

            var branch = il.Body.Instructions[0];

            il.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Ldsfld, contextSetField));

            il.Body.Instructions.Insert(1, Instruction.Create(OpCodes.Ldc_I4_0));
            il.Body.Instructions.Insert(2, Instruction.Create(OpCodes.Ceq));
            il.Body.Instructions.Insert(3, Instruction.Create(OpCodes.Brfalse, branch));

            il.Body.Instructions.Insert(4, Instruction.Create(OpCodes.Ldstr, "Pirac has not been started."));
            il.Body.Instructions.Insert(5, Instruction.Create(OpCodes.Newobj, exceptionCtor));
            il.Body.Instructions.Insert(6, Instruction.Create(OpCodes.Throw));

            method.Body.OptimizeMacros();
        }
    }

    private void AddGuardsToPiracContext()
    {
        var piracContextType = ModuleDefinition.Types.First(t => t.Name == "PiracContext");
        var argumentNullExceptionCtor = ModuleDefinition.ImportReference(ModuleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve().Methods
            .First(m => m.IsConstructor && m.Parameters.Count == 2 && m.Parameters[0].ParameterType.Name == "String" && m.Parameters[1].ParameterType.Name == "String"));

        foreach (var setter in piracContextType.Methods.Where(m => m.HasBody && m.IsSetter))
        {
            LogInfo("Adding Guard to " + setter.Name);

            setter.Body.SimplifyMacros();
            var il = setter.Body.GetILProcessor();

            var branch = il.Body.Instructions[0];

            il.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Ldarg_1));
            il.Body.Instructions.Insert(1, Instruction.Create(OpCodes.Ldnull));
            il.Body.Instructions.Insert(2, Instruction.Create(OpCodes.Ceq));
            il.Body.Instructions.Insert(3, Instruction.Create(OpCodes.Brfalse, branch));

            il.Body.Instructions.Insert(4, Instruction.Create(OpCodes.Ldstr, "value"));
            il.Body.Instructions.Insert(5, Instruction.Create(OpCodes.Ldstr, "cannot set a property of PiracContext to null."));
            il.Body.Instructions.Insert(6, Instruction.Create(OpCodes.Newobj, argumentNullExceptionCtor));
            il.Body.Instructions.Insert(7, Instruction.Create(OpCodes.Throw));

            setter.Body.OptimizeMacros();
        }
    }
}
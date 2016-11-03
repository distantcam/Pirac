namespace Pirac.Conventions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AttachmentHelper
    {
        public static void Attach(object attachment, object t)
        {
            var methods = attachment.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.InvokeMethod)
                .Where(m => MatchMethod(m, t.GetType()));

            foreach (var method in methods)
            {
                method.Invoke(attachment, new object[] { t });
            }
        }

        private static bool MatchMethod(MethodInfo method, Type t)
        {
            if (method.Name != "AttachTo" && method.Name != "Pirac.IAttachment<T>.AttachTo")
                return false;

            var parameters = method.GetParameters();

            if (parameters.Length != 1)
                return false;

            return parameters[0].ParameterType.IsAssignableFrom(t);
        }
    }
}
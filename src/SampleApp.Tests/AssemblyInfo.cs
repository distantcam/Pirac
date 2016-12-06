using ApprovalTests.Namers;
using ApprovalTests.Reporters;

[assembly: UseApprovalSubdirectory("TestResults")]
[assembly: UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
using System;
using System.Runtime.InteropServices;
using System.Security;

//[assembly: SecurityCritical]

namespace MuroxGitHubTest.Helper
{
    internal static class NativeMethods
    {
        #region Functions
        [DllImport("kernel32.dll", ExactSpelling = true), SecuritySafeCritical]
        internal static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);
        #endregion

        #region Enumerations
        [Flags]
        internal enum ExecutionState
        {
            Continuous = unchecked((int)0x80000000),
            SystemRequired = 0x1,
            DisplayRequired = 0x2,
            AwayModeRequired = 0x40
        }
        #endregion
    }
}

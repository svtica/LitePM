﻿

using System;
using System.Runtime.InteropServices;

namespace LitePMLauncherService
{
    [StructLayout(LayoutKind.Sequential)]
    class PROCESSINFO
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }
}

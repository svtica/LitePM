

using System.Runtime.InteropServices;

namespace LitePMLauncherService
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct WTS_SESSION_INFO
    {
        public int SessionId;
        public string pWinStationName;
        public int State;
    }
}

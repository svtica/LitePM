
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Namespace Native.Objects

    Public Class SystemInfo



        ' Private constants




        ' Private attributes




        ' Public properties




        ' Other public




        ' Public functions


        ' Wrapper for GetSystemInfo Win32 function
        Public Shared Function GetSystemInfo() As NativeStructs.SystemInfo
            Dim si As NativeStructs.SystemInfo
            Native.Api.NativeFunctions.GetSystemInfo(si)
            Return si
        End Function



        ' Private functions



    End Class

End Namespace

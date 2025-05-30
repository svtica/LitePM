
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Public Class cSystem


    ' Public

    Public Shared Function Hibernate(Optional ByVal force As Boolean = True) As Boolean
        Return NativeFunctions.SetSuspendState(True, force, False)
    End Function

    Public Shared Function Sleep(Optional ByVal force As Boolean = True) As Boolean
        Return NativeFunctions.SetSuspendState(False, force, False)
    End Function

    Public Shared Function Logoff(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Logoff Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Logoff, 0)
        End If
    End Function

    Public Shared Function Lock() As Boolean
        Return NativeFunctions.LockWorkStation()
    End Function

    Public Shared Function Shutdown(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Shutdown Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Shutdown, 0)
        End If
    End Function

    Public Shared Function Restart(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Reboot Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Reboot, 0)
        End If
    End Function

    Public Shared Function Poweroff(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Poweroff Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Poweroff, 0)
        End If
    End Function
End Class

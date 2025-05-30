
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcGetNonFixedInfos

    Private _pid As Integer
    Private _handle As IntPtr

    Public Structure TheseInfos
        Dim gdiO As Integer
        Dim userO As Integer
        Dim affinity As IntPtr
        Public Sub New(ByVal _gdi As Integer, ByVal _user As Integer, _
                       ByVal _affinity As IntPtr)
            gdiO = _gdi
            userO = _user
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByVal handle As IntPtr)
        _pid = pid
        _handle = handle
    End Sub

    Public Sub Process()
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _gdi As Integer = Native.Objects.Process.GetProcessGuiResourceByHandle(_handle, _
                                                    Native.Api.NativeEnums.GuiResourceType.GdiObjects)
                Dim _user As Integer = Native.Objects.Process.GetProcessGuiResourceByHandle(_handle, _
                                                    Native.Api.NativeEnums.GuiResourceType.UserObjects)
                Dim _affinity As IntPtr = Native.Objects.Process.GetProcessAffinityByHandle(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(_gdi, _user, _affinity))
        End Select
    End Sub

End Class

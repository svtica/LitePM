
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackThreadGetOtherInfos

    Private _id As Integer
    Private _handle As IntPtr
    '  Private _deg As GatheredInfos

    Public Structure TheseInfos
        Public affinity As IntPtr
        Public Sub New(ByVal _affinity As IntPtr)
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByVal handle As IntPtr)
        _id = pid
        ' _deg = deg
        _handle = handle
    End Sub

    Public Sub Process()
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _affinity As IntPtr = Native.Objects.Thread.GetThreadAffinityByHandle(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(_affinity))
        End Select
    End Sub

End Class

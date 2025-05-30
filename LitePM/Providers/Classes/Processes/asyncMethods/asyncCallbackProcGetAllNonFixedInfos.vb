
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcGetAllNonFixedInfos

    Public Event HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As Native.Api.Structs.SystemProcessInformation64, ByVal msg As String)

    Private _process As cProcess

    Public Sub New(ByRef process As cProcess)
        _process = process
    End Sub

    ' This function is only called for WMI connexion
    ' It is called when user want to refresh statistics of a process in detailed view
    Public Sub Process(ByVal state As Object)

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket


            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim _newInfos As New Native.Api.Structs.SystemProcessInformation64
                Dim ret As Boolean = _
                    Wmi.Objects.Process.RefreshProcessInformationsById(_process.Infos.ProcessId, _
                                                                ProcessProvider.wmiSearcher, msg, _newInfos)

                RaiseEvent HasGotAllNonFixedInfos(ret, _newInfos, msg)

            Case Else
                ' Local
                ' OK, normally no call for Process method for a local connection

        End Select
    End Sub

End Class

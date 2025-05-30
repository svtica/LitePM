
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackJobTerminateJob

    Private _deg As HasTerminatedJob

    Public Delegate Sub HasTerminatedJob(ByVal Success As Boolean, ByVal jobName As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasTerminatedJob)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public jobName As String
        Public newAction As Integer
        Public Sub New(ByVal name As String, _
                       ByVal act As Integer)
            newAction = act
            jobName = name
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected = False Then
            Exit Sub
        End If

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.JobTerminate, pObj.jobName)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Job.TerminateJobByJobName(pObj.jobName)
                _deg.Invoke(ret, pObj.jobName, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class

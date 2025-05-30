
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackJobAddProcess

    Private _deg As HasAddedProcessesToJob

    Public Delegate Sub HasAddedProcessesToJob(ByVal Success As Boolean, ByVal pid() As Integer, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasAddedProcessesToJob)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid() As Integer
        Public jobName As String
        Public newAction As Integer
        Public Sub New(ByVal pi() As Integer, _
                       ByVal name As String, _
                       ByVal act As Integer)
            jobName = name
            newAction = act
            pid = pi
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.JobAddProcessToJob, pObj.jobName, pObj.pid)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Job.AddProcessToJobByIds(pObj.pid, pObj.jobName)
                _deg.Invoke(ret, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class

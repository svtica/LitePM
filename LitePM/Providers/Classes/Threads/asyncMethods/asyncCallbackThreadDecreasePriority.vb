
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackThreadDecreasePriority

    Private _deg As HasDecreasedPriority

    Public Delegate Sub HasDecreasedPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasDecreasedPriority)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public level As Integer
        Public newAction As Integer
        Public pid As Integer
        Public Sub New(ByVal _id As Integer, _
                        ByVal _level As Integer, _
                       ByVal action As Integer, Optional ByVal processId As Integer = 0)
            newAction = action
            id = _id
            level = _level
            pid = processId
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ThreadDecreasePriority, pObj.pid, pObj.id, pObj.level)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _level2 As System.Diagnostics.ThreadPriorityLevel
                Select Case pObj.level
                    Case ThreadPriorityLevel.AboveNormal
                        _level2 = ThreadPriorityLevel.Normal
                    Case ThreadPriorityLevel.BelowNormal
                        _level2 = ThreadPriorityLevel.Lowest
                    Case ThreadPriorityLevel.Highest
                        _level2 = ThreadPriorityLevel.AboveNormal
                    Case ThreadPriorityLevel.Idle
                        '
                    Case ThreadPriorityLevel.Lowest
                        _level2 = ThreadPriorityLevel.Idle
                    Case ThreadPriorityLevel.Normal
                        _level2 = ThreadPriorityLevel.BelowNormal
                    Case ThreadPriorityLevel.TimeCritical
                        _level2 = ThreadPriorityLevel.Highest
                End Select

                Dim r As Boolean = Native.Objects.Thread.SetThreadPriorityById(pObj.id, _level2)
                _deg.Invoke(r, Native.Api.Win32.GetLastError, pObj.newAction)

        End Select
    End Sub

End Class


' Lite Process Monitor









'




'




Option Strict On

Namespace Native.Functions

    Public Class Thread



        ' Private constants




        ' Private attributes




        ' Public properties



        ' Other public




        ' Public functions


        ' Return a class from an int (concerning priority)
        Public Shared Function GetThreadPriorityClassFromInt(ByVal priority As Integer) As System.Diagnostics.ThreadPriorityLevel
            If priority >= 15 Then
                Return ThreadPriorityLevel.TimeCritical
            ElseIf priority >= 2 Then
                Return ThreadPriorityLevel.Highest
            ElseIf priority >= 1 Then
                Return ThreadPriorityLevel.AboveNormal
            ElseIf priority >= 0 Then
                Return ThreadPriorityLevel.Normal
            ElseIf priority >= -1 Then
                Return ThreadPriorityLevel.BelowNormal
            ElseIf priority >= -2 Then
                Return ThreadPriorityLevel.Lowest
            Else
                Return ThreadPriorityLevel.Idle
            End If
        End Function



        ' Private functions



    End Class

End Namespace

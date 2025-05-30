
' Lite Process Monitor









'




'



Option Strict On

Namespace Scripting.Items

    Public Class Tools


        ' Private constants




        ' Private attributes




        ' Public properties




        ' Public functions


        ' Remove spaces/tabs from a string
        Public Shared Function RemoveSpaces(ByVal s As String) As String
            If s IsNot Nothing Then
                Return s.Trim
            Else
                Return Nothing
            End If
        End Function



        ' Private functions



    End Class

End Namespace

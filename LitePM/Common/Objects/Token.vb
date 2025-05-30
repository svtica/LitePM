
' Lite Process Monitor


Option Strict On

Namespace Common.Objects

    Public Class Token


        ' Public functions


        ' Return current user name
        Public Shared ReadOnly Property CurrentUserName() As String
            Get
                Static retrieved As Boolean = False
                Static value As String = ""
                If retrieved = False Then
                    retrieved = True
                    value = System.Security.Principal.WindowsIdentity.GetCurrent.Name
                End If
                Return value
            End Get
        End Property


    End Class

End Namespace

' Lite Process Monitor









'




'




Option Strict On

<Serializable()> Public Class taskInfos
    Inherits windowInfos



    ' Public


    ' Constructor of this class
    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef window As windowInfos)
        MyBase.New(window)
    End Sub

    ' Retrieve all information's names availables
    Public Overloads Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(12) As String

        s(0) = "Caption"
        s(1) = "Process"
        s(2) = "CpuUsage"
        s(3) = "IsTask"
        s(4) = "Enabled"
        s(5) = "Visible"
        s(6) = "ThreadId"
        s(7) = "Height"
        s(8) = "Width"
        s(9) = "Top"
        s(10) = "Left"
        s(11) = "Opacity"
        s(12) = "Handle"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        Return s
    End Function

End Class

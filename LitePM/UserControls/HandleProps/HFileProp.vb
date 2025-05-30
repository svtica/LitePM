
' Lite Process Monitor









'




'




Option Strict On

Public Class HFileProp
    Inherits HXXXProp

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()
    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Dim bFileExists As Boolean = IO.File.Exists(Me.TheHandle.Infos.Name)
        Dim bDirExists As Boolean = IO.Directory.Exists(Me.TheHandle.Infos.Name)
        Dim _local As Boolean = (Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection)

        bFileExists = bFileExists And _local
        bDirExists = bDirExists And _local

        Me.cmdFileDetails.Enabled = bFileExists
        Me.cmdOpenDirectory.Enabled = bDirExists Or bFileExists
        Me.cmdOpen.Enabled = Me.cmdOpenDirectory.Enabled

        If _local Then
            If bFileExists Then
                Me.lblFileExists.ForeColor = Color.DarkGreen
                Me.lblFileExists.Text = "File exists"
            Else
                If bDirExists Then
                    Me.lblFileExists.ForeColor = Color.DarkGreen
                    Me.lblFileExists.Text = "Directory exists"
                Else
                    Me.lblFileExists.ForeColor = Color.DarkRed
                    Me.lblFileExists.Text = "Unknown file"
                End If
            End If
        Else
            Me.lblFileExists.ForeColor = Color.Black
            Me.lblFileExists.Text = "Remote file"
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        cFile.ShowFileProperty(Me.TheHandle.Infos.Name, Me.Handle)
    End Sub

    Private Sub cmdOpenDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpenDirectory.Click
        If IO.Directory.Exists(Me.TheHandle.Infos.Name) Then
            cFile.OpenADirectory(Me.TheHandle.Infos.Name)
        Else
            cFile.OpenDirectory(Me.TheHandle.Infos.Name)
        End If
    End Sub

    Private Sub HFileProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Common.Misc.SetToolTip(Me.cmdDetails, "Details about the object")
        Common.Misc.SetToolTip(Me.cmdOpen, "Open properties of item")
        Common.Misc.SetToolTip(Me.cmdOpenDirectory, "Open directory")
    End Sub

    Private Sub cmdFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFileDetails.Click
        Common.Misc.DisplayDetailsFile(Me.TheHandle.Infos.Name)
    End Sub
End Class

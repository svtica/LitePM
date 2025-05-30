
' Lite Process Monitor


Option Strict On

Imports Common.Misc

Public Class frmChooseClientIp

    Private _chosenIp As String = ""
    Private _nics As List(Of Native.Api.Structs.NicDescription)

    Public Sub New(ByVal nics As List(Of Native.Api.Structs.NicDescription))
        InitializeComponent()
        _nics = nics
    End Sub

    Public Property ChosenIp() As String
        Get
            Return _chosenIp
        End Get
        Set(ByVal value As String)
            _chosenIp = value
        End Set
    End Property

    Private Sub frmChooseClientIp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.CloseWithEchapKey(Me)
        SetToolTip(Me.lvNIC, "List of available netword card interface")
        SetToolTip(Me.cmdExit, "Cancel")
        SetToolTip(Me.cmdOk, "Use selected netword card interface")
        Native.Functions.Misc.SetTheme(Me.lvNIC.Handle)

        ' Display NICs
        For Each nic As Native.Api.Structs.NicDescription In _nics
            Dim it As New ListViewItem(nic.Name)
            it.SubItems.Add(nic.Ip)
            it.SubItems.Add(nic.Description)
            it.Tag = nic
            Me.lvNIC.Items.Add(it)
        Next

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.ChosenIp = CType(Me.lvNIC.SelectedItems(0).Tag, Native.Api.Structs.NicDescription).Ip
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lvNIC_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvNIC.MouseDoubleClick
        If Me.lvNIC.SelectedItems.Count = 1 Then
            Call cmdOk_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvNIC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvNIC.SelectedIndexChanged
        Me.cmdOk.Enabled = (Me.lvNIC.SelectedItems.Count = 1)
    End Sub
End Class
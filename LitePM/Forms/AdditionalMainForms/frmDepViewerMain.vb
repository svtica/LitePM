Option Strict On

Imports System.Runtime.InteropServices
Imports Common.Misc


Public Class frmDepViewerMain

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Dim tree As NativeDependenciesTree

    Public Sub OpenReferences(ByVal asmFile As String)

        Me.Text = "Dependencies - " & asmFile
        Try
            tree = New NativeDependenciesTree(asmFile)
            tvDepends.Nodes.Clear()
            tvDepends.Nodes.Add(Me.CreateExpandableReferenceNode(tree.MainDll))

            lvAllDeps.BeginUpdate()
            lvAllDeps.Items.Clear()
            For Each dep As NativeDependenciesTree.NativeDependency In tree.GetAllDependencies()
                If dep.Resolved Then
                    Dim fvi As FileVersionInfo = FileVersionInfo.GetVersionInfo(dep.PE.FileName)
                    lvAllDeps.Items.Add(New ListViewItem(New String() {System.IO.Path.GetFileName(dep.FileName), dep.PE.TimeStamp.ToString("dd/MM/yyyy HH:mm:ss"), dep.PE.NTHeader.SizeOfImage.ToString(), dep.PE.Machine.ToString(), dep.PE.NTHeader.Subsystem.ToString(), fvi.ProductName, fvi.CompanyName, fvi.FileVersion, fvi.ProductVersion, String.Format("{0}.{1}", dep.PE.OptionalHeader.MajorLinkerVersion, dep.PE.OptionalHeader.MinorLinkerVersion), dep.PE.FileName}, "dll"))
                Else
                    lvAllDeps.Items.Add(New ListViewItem(New String() {System.IO.Path.GetFileName(dep.FileName)}, "unresolved"))
                End If
            Next
            lvAllDeps.EndUpdate()

            ' Select first node in tv
            Me.tvDepends.SelectedNode = Me.tvDepends.Nodes(0)

            Dim refNode As NativeDependenciesTree.NativeDependency = DirectCast(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency)
            statusFile.Text = refNode.PE.FileName
            If refNode.Resolved Then
                Me.ShowAssemblyInfos(refNode)
            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    Private Function CreateAssemblyNode(ByVal refNode As NativeDependenciesTree.NativeDependency) As TreeNode
        Dim n As New TreeNode()
        If Not refNode.Resolved Then
            n.Text = System.IO.Path.GetFileName(refNode.FileName)
            n.ToolTipText = refNode.FileName
            n.ImageKey = "unresolved"
            n.SelectedImageKey = "unresolved"
            n.StateImageKey = "unresolved"
        Else
            n.Text = System.IO.Path.GetFileName(refNode.FileName)
            n.ToolTipText = refNode.PE.FileName
            n.ImageKey = "dll"
            n.SelectedImageKey = "dll"
            n.StateImageKey = "dll"
        End If

        n.Tag = refNode

        Return n
    End Function
    Private Function CreateExpandableReferenceNode(ByVal refNode As NativeDependenciesTree.NativeDependency) As TreeNode
        Dim n As TreeNode = Me.CreateAssemblyNode(refNode)
        If refNode.Resolved Then
            Dim dummy As New TreeNode()
            dummy.Tag = "dummy"
            n.Nodes.Add(dummy)
        End If
        Return n
    End Function

    Private Sub FillAssemblyNode(ByVal parent As TreeNode)
        Dim refNode As NativeDependenciesTree.NativeDependency = DirectCast(parent.Tag, NativeDependenciesTree.NativeDependency)

        For Each referencedDll As NativeDependenciesTree.NativeDependency In refNode.Dependencies
            parent.Nodes.Add(Me.CreateExpandableReferenceNode(referencedDll))
        Next
    End Sub

    Private Sub tvDepends_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles tvDepends.NodeMouseClick
        Try
            Dim refNode As NativeDependenciesTree.NativeDependency = DirectCast(e.Node.Tag, NativeDependenciesTree.NativeDependency)
            statusFile.Text = refNode.PE.FileName
            If refNode.Resolved Then
                Me.ShowAssemblyInfos(refNode)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub ShowAssemblyInfos(ByVal dll As NativeDependenciesTree.NativeDependency)
        lvExports.BeginUpdate()
        lvExports.Items.Clear()
        For Each export As ExportEntry In dll.PE.ExportDirectory.ExportEntries
            lvExports.Items.Add(New ListViewItem(New String() {export.Ordinal.ToString(), export.Hint.ToString(), export.Name, export.ExportRVA.ToString("X8")}, "function"))
        Next
        lvExports.EndUpdate()

        lvImports.BeginUpdate()
        lvImports.Items.Clear()
        For Each refDll As DllImportEntry In dll.PE.ImportDirectory.DllEntries
            For Each import As ImportEntry In refDll.Entries
                lvImports.Items.Add(New ListViewItem(New String() {import.Ordinal.ToString(), import.Hint.ToString(), refDll.DllName, import.Name, import.Address.ToString("X8")}, "function"))
            Next
        Next
        lvImports.EndUpdate()
    End Sub

    Private Sub tvDepends_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles tvDepends.BeforeExpand
        If Not e.Node.FirstNode Is Nothing AndAlso "dummy".Equals(e.Node.FirstNode.Tag) Then
            e.Node.FirstNode.Remove()
            Me.FillAssemblyNode(e.Node)
        End If
    End Sub

    Private Sub frmDepViewerMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Native.Functions.Misc.SetTheme(Me.lvAllDeps.Handle)
        Native.Functions.Misc.SetTheme(Me.lvExports.Handle)
        Native.Functions.Misc.SetTheme(Me.lvImports.Handle)
        CloseWithEchapKey(Me)
    End Sub

    Public Sub HideOpenMenu()
        Me.MenuItemOpen.Visible = False
        Me.MenuItemSeparatorAfterOpen.Visible = False
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Try
            If Me.tvDepends.SelectedNode IsNot Nothing Then
                Dim _p As String = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency).PE.FileName
                If IO.File.Exists(_p) Then
                    cFile.ShowFileProperty(_p, Me.Handle)
                End If
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Try
            If Me.tvDepends.SelectedNode IsNot Nothing Then
                Dim _p As String = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency).PE.FileName
                If IO.File.Exists(_p) Then
                    cFile.OpenDirectory(_p)
                End If
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileProp.Click
        Try
            For Each it As ListViewItem In Me.lvAllDeps.SelectedItems
                If IO.File.Exists(it.SubItems(10).Text) Then
                    cFile.ShowFileProperty(it.SubItems(10).Text, Me.Handle)
                End If
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemOpenDir.Click
        Try
            For Each it As ListViewItem In Me.lvAllDeps.SelectedItems
                If IO.File.Exists(it.SubItems(10).Text) Then
                    cFile.OpenDirectory(it.SubItems(10).Text)
                End If
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub tvDepends_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvDepends.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.tvDepends.SelectedNode IsNot Nothing Then
                Dim _tmp As NativeDependenciesTree.NativeDependency = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency)
                If _tmp.PE IsNot Nothing Then
                    Dim _p As String = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency).PE.FileName
                    Me.MenuItem1.Enabled = (IO.File.Exists(_p))
                    Me.MenuItem2.Enabled = Me.MenuItem1.Enabled
                Else
                    Me.MenuItem1.Enabled = False
                    Me.MenuItem2.Enabled = False
                End If
            Else
                Me.MenuItem1.Enabled = False
                Me.MenuItem2.Enabled = False
            End If
            Me.cMenu1.Show(Me.tvDepends, e.Location)
        End If
    End Sub

    Private Sub lvAllDeps_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvAllDeps.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Try
                If Me.lvAllDeps.SelectedItems.Count > 0 Then
                    Me.MenuItemFileProp.Enabled = IO.File.Exists(Me.lvAllDeps.SelectedItems(0).SubItems(10).Text)
                    Me.MenuItemOpenDir.Enabled = Me.MenuItem1.Enabled
                Else
                    Me.MenuItemFileProp.Enabled = False
                    Me.MenuItemOpenDir.Enabled = False
                End If
            Catch ex As Exception
                Me.MenuItemFileProp.Enabled = False
                Me.MenuItemOpenDir.Enabled = False
            End Try
            Me.cMenu2.Show(Me.lvAllDeps, e.Location)
        End If
    End Sub

    Private Sub MenuItem3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            If Me.tvDepends.SelectedNode IsNot Nothing Then
                Dim _p As String = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency).PE.FileName
                If IO.File.Exists(_p) Then
                    DisplayDetailsFile(_p)
                End If
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItem4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Try
            If Me.tvDepends.SelectedNode IsNot Nothing Then
                Dim _p As String = CType(Me.tvDepends.SelectedNode.Tag, NativeDependenciesTree.NativeDependency).PE.FileName
                If IO.File.Exists(_p) Then
                    Application.DoEvents()
                    Call SearchInternet(cFile.GetFileName(_p), Me.Handle)
                End If
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItemFileDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileDet.Click
        Try
            For Each it As ListViewItem In Me.lvAllDeps.SelectedItems
                If IO.File.Exists(it.SubItems(10).Text) Then
                    DisplayDetailsFile(it.SubItems(10).Text)
                End If
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItemInternetSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemInternetSearch.Click
        Try
            For Each it As ListViewItem In Me.lvAllDeps.SelectedItems
                If IO.File.Exists(it.SubItems(10).Text) Then
                    Application.DoEvents()
                    Call SearchInternet(cFile.GetFileName(it.SubItems(10).Text), Me.Handle)
                End If
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub MenuItemOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemOpen.Click
        If MenuItemOpen.Visible Then
            CDO.AddExtension = True
            CDO.CheckFileExists = True
            CDO.CheckPathExists = True
            CDO.DereferenceLinks = True
            CDO.Filter = "Assemblies (exe,dll)|*.exe;*.dll|All|*.*"
            CDO.RestoreDirectory = True
            If CDO.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Call OpenReferences(CDO.FileName)
            End If
        End If
    End Sub

    Private Sub MenuItemQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemQuit.Click
        Me.Close()
    End Sub

    Private Sub MenuItemAlwaysVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAlwaysVisible.Click
        Me.MenuItemAlwaysVisible.Checked = Not (Me.MenuItemAlwaysVisible.Checked)
        Me.TopMost = Me.MenuItemAlwaysVisible.Checked
    End Sub
End Class
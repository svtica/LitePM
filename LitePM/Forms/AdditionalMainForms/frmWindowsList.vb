﻿
' Lite Process Monitor


Option Strict On

'

Public Class frmWindowsList

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Common.Misc.CloseWithEchapKey(Me)

        Native.Functions.Misc.SetTheme(lv.Handle)
        Call RefreshWindowsList()
    End Sub

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick
        Call RefreshWindowsList()
    End Sub

    ' List all windows
    Private Sub RefreshWindowsList()

        Static first As Boolean = True

        ' Remove 'red' items (previously deleted)
        For Each it As ListViewItem In Me.lv.Items
            If it.BackColor = DELETED_ITEM_COLOR Then
                it.Remove()
            End If
        Next

        ' Deleted items
        For Each it As ListViewItem In Me.lv.Items
            Dim exist As Boolean = False
            For Each frm As Form In Application.OpenForms
                If frm.Handle = CType(it.Tag, IntPtr) Then
                    ' Still existing -> update infos
                    it.Text = frm.Text
                    exist = True
                    Exit For
                End If
            Next

            If exist = False Then
                ' Deleted
                it.BackColor = DELETED_ITEM_COLOR
            End If

        Next


        ' Remove 'green' items (previously deleted)
        For Each it As ListViewItem In Me.lv.Items
            If it.BackColor = NEW_ITEM_COLOR Then
                it.BackColor = Color.White
            End If
        Next


        ' New items
        For Each frm As Form In Application.OpenForms
            Dim exist As Boolean = False
            For Each itt As ListViewItem In Me.lv.Items
                If CType(itt.Tag, IntPtr) = frm.Handle Then
                    exist = True
                    If frm.Visible Then
                        itt.ForeColor = Color.Black
                    Else
                        itt.ForeColor = Color.Gray
                    End If
                    Exit For
                End If
            Next

            If exist = False Then
                ' Have to create
                Dim nene As New ListViewItem(frm.Text)
                If first = False Then
                    nene.BackColor = NEW_ITEM_COLOR
                End If
                nene.ForeColor = Color.FromArgb(30, 30, 30)
                nene.Tag = frm.Handle

                ' Add icon
                Try
                    Dim sKey As String = "_" & CStr(frm.Handle)
                    Me.imgList.Images.Add(sKey, frm.Icon)
                    nene.ImageKey = sKey
                Catch ex As Exception
                    nene.ImageKey = ""
                End Try

                If frm.Visible Then
                    nene.ForeColor = Color.Black
                Else
                    nene.ForeColor = Color.Gray
                End If
                Me.lv.Items.Add(nene)
            End If

        Next

        first = False
    End Sub

    Private Sub lv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.TheContextMenu.Show(Me.lv, e.Location)
        End If
    End Sub

    Private Sub lv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lv.SelectedIndexChanged
        If Me.lv.SelectedItems.Count = 0 Then
            Me.MenuItemShow.Enabled = False
            Me.MenuItemClose.Enabled = False
        Else
            Dim oneGray As Boolean = False
            For Each it As ListViewItem In Me.lv.SelectedItems
                If it.ForeColor = Color.Gray Then
                    oneGray = True
                    Exit For
                End If
            Next
            Me.MenuItemShow.Enabled = Not (oneGray)
            Me.MenuItemClose.Enabled = Not (oneGray)
        End If
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemShow.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            Dim hWnd As IntPtr = CType(it.Tag, IntPtr)
            If hWnd.IsNotNull Then
                Call cWindow.LocalShowWindowForeground(hWnd)
            End If
        Next
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            Dim hWnd As IntPtr = CType(it.Tag, IntPtr)
            If hWnd.IsNotNull Then
                Call cWindow.LocalClose(hWnd)
            End If
        Next
    End Sub
End Class
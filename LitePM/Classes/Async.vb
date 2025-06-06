﻿
' Lite Process Monitor


Option Strict On

Namespace Async


    ' Some async and threaf safe functions for the Listview control
    Public Class ListView

        Private Delegate Function degAddItem(ByVal lv As Windows.Forms.ListView, ByVal text As String) As Windows.Forms.ListViewItem
        Public Shared Function AddItem(ByVal lv As Windows.Forms.ListView, ByVal text As String) As Windows.Forms.ListViewItem
            If lv.InvokeRequired Then
                Dim d As New degAddItem(AddressOf AddItem)
                Return CType(lv.Invoke(d, lv, text), ListViewItem)
            Else
                Dim it As ListViewItem = lv.Items.Add(text)
                Return it
            End If
        End Function
        Private Delegate Function degAddItem2(ByVal lv As Windows.Forms.ListView, ByVal item As ListViewItem) As Windows.Forms.ListViewItem
        Public Shared Function AddItem(ByVal lv As Windows.Forms.ListView, ByVal item As ListViewItem) As Windows.Forms.ListViewItem
            If lv.InvokeRequired Then
                Dim d As New degAddItem2(AddressOf AddItem)
                Return CType(lv.Invoke(d, lv, item), ListViewItem)
            Else
                Dim it As ListViewItem = lv.Items.Add(item)
                Return it
            End If
        End Function

        Private Delegate Sub degChangeVirtualListSize(ByVal lv As Windows.Forms.ListView, ByVal value As Integer)
        Public Shared Sub ChangeVirtualListSize(ByVal lv As Windows.Forms.ListView, ByVal value As Integer)
            If lv.InvokeRequired Then
                Dim d As New degChangeVirtualListSize(AddressOf ChangeVirtualListSize)
                lv.Invoke(d, lv, value)
            Else
                lv.VirtualListSize = value
            End If
        End Sub

        Private Delegate Function degEnsureItemVisible(ByVal lv As System.Windows.Forms.ListView, ByVal text As String) As System.Windows.Forms.ListViewItem
        Public Shared Function EnsureItemVisible(ByVal lv As System.Windows.Forms.ListView, ByVal text As String) As System.Windows.Forms.ListViewItem
            If lv.InvokeRequired Then
                Dim d As New degEnsureItemVisible(AddressOf EnsureItemVisible)
                Return CType(lv.Invoke(d, lv, text), ListViewItem)
            Else
                Dim it As ListViewItem = lv.FindItemWithText(text)
                If it IsNot Nothing Then
                    it.Selected = True
                    it.EnsureVisible()
                End If
                Return it
            End If
        End Function

    End Class



    ' Some async and threaf safe functions for the ProgressBar control
    Public Class ProgressBar

        Private Delegate Sub degChangeValue(ByVal pgb As Windows.Forms.ProgressBar, ByVal value As Integer)
        Public Shared Sub ChangeValue(ByVal pgb As Windows.Forms.ProgressBar, ByVal value As Integer)
            If pgb.InvokeRequired Then
                Dim d As New degChangeValue(AddressOf ChangeValue)
                pgb.Invoke(d, pgb, value)
            Else
                pgb.Value = value
            End If
        End Sub

        Private Delegate Sub degChangeMaximum(ByVal pgb As Windows.Forms.ProgressBar, ByVal value As Integer)
        Public Shared Sub ChangeMaximum(ByVal pgb As Windows.Forms.ProgressBar, ByVal value As Integer)
            If pgb.InvokeRequired Then
                Dim d As New degChangeMaximum(AddressOf ChangeMaximum)
                pgb.Invoke(d, pgb, value)
            Else
                pgb.Maximum = value
            End If
        End Sub

    End Class



    ' Some async and threaf safe functions for the Button control
    Public Class Button

        Private Delegate Sub degChangeText(ByVal tb As Windows.Forms.Button, ByVal value As String)
        Public Shared Sub ChangeText(ByVal tb As Windows.Forms.Button, ByVal value As String)
            If tb.InvokeRequired Then
                Dim d As New degChangeText(AddressOf ChangeText)
                tb.Invoke(d, tb, value)
            Else
                tb.Text = value
            End If
        End Sub

    End Class



    ' Some async and threaf safe functions for the SplitContainer control
    Public Class SplitContainer

        Private Delegate Sub degChangeEnabled(ByVal tb As Windows.Forms.SplitContainer, ByVal value As Boolean)
        Public Shared Sub ChangeEnabled(ByVal tb As Windows.Forms.SplitContainer, ByVal value As Boolean)
            If tb.InvokeRequired Then
                Dim d As New degChangeEnabled(AddressOf ChangeEnabled)
                tb.Invoke(d, tb, value)
            Else
                tb.Enabled = value
            End If
        End Sub

    End Class


    ' Some async and threaf safe functions for the Form control
    Public Class Form

        Private Delegate Sub degChangeEnabled(ByVal frm As Windows.Forms.Form, ByVal value As Boolean)
        Public Shared Sub ChangeEnabled(ByVal frm As Windows.Forms.Form, ByVal value As Boolean)
            If frm.InvokeRequired Then
                Dim d As New degChangeEnabled(AddressOf ChangeEnabled)
                frm.Invoke(d, frm, value)
            Else
                frm.Enabled = value
            End If
        End Sub

    End Class

End Namespace

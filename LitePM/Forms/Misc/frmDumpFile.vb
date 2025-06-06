﻿
' Lite Process Monitor


Option Strict On
Imports Common.Misc

Public Class frmDumpFile

    Private _dir As String

    Public ReadOnly Property TargetDir() As String
        Get
            Return _dir
        End Get
    End Property
    Public ReadOnly Property DumpOption() As Native.Api.NativeEnums.MiniDumpType
        Get
            Select Case Me.cbOption.Text
                Case "MiniDumpNormal"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpNormal
                Case "MiniDumpWithDataSegs"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithDataSegs
                Case "MiniDumpWithFullMemory"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithFullMemory
                Case "MiniDumpWithHandleData"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithHandleData
                Case "MiniDumpFilterMemory"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpFilterMemory
                Case "MiniDumpScanMemory"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpScanMemory
                Case "MiniDumpWithUnloadedModules"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithUnloadedModules
                Case "MiniDumpWithIndirectlyReferencedMemory"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithIndirectlyReferencedMemory
                Case "MiniDumpFilterModulePaths"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpFilterModulePaths
                Case "MiniDumpWithProcessThreadData"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithProcessThreadData
                Case "MiniDumpWithPrivateReadWriteMemory"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithPrivateReadWriteMemory
                Case "MiniDumpWithoutOptionalData"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithoutOptionalData
                Case "MiniDumpWithFullMemoryInfo"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithFullMemoryInfo
                Case "MiniDumpWithThreadInfo"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithThreadInfo
                Case "MiniDumpWithCodeSegs"
                    Return Native.Api.NativeEnums.MiniDumpType.MiniDumpWithCodeSegs
            End Select
        End Get
    End Property

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdBrowse, "Select target directory")
        SetToolTip(Me.txtDir, "Target directory")
        SetToolTip(Me.cmdCreate, "Create the minidump now")
        SetToolTip(Me.cmdExit, "Close this window")
        SetToolTip(Me.cbOption, "Specifies the datas to include into the dump file")
        Me.cbOption.Text = "MiniDumpNormal"
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        If System.IO.Directory.Exists(_dir) Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtDir_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDir.TextChanged
        _dir = Me.txtDir.Text
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        With ChooseFolder
            .Description = "Select target directory"
            .RootFolder = Environment.SpecialFolder.MyComputer
            .ShowNewFolderButton = True
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDir.Text = .SelectedPath
            End If
        End With
    End Sub
End Class
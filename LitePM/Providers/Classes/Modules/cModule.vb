﻿
' Lite Process Monitor









'




'



Option Strict On

Public Class cModule
    Inherits cGeneralObject

    Private _moduleInfos As moduleInfos

    Private Shared _hlModuleRelocated As Boolean
    Private Shared _hlModuleRelocatedColor As System.Drawing.Color

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As moduleInfos)
        _moduleInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Module
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As moduleInfos
        Get
            Return _moduleInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As moduleInfos)
        _moduleInfos.Merge(Thr)
    End Sub

#Region "All actions on modules (unload)"

    ' Unload module
    Private _closeM As asyncCallbackModuleUnload
    Public Function UnloadModule() As Integer

        If _closeM Is Nothing Then
            _closeM = New asyncCallbackModuleUnload(New asyncCallbackModuleUnload.HasUnloadedModule(AddressOf unloadModuleDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _closeM.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackModuleUnload.poolObj(Me.Infos.ProcessId, Me.Infos.Name, Me.Infos.BaseAddress, newAction))

    End Function
    Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload module " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub


#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return moduleInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function


    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "EntryPoint"
                res = Me.Infos.EntryPoint.ToString
            Case "Size"
                res = Me.Infos.Size.ToString
            Case "Name"
                res = Me.Infos.Name
            Case "LoadCount"
                res = Me.Infos.LoadCount.ToString
            Case "Version"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileVersion
                Else
                    res = Me.Infos.Version
                End If
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileDescription
            Case "CompanyName"
                If Me.Infos.Manufacturer Is Nothing Then
                    If Me.Infos.FileInfo IsNot Nothing Then _
                    res = Me.Infos.FileInfo.CompanyName
                Else
                    res = Me.Infos.Manufacturer
                End If
            Case "Path"
                res = Me.Infos.Path
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
            Case "Comments"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Comments
            Case "FileBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileBuildPart.ToString
            Case "FileMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMajorPart.ToString
            Case "FileMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMinorPart.ToString
            Case "FilePrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FilePrivatePart.ToString
            Case "InternalName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.InternalName
            Case "IsDebug"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsDebug.ToString
            Case "IsPatched"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPatched.ToString
            Case "IsPreRelease"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPreRelease.ToString
            Case "IsPrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPrivateBuild.ToString
            Case "IsSpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsSpecialBuild.ToString
            Case "Language"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Language
            Case "LegalCopyright"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalCopyright
            Case "LegalTrademarks"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalTrademarks
            Case "OriginalFilename"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.OriginalFilename
            Case "PrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.PrivateBuild
            Case "ProductBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductBuildPart.ToString
            Case "ProductMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMajorPart.ToString
            Case "ProductMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMinorPart.ToString
            Case "ProductName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductName
            Case "ProductPrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductPrivatePart.ToString
            Case "ProductVersion"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductVersion
            Case "SpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.SpecialBuild
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "Flags"
                res = Me.Infos.Flags.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_EntryPoint As String = ""
        Static _old_Size As String = ""
        Static _old_Name As String = ""
        Static _old_Version As String = ""
        Static _old_Description As String = ""
        Static _old_CompanyName As String = ""
        Static _old_Path As String = ""
        Static _old_Address As String = ""
        Static _old_Comments As String = ""
        Static _old_FileBuildPart As String = ""
        Static _old_FileMajorPart As String = ""
        Static _old_FileMinorPart As String = ""
        Static _old_FilePrivatePart As String = ""
        Static _old_InternalName As String = ""
        Static _old_IsDebug As String = ""
        Static _old_IsPatched As String = ""
        Static _old_IsPreRelease As String = ""
        Static _old_IsPrivateBuild As String = ""
        Static _old_IsSpecialBuild As String = ""
        Static _old_Language As String = ""
        Static _old_LegalCopyright As String = ""
        Static _old_LegalTrademarks As String = ""
        Static _old_OriginalFilename As String = ""
        Static _old_PrivateBuild As String = ""
        Static _old_ProductBuildPart As String = ""
        Static _old_ProductMajorPart As String = ""
        Static _old_ProductMinorPart As String = ""
        Static _old_ProductName As String = ""
        Static _old_ProductPrivatePart As String = ""
        Static _old_ProductVersion As String = ""
        Static _old_SpecialBuild As String = ""
        Static _old_ProcessId As String = ""
        Static _old_Flags As String = ""
        Static _old_LoadCount As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "EntryPoint"
                res = Me.Infos.EntryPoint.ToString
                If res = _old_EntryPoint Then
                    hasChanged = False
                Else
                    _old_EntryPoint = res
                End If
            Case "Size"
                res = Me.Infos.Size.ToString
                If res = _old_Size Then
                    hasChanged = False
                Else
                    _old_Size = res
                End If
            Case "LoadCount"
                res = Me.Infos.LoadCount.ToString
                If res = _old_LoadCount Then
                    hasChanged = True
                Else
                    _old_LoadCount = res
                End If
            Case "Name"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "Version"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileVersion
                Else
                    res = Me.Infos.Version
                End If
                If res = _old_Version Then
                    hasChanged = False
                Else
                    _old_Version = res
                End If
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileDescription
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
            Case "CompanyName"
                If Me.Infos.Manufacturer Is Nothing Then
                    If Me.Infos.FileInfo IsNot Nothing Then _
                    res = Me.Infos.FileInfo.CompanyName
                Else
                    res = Me.Infos.Manufacturer
                End If
                If res = _old_CompanyName Then
                    hasChanged = False
                Else
                    _old_CompanyName = res
                End If
            Case "Path"
                res = Me.Infos.Path
                If res = _old_Path Then
                    hasChanged = False
                Else
                    _old_Path = res
                End If
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
                If res = _old_Address Then
                    hasChanged = False
                Else
                    _old_Address = res
                End If
            Case "Comments"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Comments
                If res = _old_Comments Then
                    hasChanged = False
                Else
                    _old_Comments = res
                End If
            Case "FileBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileBuildPart.ToString
                If res = _old_FileBuildPart Then
                    hasChanged = False
                Else
                    _old_FileBuildPart = res
                End If
            Case "FileMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMajorPart.ToString
                If res = _old_FileMajorPart Then
                    hasChanged = False
                Else
                    _old_FileMajorPart = res
                End If
            Case "FileMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMinorPart.ToString
                If res = _old_FileMinorPart Then
                    hasChanged = False
                Else
                    _old_FileMinorPart = res
                End If
            Case "FilePrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FilePrivatePart.ToString
                If res = _old_FilePrivatePart Then
                    hasChanged = False
                Else
                    _old_FilePrivatePart = res
                End If
            Case "InternalName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.InternalName
                If res = _old_InternalName Then
                    hasChanged = False
                Else
                    _old_InternalName = res
                End If
            Case "IsDebug"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsDebug.ToString
                If res = _old_IsDebug Then
                    hasChanged = False
                Else
                    _old_IsDebug = res
                End If
            Case "IsPatched"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPatched.ToString
                If res = _old_IsPatched Then
                    hasChanged = False
                Else
                    _old_IsPatched = res
                End If
            Case "IsPreRelease"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPreRelease.ToString
                If res = _old_IsPreRelease Then
                    hasChanged = False
                Else
                    _old_IsPreRelease = res
                End If
            Case "IsPrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPrivateBuild.ToString
                If res = _old_IsPrivateBuild Then
                    hasChanged = False
                Else
                    _old_IsPrivateBuild = res
                End If
            Case "IsSpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsSpecialBuild.ToString
                If res = _old_IsSpecialBuild Then
                    hasChanged = False
                Else
                    _old_IsSpecialBuild = res
                End If
            Case "Language"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Language
                If res = _old_Language Then
                    hasChanged = False
                Else
                    _old_Language = res
                End If
            Case "LegalCopyright"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalCopyright
                If res = _old_LegalCopyright Then
                    hasChanged = False
                Else
                    _old_LegalCopyright = res
                End If
            Case "LegalTrademarks"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalTrademarks
                If res = _old_LegalTrademarks Then
                    hasChanged = False
                Else
                    _old_LegalTrademarks = res
                End If
            Case "OriginalFilename"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.OriginalFilename
                If res = _old_OriginalFilename Then
                    hasChanged = False
                Else
                    _old_OriginalFilename = res
                End If
            Case "PrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.PrivateBuild
                If res = _old_PrivateBuild Then
                    hasChanged = False
                Else
                    _old_PrivateBuild = res
                End If
            Case "ProductBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductBuildPart.ToString
                If res = _old_ProductBuildPart Then
                    hasChanged = False
                Else
                    _old_ProductBuildPart = res
                End If
            Case "ProductMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMajorPart.ToString
                If res = _old_ProductMajorPart Then
                    hasChanged = False
                Else
                    _old_ProductMajorPart = res
                End If
            Case "ProductMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMinorPart.ToString
                If res = _old_ProductMinorPart Then
                    hasChanged = False
                Else
                    _old_ProductMinorPart = res
                End If
            Case "ProductName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductName
                If res = _old_ProductName Then
                    hasChanged = False
                Else
                    _old_ProductName = res
                End If
            Case "ProductPrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductPrivatePart.ToString
                If res = _old_ProductPrivatePart Then
                    hasChanged = False
                Else
                    _old_ProductPrivatePart = res
                End If
            Case "ProductVersion"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductVersion
                If res = _old_ProductVersion Then
                    hasChanged = False
                Else
                    _old_ProductVersion = res
                End If
            Case "SpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.SpecialBuild
                If res = _old_SpecialBuild Then
                    hasChanged = False
                Else
                    _old_SpecialBuild = res
                End If
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
                If res = _old_ProcessId Then
                    hasChanged = False
                Else
                    _old_ProcessId = res
                End If
            Case "Flags"
                res = Me.Infos.Flags.ToString
                If res = _old_Flags Then
                    hasChanged = False
                Else
                    _old_Flags = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

#Region "Shared function"

    Private Shared _sharedcloseM As asyncCallbackModuleUnload
    Public Shared Function SharedLRUnloadModule(ByVal pid As Integer, ByVal name As String, ByVal baseAddress As IntPtr) As Integer

        If _sharedcloseM Is Nothing Then
            _sharedcloseM = New asyncCallbackModuleUnload(New asyncCallbackModuleUnload.HasUnloadedModule(AddressOf unloadsharedModuleDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedcloseM.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackModuleUnload.poolObj(pid, name, baseAddress, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub unloadsharedModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload module " & name & " : " & msg)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

#End Region

#Region "Shared functions (local)"

    ' Return opened modules
    Public Shared Function CurrentLocalModules(ByVal pid As Integer) As Dictionary(Of String, cModule)
        Dim _d As Dictionary(Of String, moduleInfos) = _
                            Native.Objects.Module.EnumerateModulesByProcessId(pid)
        Dim _res As New Dictionary(Of String, cModule)

        For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _d
            _res.Add(pair.Key, New cModule(pair.Value))
        Next

        Return _res
    End Function

#End Region


#Region "Highlightings management"

    ' Set highlightings
    Public Shared Sub SetHighlightings(ByVal relocated As Boolean)
        _hlModuleRelocated = relocated
    End Sub
    Public Shared Sub SetHighlightingsColor(ByVal relocated As Color)
        _hlModuleRelocatedColor = relocated
    End Sub

    ' Return backcolor
    Public Overrides Function GetBackColor() As System.Drawing.Color
        If _hlModuleRelocated AndAlso (Me.Infos.Flags And Native.Api.NativeEnums.LdrpDataTableEntryFlags.ImageNotAtBase) = Native.Api.NativeEnums.LdrpDataTableEntryFlags.ImageNotAtBase Then
            Return _hlModuleRelocatedColor
        End If
        Return MyBase.GetBackColor()
    End Function

#End Region


End Class

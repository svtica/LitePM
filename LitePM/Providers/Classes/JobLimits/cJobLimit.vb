﻿
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Public Class cJobLimit
    Inherits cGeneralObject
    Implements IDisposable

    ' Infos
    Private _jobLimitsInfos As jobLimitInfos

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As jobLimitInfos)
        _jobLimitsInfos = infos
        _TypeOfObject = Enums.GeneralObjectType.JobLimit
    End Sub
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not Me.disposed Then
            ' If disposing equals true, dispose all managed 
            ' and unmanaged resources.
            If disposing Then
                ' Dispose managed resources.

            End If

            ' Note disposing has been done.
            disposed = True

        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As jobLimitInfos
        Get
            Return _jobLimitsInfos
        End Get
    End Property

#End Region


    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As jobLimitInfos)
        _jobLimitsInfos.Merge(Thr)
    End Sub


#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return jobLimitInfos.GetAvailableProperties(includeFirstProp, sorted)
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
            Case "Limit"
                res = Me.Infos.Name
            Case "Value"
                res = Me.Infos.Value
            Case "Description"
                res = Me.Infos.Description
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Value As String = ""
        Static _old_Desc As String = ""
        Static _old_Name As String = ""

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
            Case "Limit"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "Value"
                res = Me.Infos.Value
                If res = _old_Value Then
                    hasChanged = False
                Else
                    _old_Value = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Desc Then
                    hasChanged = False
                Else
                    _old_Desc = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

End Class

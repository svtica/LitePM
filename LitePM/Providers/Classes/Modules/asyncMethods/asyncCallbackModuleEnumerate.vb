
Option Strict On

Imports System.Runtime.InteropServices

Public Class asyncCallbackModuleEnumerate

    Public Structure poolObj
        Public forInstanceId As Integer
        Public pid As Integer
        Public Sub New(ByVal pi As Integer, ByVal iid As Integer)
            forInstanceId = iid
            pid = pi
        End Sub
    End Structure

    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj) As Dictionary(Of String, moduleInfos)
        Dim _dico As Dictionary(Of String, moduleInfos)

        _dico = Native.Objects.Module.EnumerateModulesByProcessId(pObj.pid, False)

        Return _dico
    End Function

End Class

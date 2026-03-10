' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 3 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackServiceEnumerate

    Public Structure poolObj
        Public forInstanceId As Integer
        Public complete As Boolean
        Public getDrivers As Boolean
        Public pid As Integer
        Public Sub New(ByVal iid As Integer, ByVal comp As Boolean, ByVal getDrv As Boolean, ByVal p As Integer)
            forInstanceId = iid
            complete = comp
            getDrivers = getDrv
            pid = p
        End Sub
    End Structure

    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj, ByVal servConn As cServiceConnection) As Dictionary(Of String, serviceInfos)
        Dim _dico As New Dictionary(Of String, serviceInfos)

        Native.Objects.Service.EnumerateServices(_dico, pObj.getDrivers)

        Return _dico
    End Function

End Class

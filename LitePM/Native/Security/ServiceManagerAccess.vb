
' Lite Process Monitor


' This file uses third-party pieces of code under GNU 
' GPL 3.0 license. See below for details







'




'


'
'
' This file uses some work under GNU GPL 3.0 license
' The following definitions are from Process Hacker by Wj32,
' which is under GNU GPL 3.0 :
' - ServiceManagerAccess

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum ServiceManagerAccess As UInteger
        Connect = &H1
        CreateService = &H2
        EnumerateService = &H4
        Lock = &H8
        QueryLockStatus = &H10
        ModifyBootConfig = &H20
        All = StandardRights.Required Or Connect Or CreateService Or EnumerateService Or Lock Or QueryLockStatus Or ModifyBootConfig
    End Enum

End Namespace
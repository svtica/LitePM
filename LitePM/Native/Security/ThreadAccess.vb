
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
' - ThreadAccess

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum ThreadAccess As UInteger
        Terminate = &H1
        SuspendResume = &H2
        Alert = &H4
        GetContext = &H8
        SetContext = &H10
        SetInformation = &H20
        QueryInformation = &H40
        SetThreadToken = &H80
        Impersonate = &H100
        DirectImpersonation = &H200
        SetLimitedInformation = &H400
        QueryLimitedInformation = &H800
        ' should be 0xffff on Vista, but is 0xfff for backwards compatibility
        All = StandardRights.Required Or StandardRights.Synchronize Or &HFFF
    End Enum

End Namespace

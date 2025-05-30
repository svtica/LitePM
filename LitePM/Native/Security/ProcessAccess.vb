
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
' - ProcessAccess

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum ProcessAccess As UInteger
        Terminate = &H1
        CreateThread = &H2
        SetSessionId = &H4
        VmOperation = &H8
        VmRead = &H10
        VmWrite = &H20
        DupHandle = &H40
        CreateProcess = &H80
        SetQuota = &H100
        SetInformation = &H200
        QueryInformation = &H400
        SetPort = &H800
        SuspendResume = &H800
        QueryLimitedInformation = &H1000
        Synchronize = StandardRights.Synchronize
        ' should be 0xffff on Vista, but is 0xfff for backwards compatibility
        All = StandardRights.Required Or StandardRights.Synchronize Or &HFFF
    End Enum

End Namespace

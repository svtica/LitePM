
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
' - StandardRights

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum StandardRights As UInteger
        Delete = &H10000
        ReadControl = &H20000
        WriteDac = &H40000
        WriteOwner = &H80000
        Synchronize = &H100000
        Required = &HF0000
        Read = ReadControl
        Write = ReadControl
        Execute = ReadControl
        All = &H1F0000
        SpecificRightsAll = &HFFFF
        AccessSystemSecurity = &H1000000
        MaximumAllowed = &H2000000
        GenericRead = &H80000000
        GenericWrite = &H40000000
        GenericExecute = &H20000000
        GenericAll = &H10000000
    End Enum

End Namespace

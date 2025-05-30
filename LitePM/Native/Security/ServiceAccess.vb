
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
' - ServiceAccess

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum ServiceAccess As UInteger
        QueryConfig = &H1
        ChangeConfig = &H2
        QueryStatus = &H4
        EnumerateDependents = &H8
        Start = &H10
        [Stop] = &H20
        PauseContinue = &H40
        Interrogate = &H80
        UserDefinedControl = &H100
        Delete = &H10000
        All = StandardRights.Required Or QueryConfig Or ChangeConfig Or QueryStatus Or EnumerateDependents Or Start Or [Stop] Or PauseContinue Or Interrogate Or UserDefinedControl
    End Enum

End Namespace
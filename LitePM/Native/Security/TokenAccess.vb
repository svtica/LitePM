
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
' - TokenAccess

Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum TokenAccess As UInteger
        AllPlusSessionId = &HF01FF
        MaximumAllowed = &H2000000
        AccessSystemSecurity = &H1000000
        AssignPrimary = &H1
        Duplicate = &H2
        Impersonate = &H4
        Query = &H8
        QuerySource = &H10
        AdjustPrivileges = &H20
        AdjustGroups = &H40
        AdjustDefault = &H80
        AdjustSessionId = &H100
        All = StandardRights.Required Or AssignPrimary Or Duplicate Or Impersonate Or Query Or QuerySource Or AdjustPrivileges Or AdjustGroups Or AdjustDefault Or AdjustSessionId
        GenericRead = StandardRights.Read Or Query
        GenericWrite = StandardRights.Write Or AdjustPrivileges Or AdjustGroups Or AdjustDefault
        GenericExecute = StandardRights.Execute
    End Enum

End Namespace


' Lite Process Monitor









'




'



Option Strict On

Namespace Native.Security

    <Flags()> _
    Public Enum JobAccess As UInteger
        AssignProcess = &H1
        SetAttributes = &H2
        Query = &H4
        Terminate = &H8
        SetSecurityAttributes = &H10
        All = &H1F001F
    End Enum

End Namespace
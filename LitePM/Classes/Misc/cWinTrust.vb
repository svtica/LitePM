
' Lite Process Monitor









'




'



' This code comes from here :
' http://www.pinvoke.net/default.aspx/wintrust/WinVerifyTrust.html

Option Strict On
Imports Native.Api

Namespace Security.WinTrust

    Public Class WinTrust

        ' GUID of the action to perform
        Private Shared GuidVerifyAction As Guid = New Guid(NativeConstants.WintrustActionGenericVerify2)

        ' Check file signature
        Public Shared Function VerifyEmbeddedSignature(ByVal fileName As String) As Boolean
            Dim wtd As New NativeStructs.WinTrustData(fileName)
            Dim result As NativeEnums.WinVerifyTrustResult = NativeFunctions.WinVerifyTrust(NativeConstants.InvalidHandleValue, GuidVerifyAction, wtd)
            Return (result = NativeEnums.WinVerifyTrustResult.Trusted)
        End Function
        Public Shared Function VerifyEmbeddedSignature2(ByVal fileName As String) As NativeEnums.WinVerifyTrustResult
            Dim wtd As New NativeStructs.WinTrustData(fileName)
            Return NativeFunctions.WinVerifyTrust(NativeConstants.InvalidHandleValue, GuidVerifyAction, wtd)
        End Function

    End Class

End Namespace
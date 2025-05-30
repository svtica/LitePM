
' Lite Process Monitor









'




'




Option Strict On

Public Class HXXXProp
    Inherits UserControl

    ' Concerned handle
    Private _handle As cHandle
    Public Property TheHandle() As cHandle
        Get
            Return _handle
        End Get
        Set(ByVal value As cHandle)
            _handle = value
        End Set
    End Property

    Public Sub New()
        '
    End Sub
    Public Sub New(ByVal handle As cHandle)
        _handle = handle
    End Sub

    ' Refresh displayed informations
    Public Overridable Sub RefreshInfos()
        '
    End Sub

End Class

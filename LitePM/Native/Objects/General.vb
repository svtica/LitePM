
' Lite Process Monitor









'




'



Option Strict On

Namespace Native.Objects

    Public Class General



        ' Private constants




        ' Private attributes




        ' Public properties



        ' Other public




        ' Public functions


        ' Close a handle
        Public Shared Sub CloseHandle(ByVal handle As IntPtr)
            Native.Api.NativeFunctions.CloseHandle(handle)
        End Sub


        ' Private functions



    End Class

End Namespace

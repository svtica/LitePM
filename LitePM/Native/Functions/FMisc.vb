
' Lite Process Monitor









'




'




Option Strict On

Namespace Native.Functions

    Public Class Misc


        ' Private constants




        ' Private attributes




        ' Public properties




        ' Public functions


        ' Set 'explorer' theme
        Public Shared Sub SetTheme(ByVal handle As IntPtr)
            Native.Api.NativeFunctions.SetWindowTheme(handle, "explorer", Nothing)
        End Sub

        ' Set a listview as 'double buffered'
        Public Shared Sub SetListViewAsDoubleBuffered(ByRef lv As ListView)
            Dim styles As IntPtr = Native.Api.NativeFunctions.SendMessage(lv.Handle, Native.Api.NativeEnums.LVM.GetExtendedListviewStyle, IntPtr.Zero, IntPtr.Zero)
            styles = CType(styles.ToInt32 Or Native.Api.NativeEnums.LvsEx.DoubleBuffer Or Native.Api.NativeEnums.LvsEx.BorderSelect, IntPtr)
            Native.Api.NativeFunctions.SendMessage(lv.Handle, Native.Api.NativeEnums.LVM.SetExtendedListviewStyle, IntPtr.Zero, styles)
        End Sub



        ' Private functions



    End Class

End Namespace

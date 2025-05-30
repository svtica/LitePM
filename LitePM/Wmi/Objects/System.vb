
' Lite Process Monitor









'




'



Imports System.Management
Imports Native.Api.Enums

Namespace Wmi.Objects

    Public Class cSystem



        ' Private constants




        ' Private attributes




        ' Public properties


        ' Shutdown remote pc
        Public Shared Function ShutdownRemoteComputer(ByVal type As ShutdownType, _
                        ByVal force As Boolean, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef msgError As String) As Boolean

            Try
                Dim res As WBEMStatus
                Dim param As WmiShutdownValues
                If force Then
                    param = param Or WmiShutdownValues.Force
                End If
                Select Case type
                    Case ShutdownType.Logoff
                        param = param Or WmiShutdownValues.LogOff
                    Case ShutdownType.Poweroff
                        param = param Or WmiShutdownValues.PowerOff
                    Case ShutdownType.Restart
                        param = param Or WmiShutdownValues.Reboot
                    Case ShutdownType.Shutdown
                        param = param Or WmiShutdownValues.Shutdown
                End Select
                Dim obj(0) As Object
                obj(0) = CObj(param)
                For Each osObj As ManagementObject In objSearcher.Get
                    res = CType(osObj.InvokeMethod("Win32Shutdown", obj), WBEMStatus)
                    Exit For
                Next

                msgError = res.ToString
                Return (res = WBEMStatus.WBEM_NO_ERROR)
            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try

        End Function



        ' Other public




        ' Public functions




        ' Private functions



    End Class

End Namespace

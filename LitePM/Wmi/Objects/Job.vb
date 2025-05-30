
' Lite Process Monitor









'




'



Imports System.Management
Imports Native.Api.Enums

Namespace Wmi.Objects

    Public Class Job




        ' Private constants




        ' Private attributes




        ' Public properties


        ' Enumerate services
        Public Shared Function EnumerateJobs(ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, jobInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

            ' For each job...
            For Each refJob As Management.ManagementObject In res

                ' Job name
                Dim jobName As String = CStr(refJob.GetPropertyValue(WmiInfoJob.CollectionID.ToString))

                ' TODO : have to retrieve ProcessesCount ?

                If _dico.ContainsKey(jobName) = False Then
                    _dico.Add(jobName, New jobInfos(jobName))
                End If
            Next

            Return True

        End Function



        ' Other public




        ' Public functions




        ' Private functions


    End Class

End Namespace


' Lite Process Monitor









'




'



Imports System.Management

Namespace Wmi.Objects

    Public Class [Module]




        ' Private constants




        ' Private attributes




        ' Public properties


        ' Enumerate modules
        Public Shared Function EnumerateModuleById(ByVal pid As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, moduleInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try


            'For Each refProcess As Management.ManagementObject In res
            '    Dim colMod As ManagementObjectCollection = refProcess.GetRelationships("CIM_ProcessExecutable")
            '    Dim _dicoBaseA As New Dictionary(Of String, Integer)
            '    For Each refModule As ManagementObject In colMod
            '        Dim _s As String = CStr(refModule.GetPropertyValue("Antecedent")).ToLowerInvariant
            '        ' Extract dll path from _s
            '        Dim i As Integer = InStr(_s, "name=", CompareMethod.Binary)
            '        Dim __s As String = vbNullString
            '        If i > 0 Then
            '            __s = _s.Substring(i + 5, _s.Length - i - 6).Replace("\\", "\")
            '        End If
            '        If __s IsNot Nothing Then
            '            _dicoBaseA.Add(__s, CInt(refModule.GetPropertyValue("BaseAddress")))
            '        End If
            '    Next
            'Next

            For Each refProcess As Management.ManagementObject In res

                Dim aPid As Integer = CInt(refProcess.GetPropertyValue(Native.Api.Enums.WmiInfoProcess.ProcessId.ToString))

                ' OK, we get modules for this process
                If pid = aPid Then

                    Dim colModule As ManagementObjectCollection = refProcess.GetRelated("CIM_DataFile")
                    For Each refModule As ManagementObject In colModule
                        Dim obj As New Native.Api.NativeStructs.LdrDataTableEntry
                        Dim path As String = CStr(refModule.GetPropertyValue("Name"))

                        With obj
                            ' Get base address from dico
                            ' TOCHANGE
                            .DllBase = IntPtr.Zero
                            .EntryPoint = IntPtr.Zero
                            .SizeOfImage = 0
                        End With

                        Dim _manuf As String = CStr(refModule.GetPropertyValue("Manufacturer"))
                        Dim _vers As String = CStr(refModule.GetPropertyValue("Version"))
                        Dim _module As New moduleInfos(obj, aPid, path, _vers, _manuf)
                        Dim _key As String = path & "-" & pid.ToString & "-" & obj.DllBase.ToString
                        _dico.Add(_key, _module)

                    Next
                    Exit For
                End If
            Next

            Return True
        End Function



        ' Other public




        ' Public functions




        ' Private functions



    End Class

End Namespace

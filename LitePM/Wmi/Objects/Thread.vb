﻿
' Lite Process Monitor









'




'



Imports System.Management
Imports Native.Api

Namespace Wmi.Objects

    Public Class Thread




        ' Private constants




        ' Private attributes




        ' Public properties


        ' Enumerate threads
        Public Shared Function EnumerateThreadByIds(ByVal pid As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, threadInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

            For Each refThread As Management.ManagementObject In res

                Dim wmiId As Integer = CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.ProcessHandle.ToString))

                ' If we have to get threads for this process...
                If pid = wmiId Then
                    Dim obj As New Native.Api.NativeStructs.SystemThreadInformation
                    With obj
                        .BasePriority = CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.PriorityBase.ToString))
                        .CreateTime = 0
                        .ClientId = New Native.Api.NativeStructs.ClientId(wmiId, _
                                                      CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.Handle.ToString)))
                        .KernelTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.KernelModeTime.ToString))
                        .Priority = CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.Priority.ToString))
                        Try
                            .StartAddress = New IntPtr(CLng(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.StartAddress.ToString)))
                        Catch ex0 As Exception
                            .StartAddress = NativeConstants.InvalidHandleValue
                        End Try
                        .State = CType(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.ThreadState.ToString), ThreadState)
                        .UserTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.UserModeTime.ToString))
                        .WaitReason = CType(CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.ThreadWaitReason.ToString)), Native.Api.NativeEnums.KwaitReason)
                        Try
                            .WaitTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WmiInfoThread.ElapsedTime.ToString))
                        Catch ex1 As Exception
                            '
                        End Try
                    End With
                    Dim _procInfos As New threadInfos(obj)
                    Dim _key As String = obj.ClientId.UniqueThread.ToString & "-" & obj.ClientId.UniqueProcess.ToString
                    If _dico.ContainsKey(_key) = False Then
                        _dico.Add(_key, _procInfos)
                    End If
                End If
            Next

            Return True
        End Function



        ' Other public




        ' Public functions




        ' Private functions



    End Class

End Namespace

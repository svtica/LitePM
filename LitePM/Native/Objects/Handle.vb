
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Namespace Native.Objects

    Public Class Handle



        ' Private constants


        ' Handle enumeration class
        Private Shared hEnum As HandleEnumeration



        ' Private attributes




        ' Public properties


        Public Shared Property HandleEnumerationClass() As HandleEnumeration
            Get
                Return hEnum
            End Get
            Set(ByVal value As HandleEnumeration)
                hEnum = value
            End Set
        End Property



        ' Other public




        ' Public functions


        ' Close a handle in another process
        Public Shared Function CloseProcessLocalHandle(ByVal dwProcessID As Integer, _
                                                ByVal hHandle As IntPtr) As Integer
            Dim hMod As IntPtr
            Dim lpProc As IntPtr
            Dim hThread As IntPtr
            Dim hProcess As IntPtr
            Dim res As Integer

            hMod = NativeFunctions.GetModuleHandle("kernel32.dll")
            lpProc = NativeFunctions.GetProcAddress(hMod, "CloseHandle")
            hProcess = Native.Objects.Process.GetProcessHandleById(dwProcessID, Native.Security.ProcessAccess.CreateThread Or _
                                                Native.Security.ProcessAccess.VmOperation Or _
                                                Native.Security.ProcessAccess.VmWrite Or _
                                                Native.Security.ProcessAccess.VmRead)
            If hProcess.IsNotNull Then
                hThread = NativeFunctions.CreateRemoteThread(hProcess, IntPtr.Zero, 0, _
                                                             lpProc, hHandle, 0, 0)
                If hThread.IsNotNull Then
                    NativeFunctions.WaitForSingleObject(hThread, NativeConstants.WAIT_INFINITE)
                    NativeFunctions.GetExitCodeThread(hThread, res)
                    NativeFunctions.CloseHandle(hThread)
                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return res
        End Function

        ' Return handles of some processes
        Public Shared Sub EnumerateHandleByProcessId(ByVal pid As Integer, _
                                                     ByVal showUnNamed As Boolean, _
                                                     ByRef _dico As Dictionary(Of String, handleInfos))

            ' Handle enumeration class not initialized...
            If hEnum Is Nothing Then
                Exit Sub
            End If

            ' Protection !
            SyncLock hEnum

                ' Refresh handles
                Call hEnum.Refresh(pid)

                For i As Integer = 0 To hEnum.Count - 1
                    If hEnum.IsNotNull(i) AndAlso hEnum.GetHandle(i).IsNotNull Then
                        If showUnNamed OrElse (Len(hEnum.GetObjectName(i)) > 0) Then

                            Dim _key As String = hEnum.GetHandleInfos(i).Key

                            ' This verification should not be needed, but in reality
                            ' it IS needed
                            ' TOCHECK
                            If _dico.ContainsKey(_key) = False Then
                                _dico.Add(_key, hEnum.GetHandleInfos(i))
                            End If

                        End If
                    End If
                Next

            End SyncLock

        End Sub

        ' Return all local handles
        Public Shared Function EnumerateCurrentLocalHandles(Optional ByVal all As Boolean = True) As Dictionary(Of String, cHandle)

            Dim _dico As New Dictionary(Of String, cHandle)

            ' Protection !
            SyncLock hEnum

                ' Handle enumeration class not initialized...
                If hEnum Is Nothing Then
                    Return _dico
                End If


                ' Refresh handles
                Call hEnum.Refresh(-1)    ' Refresh all

                For i As Integer = 0 To hEnum.Count - 1
                    If hEnum.IsNotNull(i) AndAlso hEnum.GetHandle(i).IsNotNull Then
                        If all OrElse (Len(hEnum.GetObjectName(i)) > 0) Then

                            Dim _key As String = hEnum.GetHandleInfos(i).Key

                            ' This verification should not be needed, but in reality
                            ' it IS needed
                            ' TOCHECK
                            If _dico.ContainsKey(_key) = False Then
                                _dico.Add(_key, New cHandle(hEnum.GetHandleInfos(i)))
                            End If

                        End If
                    End If
                Next

            End SyncLock

            Return _dico

        End Function



        ' Private functions



    End Class

End Namespace

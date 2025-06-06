﻿
' Lite Process Monitor









'




'



Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

Namespace Native.Objects

    Public Class Thread


        ' Private constants




        ' Private attributes


        ' Memory alloc for thread enumeration
        Private Shared memAllocForThreadEnum As New Native.Memory.MemoryAlloc(&H1000)   ' NOTE : never unallocated



        ' Public properties


        ' Min rights for Query
        Public Shared ReadOnly Property ThreadQueryMinRights() As Native.Security.ThreadAccess
            Get
                Static _minRights As Native.Security.ThreadAccess = Native.Security.ThreadAccess.QueryInformation
                Static checked As Boolean = False
                If checked = False Then
                    checked = True
                    If cEnvironment.SupportsMinRights Then
                        _minRights = Native.Security.ThreadAccess.QueryLimitedInformation
                    End If
                End If
                Return _minRights
            End Get
        End Property



        ' Other public




        ' Public functions


        ' Get affinity
        Public Shared Function GetThreadAffinityByHandle(ByVal handle As IntPtr) As IntPtr
            Dim infos As New Native.Api.NativeStructs.ThreadBasicInformation
            Dim ret As Integer
            Native.Api.NativeFunctions.NtQueryInformationThread(handle, _
                        Native.Api.NativeEnums.ThreadInformationClass.ThreadBasicInformation, _
                        infos, Marshal.SizeOf(infos), ret)
            Return infos.AffinityMask
        End Function

        ' Set affinity
        Public Shared Function SetThreadAffinityById(ByVal tid As Integer, ByVal affinity As IntPtr) As Boolean
            Dim hThread As IntPtr
            Dim r As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.QueryInformation Or _
                                                Security.ThreadAccess.SetInformation, _
                                                False, tid)
            If hThread.IsNotNull Then
                r = (Native.Api.NativeFunctions.SetThreadAffinityMask(hThread, affinity).IsNotNull)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If
            Return r
        End Function

        ' Set priority
        Public Shared Function SetThreadPriorityById(ByVal tid As Integer, ByVal priority As System.Diagnostics.ThreadPriorityLevel) As Boolean
            Dim hThread As IntPtr
            Dim r As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.SetInformation, _
                                                          False, tid)
            If hThread.IsNotNull Then
                r = Native.Api.NativeFunctions.SetThreadPriority(hThread, priority)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If
            Return r
        End Function

        ' Get priority of a thread
        Public Shared Function GetThreadPriorityByHandle(ByVal handle As IntPtr) As Integer
            Return Native.Api.NativeFunctions.GetThreadPriority(handle)
        End Function

        ' Get a valid handle on a thread
        Public Shared Function GetThreadHandle(ByVal tid As Integer, ByVal access As Native.Security.ThreadAccess) As IntPtr
            Return Native.Api.NativeFunctions.OpenThread(access, False, tid)
        End Function

        ' Resume a thread
        Public Shared Function ResumeThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread.IsNotNull Then
                Return NativeFunctions.ResumeThread(hThread) > 0
            Else
                Return False
            End If
        End Function
        Public Shared Function ResumeThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, resume thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(Security.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = ResumeThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function


        ' Suspend a thread
        Public Shared Function SuspendThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread.IsNotNull Then
                Return NativeFunctions.SuspendThread(hThread) > -1
            Else
                Return False
            End If
        End Function
        Public Shared Function SuspendThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, suspend thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(Security.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = SuspendThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function

        ' Kill a thread
        Public Shared Function KillThreadById(ByVal tid As Integer, _
                                              Optional ByVal exitCode As Integer = 0) As Boolean
            Dim hThread As IntPtr
            Dim ret As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.Terminate, _
                                                          False, tid)
            If hThread.IsNotNull Then
                ret = Native.Api.NativeFunctions.TerminateThread(hThread, exitCode)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If

            Return ret
        End Function

        ' Kill a thread 
        Public Shared Function KillThreadByHandle(ByVal hThread As IntPtr, _
                                      Optional ByVal exitCode As Integer = 0) As Boolean
            Dim ret As Boolean
            If hThread.IsNotNull Then
                ret = Native.Api.NativeFunctions.TerminateThread(hThread, exitCode)
            End If
            Return ret
        End Function

        ' Enumerate threads
        Public Shared Sub EnumerateThreadsByProcessId(ByRef _dico As Dictionary(Of String, threadInfos), ByVal pid As Integer)

            Dim deltaOff As Integer = Marshal.SizeOf(GetType(Native.Api.NativeStructs.SystemProcessInformation))

            Dim ret As Integer
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForThreadEnum.Pointer, _
                            memAllocForThreadEnum.Size, _
                            ret)
            If memAllocForThreadEnum.Size < ret Then
                memAllocForThreadEnum.ResizeNew(ret)
                Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                                memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)
            End If

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Do While True

                Dim obj As Native.Api.NativeStructs.SystemProcessInformation = _
                        memAllocForThreadEnum.ReadStructOffset(Of Native.Api.NativeStructs.SystemProcessInformation)(offset)

                ' If this is the desired process...
                If obj.ProcessId = pid Then
                    For j As Integer = 0 To obj.NumberOfThreads - 1

                        Dim thread As Native.Api.NativeStructs.SystemThreadInformation = _
                            memAllocForThreadEnum.ReadStruct(Of Native.Api.NativeStructs.SystemThreadInformation)(offset + deltaOff, j)

                        Dim _key As String = thread.ClientId.UniqueThread.ToString & "-" & thread.ClientId.UniqueProcess.ToString
                        Dim _th As New threadInfos(thread)
                        If _dico.ContainsKey(_key) = False Then
                            _dico.Add(_key, _th)
                        End If
                    Next
                End If

                offset += obj.NextEntryOffset

                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop

        End Sub




        ' Private functions



    End Class

End Namespace

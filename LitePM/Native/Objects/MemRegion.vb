﻿
' Lite Process Monitor









'




'



Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

Namespace Native.Objects

    Public Class MemRegion



        ' Private constants




        ' Private attributes




        ' Public properties




        ' Other public




        ' Public functions


        ' Change protection type
        Public Shared Function ChangeMemoryRegionProtectionType(ByVal processId As Integer, _
                                                                ByVal address As IntPtr, _
                                                                ByVal regSize As IntPtr, _
                                                                ByVal newProtection As NativeEnums.MemoryProtectionType) As Boolean
            Dim ret As Boolean
            Dim hProcess As IntPtr
            Dim old As NativeEnums.MemoryProtectionType

            hProcess = Native.Objects.Process.GetProcessHandleById(processId, Security.ProcessAccess.VmOperation)
            If hProcess.IsNotNull Then
                ret = NativeFunctions.VirtualProtectEx(hProcess, address, regSize.ToInt32, newProtection, old)
                Call NativeFunctions.CloseHandle(hProcess)
            End If

            Return ret

        End Function

        ' Free memory (decommit or release)
        Public Shared Function FreeMemory(ByVal processId As Integer, _
                                        ByVal address As IntPtr, _
                                        ByVal regSize As IntPtr, _
                                        ByVal type As NativeEnums.MemoryState) As Boolean

            Dim ret As Boolean
            Dim hProcess As IntPtr

            hProcess = Native.Objects.Process.GetProcessHandleById(processId, Security.ProcessAccess.VmOperation)
            If hProcess.IsNotNull Then
                ret = Native.Api.NativeFunctions.VirtualFreeEx(hProcess, address, regSize.ToInt32, type)
                Call Native.Api.NativeFunctions.CloseHandle(hProcess)
            End If

            Return ret

        End Function

        ' Dump memory
        Public Shared Function DumpMemory(ByVal processId As Integer, _
                                        ByVal address As IntPtr, _
                                        ByVal regSize As IntPtr, _
                                        ByVal file As String) As Boolean

            Dim ret As Boolean
            Using pRW As New ProcessRW(processId)
                ' Read from process memory
                Dim b() As Byte = pRW.ReadByteArray(address, regSize.ToInt32, ret)
                If ret Then
                    ' Create file (replace if existing)
                    Dim hFile As IntPtr = NativeFunctions.CreateFile(file, _
                                            NativeEnums.EFileAccess.GenericWrite, _
                                            NativeEnums.EFileShare.Read, _
                                            IntPtr.Zero, _
                                            NativeEnums.ECreationDisposition.CreateAlways, _
                                            0, _
                                            IntPtr.Zero)
                    If hFile.IsNotNull Then
                        ' Save file
                        Dim res As Integer
                        Dim ol As New Threading.NativeOverlapped
                        NativeFunctions.WriteFile(hFile, _
                                                  b, _
                                                  regSize.ToInt32, _
                                                  res, _
                                                  IntPtr.Zero)

                        ' Success ?
                        ret = (res = regSize.ToInt32)

                        ' Close file handle
                        Objects.General.CloseHandle(hFile)
                    Else
                        ret = False
                    End If
                End If
                ReDim b(0)
            End Using

            Return ret

        End Function
        ' Enumerate memory regions
        Public Shared Sub EnumerateMemoryRegionsByProcessId(ByVal pid As Integer, _
                                    ByRef _dico As Dictionary(Of String, memRegionInfos))
            Dim lHandle As IntPtr
            Dim lPosMem As IntPtr
            Dim mbi As New Native.Api.NativeStructs.MemoryBasicInformation
            Dim mbiSize As Integer = Marshal.SizeOf(mbi)

            lHandle = Native.Objects.Process.GetProcessHandleById(pid, Security.ProcessAccess.QueryInformation Or _
                                                                Security.ProcessAccess.VmRead)

            If lHandle.IsNotNull Then
                ' We'll exit when VirtualQueryEx will fail
                Do While Native.Api.NativeFunctions.VirtualQueryEx(lHandle, lPosMem, mbi, mbiSize) <> 0
                    _dico.Add(mbi.BaseAddress.ToString, _
                              New memRegionInfos(mbi, pid))

                    lPosMem = lPosMem.Increment(mbi.RegionSize)
                Loop
                Native.Api.NativeFunctions.CloseHandle(lHandle)
            End If

        End Sub




        ' Private functions



    End Class

End Namespace

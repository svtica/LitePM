﻿
' Lite Process Monitor









'




'




Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports Native.Api.NativeEnums
Imports Native.Api.NativeFunctions

Namespace Native.Api

    Public Class Win32



        ' Private constants




        ' Private attributes


        ' Contains NtStatus <-> Description
        Private Shared _dicoNtStatus As New Dictionary(Of UInt32, String)



        ' Public properties




        ' Other public




        ' Public functions


        ' Return last error as a string
        Public Shared Function GetLastError() As String

            Dim nLastError As Integer = Marshal.GetLastWin32Error

            If nLastError = 0 Then
                ' Error occured
                Return ""
            Else

                Dim lpMsgBuf As New System.Text.StringBuilder(&H100)
                Dim dwChars As UInteger = FormatMessage(FormatMessageFlags.AllocateBuffer _
                                                        Or FormatMessageFlags.FromSystem _
                                                        Or FormatMessageFlags.MessageIgnoreInserts, _
                                            IntPtr.Zero, nLastError, 0, lpMsgBuf, lpMsgBuf.Capacity, IntPtr.Zero)

                ' Unknown error
                If dwChars = 0 Then
                    Return "Unknown error occured (0x" & nLastError.ToString("x") & ")"
                End If

                ' Retrieve string
                Return lpMsgBuf.ToString
            End If

        End Function


        ' Get elapsed time since Windows started
        Public Shared Function GetElapsedTime() As Integer
            Return Native.Api.NativeFunctions.GetTickCount
        End Function


        ' Return message associated to a NtStatus
        Public Shared Function GetNtStatusMessageAsString(ByVal status As UInt32) As String

            Dim sRes As String
            If status = 0 Then

                sRes = "Success"

            Else

                ' If the status has already been retrieved, return result immediately
                If _dicoNtStatus.ContainsKey(status) Then
                    Return _dicoNtStatus.Item(status)
                End If

                Dim lpMessageBuffer As New StringBuilder(&H200)
                Dim Hand As IntPtr = LoadLibrary("NTDLL.DLL")

                ' Get the buffer
                FormatMessage(FormatMessageFlags.AllocateBuffer Or _
                            FormatMessageFlags.FromSystem Or _
                            FormatMessageFlags.FromHModule, _
                            Hand, _
                            status, _
                            MAKELANGID(NativeConstants.LANG_NEUTRAL, _
                                     NativeConstants.SUBLANG_DEFAULT), _
                            lpMessageBuffer, _
                            0, _
                            Nothing)

                ' Now get the string
                sRes = lpMessageBuffer.ToString
                FreeLibrary(Hand)

                ' Add to dico
                If _dicoNtStatus.ContainsKey(status) = False Then
                    _dicoNtStatus.Add(status, sRes)
                End If

            End If

            Return sRes
        End Function




        ' Private functions


        Private Shared Function MAKELANGID(ByVal primary As Integer, ByVal [sub] As Integer) As Integer
            Return (CUShort([sub]) << 10) Or CUShort(primary)
        End Function
        Private Shared Function PRIMARYLANGID(ByVal lcid As Integer) As Integer
            Return CUShort(lcid) And &H3FF
        End Function
        Private Shared Function SUBLANGID(ByVal lcid As Integer) As Integer
            Return CUShort(lcid) >> 10
        End Function

    End Class

End Namespace

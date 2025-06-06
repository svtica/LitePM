﻿
' Lite Process Monitor









'




'




Option Strict On
Imports Native.Api.NativeEnums

Namespace Native.Functions

    Public Class Service

        ' Get state/errorcontrol/starttype from a string as a type
        Public Shared Function GetServiceErrorControlFromStringH(ByVal s As String) As ServiceErrorControl
            Select Case s
                Case "Ignore"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Ignore
                Case "Normal"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Normal
                Case "Severe"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Severe
                Case "Critical"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Critical
                Case Else
                    Return Native.Api.NativeEnums.ServiceErrorControl.Unknown
            End Select
        End Function

        Public Shared Function GetServiceStartTypeFromStringH(ByVal s As String) As ServiceStartType
            Select Case s
                Case "Boot"
                    Return Native.Api.NativeEnums.ServiceStartType.BootStart
                Case "System"
                    Return Native.Api.NativeEnums.ServiceStartType.SystemStart
                Case "Auto"
                    Return Native.Api.NativeEnums.ServiceStartType.AutoStart
                Case "Manual"
                    Return Native.Api.NativeEnums.ServiceStartType.DemandStart
                Case "Disabled"
                    Return Native.Api.NativeEnums.ServiceStartType.StartDisabled
            End Select
        End Function

        Public Shared Function GetServiceStateFromStringH(ByVal s As String) As ServiceState
            Select Case s
                Case "Stopped"
                    Return Native.Api.NativeEnums.ServiceState.Stopped
                Case "Start Pending"
                    Return Native.Api.NativeEnums.ServiceState.StartPending
                Case "Stop Pending"
                    Return Native.Api.NativeEnums.ServiceState.StopPending
                Case "Running"
                    Return Native.Api.NativeEnums.ServiceState.Running
                Case "Continue Pending"
                    Return Native.Api.NativeEnums.ServiceState.ContinuePending
                Case "Pause Pending"
                    Return Native.Api.NativeEnums.ServiceState.PausePending
                Case "Paused"
                    Return Native.Api.NativeEnums.ServiceState.Paused
                Case Else
                    Return Native.Api.NativeEnums.ServiceState.Unknown
            End Select
        End Function

        Public Shared Function GetServiceTypeFromStringH(ByVal s As String) As ServiceType
            Select Case s
                Case "Kernel Driver"
                    Return Native.Api.NativeEnums.ServiceType.KernelDriver
                Case "File System Driver"
                    Return Native.Api.NativeEnums.ServiceType.FileSystemDriver
                Case "Adapter"
                    Return Native.Api.NativeEnums.ServiceType.Adapter
                Case "Recognizer Driver"
                    Return Native.Api.NativeEnums.ServiceType.RecognizerDriver
                Case "Own Process"
                    Return Native.Api.NativeEnums.ServiceType.Win32OwnProcess
                Case "Share Process"
                    Return Native.Api.NativeEnums.ServiceType.Win32ShareProcess
                Case "Interactive Process"
                    Return Native.Api.NativeEnums.ServiceType.InteractiveProcess
            End Select
        End Function

        Public Shared Function GetServiceErrorControlFromString(ByVal s As String) As ServiceErrorControl
            Return DirectCast([Enum].Parse(GetType(ServiceErrorControl), s), ServiceErrorControl)
        End Function

        Public Shared Function GetServiceStartTypeFromString(ByVal s As String) As ServiceStartType
            Return DirectCast([Enum].Parse(GetType(ServiceStartType), s), ServiceStartType)
        End Function

        Public Shared Function GetServiceStateFromString(ByVal s As String) As ServiceState
            Return DirectCast([Enum].Parse(GetType(ServiceState), s), ServiceState)
        End Function

        Public Shared Function GetServiceTypeFromString(ByVal s As String) As ServiceType
            Return DirectCast([Enum].Parse(GetType(ServiceType), s), ServiceType)
        End Function

    End Class

End Namespace

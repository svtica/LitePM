﻿
' Lite Process Monitor









'




'




Option Strict On

Imports System.Net
Imports System.Runtime.InteropServices
Imports Native.Api.Enums

Namespace Native.Api

    Public Class Structs

        ' OK
#Region "Declarations used for network"

        Public Structure LightConnection
            Dim dwState As Integer
            Dim local As IPEndPoint
            Dim remote As IPEndPoint
            Dim dwOwningPid As Integer
            Dim dwType As NetworkProtocol
        End Structure

        Public Structure NicDescription
            Public Name As String
            Public Description As String
            Public Ip As String
            Public Sub New(ByVal aName As String, ByVal aDesc As String, ByVal aIp As String)
                Name = aName
                Description = aDesc
                Ip = aIp
            End Sub
        End Structure

#End Region

        ' OK
#Region "Declarations used for processes"

        Public Structure ProcTimeInfo
            Dim time As Long
            Dim kernel As Long
            Dim user As Long
            Dim total As Long
            Public Sub New(ByVal aTime As Long, ByVal aUser As Long, ByVal aKernel As Long)
                time = aTime
                kernel = aKernel
                user = aUser
                total = user + kernel
            End Sub
        End Structure

        Public Structure ProcMemInfo
            Dim time As Long
            Dim mem As VmCountersEx64
            Public Sub New(ByVal aTime As Long, ByRef aMem As VmCountersEx64)
                time = aTime
                mem = aMem
            End Sub
        End Structure

        Public Structure ProcIoInfo
            Dim time As Long
            Dim io As Native.Api.NativeStructs.IoCounters
            Public Sub New(ByVal aTime As Long, ByRef aIo As Native.Api.NativeStructs.IoCounters)
                time = aTime
                io = aIo
            End Sub
        End Structure

        Public Structure ProcMiscInfo
            Dim time As Long
            Dim gdiO As Integer
            Dim userO As Integer
            Dim cpuUsage As Double
            Dim averageCpuUsage As Double
            Public Sub New(ByVal aTime As Long, ByVal aGdi As Integer, ByVal aUser As _
                           Integer, ByVal aCpu As Double, ByVal aAverage As Double)
                time = aTime
                gdiO = aGdi
                userO = aUser
                cpuUsage = aCpu
                averageCpuUsage = aAverage
            End Sub
        End Structure

        ' This "encapsulate" SystemProcessInformation structure which is OS-dependent
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemProcessInformation64
            Public NextEntryOffset As Integer
            Public NumberOfThreads As Integer
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
            Public Reserved1 As Long()
            Public CreateTime As Long
            Public UserTime As Long
            Public KernelTime As Long
            Public ImageName As NativeStructs.UnicodeString
            Public BasePriority As Integer
            ' This two variables are private cause we prefer
            ' access to ProcessId and Inherited...Id as Int32
            Private _ProcessId As IntPtr
            Private _InheritedFromProcessId As IntPtr
            Public HandleCount As Integer
            Public SessionId As Integer
            Public PageDirectoryBase As IntPtr
            Public VirtualMemoryCounters As VmCountersEx64
            Public IoCounters As NativeStructs.IoCounters

            ' 2 properties to access to private variables
            Public Property ProcessId() As Integer
                Get
                    Return _ProcessId.ToInt32
                End Get
                Set(ByVal value As Integer)
                    _ProcessId = New IntPtr(value)
                End Set
            End Property
            Public Property InheritedFromProcessId() As Integer
                Get
                    Return _InheritedFromProcessId.ToInt32
                End Get
                Set(ByVal value As Integer)
                    _InheritedFromProcessId = New IntPtr(value)
                End Set
            End Property

            Public Sub New(ByVal procInfo As NativeStructs.SystemProcessInformation)
                With procInfo
                    NextEntryOffset = .NextEntryOffset
                    NumberOfThreads = .NumberOfThreads
                    Reserved1 = .Reserved1
                    CreateTime = .CreateTime
                    UserTime = .UserTime
                    KernelTime = .KernelTime
                    ImageName = .ImageName
                    BasePriority = .BasePriority
                    ProcessId = .ProcessId
                    InheritedFromProcessId = .InheritedFromProcessId
                    HandleCount = .HandleCount
                    SessionId = .SessionId
                    PageDirectoryBase = .PageDirectoryBase
                    VirtualMemoryCounters = .VirtualMemoryCounters.ToVmCountersEx64
                    IoCounters = .IoCounters
                End With
            End Sub

        End Structure

        ' This "encapsulate" VmCountersEx structure which is OS-dependent
        <StructLayout(LayoutKind.Sequential), Serializable()> _
        Public Structure VmCountersEx64
            Public PeakVirtualSize As Long
            Public VirtualSize As Long
            Public PageFaultCount As Integer
            Public PeakWorkingSetSize As Long
            Public WorkingSetSize As Long
            Public QuotaPeakPagedPoolUsage As Long
            Public QuotaPagedPoolUsage As Long
            Public QuotaPeakNonPagedPoolUsage As Long
            Public QuotaNonPagedPoolUsage As Long
            Public PagefileUsage As Long
            Public PeakPagefileUsage As Long
            Public PrivateBytes As Long
            Public Shared Operator <>(ByVal m1 As VmCountersEx64, ByVal m2 As VmCountersEx64) As Boolean
                Return Not (m1 = m2)
            End Operator
            Public Shared Operator =(ByVal i1 As VmCountersEx64, ByVal i2 As VmCountersEx64) As Boolean
                Return (i1.PeakVirtualSize = i2.PeakVirtualSize AndAlso _
                    i1.VirtualSize = i2.VirtualSize AndAlso _
                    i1.PageFaultCount = i2.PageFaultCount AndAlso _
                    i1.PeakWorkingSetSize = i2.PeakWorkingSetSize AndAlso _
                    i1.WorkingSetSize = i2.WorkingSetSize AndAlso _
                    i1.QuotaPeakPagedPoolUsage = i2.QuotaPeakPagedPoolUsage AndAlso _
                    i1.QuotaPagedPoolUsage = i2.QuotaPagedPoolUsage AndAlso _
                    i1.QuotaPeakNonPagedPoolUsage = i2.QuotaPeakNonPagedPoolUsage AndAlso _
                    i1.QuotaNonPagedPoolUsage = i2.QuotaNonPagedPoolUsage AndAlso _
                    i1.PagefileUsage = i2.PagefileUsage AndAlso _
                    i1.PeakPagefileUsage = i2.PeakPagefileUsage AndAlso _
                    i1.PrivateBytes = i2.PrivateBytes)
            End Operator
            Public Sub New(ByVal vmCounter As NativeStructs.VmCountersEx)
                With vmCounter
                    PeakVirtualSize = .PeakVirtualSize.ToInt64
                    VirtualSize = .VirtualSize.ToInt64
                    PageFaultCount = .PageFaultCount
                    PeakWorkingSetSize = .PeakWorkingSetSize.ToInt64
                    WorkingSetSize = .WorkingSetSize.ToInt64
                    QuotaPeakPagedPoolUsage = .QuotaPeakPagedPoolUsage.ToInt64
                    QuotaPagedPoolUsage = .QuotaPagedPoolUsage.ToInt64
                    QuotaPeakNonPagedPoolUsage = .QuotaPeakNonPagedPoolUsage.ToInt64
                    QuotaNonPagedPoolUsage = .QuotaNonPagedPoolUsage.ToInt64
                    PagefileUsage = .PagefileUsage.ToInt64
                    PeakPagefileUsage = .PeakPagefileUsage.ToInt64
                    PrivateBytes = .PrivateBytes.ToInt64
                End With
            End Sub
        End Structure

#End Region

        ' OK
#Region "Declarations used for services"

        Public Structure ServiceCreationParams
            Public ServiceName As String
            Public DisplayName As String
            Public Type As NativeEnums.ServiceType
            Public StartType As NativeEnums.ServiceStartType
            Public ErrorControl As NativeEnums.ServiceErrorControl
            Public FilePath As String
            Public Arguments As String
            Public RegMachine As String
            Public RegUser As String
            Public RegPassword As System.Security.SecureString
        End Structure

#End Region

        ' OK
#Region "Declarations used for windows"

        ' These are non-fixed infos about a window
        Public Structure WindowNonFixedInfo
            Public enabled As Boolean
            Public height As Integer
            Public isTask As Boolean
            Public left As Integer
            Public opacity As Byte
            Public top As Integer
            Public visible As Boolean
            Public width As Integer
            Public theRect As Native.Api.NativeStructs.Rect
            Public caption As String
            Public Sub New(ByVal enab As Boolean, ByVal isTas As Boolean, _
                           ByVal opac As Byte, ByRef r As Native.Api.NativeStructs.Rect, ByVal scap As String, ByVal isV As Boolean)
                enabled = enab
                isTask = isTas
                opacity = opac
                height = r.Bottom - r.Top
                width = r.Right - r.Left
                top = r.Top
                left = r.Left
                theRect = r
                caption = scap
                visible = isV
            End Sub
        End Structure

#End Region

#Region "Other declarations"

        Public Structure PerfCounter
            Public InstanceName As String
            Public CounterTypeName As String
            Public CategoryName As String
            Public Sub New(ByVal aCat As String, ByVal aCount As String, ByVal aName As String)
                InstanceName = aName
                CounterTypeName = aCount
                CategoryName = aCat
            End Sub
        End Structure

        Public Structure QueryResult
            Public Success As Boolean
            Public ErrorMessage As String
            Public [Exception] As Exception
            Public Sub New(ByVal ok As Boolean, Optional ByVal errorMsg As String = Nothing)
                Success = ok
                ErrorMessage = errorMsg
            End Sub
            Public Sub New(ByVal ex As Exception)
                Success = False
                ErrorMessage = ex.Message
                [Exception] = ex
            End Sub
        End Structure

#End Region

    End Class

End Namespace
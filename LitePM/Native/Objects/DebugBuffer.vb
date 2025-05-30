
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Namespace Native.Objects

    Public Class DebugBuffer
        Implements IDisposable



        ' Private constants




        ' Private attributes

        Private _buffer As IntPtr = IntPtr.Zero



        ' Operators

        Public Shared Widening Operator CType(ByVal buf As DebugBuffer) As Integer
            Return buf.Buffer.ToInt32()
        End Operator

        Public Shared Widening Operator CType(ByVal buf As DebugBuffer) As Long
            Return buf.Buffer.ToInt64()
        End Operator

        Public Shared Widening Operator CType(ByVal buf As DebugBuffer) As IntPtr
            Return buf.Buffer
        End Operator



        ' Public properties

        Public ReadOnly Property Buffer() As IntPtr
            Get
                Return _buffer
            End Get
        End Property



        ' Other public




        ' Public functions


        ' Constructor
        Public Sub New()
            _buffer = NativeFunctions.RtlCreateQueryDebugBuffer(0, True)
        End Sub

        ' Return memory as DebugInformation
        Public Function GetDebugInformation() As NativeStructs.DebugInformation
            Dim res As NativeStructs.DebugInformation = Nothing
            If _buffer.IsNotNull Then
                Dim mem As New Native.Memory.MemoryAlloc(_buffer)
                res = mem.ReadStruct(Of NativeStructs.DebugInformation)()
            End If
            Return res
        End Function

        ' Query debug information
        Public Sub Query(ByVal pid As Integer, ByVal flags As NativeEnums.RtlQueryProcessDebugInformationFlags)
            If _buffer.IsNotNull Then
                NativeFunctions.RtlQueryProcessDebugInformation(New IntPtr(pid), flags, _buffer)
            End If
        End Sub

        ' Destructor
        Public Sub Dispose() Implements IDisposable.Dispose
            If _buffer.IsNotNull Then
                NativeFunctions.RtlDestroyQueryDebugBuffer(_buffer)
            End If
        End Sub

    End Class

End Namespace
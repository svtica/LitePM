﻿
' Lite Process Monitor









'




'


'
' This class has been inspired (but simplified for LitePM)
' by a work of wj32 (Process Hacker)

Imports System.Runtime.InteropServices
Imports System.Text

Namespace Native.Memory

    Public Class MemoryAlloc
        Implements IDisposable


        ' Private

        Private _ptr As IntPtr
        Private _size As Integer




        ' Operators

        Public Shared Widening Operator CType(ByVal memory As MemoryAlloc) As Integer
            Return memory.Pointer.ToInt32()
        End Operator

        Public Shared Widening Operator CType(ByVal memory As MemoryAlloc) As Long
            Return memory.Pointer.ToInt64()
        End Operator

        Public Shared Widening Operator CType(ByVal memory As MemoryAlloc) As IntPtr
            Return memory.Pointer
        End Operator




        ' Constructors / destructors

        Public Sub New()
            ' Invalid pointer which should manually be set
        End Sub
        Public Sub New(ByVal ptr As IntPtr)
            Me.New(ptr, 0)
        End Sub
        Public Sub New(ByVal ptr As IntPtr, ByVal size As Integer)
            MyBase.New()
            _ptr = ptr
            _size = size
        End Sub
        Public Sub New(ByVal size As Integer)
            _ptr = Marshal.AllocHGlobal(size)
            _size = size
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Free()
        End Sub




        ' Public properties

        Public Property Pointer() As IntPtr
            Get
                Return _ptr
            End Get
            Set(ByVal value As IntPtr)
                _ptr = value
            End Set
        End Property
        Public Property Size() As Integer
            Get
                Return _size
            End Get
            Set(ByVal value As Integer)
                _size = value
            End Set
        End Property




        ' Public functions


        ' Free memory
        Public Sub Free()
            If Me.Pointer <> IntPtr.Zero Then
                Marshal.FreeHGlobal(Me.Pointer)
            End If
        End Sub

        ' Read functions
        Public Function ReadBytes(ByVal length As Integer) As Byte()
            Return Me.ReadBytes(0, length)
        End Function
        Public Function ReadBytes(ByVal offset As Integer, ByVal length As Integer) As Byte()
            Dim buffer(length - 1) As Byte
            Me.ReadBytes(offset, buffer, 0, length)
            Return buffer
        End Function
        Public Sub ReadBytes(ByVal buffer As Byte(), ByVal startIndex As Integer, ByVal length As Integer)
            Me.ReadBytes(0, buffer, startIndex, length)
        End Sub
        Public Sub ReadBytes(ByVal offset As Integer, ByVal buffer As Byte(), ByVal startIndex As Integer, ByVal length As Integer)
            Marshal.Copy(_ptr.Increment(offset), buffer, startIndex, length)
        End Sub

        Public Function ReadByte(ByVal offset As Integer) As Integer
            Return Me.ReadByte(offset, 0)
        End Function
        Public Function ReadByte(ByVal offset As Integer, ByVal index As Integer) As Integer
            Return Marshal.ReadByte(_ptr, offset + index * 4)
        End Function

        Public Function ReadInt32(ByVal offset As Integer) As Integer
            Return Me.ReadInt32(offset, 0)
        End Function
        Public Function ReadInt32(ByVal offset As Integer, ByVal index As Integer) As Integer
            Return Marshal.ReadInt32(_ptr, offset + index * 4)
        End Function

        Public Function ReadIntPtr(ByVal offset As Integer) As IntPtr
            Return Me.ReadIntPtr(offset, 0)
        End Function
        Public Function ReadIntPtr(ByVal offset As Integer, ByVal index As Integer) As IntPtr
            Return Marshal.ReadIntPtr(_ptr, offset + index * IntPtr.Size)
        End Function

        Public Function ReadUInt32(ByVal offset As Integer) As UInteger
            Return Me.ReadUInt32(offset, 0)
        End Function
        Public Function ReadUInt32(ByVal offset As Integer, ByVal index As Integer) As UInteger
            Return CUInt(Me.ReadInt32(offset, index))
        End Function

        Public Function ReadStruct(Of T)() As T
            Return Me.ReadStruct(Of T)(0)
        End Function
        Public Function ReadStruct(Of T)(ByVal index As Integer) As T
            Return Me.ReadStruct(Of T)(0, index)
        End Function
        Public Function ReadStruct(Of T)(ByVal offset As Integer, ByVal index As Integer) As T
            Return DirectCast(Marshal.PtrToStructure(_ptr.Increment(offset + Marshal.SizeOf(GetType(T)) * index), GetType(T)), T)
        End Function
        Public Function ReadStructOffset(Of T)(ByVal offset As Integer) As T
            Return DirectCast(Marshal.PtrToStructure(_ptr.Increment(offset), GetType(T)), T)
        End Function

        ' Resize memory allocation
        Public Sub Resize(ByVal newSize As Integer)
            _ptr = Marshal.ReAllocHGlobal(_ptr, New IntPtr(newSize))
            _size = newSize
        End Sub
        Public Sub ResizeNew(ByVal newSize As Integer)
            ' This is 2x faster then re-allocating a new size to an existing pointer
            Marshal.FreeHGlobal(_ptr)
            _ptr = Marshal.AllocHGlobal(newSize)
            _size = newSize
        End Sub
        Public Sub IncrementSize(ByVal newBytesCount As Integer)
            _ptr = Marshal.ReAllocHGlobal(_ptr, New IntPtr(newBytesCount + _size))
            _size = newBytesCount + _size
        End Sub


        ' Write functions
        Public Sub WriteByte(ByVal offset As Integer, ByVal b As Byte)
            Marshal.WriteByte(Me, offset, b)
        End Sub
        Public Sub WriteBytes(ByVal offset As Integer, ByVal b As Byte())
            Marshal.Copy(b, 0, _ptr.Increment(offset), b.Length)
        End Sub

        Public Sub WriteInt16(ByVal offset As Integer, ByVal i As Short)
            Marshal.WriteInt16(Me, offset, i)
        End Sub
        Public Sub WriteInt32(ByVal offset As Integer, ByVal i As Integer)
            Marshal.WriteInt32(Me, offset, i)
        End Sub
        Public Sub WriteIntPtr(ByVal offset As Integer, ByVal i As IntPtr)
            Marshal.WriteIntPtr(Me, offset, i)
        End Sub

        Public Sub WriteStruct(Of T)(ByVal s As T)
            Me.WriteStruct(Of T)(0, s)
        End Sub
        Public Sub WriteStruct(Of T)(ByVal index As Integer, ByVal s As T)
            Me.WriteStruct(Of T)(0, index, s)
        End Sub
        Public Sub WriteStruct(Of T)(ByVal offset As Integer, ByVal index As Integer, ByVal s As T)
            Marshal.StructureToPtr(s, _ptr.Increment(offset + Marshal.SizeOf(GetType(T)) * index), False)
        End Sub

        Public Sub WriteUnicodeString(ByVal offset As Integer, ByVal s As String)
            Dim b As Byte() = UnicodeEncoding.Unicode.GetBytes(s)

            For i As Integer = 0 To b.Length - 1
                Marshal.WriteByte(Me.Pointer, offset + i, b(i))
            Next
        End Sub

    End Class

End Namespace
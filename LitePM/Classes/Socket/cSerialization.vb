
' Lite Process Monitor


Option Strict On

Imports System.IO
Imports System.IO.Compression

Public Class cSerialization

    ' Return byte array from data class
    Public Shared Function GetSerializedObject(ByVal obj As Object) As Byte()
        Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Using ms As New MemoryStream()
            formatter.Serialize(ms, obj)
            Return CompressByteArray(ms.ToArray())
        End Using
    End Function

    ' Return data class from byte array
    Public Shared Function DeserializeObject(ByVal dataBytes As Byte()) As cSocketData
        Try
            Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Using ms As New MemoryStream(DeCompressByteArray(dataBytes))
                Return DirectCast(formatter.Deserialize(ms), cSocketData)
            End Using
        Catch ex As Exception
            Trace.WriteLine("Error during serialization : " & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Shared Function DeserializeObject(Of T)(ByVal dataBytes As Byte()) As T
        Try
            Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Using ms As New MemoryStream(DeCompressByteArray(dataBytes))
                Return DirectCast(formatter.Deserialize(ms), T)
            End Using
        Catch ex As Exception
            Trace.WriteLine("Error during serialization : " & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Shared Function DeserializeObject(Of T)(ByVal dataBytes As Byte(), ByVal binder As System.Runtime.Serialization.SerializationBinder) As T
        Try
            Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Using ms As New MemoryStream(DeCompressByteArray(dataBytes))
                formatter.Binder = binder
                Return DirectCast(formatter.Deserialize(ms), T)
            End Using
        Catch ex As Exception
            Trace.WriteLine("Error during serialization : " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Shared Function CompressByteArray(ByRef b() As Byte) As Byte()
        'Return b
        Dim ms As New MemoryStream()
        Dim s As Stream = New GZipStream(ms, CompressionMode.Compress)
        s.Write(b, 0, b.Length)
        s.Close()
        Return DirectCast(ms.ToArray(), Byte())
    End Function

    Private Shared Function DeCompressByteArray(ByRef b() As Byte) As Byte()
        ' Return b
        Dim writeData(4096) As Byte ' = new byte[4096]
        Dim memStream As MemoryStream = New MemoryStream()
        Dim s2 As Stream = New GZipStream(New MemoryStream(b), CompressionMode.Decompress)
        Dim size As Integer = 1

        While (size > 0)
            size = s2.Read(writeData, 0, writeData.Length)
            memStream.Write(writeData, 0, size)
            memStream.Flush()
        End While
        Return memStream.ToArray()

    End Function

End Class


' Lite Process Monitor









'




'




Option Strict On

Public Class InstanceIdProvider


    ' Private constants




    ' Private attributes


    ' Instance ID 
    Private Shared _instanceId As Integer



    ' Public properties




    ' Other public




    ' Public functions


    ' Get a new unique instance id
    Public Shared Function GetNewInstanceId() As Integer
        System.Threading.Interlocked.Increment(_instanceId)
        Return _instanceId
    End Function



    ' Private functions



End Class

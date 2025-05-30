
' Lite Process Monitor


Imports System.ServiceProcess
Imports System.Threading

Namespace LitePMLauncherService

    Partial Class InteractiveProcess
        Inherits ServiceBase

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub OnStart(ByVal args As String())
            ThreadPool.QueueUserWorkItem(AddressOf LaunchService)
        End Sub

        Public Sub LaunchService(ByVal context As Object)

            ' Parse port text file from resources
            Call cNetwork.ParsePortTextFile()

            ' Enable some privileges
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.ShutdownPrivilege)

            ' Instanciate 'forms'
            _frmServer = New frmServer
            _frmServer.Show()

        End Sub

    End Class

End Namespace
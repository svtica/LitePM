
' Lite Process Monitor


Option Strict On

Imports Common.Misc

Public Class frmNetworkInfo

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick

        ' Get infos
        Dim udp As Native.Api.NativeStructs.MibUdpStats = _
                            Native.Objects.Network.GetUdpStatistics
        Dim tcp As Native.Api.NativeStructs.MibTcpStats = _
                            Native.Objects.Network.GetTcpStatistics


        ' UDP stats
        Static _old_InDatagrams As UInteger = 0
        Static _old_OutDatagrams As UInteger = 0
        Static _old_InErrors As UInteger = 0
        Static _old_NoPort As UInteger = 0
        Static _old_NumAddrs As UInteger = 0

        If _old_InDatagrams = 0 Then _old_InDatagrams = udp.InDatagrams
        If _old_OutDatagrams = 0 Then _old_OutDatagrams = udp.OutDatagrams
        If _old_InErrors = 0 Then _old_InErrors = udp.InErrors
        If _old_NoPort = 0 Then _old_NoPort = udp.NoPorts
        If _old_NumAddrs = 0 Then _old_NumAddrs = udp.NumAddrs

        Dim _diff_InDatagrams As UInteger = udp.InDatagrams - _old_InDatagrams
        Dim _diff_OutDatagrams As UInteger = udp.OutDatagrams - _old_OutDatagrams
        Dim _diff_InErrors As UInteger = udp.InErrors - _old_InErrors
        Dim _diff_NoPort As UInteger = udp.NoPorts - _old_NoPort
        Dim _diff_NumAddrs As UInteger = udp.NumAddrs - _old_NumAddrs

        _old_InDatagrams = udp.InDatagrams
        _old_OutDatagrams = udp.OutDatagrams
        _old_InErrors = udp.InErrors
        _old_NoPort = udp.NoPorts
        _old_NumAddrs = udp.NumAddrs

        Me.lblInDatagrams.Text = String.Format("{0} ({1})", _old_InDatagrams.ToString, _diff_InDatagrams.ToString)
        Me.lblOutDatagrams.Text = String.Format("{0} ({1})", _old_OutDatagrams.ToString, _diff_OutDatagrams.ToString)
        Me.lblInErrors.Text = String.Format("{0} ({1})", _old_InErrors.ToString, _diff_InErrors.ToString)
        Me.lblNoPorts.Text = String.Format("{0} ({1})", _old_NoPort.ToString, _diff_NoPort.ToString)
        Me.lblNumAddrs.Text = String.Format("{0} ({1})", _old_NumAddrs.ToString, _diff_NumAddrs.ToString)



        ' TCP stats
        Static _old_ActiveOpens As UInteger = 0
        Static _old_PassiveOpens As UInteger = 0
        Static _old_AttemptFails As UInteger = 0
        Static _old_EstabResets As UInteger = 0
        Static _old_CurrEstab As UInteger = 0
        Static _old_InSegs As UInteger = 0
        Static _old_OutSegs As UInteger = 0
        Static _old_RetransSegs As UInteger = 0
        Static _old_InErrs As UInteger = 0
        Static _old_OutRsts As UInteger = 0
        Static _old_NumConns As UInteger = 0

        If _old_ActiveOpens = 0 Then _old_ActiveOpens = tcp.ActiveOpens
        If _old_PassiveOpens = 0 Then _old_PassiveOpens = tcp.PassiveOpens
        If _old_AttemptFails = 0 Then _old_AttemptFails = tcp.AttemptFails
        If _old_EstabResets = 0 Then _old_EstabResets = tcp.EstabResets
        If _old_CurrEstab = 0 Then _old_CurrEstab = tcp.CurrEstab
        If _old_InSegs = 0 Then _old_InSegs = tcp.InSegs
        If _old_OutSegs = 0 Then _old_OutSegs = tcp.OutSegs
        If _old_RetransSegs = 0 Then _old_RetransSegs = tcp.RetransSegs
        If _old_InErrs = 0 Then _old_InErrs = tcp.InErrs
        If _old_OutRsts = 0 Then _old_OutRsts = tcp.OutRsts
        If _old_NumConns = 0 Then _old_NumConns = tcp.NumConns

        Dim _diff_ActiveOpens As UInteger = tcp.ActiveOpens - _old_ActiveOpens
        Dim _diff_PassiveOpens As UInteger = tcp.PassiveOpens - _old_PassiveOpens
        Dim _diff_AttemptFails As UInteger = tcp.AttemptFails - _old_AttemptFails
        Dim _diff_EstabResets As UInteger = tcp.EstabResets - _old_EstabResets
        Dim _diff_CurrEstab As UInteger = tcp.CurrEstab - _old_CurrEstab
        Dim _diff_InSegs As UInteger = tcp.InSegs - _old_InSegs
        Dim _diff_OutSegs As UInteger = tcp.OutSegs - _old_OutSegs
        Dim _diff_RetransSegs As UInteger = tcp.RetransSegs - _old_RetransSegs
        Dim _diff_InErrs As UInteger = tcp.InErrs - _old_InErrs
        Dim _diff_OutRsts As UInteger = tcp.OutRsts - _old_OutRsts
        Dim _diff_NumConns As UInteger = tcp.NumConns - _old_NumConns

        _old_ActiveOpens = tcp.ActiveOpens
        _old_PassiveOpens = tcp.PassiveOpens
        _old_AttemptFails = tcp.AttemptFails
        _old_EstabResets = tcp.EstabResets
        _old_CurrEstab = tcp.CurrEstab
        _old_InSegs = tcp.InSegs
        _old_OutSegs = tcp.OutSegs
        _old_RetransSegs = tcp.RetransSegs
        _old_InErrs = tcp.InErrs
        _old_OutRsts = tcp.OutRsts
        _old_NumConns = tcp.NumConns

        Me.lblRtoAlgo.Text = tcp.RtoAlgorithm.ToString
        Me.lblRtoMin.Text = tcp.RtoMin.ToString
        Me.lblRtoMax.Text = tcp.RtoMax.ToString
        Me.lblMaxConn.Text = tcp.MaxConn.ToString
        Me.lblActiveOpens.Text = String.Format("{0} ({1})", _old_ActiveOpens.ToString, _diff_ActiveOpens)
        Me.lblPassiveOpens.Text = String.Format("{0} ({1})", _old_PassiveOpens.ToString, _diff_PassiveOpens)
        Me.lblAttemptFails.Text = String.Format("{0} ({1})", _old_AttemptFails.ToString, _diff_AttemptFails)
        Me.lblEstabResets.Text = String.Format("{0} ({1})", _old_EstabResets.ToString, _diff_EstabResets)
        Me.lblCurEstab.Text = String.Format("{0} ({1})", _old_CurrEstab.ToString, _diff_CurrEstab)
        Me.lblInSegs.Text = String.Format("{0} ({1})", _old_InSegs.ToString, _diff_InSegs)
        Me.lblOutSegs.Text = String.Format("{0} ({1})", _old_OutSegs.ToString, _diff_OutSegs)
        Me.lblRetransSegs.Text = String.Format("{0} ({1})", _old_RetransSegs.ToString, _diff_RetransSegs)
        Me.lblInErrs.Text = String.Format("{0} ({1})", _old_InErrs.ToString, _diff_InErrs)
        Me.lblOutRsTs.Text = String.Format("{0} ({1})", _old_OutRsts.ToString, _diff_OutRsts)
        Me.lblNumConns.Text = String.Format("{0} ({1})", _old_NumConns.ToString, _diff_NumConns)



        ' = Graphics
        ' g1 (TCP)
        Me.g1.Add2Values(_diff_InSegs, _diff_OutSegs)
        Me.g1.TopText = "TCP in/out segments"
        Me.g1.Refresh()


        ' g2 (UDP)
        Me.g2.Add2Values(_diff_InDatagrams, _diff_OutDatagrams)
        Me.g2.TopText = "UDP in/out datagrams"
        Me.g2.Refresh()


    End Sub

    Private Sub frmNetworkInfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Save position & size
        Pref.SaveFormPositionAndSize(Me, "PSfrmNetworkInfo")
        ' Hide, and do not close
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frmSystemInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetToolTip(Me.chkTopMost, "Display window always on top")
        SetToolTip(Me.lblInDatagrams, "The number of datagrams received")
        SetToolTip(Me.lblInErrors, "The number of erroneous datagrams that were received")
        SetToolTip(Me.lblNoPorts, "The number of received datagrams that were discarded because the port specified was invalid")
        SetToolTip(Me.lblNumAddrs, "The number of entries in the UDP listener table")
        SetToolTip(Me.lblOutDatagrams, "The number of datagrams transmitted")
        SetToolTip(Me.lblRtoAlgo, "The retransmission time-out algorithm in use")
        SetToolTip(Me.lblRtoMax, "The maximum retransmission time-out value in milliseconds")
        SetToolTip(Me.lblRtoMin, "The minimum retransmission time-out value in milliseconds")
        SetToolTip(Me.lblMaxConn, "The maximum number of connections. If this member is -1, the maximum number of connections is dynamic")
        SetToolTip(Me.lblActiveOpens, "The number of active opens. In an active open, the client is initiating a connection with the server")
        SetToolTip(Me.lblPassiveOpens, "The number of passive opens. In a passive open, the server is listening for a connection request from a client")
        SetToolTip(Me.lblAttemptFails, "The number of failed connection attempts")
        SetToolTip(Me.lblEstabResets, "The number of established connections that have been reset")
        SetToolTip(Me.lblCurEstab, "The number of currently established connections")
        SetToolTip(Me.lblInSegs, "The number of segments received or transmitted")
        SetToolTip(Me.lblOutSegs, "The number of segments transmitted. This number does not include retransmitted segments")
        SetToolTip(Me.lblRetransSegs, "The number of segments retransmitted")
        SetToolTip(Me.lblInErrs, "The number of errors received")
        SetToolTip(Me.lblOutRsTs, "The number of segments transmitted with the reset flag set")
        SetToolTip(Me.lblNumConns, "The cumulative number of connections")

        CloseWithEchapKey(Me)

        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmNetworkInfo")

        Me.timerRefresh.Interval = My.Settings.SystemInterval
        Call timerRefresh_Tick(Nothing, Nothing)

        Me.chkTopMost.Checked = My.Settings.NetworkInfoTopMost

        ' Add handlers for graph tooltips
        Me.g1.ReturnTooltipText = AddressOf Me.impTooltipTCP
        Me.g2.ReturnTooltipText = AddressOf Me.impTooltipUDP

    End Sub

    Private Sub frmSystemInfo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.g2.Refresh()
        Me.g1.Refresh()
    End Sub

    Private Sub chkTopMost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTopMost.CheckedChanged
        If Me.chkTopMost.Checked <> My.Settings.NetworkInfoTopMost Then
            My.Settings.NetworkInfoTopMost = Me.chkTopMost.Checked
            Try
                My.Settings.Save()
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If
        Me.TopMost = Me.chkTopMost.Checked
    End Sub

#Region "Graph tooltips proc"

    Private Function impTooltipTCP(ByVal index As Integer, ByVal time As Long) As String
        Dim s As String = "TCP traffic"
        s &= vbNewLine & "In segments : " & Me.g1.Values(index).ToString
        s &= vbNewLine & "Out segments : " & Me.g1.Values2(index).ToString
        Dim d As New Date(time)
        s &= vbNewLine & d.ToShortDateString & " " & d.ToLongTimeString
        Return s
    End Function

    Private Function impTooltipUDP(ByVal index As Integer, ByVal time As Long) As String
        Dim s As String = "UDP traffic"
        s &= vbNewLine & "In datagrams : " & Me.g2.Values(index).ToString
        s &= vbNewLine & "Out datagrams : " & Me.g2.Values2(index).ToString
        Dim d As New Date(time)
        s &= vbNewLine & d.ToShortDateString & " " & d.ToLongTimeString
        Return s
    End Function

#End Region

End Class

' Lite Process Monitor









'




'




Option Strict On

'http://www.pscode.com/vb/scripts/ShowCode.asp?txtCodeId=6362&lngWId=1

Imports Common.Misc
Imports Native.Api

Public Class frmFindWindow

    Private Selecting As Boolean ' Amd I currently selecting a window?
    Private BorderDrawn As Boolean ' Is there a border currently drawn that needs To be undrawn?
    Private Myhwnd As IntPtr  ' The current hWnd that has a border drawn on it
    Private SelectedHwnd As IntPtr  ' The selecte hWnd


    Private Sub Draw()
        Dim Cursor As NativeStructs.PointApi  ' Cursor position
        Dim RetVal As IntPtr  ' Dummy returnvalue
        Dim hdc As IntPtr  ' hDC that we're going To be using
        Dim Pen As IntPtr ' Handle To a GDI Pen object
        Dim Brush As IntPtr ' Handle To a GDI Brush object
        Dim OldPen As IntPtr ' Handle To previous Pen object (to restore it)
        Dim OldBrush As IntPtr ' Handle To previous brush object (to restore it)
        Dim OldROP As NativeEnums.GdiBlendMode  ' Value of the previous ROP
        Dim Region As IntPtr  ' Handle To a GDI Region object that I create
        Dim OldRegion As IntPtr  ' Handle To previous Region object For the hDC
        Dim FullWind As Native.Api.NativeStructs.Rect ' the bounding rectangle of the window in screen coords
        Dim Draw As Native.Api.NativeStructs.Rect ' The drawing rectangle

        ' Get the cursor
        NativeFunctions.GetCursorPos(Cursor)
        ' Get the window
        RetVal = NativeFunctions.WindowFromPoint(New Point(Cursor.X, Cursor.Y))
        ' If the new hWnd is the same as the old
        '     one, skip drawing it, so to avoid flicker
        If RetVal = Myhwnd Then Exit Sub
        ' New hWnd. If there is currently a border drawn, undraw it.
        If BorderDrawn = True Then UnDraw()
        ' Set the BorderDrawn property to true, as we're just about to draw it.
        BorderDrawn = True
        ' And set the hWnd to the new value.
        ' Note, I didn't do it before, because the UnDraw routine uses the Myhwnd variable
        Myhwnd = RetVal
        ' You could extract other information from the window, such as window title,
        ' class name, parent, etc., and print it here, too.
        ' Get the full Rect of the window in screen co-ords
        NativeFunctions.GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = NativeFunctions.CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = NativeFunctions.GetWindowDC(Myhwnd)
        ' Save the old region
        NativeFunctions.GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = NativeFunctions.SelectObject(hdc, Region)
        ' Create a pen
        Pen = NativeFunctions.CreatePen(0, 6, IntPtr.Zero) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = NativeFunctions.SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so th
        '     at it doesn't edit anything
        Brush = NativeFunctions.GetStockObject(NativeEnums.GdiStockObject.NullBrush)
        ' Select the brush
        OldBrush = NativeFunctions.SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = NativeFunctions.SetROP2(hdc, NativeEnums.GdiBlendMode.Not) ' vbInvert means, whatever is draw,
        ' invert those pixels. This means that I can undraw it by doing the same.
        '
        ' The Drawing Bits
        '
        ' Put a box around the outside of the window, using the current hDC.
        ' These coords are in device co-ordinates, i.e., of the hDC.


        With Draw
            .Left = 0
            .Top = 0
            .Bottom = FullWind.Bottom - FullWind.Top
            .Right = FullWind.Right - FullWind.Left
            NativeFunctions.Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        NativeFunctions.SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        NativeFunctions.SetROP2(hdc, OldROP)
        ' Return to the previous brush


        NativeFunctions.SelectObject(hdc, OldBrush)
        ' Return the previous pen


        NativeFunctions.SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        NativeFunctions.DeleteObject(Brush)
        ' Delete the Pen I created
        NativeFunctions.DeleteObject(Pen)
        ' Delete the region I created
        NativeFunctions.DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        NativeFunctions.ReleaseDC(Myhwnd, hdc)
    End Sub



    Private Sub UnDraw()

        '
        ' Note, this sub is almost identical to the other one, except it doesn't go looking
        ' for the hWnd, it accesses the old one. Also, it doesn't clear the form.
        ' Otherwise, it just draws on top of the old one with an invert pen.
        ' 2 inverts = original
        '
        ' If there hasn't been a border drawn, then get out of here.
        If BorderDrawn = False Then Exit Sub
        ' Now set it
        BorderDrawn = False
        ' If there isn't a current hWnd, then exit.
        ' That's why in the mouseup event we get out, because otherwise a border would be draw
        ' around the old window
        If Myhwnd.IsNull Then Exit Sub
        Dim RetVal As IntPtr ' Dummy returnvalue
        Dim hdc As IntPtr ' hDC that we're going To be using
        Dim Pen As IntPtr ' Handle To a GDI Pen object
        Dim Brush As IntPtr ' Handle To a GDI Brush object
        Dim OldPen As IntPtr ' Handle To previous Pen object (to restore it)
        Dim OldBrush As IntPtr  ' Handle To previous brush object (to restore it)
        Dim OldROP As NativeEnums.GdiBlendMode   ' Value of the previous ROP
        Dim Region As IntPtr  ' Handle To a GDI Region object that I create
        Dim OldRegion As IntPtr  ' Handle To previous Region object For the hDC
        Dim FullWind As Native.Api.NativeStructs.Rect  ' the bounding rectangle of the window in screen coords
        Dim Draw As Native.Api.NativeStructs.Rect ' The drawing rectangle
        '
        ' Getting all of the ingredients ready
        '
        ' Get the full Rect of the window in screen co-ords
        NativeFunctions.GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = NativeFunctions.CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = NativeFunctions.GetWindowDC(Myhwnd)
        ' Save the old region
        NativeFunctions.GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = NativeFunctions.SelectObject(hdc, Region)
        ' Create a pen
        Pen = NativeFunctions.CreatePen(0, 6, IntPtr.Zero) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = NativeFunctions.SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so that it doesn't edit anything
        Brush = NativeFunctions.GetStockObject(NativeEnums.GdiStockObject.NullBrush)
        ' Select the brush
        OldBrush = NativeFunctions.SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = NativeFunctions.SetROP2(hdc, NativeEnums.GdiBlendMode.Not) ' vbInvert means, whatever is draw,
        ' invert those pixels. This means that I can undraw it by doing the same.
        '
        ' The Drawing Bits
        '
        ' Put a box around the outside of the window, using the current hDC.
        ' These coords are in device co-ordinates, i.e., of the hDC.


        With Draw
            .Left = 0
            .Top = 0
            .Bottom = FullWind.Bottom - FullWind.Top
            .Right = FullWind.Right - FullWind.Left
            NativeFunctions.Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        NativeFunctions.SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        NativeFunctions.SetROP2(hdc, OldROP)
        ' Return to the previous brush


        NativeFunctions.SelectObject(hdc, OldBrush)
        ' Return the previous pen


        NativeFunctions.SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        NativeFunctions.DeleteObject(Brush)
        ' Delete the Pen I created
        NativeFunctions.DeleteObject(Pen)
        ' Delete the region I created
        NativeFunctions.DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        NativeFunctions.ReleaseDC(Myhwnd, hdc)
    End Sub

    Private Sub Form_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        ' Set the selecting flag
        Selecting = True
        ' Capture all mouse events to this window form
        NativeFunctions.SetCapture(Me.Handle)
        ' Simulate a mouse movement event to draw the border when the mouse button goes down
        Form_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub Form_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        ' Security catch to make sure that the graphics don't get mucked up when not selecting
        If Selecting = False Then Exit Sub
        ' Call the "Draw" subroutine
        Draw()
        ' Refresh infos
        RefreshInfos(Myhwnd)
    End Sub

    Private Sub Form_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        ' If not selecting, then skip
        If Selecting = False Then Exit Sub
        ' Clean up the graphics drawn
        UnDraw()
        ' Release mouse capture
        NativeFunctions.ReleaseCapture()
        ' Not selecting
        Selecting = False
        ' Found our handle
        SelectedHwnd = Myhwnd
        ' Reset the variable
        Myhwnd = IntPtr.Zero
        Me.cmdGoToProcess.Enabled = (SelectedHwnd.IsNotNull)
        Me.cmdGoToThread.Enabled = Me.cmdGoToProcess.Enabled
    End Sub

    Private Sub Found(ByVal hWnd As IntPtr, Optional ByVal selectThread As Boolean = False)

        ' Get process ID
        Dim pid As Integer
        Dim tid As Integer = NativeFunctions.GetWindowThreadProcessId(hWnd, pid)
        Dim cP As cProcess = Nothing

        _frmMain.lvProcess.SelectedItems.Clear()
        For Each it As ListViewItem In _frmMain.lvProcess.Items
            cP = _frmMain.lvProcess.GetItemByKey(it.Name)
            If cp IsNot Nothing AndAlso cp.Infos.ProcessId = pid Then
                it.Selected = True
                it.EnsureVisible()
                Exit For
            End If
        Next

        ' Select 'process' tab in main form
        _frmMain.Ribbon.ActiveTab = _frmMain.ProcessTab
        Call _frmMain.Ribbon_MouseMove(Nothing, Nothing)
        _frmMain.Show()
        _frmMain.lvProcess.Focus()

        If selectThread AndAlso cP IsNot Nothing Then
            ' Here we select the thread in detailed window
            Dim frm As New frmProcessInfo
            frm.SetProcess(cP)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
            frm.tabProcess.SelectedTab = frm.TabPageThreads
            ' Create a thread which wait for threads to be added in the lvThread
            ' and then select the good thread
            Threading.ThreadPool.QueueUserWorkItem(AddressOf selectThreadImp, New contextObj(tid, frm))
        End If

        Me.Close()

    End Sub

#Region "Select thread code"

    Private Structure contextObj
        Public tid As Integer
        Public frmProcInfo As frmProcessInfo
        Public Sub New(ByVal threadId As Integer, ByVal form As frmProcessInfo)
            tid = threadId
            frmProcInfo = form
        End Sub
    End Structure
    Private Sub selectThreadImp(ByVal context As Object)
        Dim pObj As contextObj = DirectCast(context, contextObj)

        ' Wait for threads to be added
        While pObj.frmProcInfo.lvThreads.Items.Count = 0
            Threading.Thread.Sleep(50)
        End While

        ' Select the good thread
        Async.ListView.EnsureItemVisible(pObj.frmProcInfo.lvThreads, pObj.tid.ToString)
    End Sub

#End Region

    Private Sub RefreshInfos(ByVal hWnd As IntPtr)
        ' Get thread & process ID
        Dim pid As Integer
        Dim tid As Integer = NativeFunctions.GetWindowThreadProcessId(hWnd, pid)
        If tid > 0 Then
            Me.lblThread.Text = tid.ToString
        Else
            Me.lblThread.Text = NO_INFO_RETRIEVED
        End If
        Dim cP As Process = Process.GetProcessById(pid)
        If cP IsNot Nothing Then
            Me.lblProcess.Text = cP.ProcessName & " (" & cP.Id.ToString & ")"
        Else
            Me.lblProcess.Text = NO_INFO_RETRIEVED
        End If
    End Sub

    Private Sub frmFindWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdGoToProcess, "Select associated process")
        SetToolTip(Me.cmdGoToThread, "Select associated thread")
    End Sub

    Private Sub cmdGoToProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoToProcess.Click
        ' Found our handle
        Found(SelectedHwnd)
    End Sub

    Private Sub frmFindWindow_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        ' Paint an image on our Form (target image)
        Dim g As Graphics = e.Graphics
        g.DrawImage(My.Resources.target32, New PointF(12, 46))
    End Sub

    Private Sub cmdGoToThread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoToThread.Click
        ' Found our handle
        Found(SelectedHwnd, True)
    End Sub
End Class
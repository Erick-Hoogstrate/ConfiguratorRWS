Public Class ResizeControl
    Private Target As Control = New Control()

    Private topBox As ResizeBox
    Private bottomBox As ResizeBox
    Private leftBox As ResizeBox
    Private rightBox As ResizeBox
    Private topLeftBox As ResizeBox
    Private topRightBox As ResizeBox
    Private BottomLeftBox As ResizeBox
    Private BottomRightBox As ResizeBox

    Private boxesAdded As Boolean = False

    Private showResizeBoxes As Boolean = False


    Public Class ResizeBox
        Inherits Label

        Public Position As BoxPosition = BoxPosition.Top

        Public Enum BoxPosition
            Top
            Bottom
            Left
            Right
            TopLeft
            TopRight
            BottomLeft
            BottomRight
        End Enum

        Public Sub New(position As BoxPosition)
            InitializeComponent()
            Me.Position = position
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.AutoSize = False
            Me.BackColor = Color.White
            Me.BorderStyle = BorderStyle.FixedSingle
            Me.Name = "ResizeBox"
            Me.Size = New Size(6, 6)
            AddHandler Me.MouseEnter, AddressOf ResizeBoxMouseEnter
            AddHandler Me.MouseLeave, AddressOf ResizeBoxMouseLeave
            Me.ResumeLayout(False)
        End Sub

        Private Sub ResizeBoxMouseEnter(sender As Object, e As EventArgs)
            Select Case Me.Position
                Case BoxPosition.Top
                    Me.Cursor = Cursors.SizeNS
                    Exit Sub

                Case BoxPosition.Left
                    Me.Cursor = Cursors.SizeWE
                    Exit Sub

                Case BoxPosition.Right
                    Me.Cursor = Cursors.SizeWE
                    Exit Sub

                Case BoxPosition.Bottom
                    Me.Cursor = Cursors.SizeNS
                    Exit Sub

                Case BoxPosition.topLeft
                    Me.Cursor = Cursors.SizeNWSE
                    Exit Sub

                Case BoxPosition.topRight
                    Me.Cursor = Cursors.SizeNESW
                    Exit Sub

                Case BoxPosition.bottomLeft
                    Me.Cursor = Cursors.SizeNESW
                    Exit Sub

                Case BoxPosition.bottomRight
                    Me.Cursor = Cursors.SizeNWSE
                    Exit Sub
            End Select
        End Sub

        Private Sub ResizeBoxMouseLeave(sender As Object, e As EventArgs)
            Me.Cursor = Cursors.Default
        End Sub
    End Class

    Public Sub New(newTarget As Control, newShowResizeBoxes As Boolean)
        showResizeBoxes = newShowResizeBoxes

        Target = newTarget


        topBox = New ResizeBox(ResizeBox.BoxPosition.Top)
        bottomBox = New ResizeBox(ResizeBox.BoxPosition.Bottom)
        leftBox = New ResizeBox(ResizeBox.BoxPosition.Left)
        rightBox = New ResizeBox(ResizeBox.BoxPosition.Right)
        topLeftBox = New ResizeBox(ResizeBox.BoxPosition.TopLeft)
        topRightBox = New ResizeBox(ResizeBox.BoxPosition.TopRight)
        BottomLeftBox = New ResizeBox(ResizeBox.BoxPosition.BottomLeft)
        BottomRightBox = New ResizeBox(ResizeBox.BoxPosition.BottomRight)

        topBox.Visible = False
        bottomBox.Visible = False
        leftBox.Visible = False
        rightBox.Visible = False
        topLeftBox.Visible = False
        topRightBox.Visible = False
        BottomLeftBox.Visible = False
        BottomRightBox.Visible = False


        AddHandler topBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler bottomBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler leftBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler rightBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler topRightBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler topLeftBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler BottomRightBox.MouseDown, AddressOf Boxes_MouseDown
        AddHandler BottomLeftBox.MouseDown, AddressOf Boxes_MouseDown

        AddHandler topBox.MouseMove, AddressOf topBox_MouseMove
        AddHandler bottomBox.MouseMove, AddressOf bottomBox_MouseMove
        AddHandler rightBox.MouseMove, AddressOf rightBox_MouseMove
        AddHandler leftBox.MouseMove, AddressOf leftBox_MouseMove
        AddHandler topLeftBox.MouseMove, AddressOf topLeftBox_MouseMove
        AddHandler topRightBox.MouseMove, AddressOf topRightBox_MouseMove
        AddHandler BottomLeftBox.MouseMove, AddressOf bottomLeftBox_MouseMove
        AddHandler BottomRightBox.MouseMove, AddressOf bottomRightBox_MouseMove

        AddHandler Target.LocationChanged, AddressOf target_LocationChanged
    End Sub

    Public Sub Parent_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        Dim Pen As Pen = New Pen(Brushes.Black, 1)
        Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
        g.DrawRectangle(Pen, New Rectangle(Target.Left - 3, Target.Top - 3, Target.Width + 6, Target.Height + 6))
    End Sub

    Public Sub ShowResizeBoxes1()
        PositionTopBox()
        PositionBottomBox()
        PositionLeftBox()
        PositionRightBox()
        PositionTopLeftBox()
        PositionTopRightBox()
        PositionBottomRightBox()
        PositionBottomLeftBox()
        topBox.Visible = True
        bottomBox.Visible = True
        leftBox.Visible = True
        rightBox.Visible = True
        topRightBox.Visible = True
        topLeftBox.Visible = True
        BottomLeftBox.Visible = True
        BottomRightBox.Visible = True

        If Not boxesAdded Then
            Target.Parent.Controls.Add(topBox)
            Target.Parent.Controls.Add(bottomBox)
            Target.Parent.Controls.Add(leftBox)
            Target.Parent.Controls.Add(rightBox)
            Target.Parent.Controls.Add(topLeftBox)
            Target.Parent.Controls.Add(topRightBox)
            Target.Parent.Controls.Add(BottomLeftBox)
            Target.Parent.Controls.Add(BottomRightBox)
            boxesAdded = True
        End If

        topBox.BringToFront()
        bottomBox.BringToFront()
        leftBox.BringToFront()
        rightBox.BringToFront()
        topRightBox.BringToFront()
        topLeftBox.BringToFront()
        BottomRightBox.BringToFront()
        BottomLeftBox.BringToFront()
    End Sub

    Public Sub HideResizeBoxes()
        topBox.Visible = False
        bottomBox.Visible = False
        leftBox.Visible = False
        rightBox.Visible = False
        topRightBox.Visible = False
        topLeftBox.Visible = False
        BottomLeftBox.Visible = False
        BottomRightBox.Visible = False
    End Sub


    Private MouseLocation As Point = New Point

    Private Sub Boxes_MouseDown(Sender As Object, e As MouseEventArgs)
        MouseLocation.X = e.X
        MouseLocation.Y = e.Y
    End Sub

    Private Sub target_LocationChanged()
        PositionTopBox()
        PositionBottomBox()
        PositionLeftBox()
        PositionRightBox()
        PositionTopLeftBox()
        PositionTopRightBox()
        PositionBottomRightBox()
        PositionBottomLeftBox()
    End Sub

    Private Sub topBox_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxTop As Int32 = topBox.Top + (e.Y - MouseLocation.Y)
            Dim oldTargetTop As Int32 = Target.Top
            Dim newTargetHeight As Int32 = Target.Height + (Target.Top - (topBox.Top + topBox.Height + 1))

            If newTargetHeight > 15 Or newBoxTop <= topBox.Top Then
                Target.Top = newBoxTop + topBox.Height + 1
                Target.Height += (oldTargetTop - Target.Top)
                topBox.Top = newBoxTop
                PositionLeftBox()
                PositionRightBox()
                PositionTopLeftBox()
                PositionTopRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub bottomBox_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxTop As Int32 = bottomBox.Top + (e.Y - MouseLocation.Y)
            Dim newTargetHeight As Int32 = bottomBox.Top - Target.Top - 1

            If newTargetHeight > 15 Or newBoxTop >= bottomBox.Top Then
                Target.Height = newTargetHeight
                bottomBox.Top = newBoxTop
                PositionLeftBox()
                PositionRightBox()
                PositionBottomLeftBox()
                PositionBottomRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub rightBox_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxLeft As Int32 = rightBox.Left + (e.X - MouseLocation.X)
            Dim newTargetWidth As Int32 = rightBox.Left - Target.Left - 1

            If newTargetWidth > 30 Or newBoxLeft >= rightBox.Left Then
                Target.Width = newTargetWidth
                rightBox.Left = newBoxLeft
                PositionTopBox()
                PositionBottomBox()
                PositionTopRightBox()
                PositionBottomRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub leftBox_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxLeft As Int32 = leftBox.Left + (e.X - MouseLocation.X)
            Dim oldTargetLeft As Int32 = Target.Left
            Dim newTargetWidth As Int32 = Target.Width + (oldTargetLeft - Target.Left)

            If newTargetWidth > 30 Or newBoxLeft <= leftBox.Left Then
                Target.Left = newBoxLeft + leftBox.Width + 1
                Target.Width += (oldTargetLeft - Target.Left)
                leftBox.Left = newBoxLeft
                PositionTopBox()
                PositionBottomBox()
                PositionTopLeftBox()
                PositionBottomLeftBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub topLeftBox_MouseMove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxTop As Int32 = topLeftBox.Top + (e.Y - MouseLocation.Y)
            Dim oldTargetTop As Int32 = Target.Top
            Dim newTargetHeight As Int32 = Target.Height + (Target.Top - (topLeftBox.Top + topLeftBox.Height + 1))
            Dim newBoxLeft As Int32 = topLeftBox.Left + (e.X - MouseLocation.X)
            Dim oldTargetLeft As Int32 = Target.Left
            Dim newTargetWidth As Int32 = Target.Width + (oldTargetLeft - Target.Left)

            If (newTargetWidth > 30 Or newBoxLeft <= topLeftBox.Left) Then
                Target.Left = newBoxLeft + topLeftBox.Width + 1
                Target.Width += (oldTargetLeft - Target.Left)
                topLeftBox.Left = newBoxLeft
                PositionTopLeftBox()
                PositionBottomLeftBox()
                PositionTopBox()
                PositionBottomBox()
                PositionLeftBox()
            End If
            If (newTargetHeight > 15 Or newBoxTop <= topBox.Top) Then
                Target.Top = newBoxTop + topLeftBox.Height + 1
                Target.Height += (oldTargetTop - Target.Top)
                topLeftBox.Top = newBoxTop
                PositionTopLeftBox()
                PositionTopRightBox()
                PositionLeftBox()
                PositionRightBox()
                PositionTopBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub topRightBox_MouseMove(sender As Object, e As MouseEventArgs)
        If (e.Button = MouseButtons.Left) Then
            Dim newBoxTop As Int32 = topRightBox.Top + (e.Y - MouseLocation.Y)
            Dim oldTargetTop As Int32 = Target.Top
            Dim newTargetHeight As Int32 = Target.Height + (Target.Top - (topRightBox.Top + topRightBox.Height + 1))
            Dim newBoxLeft As Int32 = topRightBox.Left + (e.X - MouseLocation.X)
            Dim newTargetWidth As Int32 = topRightBox.Left - Target.Left - 1

            If (newTargetWidth > 30 Or newBoxLeft >= topRightBox.Left) Then
                Target.Width = newTargetWidth
                topRightBox.Left = newBoxLeft
                PositionBottomRightBox()
                PositionTopBox()
                PositionBottomBox()
            End If
            If (newTargetHeight > 15 Or newBoxTop <= topBox.Top) Then
                Target.Top = newBoxTop + topRightBox.Height + 1
                Target.Height += (oldTargetTop - Target.Top)
                topRightBox.Top = newBoxTop
                PositionTopLeftBox()
                PositionLeftBox()
                PositionRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12));
        End If
    End Sub

    Private Sub bottomLeftBox_MouseMove(send As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim newBoxTop As Int32 = BottomLeftBox.Top + (e.Y - MouseLocation.Y)
            Dim newTargetHeight As Int32 = BottomLeftBox.Top - Target.Top - 1
            Dim newBoxLeft As Int32 = BottomLeftBox.Left + (e.X - MouseLocation.X)
            Dim oldTargetLeft As Int32 = Target.Left
            Dim newTargetWidth As Int32 = Target.Width + (oldTargetLeft - Target.Left)

            If (newTargetWidth > 30 Or newBoxLeft <= BottomLeftBox.Left) Then
                Target.Left = newBoxLeft + BottomLeftBox.Width + 1
                Target.Width += (oldTargetLeft - Target.Left)
                BottomLeftBox.Left = newBoxLeft
                PositionTopLeftBox()
                PositionTopBox()
                PositionBottomBox()
            End If
            If (newTargetHeight > 15 Or newBoxTop >= BottomLeftBox.Top) Then
                Target.Height = newTargetHeight
                BottomLeftBox.Top = newBoxTop
                PositionBottomRightBox()
                PositionLeftBox()
                PositionRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub bottomRightBox_MouseMove(sender As Object, e As MouseEventArgs)
        If (e.Button = MouseButtons.Left) Then
            Dim newBoxTop As Int32 = BottomRightBox.Top + (e.Y - MouseLocation.Y)
            Dim newTargetHeight As Int32 = BottomRightBox.Top - Target.Top - 1
            Dim newBoxLeft As Int32 = BottomRightBox.Left + (e.X - MouseLocation.X)
            Dim newTargetWidth As Int32 = BottomRightBox.Left - Target.Left - 1

            If (newTargetWidth > 30 Or newBoxLeft >= BottomRightBox.Left) Then
                Target.Width = newTargetWidth
                BottomRightBox.Left = newBoxLeft
                PositionTopRightBox()
                PositionTopBox()
                PositionBottomBox()
            End If
            If (newTargetHeight > 15 Or newBoxTop >= BottomRightBox.Top) Then
                Target.Height = newTargetHeight
                BottomRightBox.Top = newBoxTop
                PositionBottomLeftBox()
                PositionLeftBox()
                PositionRightBox()
            End If
            'Target.Parent.Invalidate(New Rectangle(Target.Left - 6, Target.Top - 6, Target.Width + 12, Target.Height + 12))
        End If
    End Sub

    Private Sub PositionTopBox()
        topBox.Top = Target.Top - topBox.Height - 1
        topBox.Left = Target.Left + (Target.Width / 2) - (topBox.Width / 2)
    End Sub
    Private Sub PositionBottomBox()
        bottomBox.Top = Target.Top + Target.Height + 1
        bottomBox.Left = Target.Left + (Target.Width / 2) - (bottomBox.Width / 2)
    End Sub
    Private Sub PositionLeftBox()
        leftBox.Top = Target.Top + (Target.Height / 2) - (leftBox.Height / 2)
        leftBox.Left = Target.Left - leftBox.Width - 1
    End Sub
    Private Sub PositionRightBox()
        rightBox.Top = Target.Top + (Target.Height / 2) - (rightBox.Height / 2)
        rightBox.Left = Target.Left + Target.Width + 1
    End Sub
    Private Sub PositionTopLeftBox()
        topLeftBox.Top = Target.Top - topLeftBox.Height - 1
        topLeftBox.Left = Target.Left - topLeftBox.Width - 1
    End Sub
    Private Sub PositionTopRightBox()
        topRightBox.Top = Target.Top - topRightBox.Height - 1
        topRightBox.Left = Target.Left + Target.Width + 1
    End Sub
    Private Sub PositionBottomLeftBox()
        BottomLeftBox.Top = Target.Top + Target.Height + 1
        BottomLeftBox.Left = Target.Left - leftBox.Width - 1
    End Sub
    Private Sub PositionBottomRightBox()
        BottomRightBox.Top = Target.Top + Target.Height + 1
        BottomRightBox.Left = Target.Left + Target.Width + 1
    End Sub

End Class

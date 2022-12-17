Public Class CComponent
    Inherits Label

    Private Name_ As String
    Public Rotation As Integer = 0
    Public Type As ComponentTypesEnum
    Public Waterway As Integer = 0
    Public Options As Options

    Private Active As Boolean = False

    Private rc As ResizeControl = New ResizeControl(Me, False)
    Public canvas As CCanvas

    ''' <summary>
    ''' Raises an event when the Value of the Name property is changed
    ''' </summary>
    ''' <param name="NewName">The new name.</param>
    Public Event NameChanged(sender As CComponent, NewName As String)

    Public Overloads Property Name As String
        Get
            Return Name_
        End Get
        Set(Value As String)
            Name_ = Value
            ISSDT.ToolTip.SetToolTip(Me, Name)
            RaiseEvent NameChanged(Me, Value)
        End Set
    End Property

    Public Sub New()
        MyBase.New
        ISSDT.ToolTip.SetToolTip(Me, Name)
    End Sub

    Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
        Name = NewName
        Location = NewLocation
        Type = newComponentType
        canvas = newCanvas
        BackColor = Color.Transparent
        ISSDT.ToolTip.SetToolTip(Me, Name)
    End Sub

    Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewColor As Color)
        Me.New(NewName, newComponentType, NewLocation, newCanvas)
        BackColor = NewColor
        Size = NewSize
    End Sub

    Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color, NewImage As Image)
        Me.New(NewName, newComponentType, NewLocation, newCanvas)
        Image = NewImage
        Rotate(NewRotation)
        Size = NewSize
        BackColor = NewColor
    End Sub

    ''' <summary>
    ''' Sets this component as active component, i.e., it shows the resize boxes, the border, and its options.
    ''' </summary>
    Overridable Sub SetActive()
        If Not Active Then
            canvas.SetActive(Me)
            ShowResizeBoxes()
            BorderStyle = BorderStyle.FixedSingle
            Active = True
            Options.Show()
        End If
    End Sub

    ''' <summary>
    ''' Removes this component as active component, i.e., it removes the resize boxes, the border, and its options.
    ''' </summary>
    Overridable Sub ResetActive()
        If Active Then
            canvas.ResetActive(Me)
            HideResizeBoxes()
            BorderStyle = BorderStyle.None
            Active = False
            Options.Hide()
        End If
    End Sub

    ''' <summary>
    ''' Rotates a component around its center.
    ''' </summary>
    ''' <param name="NewRotation">Rotation, either 0, 90, 180, or 270 degrees</param>
    Public Sub Rotate(NewRotation As Integer)
        If Image IsNot Nothing Then
            Dim bmp As Bitmap = New Bitmap(Image)
            Select Case NewRotation
                Case 0
                Case 90
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Size = New Size(Height, Width)
                Case 180
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipNone)
                Case 270
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    Size = New Size(Height, Width)
                Case Else
                    MsgBox("Rotation error, only rotations of 90, 180, or 270 degree are supported.", vbCritical, "Error")
                    Exit Sub
            End Select
            Rotation = (Rotation + NewRotation) Mod 360
            Image = bmp
        End If
    End Sub

    ''' <summary>
    ''' Shows the six resize boxes around the component.
    ''' </summary>
    Public Sub ShowResizeBoxes()
        rc.ShowResizeBoxes1()
    End Sub

    ''' <summary>
    ''' Hides the six resize boxes around the component.
    ''' </summary>
    Public Sub HideResizeBoxes()
        rc.HideResizeBoxes()
    End Sub

    ''' <summary>
    ''' Sets the component active if not already. Implements drag functionality.
    ''' </summary>
    ''' <param name="comp"></param>
    ''' <param name="e"></param>
    Private Sub HandleComponentMouseDown(comp As CComponent, e As MouseEventArgs) Handles Me.MouseDown
        If Not Active Then
            ISSDT.canvasPlant.ResetActive()
            SetActive()
        End If

        If e.Button = MouseButtons.Left Then
            canvas.startx = MousePosition.X
            canvas.starty = MousePosition.Y
            canvas.mdown = True
            canvas.valx = False
            canvas.valy = False
            Me.Cursor = Cursors.SizeAll
        End If
    End Sub

    ''' <summary>
    ''' Resets the drag functionality.
    ''' </summary>
    Private Sub HandleComponentMouseUp() Handles Me.MouseUp
        canvas.mdown = False
        canvas.valx = False
        canvas.valy = False
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Drags the component around the canvas.
    ''' </summary>
    Private Sub HandleComponentMouseMove() Handles Me.MouseMove
        'Check if mouse=down
        If canvas.mdown = True And canvas.activeComp IsNot Nothing Then
            canvas.endx = (MousePosition.X - canvas.Left)
            canvas.endy = (MousePosition.Y - canvas.Top)

            If canvas.valy = False Then
                canvas.starty = canvas.endy - canvas.activeComp.Top
                canvas.valy = True
            End If
            If canvas.valx = False Then
                canvas.startx = canvas.endx - canvas.activeComp.Left
                canvas.valx = True
            End If
            canvas.activeComp.Left = canvas.endx - canvas.startx
            canvas.activeComp.Top = canvas.endy - canvas.starty
            ISSDT.changedAfterSave = True
        End If
    End Sub

    ''' <summary>
    ''' Implements the right click item box.
    ''' </summary>
    ''' <param name="comp"></param>
    ''' <param name="e"></param>
    Private Sub HandleComponentMouseClick(comp As CComponent, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Not Active Then
                ISSDT.canvasPlant.ResetActive()
                SetActive()
            End If
            comp.HideResizeBoxes()
            Dim cms = New ContextMenuStrip
            Dim item1 As ToolStripMenuItem = New ToolStripMenuItem("Rename", Nothing, AddressOf renameComponent)
            Dim item2 As ToolStripMenuItem = New ToolStripMenuItem("Delete", Nothing, AddressOf deleteComponent)
            Dim item3 As ToolStripMenuItem = New ToolStripMenuItem("Rotate clockwise", Nothing, AddressOf rotateClockwise)
            Dim item4 As ToolStripMenuItem = New ToolStripMenuItem("Rotate counter-clockwise", Nothing, AddressOf rotateCounterClockwise)
            Dim item5 As ToolStripMenuItem = New ToolStripMenuItem("Raise to top", Nothing, AddressOf BringToFront)
            Dim item6 As ToolStripMenuItem = New ToolStripMenuItem("Send to bottom", Nothing, AddressOf SendToBack)
            Dim item7 As ToolStripMenuItem = New ToolStripMenuItem("Show model", Nothing, AddressOf showModel)

            cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {item1, item2, item3, item4, item5, item6, item7})
            cms.Show(comp, e.Location)
        End If
    End Sub



    ''' <summary>
    ''' This method requests a new name from the user for a component.
    ''' </summary>
    Private Sub renameComponent()
        Dim newComponentName = getComponentName()

        If newComponentName = "" Then
            Exit Sub
        End If

        Name = newComponentName
        ISSDT.changedAfterSave = True
    End Sub

    ''' <summary>
    ''' This method removes the component from the canvas and resets the active component.
    ''' </summary>
    Private Sub deleteComponent()
        canvas.Controls.Remove(Me)
        ResetActive()
        ISSDT.changedAfterSave = True
    End Sub

    ''' <summary>
    ''' This method rotates a component 90 degrees clockwise on the canvas.
    ''' </summary>
    Private Sub rotateClockwise()
        Rotate(90)
        ISSDT.changedAfterSave = True
    End Sub

    ''' <summary>
    ''' This method rotates a component 90 degrees counter-clockwise on the canvas.
    ''' </summary>
    Private Sub rotateCounterClockwise()
        Rotate(270)
        ISSDT.changedAfterSave = True
    End Sub

    Private Sub showModel()
        Dim formModel As Form = New Form()
        formModel.Location = New Point(0, 0)

        Dim pictureBoxModel As PictureBox = New PictureBox()
        pictureBoxModel.Image = getModelImage()
        pictureBoxModel.Size = pictureBoxModel.Image.Size

        formModel.Size = pictureBoxModel.Image.Size
        formModel.Controls.Add(pictureBoxModel)
        formModel.Text = "Template of the " + Type.ToString + " template"


        formModel.Show()
    End Sub

    Private Function getModelImage() As Image
        Dim directoryPath As String = My.Application.Info.DirectoryPath
        Dim modelName As String = ""
        If Type = ComponentTypesEnum.BoomBarrier Then
            modelName = "BB.PNG"
        End If

        Return Image.FromFile(System.IO.Path.Combine(directoryPath, "Automata\" + modelName))
    End Function

End Class

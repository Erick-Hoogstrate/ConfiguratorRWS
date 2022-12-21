Public Module MComponentTypes
    Dim path As String = My.Application.Info.DirectoryPath
    ''' <summary>
    ''' All different types of components that can be used as building blocks.
    ''' </summary>
    Public Enum ComponentTypesEnum
        NoType
        Square
        TextLabel
        MitreGate
        TL_Entering
        TL_Leaving
        LockWall
        Water
        Quay
    End Enum

    ''' -------------------------------------------------------------------------------------------------------------------------------------------------------
    ''' --------------------------------------------------START COMPONENT DECLARATION--------------------------------------------------------------------------
    ''' -------------------------------------------------------------------------------------------------------------------------------------------------------



    Public Class Square : Inherits CComponent
        Public Sub New(newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New("drawing", newComponentType, NewLocation, newCanvas)
            Size = New Size(20, 20)

            Options = New SquareOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewColor)

            Options = New SquareOptions(Me)
        End Sub
    End Class

    Public Class SquareOptions
        Inherits Options

        Dim Square As Square

        Public Sub New(ByRef NewSquare As Square)
            MyBase.New()
            Square = NewSquare

            textBoxName.Text = Square.Name
            textBoxType.Text = Square.Type.ToString
            AddHandler Square.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub
        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub
    End Class



    ''' <summary>
    ''' The text label component.
    ''' </summary>
    Public Class TextLabel : Inherits CComponent
        Public Sub New(newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New("text", newComponentType, NewLocation, newCanvas)
            AutoSize = True
            Text = Name

            Options = New TextLabelOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewColor)
            AutoSize = True

            Options = New TextLabelOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the text label component.
    ''' </summary>
    Public Class TextLabelOptions
        Inherits Options

        Dim textLabel As TextLabel
        Dim LabelText As Label
        Dim textBoxText As TextBox

        Public Sub New(ByRef NewTextLabel As TextLabel)
            MyBase.New()
            textLabel = NewTextLabel

            'Name options
            textBoxName.Text = textLabel.Name
            textBoxType.Text = textLabel.Type.ToString
            AddHandler textLabel.NameChanged, AddressOf NameChanged

            'Text option
            Dim LbltextBoxText = CreateLabelAndTextBox("Text:", New Point(3, 64), "", False, 200)
            LabelText = LbltextBoxText.Label
            textBoxText = LbltextBoxText.textBox
            ISSDT.ToolTip.SetToolTip(LabelText, "The text to display.")
            AddHandler textBoxText.TextChanged, AddressOf TextInputChanged

            Controls.Add(LabelText)
            Controls.Add(textBoxText)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub TextInputChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            textLabel.Text = sender.Text
        End Sub
    End Class




    ''' <summary>
    ''' The mitre gate component.
    ''' </summary>
    Public Class MitreGate : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\MitreGate.png"))
            Size = New Size(60, 100)

            Options = New MitreGateOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\MitreGate.png")))

            Options = New MitreGateOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the mitre gate component.
    ''' </summary>
    Public Class MitreGateOptions
        Inherits Options

        Dim mitregate As MitreGate

        Public Sub New(ByRef NewMitreGate As MitreGate)
            MyBase.New()
            mitregate = NewMitreGate

            'Name Option
            textBoxName.Text = mitregate.Name
            textBoxType.Text = mitregate.Type.ToString
            AddHandler mitregate.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' <summary>
    ''' The traffic light entering component.
    ''' </summary>
    Public Class TL_Entering : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\TL_Entering.png"))
            Size = New Size(30, 75)

            Options = New TL_EnteringOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\TL_Entering.png")))

            Options = New TL_EnteringOptions(Me)
        End Sub

    End Class

    ''' <summary>
    ''' The option window for the traffic light entering component.
    ''' </summary>
    Public Class TL_EnteringOptions
        Inherits Options

        Dim tl_entering As TL_Entering

        Public Sub New(ByRef NewTL_Entering As TL_Entering)
            MyBase.New()
            TL_Entering = NewTL_Entering

            'Name Option
            textBoxName.Text = tl_entering.Name
            textBoxType.Text = tl_entering.Type.ToString
            AddHandler tl_entering.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' <summary>
    ''' The traffic light leaving component.
    ''' </summary>
    Public Class TL_Leaving : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\TL_Leaving.png"))
            Size = New Size(25, 50)

            Options = New TL_LeavingOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\TL_Leaving.png")))

            Options = New TL_LeavingOptions(Me)
        End Sub

    End Class

    ''' <summary>
    ''' The option window for the traffic light leaving component.
    ''' </summary>
    Public Class TL_LeavingOptions
        Inherits Options

        Dim tl_leaving As TL_Leaving

        Public Sub New(ByRef NewTL_Leaving As TL_Leaving)
            MyBase.New()
            tl_leaving = NewTL_Leaving

            'Name Option
            textBoxName.Text = tl_leaving.Name
            textBoxType.Text = tl_leaving.Type.ToString
            AddHandler tl_leaving.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' <summary>
    ''' The lock wall component.
    ''' </summary>
    Public Class LockWall : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\LockWall.png"))
            Size = New Size(100, 20)

            Options = New LockWallOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\LockWall.png")))

            Options = New LockWallOptions(Me)
        End Sub

    End Class

    ''' <summary>
    ''' The option window for the lock wall component.
    ''' </summary>
    Public Class LockWallOptions
        Inherits Options

        Dim lock_wall As LockWall

        Public Sub New(ByRef NewLockWall As LockWall)
            MyBase.New()
            lock_wall = NewLockWall

            'Name Option
            textBoxName.Text = lock_wall.Name
            textBoxType.Text = lock_wall.Type.ToString
            AddHandler lock_wall.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' <summary>
    ''' The water component.
    ''' </summary>
    Public Class Water : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Water.png"))
            Size = New Size(200, 100)

            Options = New WaterOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Water.png")))

            Options = New WaterOptions(Me)
        End Sub

    End Class

    ''' <summary>
    ''' The option window for the water component.
    ''' </summary>
    Public Class WaterOptions
        Inherits Options

        Dim water As Water

        Public Sub New(ByRef NewWater As Water)
            MyBase.New()
            water = NewWater

            'Name Option
            textBoxName.Text = water.Name
            textBoxType.Text = water.Type.ToString
            AddHandler water.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' <summary>
    ''' The quay component.
    ''' </summary>
    Public Class Quay : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Quay.png"))
            Size = New Size(250, 50)

            Options = New QuayOptions(Me)
        End Sub
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Quay.png")))

            Options = New QuayOptions(Me)
        End Sub

    End Class

    ''' <summary>
    ''' The option window for the quay component.
    ''' </summary>
    Public Class QuayOptions
        Inherits Options

        Dim quay As Quay

        Public Sub New(ByRef NewQuay As Quay)
            MyBase.New()
            quay = NewQuay

            'Name Option
            textBoxName.Text = quay.Name
            textBoxType.Text = quay.Type.ToString
            AddHandler quay.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

    End Class




    ''' -------------------------------------------------------------------------------------------------------------------------------------------------------
    ''' --------------------------------------------------END COMPONENT DECLARATION----------------------------------------------------------------------------
    ''' -------------------------------------------------------------------------------------------------------------------------------------------------------



    ''' <summary>
    ''' Base class for the option window, must be inheritted.
    ''' </summary>
    Public MustInherit Class Options
        Inherits Panel

        Public LabelName As Label = New Label()
        Public textBoxName As TextBox = New TextBox()
        Public LabelType As Label = New Label()
        Public textBoxType As TextBox = New TextBox()
        Public textBoxWaterway As TextBox = New TextBox()

        Public Sub New()
            Location = New Point(1016, 373)
            Size = New Size(364, 185)
            BorderStyle = BorderStyle.FixedSingle
            AutoScroll = True

            Dim LbltextBoxName = CreateLabelAndTextBox("Name:", New Point(3, 16), "", True)
            LabelName = LbltextBoxName.Label
            textBoxName = LbltextBoxName.textBox
            ISSDT.ToolTip.SetToolTip(LabelName, "The name of the component.")

            Dim LbltextBoxType = CreateLabelAndTextBox("Type:", New Point(3, 40), "", True)
            LabelType = LbltextBoxType.Label
            textBoxType = LbltextBoxType.textBox
            ISSDT.ToolTip.SetToolTip(LabelType, "The Type of the component.")

            Controls.Add(LabelName)
            Controls.Add(textBoxName)
            Controls.Add(LabelType)
            Controls.Add(textBoxType)
            Me.Hide()
        End Sub
    End Class

    ''' <summary>
    ''' Creates a check box control.
    ''' </summary>
    ''' <param name="Text">The check box text.</param>
    ''' <param name="Size">The check box size.</param>
    ''' <param name="Location">The check box location.</param>
    ''' <param name="InitialValue">The initial value of the check box</param>
    ''' <returns>The check box control.</returns>
    Public Function CreateCheckBox(Text As String, Size As Size, Location As Point, InitialValue As Boolean) As CheckBox
        Dim CheckBox As CheckBox = New CheckBox
        CheckBox.Text = Text
        CheckBox.Size = Size
        CheckBox.Location = Location
        CheckBox.Checked = InitialValue

        Return CheckBox
    End Function

    ''' <summary>
    ''' Creates a label control and a text box control.
    ''' </summary>
    ''' <param name="LabelText">The label text.</param>
    ''' <param name="Location">The label location.</param>
    ''' <param name="InitialValue">The initial value of the text box.</param>
    ''' <param name="ReadOnlyy">Whether the textbox is read only.</param>
    ''' <returns>The label control and the text box control.</returns>
    Public Function CreateLabelAndTextBox(LabelText As String, Location As Point, InitialValue As String, ReadOnlyy As Boolean, Optional Width As Integer = 100) As (Label As Label, textBox As TextBox)
        Dim Label As Label = New Label
        Dim textBox As TextBox = New TextBox

        Label.Location = Location
        Label.Text = LabelText
        Label.AutoSize = True

        textBox.Location = New Point(Location.X + 90, Location.Y - 3)
        textBox.Size = New Size(Width, 20)
        textBox.Text = InitialValue
        textBox.ReadOnly = ReadOnlyy

        Return (Label, textBox)
    End Function

    ''' <summary>
    ''' Creates a label control and a combo box control.
    ''' </summary>
    ''' <param name="LabelText">The label text.</param>
    ''' <param name="Location">The label location.</param>
    ''' <param name="InitialValue">The initial value of the combo box.</param>
    ''' <returns>The label control and the combo box control.</returns>
    Public Function CreateLabelAndComboBox(LabelText As String, Location As Point, InitialValue As String) As (Label As Label, comboBox As ComboBox)
        Dim Label As Label = New Label
        Dim comboBox As ComboBox = New ComboBox

        Label.Location = Location
        Label.Text = LabelText
        Label.AutoSize = True

        comboBox.Location = New Point(Location.X + 90, Location.Y - 3)
        comboBox.Size = New Size(100, 20)
        comboBox.Text = InitialValue
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList

        Return (Label, comboBox)
    End Function

    ''' <summary>
    ''' Adds all components on the canvas of a specific type to the item list of a combo box.
    ''' </summary>
    ''' <typeparam name="componentType">The component type to add.</typeparam>
    ''' <param name="comboBox">The combo box to add the components to.</param>
    Private Sub AddComponentsOfTypeToComboBox(Of componentType As CComponent)(ByRef comboBox As ComboBox)
        Dim itemsInCanvas As List(Of String) = New List(Of String)
        Dim itemsInComboBox As List(Of String) = New List(Of String)

        For Each component As componentType In ISSDT.canvasPlant.Controls.OfType(Of componentType)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each item As String In comboBox.Items
            itemsInComboBox.Add(item)
        Next

        UpdateComboBoxAccordingToCanvas(itemsInComboBox, itemsInCanvas, comboBox)
    End Sub

    ''' <summary>
    ''' Adds all components on the canvas of a specific type to the item list of a combo box. If the item already exists in the combobox, the original item is kept. 
    ''' If an item is in the combo box that does not exist on the canvas it is removed.
    ''' </summary>
    ''' <typeparam name="componentType">The component type to add.</typeparam>
    ''' <param name="comboBox">The combo box to add the components to.</param>
    Private Sub AddComponentsOfTypeToComboBox(Of componentType As CComponent, ComponentType2 As CComponent)(ByRef comboBox As ComboBox)
        Dim itemsInCanvas As List(Of String) = New List(Of String)
        Dim itemsInComboBox As List(Of String) = New List(Of String)

        For Each component As componentType In ISSDT.canvasPlant.Controls.OfType(Of componentType)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each component As ComponentType2 In ISSDT.canvasPlant.Controls.OfType(Of ComponentType2)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each item As String In comboBox.Items
            itemsInComboBox.Add(item)
        Next

        UpdateComboBoxAccordingToCanvas(itemsInComboBox, itemsInCanvas, comboBox)
    End Sub

    ''' <summary>
    ''' Adds all components on the canvas of a specific type to the item list of a combo box. If the item already exists in the combobox, the original item is kept. 
    ''' If an item is in the combo box that does not exist on the canvas it is removed.
    ''' </summary>
    ''' <typeparam name="componentType">The component type to add.</typeparam>
    ''' <param name="comboBox">The combo box to add the components to.</param>
    Private Sub AddComponentsOfTypeToComboBox(Of componentType As CComponent, ComponentType2 As CComponent, ComponentType3 As CComponent)(ByRef comboBox As ComboBox)
        Dim itemsInCanvas As List(Of String) = New List(Of String)
        Dim itemsInComboBox As List(Of String) = New List(Of String)

        For Each component As componentType In ISSDT.canvasPlant.Controls.OfType(Of componentType)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each component As ComponentType2 In ISSDT.canvasPlant.Controls.OfType(Of ComponentType2)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each component As ComponentType3 In ISSDT.canvasPlant.Controls.OfType(Of ComponentType3)()
            itemsInCanvas.Add(component.Name)
        Next

        For Each item As String In comboBox.Items
            itemsInComboBox.Add(item)
        Next

        UpdateComboBoxAccordingToCanvas(itemsInComboBox, itemsInCanvas, comboBox)
    End Sub

    ''' <summary>
    ''' Adds items that are in the canvas, but not in the combo box and removes items that are not in the canvas, but are in the combo box.
    ''' </summary>
    ''' <param name="itemsInComboBox">The items in the combo box.</param>
    ''' <param name="itemsInCanvas">The items in the canvas.</param>
    ''' <param name="comboBox">The combo box to update</param>
    Private Sub UpdateComboBoxAccordingToCanvas(itemsInComboBox As List(Of String), itemsInCanvas As List(Of String), comboBox As ComboBox)
        'Remove items that have been removed from the canvas.
        For Each item As String In itemsInComboBox
            If Not itemsInCanvas.Contains(item) Then
                comboBox.Items.Remove(item)
            End If
        Next

        'Add items that have been added to the canvas.
        For Each item As String In itemsInCanvas
            If Not comboBox.Items.Contains(item) Then
                comboBox.Items.Add(item)
            End If
        Next

        comboBox.Sorted = True
    End Sub

    ''' <summary>
    ''' Creates a label control and a checked list control.
    ''' </summary>
    ''' <param name="LabelText">The label text.</param>
    ''' <param name="Location">The label location.</param>
    ''' <returns>The label control and the chcked list control.</returns>
    Public Function CreateLabelAndCheckedListBox(labelText As String, location As Point) As (label As Label, checkedListBox As CheckedListBox)
        Dim Label As Label = New Label
        Dim checkedListBox As CheckedListBox = New CheckedListBox

        Label.Location = location
        Label.Text = labelText
        Label.AutoSize = True

        checkedListBox.Location = New Point(location.X + 90, location.Y - 3)
        checkedListBox.Size = New Size(100, 40)

        Return (Label, checkedListBox)
    End Function

    ''' <summary>
    ''' Verifies whether the numeric input in a textbox is valid (in the form "digit" or "digit.digit"). If it is, the backcolor will be set to white, otherwise to red.
    ''' </summary>
    ''' <param name="textBox">The text box to verify.</param>
    Public Sub VerifyNumericTextBox(textBox As TextBox)
        If Not (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "\d+\.\d+$") Or System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "\d+$")) Then
            textBox.BackColor = Color.Red
        ElseIf textBox.BackColor = Color.Red Then
            textBox.BackColor = Color.White
        End If
    End Sub
End Module

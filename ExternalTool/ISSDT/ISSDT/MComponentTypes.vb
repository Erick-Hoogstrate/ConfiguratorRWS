Public Module MComponentTypes
    Dim path As String = My.Application.Info.DirectoryPath
    ''' <summary>
    ''' All different types of components that can be used as building blocks.
    ''' </summary>
    Public Enum ComponentTypesEnum
        StopSign
        BoomBarrier
        Quay
        ApproachSign
        RotatingBridge
        EnteringTrafficSign
        LeavingTrafficSign
        GUIEnteringTrafficSign
        GUILeavingTrafficSign
        Square
        NoType
        GUIRotatingBridge
        GUIStopSign
        GUIBoomBarrier
        GUIBridgeWindow
        Timer
        Actuator
        Sensor
        StopSignDouble
        DrawBridge
        TextLabel
    End Enum

    ''' <summary>
    ''' The stop sign component.
    ''' </summary>
    Public Class StopSign : Inherits CComponent
        Public standAlone As Boolean = True
        Public actuator As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSign.png"))
            Size = New Size(19, 19)

            Options = New StopSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSign.png")))

            Options = New StopSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the stop sign component. 
    ''' </summary>
    Public Class StopSignOptions
        Inherits Options

        Dim stopSign As StopSign
        Dim checkBoxStandAlone As CheckBox
        Dim lblActuator As Label
        Dim comboBoxActuator As ComboBox

        Public Sub New(ByRef NewStopSign As StopSign)
            MyBase.New()
            StopSign = NewStopSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = StopSign.Name
            textBoxType.Text = StopSign.Type.ToString
            AddHandler StopSign.NameChanged, AddressOf NameChanged

            'Stand alone option
            CheckboxStandAlone = CreateCheckBox("Standalone", New Size(80, 17), New Point(3, 64), True)
            AddHandler CheckboxStandAlone.CheckedChanged, AddressOf StandAloneChanged
            ISSDT.ToolTip.SetToolTip(CheckboxStandAlone, "Whether the stop sign has its own actuator or is controlled via another actuator.")

            'Actuator option
            Dim LblComboBoxActuator = CreateLabelAndComboBox("Actuator:", New Point(3, 88), "")
            lblActuator = LblComboBoxActuator.Label
            comboBoxActuator = LblComboBoxActuator.comboBox
            AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
            comboBoxActuator.Items.Add(StopSign.Name)
            AddHandler comboBoxActuator.SelectionChangeCommitted, AddressOf ActuatorChanged
            ISSDT.ToolTip.SetToolTip(lblActuator, "The actuator that controls this stop sign.")

            If StopSign.StandAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            End If

            Controls.Add(CheckboxStandAlone)
            Controls.Add(lblActuator)
            Controls.Add(comboBoxActuator)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub StandAloneChanged(ByVal sender As CheckBox, ByVal e As EventArgs)
            StopSign.StandAlone = sender.Checked

            If StopSign.StandAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            Else
                lblActuator.Show()
                comboBoxActuator.Show()
            End If
        End Sub

        Private Sub ActuatorChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            StopSign.Actuator = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                checkBoxStandAlone.Checked = stopSign.standAlone
                AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
                comboBoxActuator.SelectedIndex = comboBoxActuator.FindStringExact(stopSign.actuator)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The approach sign component.
    ''' </summary>
    Public Class ApproachSign : Inherits CComponent
        Public StandAlone As Boolean = True
        Public Actuator As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\ApproachSign.png"))
            Size = New Size(19, 19)

            Options = New ApproachSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\ApproachSign.png")))

            Options = New ApproachSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the approach sign component.
    ''' </summary>
    Public Class ApproachSignOptions
        Inherits Options

        Dim approachSign As ApproachSign
        Dim checkBoxStandAlone As CheckBox
        Dim lblActuator As Label
        Dim comboBoxActuator As ComboBox

        Public Sub New(ByRef NewApproachSign As ApproachSign)
            MyBase.New()
            ApproachSign = NewApproachSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = ApproachSign.Name
            textBoxType.Text = ApproachSign.Type.ToString

            'Stand alone option
            CheckboxStandAlone = CreateCheckBox("Standalone", New Size(80, 17), New Point(3, 64), True)
            AddHandler CheckboxStandAlone.CheckedChanged, AddressOf StandAloneChanged
            ISSDT.ToolTip.SetToolTip(CheckboxStandAlone, "Whether the approach sign has its own actuator or is controlled via another actuator.")

            'Actuator option
            Dim LblComboBoxActuator = CreateLabelAndComboBox("Actuator:", New Point(3, 88), "")
            lblActuator = LblComboBoxActuator.Label
            comboBoxActuator = LblComboBoxActuator.comboBox
            AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
            comboBoxActuator.Items.Add(ApproachSign.Name)
            AddHandler comboBoxActuator.SelectionChangeCommitted, AddressOf ActuatorChanged
            ISSDT.ToolTip.SetToolTip(comboBoxActuator, "The actuator that controls this approach sign.")

            If ApproachSign.StandAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            End If

            Controls.Add(CheckboxStandAlone)
            Controls.Add(lblActuator)
            Controls.Add(comboBoxActuator)

            ISSDT.Controls.Add(Me)
        End Sub

        Private Sub CheckChanged(ByVal sender As CheckBox, ByVal e As EventArgs)
            ApproachSign.StandAlone = sender.Checked
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub StandAloneChanged(ByVal sender As CheckBox, ByVal e As EventArgs)
            ApproachSign.StandAlone = sender.Checked

            If ApproachSign.StandAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            Else
                lblActuator.Show()
                comboBoxActuator.Show()
            End If
        End Sub

        Private Sub ActuatorChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            ApproachSign.Actuator = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                checkBoxStandAlone.Checked = approachSign.StandAlone
                AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
                comboBoxActuator.SelectedIndex = comboBoxActuator.FindStringExact(approachSign.Actuator)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The boom barrier component.
    ''' </summary>
    Public Class BoomBarrier : Inherits CComponent
        Public barrierLights As String
        Public BarrierLightsActuator As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\BoomBarrier.png"))
            Size = New Size(181, 25)

            Options = New BoomBarrierOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\BoomBarrier.png")))

            Options = New BoomBarrierOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the boom barrier component.
    ''' </summary>
    Public Class BoomBarrierOptions
        Inherits Options

        Dim boomBarrier As BoomBarrier
        Dim lblBarrierLights As Label
        Dim comboBoxBarrierLights As ComboBox
        Dim lblBarrierLightsActuator As Label
        Dim comboBoxBarrierLightsActuator As ComboBox

        Public Sub New(ByRef NewBoomBarrier As BoomBarrier)
            MyBase.New()
            boomBarrier = NewBoomBarrier
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = boomBarrier.Name
            textBoxType.Text = boomBarrier.Type.ToString
            AddHandler boomBarrier.NameChanged, AddressOf NameChanged

            'Barrier lights option
            Dim lblComboBoxBarrierLights = CreateLabelAndComboBox("Barrier_lights", New Point(3, 64), "None")
            lblBarrierLights = lblComboBoxBarrierLights.Label
            comboBoxBarrierLights = lblComboBoxBarrierLights.comboBox
            comboBoxBarrierLights.Items.AddRange({"None", "Stand alone", "Central"})
            AddHandler comboBoxBarrierLights.SelectionChangeCommitted, AddressOf BarrierLightsChanged
            ISSDT.ToolTip.SetToolTip(lblBarrierLights, "Whether there are barrier Lights present. Use central if it is actuated via a central actuator.")

            'Barrier lights actuator option
            Dim LblComboBoxBarrierLightsActuator = CreateLabelAndComboBox("Actuator:", New Point(3, 88), "")
            lblBarrierLightsActuator = LblComboBoxBarrierLightsActuator.Label
            comboBoxBarrierLightsActuator = LblComboBoxBarrierLightsActuator.comboBox
            AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxBarrierLightsActuator)
            AddHandler comboBoxBarrierLightsActuator.SelectionChangeCommitted, AddressOf BarrierLightsActuatorChanged
            ISSDT.ToolTip.SetToolTip(lblBarrierLightsActuator, "The actuator that controls the barrier lights.")

            lblBarrierLightsActuator.Hide()
            comboBoxBarrierLightsActuator.Hide()

            Controls.Add(lblBarrierLights)
            Controls.Add(comboBoxBarrierLights)
            Controls.Add(lblBarrierLightsActuator)
            Controls.Add(comboBoxBarrierLightsActuator)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub BarrierLightsChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            boomBarrier.barrierLights = sender.SelectedItem
        End Sub

        Private Sub BarrierLightsActuatorChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            boomBarrier.BarrierLightsActuator = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                comboBoxBarrierLights.SelectedIndex = comboBoxBarrierLights.FindStringExact(boomBarrier.barrierLights)

                AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxBarrierLightsActuator)
                comboBoxBarrierLightsActuator.SelectedIndex = comboBoxBarrierLightsActuator.FindStringExact(boomBarrier.BarrierLightsActuator)

                If comboBoxBarrierLights.SelectedItem IsNot "Central" Then
                    lblBarrierLightsActuator.Hide()
                    comboBoxBarrierLightsActuator.Hide()
                Else
                    lblBarrierLightsActuator.Show()
                    comboBoxBarrierLightsActuator.Show()
                End If

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The entering traffic sign component.
    ''' </summary>
    Public Class EnteringTrafficSign : Inherits CComponent
        Public ControlledVia As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\EnteringTrafficSign.png"))
            Size = New Size(20, 51)

            Options = New EnteringTrafficSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\EnteringTrafficSign.png")))

            Options = New EnteringTrafficSignOptions(Me)
        End Sub
    End Class

    Public Class EnteringTrafficSignOptions
        Inherits Options

        Dim enteringTrafficSign As EnteringTrafficSign
        Dim lblControlledVia As Label
        Dim comboBoxControlledVia As ComboBox

        Public Sub New(ByRef NewEnteringTrafficSign As EnteringTrafficSign)
            MyBase.New()
            enteringTrafficSign = NewEnteringTrafficSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name Option
            textBoxName.Text = enteringTrafficSign.Name
            textBoxType.Text = enteringTrafficSign.Type.ToString
            AddHandler enteringTrafficSign.NameChanged, AddressOf NameChanged

            'Controlled via option  
            Dim LblComboBoxControlledVia = CreateLabelAndComboBox("Controlled via:", New Point(3, 64), "")
            lblControlledVia = LblComboBoxControlledVia.Label
            comboBoxControlledVia = LblComboBoxControlledVia.comboBox
            AddComponentsOfTypeToComboBox(Of GUIEnteringTrafficSign)(comboBoxControlledVia)
            AddHandler comboBoxControlledVia.SelectionChangeCommitted, AddressOf ControlledViaChanged
            ISSDT.ToolTip.SetToolTip(lblControlledVia, "The GUI element that controls this entering traffic sign.")

            Controls.Add(lblControlledVia)
            Controls.Add(comboBoxControlledVia)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub ControlledViaChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            enteringTrafficSign.ControlledVia = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                AddComponentsOfTypeToComboBox(Of GUIEnteringTrafficSign)(comboBoxControlledVia)
                comboBoxControlledVia.SelectedIndex = comboBoxControlledVia.FindStringExact(enteringTrafficSign.ControlledVia)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The leaving traffic sign component.
    ''' </summary>
    Public Class LeavingTrafficSign : Inherits CComponent
        Public ControlledVia As String
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\LeavingTrafficSign.png"))
            Size = New Size(22, 38)

            Options = New LeavingTrafficSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\LeavingTrafficSign.png")))

            Options = New LeavingTrafficSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the leaving traffic sign component.
    ''' </summary>
    Public Class LeavingTrafficSignOptions
        Inherits Options

        Dim LeavingTrafficSign As LeavingTrafficSign
        Dim lblControlledVia As Label
        Dim comboBoxControlledVia As ComboBox

        Public Sub New(ByRef NewLeavingTrafficSign As LeavingTrafficSign)
            MyBase.New()
            LeavingTrafficSign = NewLeavingTrafficSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name options.
            textBoxName.Text = LeavingTrafficSign.Name
            textBoxType.Text = LeavingTrafficSign.Type.ToString
            AddHandler LeavingTrafficSign.NameChanged, AddressOf NameChanged

            'Controlled via option  
            Dim LblComboBoxControlledVia = CreateLabelAndComboBox("Controlled via:", New Point(3, 64), "")
            lblControlledVia = LblComboBoxControlledVia.Label
            comboBoxControlledVia = LblComboBoxControlledVia.comboBox
            AddComponentsOfTypeToComboBox(Of GUILeavingTrafficSign)(comboBoxControlledVia)
            AddHandler comboBoxControlledVia.SelectionChangeCommitted, AddressOf ControlledViaChanged
            ISSDT.ToolTip.SetToolTip(lblControlledVia, "The GUI element that controls this leaving traffic sign.")

            Controls.Add(lblControlledVia)
            Controls.Add(comboBoxControlledVia)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub ControlledViaChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            LeavingTrafficSign.ControlledVia = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                AddComponentsOfTypeToComboBox(Of GUILeavingTrafficSign)(comboBoxControlledVia)
                comboBoxControlledVia.SelectedIndex = comboBoxControlledVia.FindStringExact(LeavingTrafficSign.ControlledVia)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The rotating bridge component.
    ''' </summary>
    Public Class RotatingBridge : Inherits CComponent
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\RotatingBridge.png"))
            Size = New Size(395, 96)

            Options = New RotatingBridgeOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\RotatingBridge.png")))

            Options = New RotatingBridgeOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the rotating bridge component.
    ''' </summary>
    Public Class RotatingBridgeOptions
        Inherits Options

        Dim RotatingBridge As RotatingBridge

        Public Sub New(ByRef NewRotatingBridge As RotatingBridge)
            MyBase.New()
            RotatingBridge = NewRotatingBridge
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            textBoxName.Text = RotatingBridge.Name
            textBoxType.Text = RotatingBridge.Type.ToString
            AddHandler RotatingBridge.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                'Nothing to update

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The leaving traffic sign GUI component.
    ''' </summary>
    Public Class GUILeavingTrafficSign : Inherits CComponent
        Public Green As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\OVTLGUI.png"))
            Size = New Size(27, 51)

            Options = New GUILeavingTrafficSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\OVTLGUI.png")))

            Options = New GUILeavingTrafficSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the leaving traffic sign GUI component.
    ''' </summary>
    Public Class GUILeavingTrafficSignOptions
        Inherits Options

        Dim GUILeavingTrafficSign As GUILeavingTrafficSign
        Dim LabelGreen As Label
        Dim textBoxGreen As TextBox

        Public Sub New(ByRef NewGUILeavingTrafficSign As GUILeavingTrafficSign)
            MyBase.New()
            GUILeavingTrafficSign = NewGUILeavingTrafficSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = GUILeavingTrafficSign.Name
            textBoxType.Text = GUILeavingTrafficSign.Type.ToString
            AddHandler GUILeavingTrafficSign.NameChanged, AddressOf NameChanged

            'Green option
            Dim LbltextBoxGreen = CreateLabelAndTextBox("Green Ok:", New Point(3, 64), "", False, 200)
            LabelGreen = LbltextBoxGreen.Label
            textBoxGreen = LbltextBoxGreen.textBox
            ISSDT.ToolTip.SetToolTip(LabelGreen, "The condition for when a green command may be given.")
            AddHandler textBoxGreen.TextChanged, AddressOf GreenChanged

            Controls.Add(LabelGreen)
            Controls.Add(textBoxGreen)
            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub GreenChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUILeavingTrafficSign.Green = sender.Text
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                textBoxGreen.Text = GUILeavingTrafficSign.Green

            End If
        End Sub

    End Class

    ''' <summary>
    ''' The entering traffic sign GUI component.
    ''' </summary>
    Public Class GUIEnteringTrafficSign : Inherits CComponent
        Public Green As String
        Public RedGreen As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\EVTLGUI.png"))
            Size = New Size(27, 75)

            Options = New GUIEnteringTrafficSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\EVTLGUI.png")))

            Options = New GUIEnteringTrafficSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the entering traffic sign GUI component.
    ''' </summary>
    Public Class GUIEnteringTrafficSignOptions
        Inherits Options

        Dim GUIEnteringTrafficSign As GUIEnteringTrafficSign
        Dim LabelGreen As Label
        Dim textBoxGreen As TextBox
        Dim LabelRedGreen As Label
        Dim textBoxRedGreen As TextBox

        Public Sub New(ByRef NewGUIEnteringTrafficSign As GUIEnteringTrafficSign)
            MyBase.New()
            GUIEnteringTrafficSign = NewGUIEnteringTrafficSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = GUIEnteringTrafficSign.Name
            textBoxType.Text = GUIEnteringTrafficSign.Type.ToString
            AddHandler GUIEnteringTrafficSign.NameChanged, AddressOf NameChanged

            'Green option
            Dim LbltextBoxGreen = CreateLabelAndTextBox("Green Ok:", New Point(3, 64), "", False, 200)
            LabelGreen = LbltextBoxGreen.Label
            textBoxGreen = LbltextBoxGreen.textBox
            ISSDT.ToolTip.SetToolTip(LabelGreen, "The condition for when a green command may be given.")
            AddHandler textBoxGreen.TextChanged, AddressOf GreenChanged

            'RedGreen option
            Dim LbltextBoxRedGreen = CreateLabelAndTextBox("Red-Green Ok:", New Point(3, 88), "", False, 200)
            LabelRedGreen = LbltextBoxRedGreen.Label
            textBoxRedGreen = LbltextBoxRedGreen.textBox
            ISSDT.ToolTip.SetToolTip(LabelRedGreen, "The condition for when a red-green command may be given.")
            AddHandler textBoxRedGreen.TextChanged, AddressOf RedGreenChanged

            Controls.Add(LabelGreen)
            Controls.Add(textBoxGreen)
            Controls.Add(LabelRedGreen)
            Controls.Add(textBoxRedGreen)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub GreenChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIEnteringTrafficSign.Green = sender.Text
        End Sub

        Private Sub RedGreenChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIEnteringTrafficSign.RedGreen = sender.Text
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                textBoxGreen.Text = GUIEnteringTrafficSign.Green
                textBoxRedGreen.Text = GUIEnteringTrafficSign.RedGreen
            End If
        End Sub

    End Class

    ''' <summary>
    ''' The rotating bridge GUI component.
    ''' </summary>
    Public Class GUIRotatingBridge : Inherits CComponent
        Public Bridge As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\RotatingBridgeGUI3.png"))
            Size = New Size(140, 306)

            Options = New GUIRotatingBridgeOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\RotatingBridgeGUI3.png")))

            Options = New GUIRotatingBridgeOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the rotating bridge GUI component.
    ''' </summary>
    Public Class GUIRotatingBridgeOptions
        Inherits Options

        Dim GUIRotatingBridge As GUIRotatingBridge
        Dim lblBridge As Label
        Dim comboBoxBridge As ComboBox

        Public Sub New(ByRef NewGUIRotatingBridge As GUIRotatingBridge)
            MyBase.New()
            GUIRotatingBridge = NewGUIRotatingBridge
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name Option
            textBoxName.Text = GUIRotatingBridge.Name
            textBoxType.Text = GUIRotatingBridge.Type.ToString
            AddHandler GUIRotatingBridge.NameChanged, AddressOf NameChanged

            'Bridge Option
            Dim LblComboBoxBridge = CreateLabelAndComboBox("Bridge:", New Point(3, 64), "")
            lblBridge = LblComboBoxBridge.Label
            comboBoxBridge = LblComboBoxBridge.comboBox
            AddComponentsOfTypeToComboBox(Of RotatingBridge)(comboBoxBridge)
            AddHandler comboBoxBridge.SelectionChangeCommitted, AddressOf BridgeChanged
            ISSDT.ToolTip.SetToolTip(lblBridge, "The bridge that is connected to this GUI element.")

            Controls.Add(lblBridge)
            Controls.Add(comboBoxBridge)

            ISSDT.Controls.Add(Me)
        End Sub
        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub BridgeChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            GUIRotatingBridge.Bridge = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                AddComponentsOfTypeToComboBox(Of RotatingBridge)(comboBoxBridge)
                comboBoxBridge.SelectedIndex = comboBoxBridge.FindStringExact(GUIRotatingBridge.Bridge)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The land traffic sign GUI component.
    ''' </summary>
    Public Class GUIStopSign : Inherits CComponent
        Public ActivatedCondition As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSignGUI.png"))
            Size = New Size(27, 26)

            Options = New GUIStopSignOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSignGUI.png")))

            Options = New GUIStopSignOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the land traffic sign GUI component.
    ''' </summary>
    Public Class GUIStopSignOptions
        Inherits Options

        Dim GUIStopSign As GUIStopSign
        Dim labelActivated As Label
        Dim TextBoxActivated As TextBox

        Public Sub New(ByRef NewGUIStopSign As GUIStopSign)
            MyBase.New()
            GUIStopSign = NewGUIStopSign
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = GUIStopSign.Name
            textBoxType.Text = GUIStopSign.Type.ToString
            AddHandler GUIStopSign.NameChanged, AddressOf NameChanged

            'Enablement option
            Dim LblTextBoxActivated = CreateLabelAndTextBox("Activated when:", New Point(3, 64), "", False, 200)
            labelActivated = LblTextBoxActivated.Label
            TextBoxActivated = LblTextBoxActivated.textBox
            ISSDT.ToolTip.SetToolTip(labelActivated, "The condition for when the land traffic sign is activated.")
            AddHandler TextBoxActivated.TextChanged, AddressOf ActivatedChanged

            Controls.Add(labelActivated)
            Controls.Add(TextBoxActivated)
            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub ActivatedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIStopSign.ActivatedCondition = sender.Text
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                TextBoxActivated.Text = GUIStopSign.ActivatedCondition
            End If
        End Sub

    End Class

    ''' <summary>
    ''' The boom barrier GUI component.
    ''' </summary>
    Public Class GUIBoomBarrier : Inherits CComponent
        Public BoomBarrier As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\BoomBarrierGUI.png"))
            Size = New Size(140, 33)

            Options = New GUIBoomBarrierOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\BoomBarrierGUI.png")))

            Options = New GUIBoomBarrierOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the boom barrier GUI component.
    ''' </summary>
    Public Class GUIBoomBarrierOptions
        Inherits Options

        Dim GUIBoomBarrier As GUIBoomBarrier
        Dim lblBoomBarrier As Label
        Dim comboBoxBoomBarrier As ComboBox

        Public Sub New(ByRef NewGUIBoomBarrier As GUIBoomBarrier)
            MyBase.New()
            GUIBoomBarrier = NewGUIBoomBarrier
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name Option
            textBoxName.Text = GUIBoomBarrier.Name
            textBoxType.Text = GUIBoomBarrier.Type.ToString
            AddHandler GUIBoomBarrier.NameChanged, AddressOf NameChanged

            'Boom Barrier Option
            Dim LblComboBoxBoomBarrier = CreateLabelAndComboBox("BoomBarrier:", New Point(3, 64), "")
            lblBoomBarrier = LblComboBoxBoomBarrier.Label
            comboBoxBoomBarrier = LblComboBoxBoomBarrier.comboBox
            AddComponentsOfTypeToComboBox(Of BoomBarrier)(comboBoxBoomBarrier)
            AddHandler comboBoxBoomBarrier.SelectionChangeCommitted, AddressOf BoomBarrierChanged
            ISSDT.ToolTip.SetToolTip(lblBoomBarrier, "The boom barrier that is connected to this GUI element.")

            Controls.Add(lblBoomBarrier)
            Controls.Add(comboBoxBoomBarrier)
            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub BoomBarrierChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            GUIBoomBarrier.BoomBarrier = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                AddComponentsOfTypeToComboBox(Of BoomBarrier)(comboBoxBoomBarrier)
                comboBoxBoomBarrier.SelectedIndex = comboBoxBoomBarrier.FindStringExact(GUIBoomBarrier.BoomBarrier)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The bridge window GUI component.
    ''' </summary>
    Public Class GUIBridgeWindow : Inherits CComponent
        Public LTStopped As String
        Public LTReleased As String
        Public BBClosed As String
        Public BBOpen As String
        Public BBStopped As String
        Public BOpen As String
        Public BClosed As String
        Public BStopped As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Window.png"))
            Size = New Size(122, 152)

            Options = New GUIBridgeWindowOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Window.png")))

            Options = New GUIBridgeWindowOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the bridge window GUI component.
    ''' </summary>
    Public Class GUIBridgeWindowOptions
        Inherits Options

        Dim GUIBridgeWindow As GUIBridgeWindow
        Dim labelLTStopped As Label
        Dim TextBoxLTStopped As TextBox
        Dim labelLTReleased As Label
        Dim TextBoxLTReleased As TextBox
        Dim labelBBClosed As Label
        Dim TextBoxBBClosed As TextBox
        Dim labelBBOpen As Label
        Dim TextBoxBBOpen As TextBox
        Dim labelBBStopped As Label
        Dim TextBoxBBStopped As TextBox
        Dim labelBOpen As Label
        Dim TextBoxBOpen As TextBox
        Dim labelBClosed As Label
        Dim TextBoxBClosed As TextBox
        Dim labelBStopped As Label
        Dim TextBoxBStopped As TextBox

        Public Sub New(ByRef NewGUIBridgeWindow As GUIBridgeWindow)
            MyBase.New()
            GUIBridgeWindow = NewGUIBridgeWindow
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = GUIBridgeWindow.Name
            textBoxType.Text = GUIBridgeWindow.Type.ToString
            AddHandler GUIBridgeWindow.NameChanged, AddressOf NameChanged

            'Land traffic stopped option
            Dim LblTextBoxLTStopped = CreateLabelAndTextBox("LTStopped:", New Point(3, 64), "", False, 200)
            labelLTStopped = LblTextBoxLTStopped.Label
            TextBoxLTStopped = LblTextBoxLTStopped.textBox
            ISSDT.ToolTip.SetToolTip(labelLTStopped, "The condition for when the land traffic is stopped.")
            AddHandler TextBoxLTStopped.TextChanged, AddressOf LTStoppedChanged

            'Land traffic released option
            Dim LblTextBoxLTReleased = CreateLabelAndTextBox("LTReleased:", New Point(3, 88), "", False, 200)
            labelLTReleased = LblTextBoxLTReleased.Label
            TextBoxLTReleased = LblTextBoxLTReleased.textBox
            ISSDT.ToolTip.SetToolTip(labelLTReleased, "The condition for when the land traffic is Released.")
            AddHandler TextBoxLTReleased.TextChanged, AddressOf LTReleasedChanged

            'Boom barrier closed option
            Dim LblTextBoxBBClosed = CreateLabelAndTextBox("BBClosed:", New Point(3, 112), "", False, 200)
            labelBBClosed = LblTextBoxBBClosed.Label
            TextBoxBBClosed = LblTextBoxBBClosed.textBox
            ISSDT.ToolTip.SetToolTip(labelBBClosed, "The condition for when the boom barrier are closed.")
            AddHandler TextBoxBBClosed.TextChanged, AddressOf BBClosedChanged

            'Boom barrier open option
            Dim LblTextBoxBBOpen = CreateLabelAndTextBox("BBOpen:", New Point(3, 136), "", False, 200)
            labelBBOpen = LblTextBoxBBOpen.Label
            TextBoxBBOpen = LblTextBoxBBOpen.textBox
            ISSDT.ToolTip.SetToolTip(labelBBOpen, "The condition for when the boom barriers are open.")
            AddHandler TextBoxBBOpen.TextChanged, AddressOf BBOpenChanged

            'Boom barrier stopped option
            Dim LblTextBoxBBStopped = CreateLabelAndTextBox("BBStopped:", New Point(3, 160), "", False, 200)
            labelBBStopped = LblTextBoxBBStopped.Label
            TextBoxBBStopped = LblTextBoxBBStopped.textBox
            ISSDT.ToolTip.SetToolTip(labelBBStopped, "The condition for when the boom barriers are stopped.")
            AddHandler TextBoxBBStopped.TextChanged, AddressOf BBStoppedChanged

            'Bridge open option
            Dim LblTextBoxBOpen = CreateLabelAndTextBox("BOpen:", New Point(3, 184), "", False, 200)
            labelBOpen = LblTextBoxBOpen.Label
            TextBoxBOpen = LblTextBoxBOpen.textBox
            ISSDT.ToolTip.SetToolTip(labelBOpen, "The condition for when the Bridge is open.")
            AddHandler TextBoxBOpen.TextChanged, AddressOf BOpenChanged

            'Bridge closed option
            Dim LblTextBoxBClosed = CreateLabelAndTextBox("BClosed:", New Point(3, 208), "", False, 200)
            labelBClosed = LblTextBoxBClosed.Label
            TextBoxBClosed = LblTextBoxBClosed.textBox
            ISSDT.ToolTip.SetToolTip(labelBClosed, "The condition for when the bridge is closed.")
            AddHandler TextBoxBClosed.TextChanged, AddressOf BClosedChanged

            'Bridge stopped option
            Dim LblTextBoxBStopped = CreateLabelAndTextBox("BStopped:", New Point(3, 232), "", False, 200)
            labelBStopped = LblTextBoxBStopped.Label
            TextBoxBStopped = LblTextBoxBStopped.textBox
            ISSDT.ToolTip.SetToolTip(labelBStopped, "The condition for when the bridge is stopped.")
            AddHandler TextBoxBStopped.TextChanged, AddressOf BStoppedChanged

            Controls.Add(labelLTStopped)
            Controls.Add(TextBoxLTStopped)
            Controls.Add(labelLTReleased)
            Controls.Add(TextBoxLTReleased)
            Controls.Add(labelBBClosed)
            Controls.Add(TextBoxBBClosed)
            Controls.Add(labelBBOpen)
            Controls.Add(TextBoxBBOpen)
            Controls.Add(labelBBStopped)
            Controls.Add(TextBoxBBStopped)
            Controls.Add(labelBOpen)
            Controls.Add(TextBoxBOpen)
            Controls.Add(labelBClosed)
            Controls.Add(TextBoxBClosed)
            Controls.Add(labelBStopped)
            Controls.Add(TextBoxBStopped)
            ISSDT.Controls.Add(Me)
        End Sub
        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub LTStoppedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.LTStopped = sender.Text
        End Sub

        Private Sub LTReleasedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.LTReleased = sender.Text
        End Sub

        Private Sub BBClosedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BBClosed = sender.Text
        End Sub

        Private Sub BBOpenChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BBOpen = sender.Text
        End Sub

        Private Sub BBStoppedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BBStopped = sender.Text
        End Sub

        Private Sub BOpenChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BOpen = sender.Text
        End Sub

        Private Sub BClosedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BClosed = sender.Text
        End Sub

        Private Sub BStoppedChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            GUIBridgeWindow.BStopped = sender.Text
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                TextBoxLTStopped.Text = GUIBridgeWindow.LTStopped
                TextBoxLTReleased.Text = GUIBridgeWindow.LTReleased
                TextBoxBBClosed.Text = GUIBridgeWindow.BBClosed
                TextBoxBBOpen.Text = GUIBridgeWindow.BBOpen
                TextBoxBBStopped.Text = GUIBridgeWindow.BBStopped
                TextBoxBOpen.Text = GUIBridgeWindow.BOpen
                TextBoxBClosed.Text = GUIBridgeWindow.BClosed
                TextBoxBStopped.Text = GUIBridgeWindow.BStopped

            End If
        End Sub

    End Class

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
    ''' The Timer component.
    ''' </summary>
    Public Class Timer : Inherits CComponent
        Public duration As String = ""
        Public startCondition As String = ""
        Public stopCondition As String = ""

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Timer.png"))
            Size = New Size(30, 36)

            Options = New TimerOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Timer.png")))

            Options = New TimerOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the Timer component.
    ''' </summary>
    Public Class TimerOptions
        Inherits Options

        Dim timer As Timer
        Dim lblDuration As Label
        Dim textBoxDuration As TextBox
        Dim lblStartCondition As Label
        Dim textBoxStartCondition As TextBox
        Dim lblStopCondition As Label
        Dim textBoxStopCondition As TextBox

        Public Sub New(ByRef newTimer As Timer)
            MyBase.New()
            timer = newTimer
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = timer.Name
            textBoxType.Text = timer.Type.ToString
            AddHandler timer.NameChanged, AddressOf NameChanged

            'Duration option
            Dim LbltextBoxDuration = CreateLabelAndTextBox("Duration [s]:", New Point(3, 64), "", False)
            lblDuration = LbltextBoxDuration.Label
            textBoxDuration = LbltextBoxDuration.textBox
            ISSDT.ToolTip.SetToolTip(lblDuration, "The duration of the timer in seconds.")
            AddHandler textBoxDuration.TextChanged, AddressOf DurationChanged

            'StartCondition option
            Dim LbltextBoxStartCondition = CreateLabelAndTextBox("StartCondition:", New Point(3, 88), "", False, 200)
            lblStartCondition = LbltextBoxStartCondition.Label
            textBoxStartCondition = LbltextBoxStartCondition.textBox
            ISSDT.ToolTip.SetToolTip(lblStartCondition, "The start condition of the timer.")
            AddHandler textBoxStartCondition.TextChanged, AddressOf StartConditionChanged

            'StopCondition option
            Dim LbltextBoxStopCondition = CreateLabelAndTextBox("StopCondition:", New Point(3, 112), "", False, 200)
            lblStopCondition = LbltextBoxStopCondition.Label
            textBoxStopCondition = LbltextBoxStopCondition.textBox
            ISSDT.ToolTip.SetToolTip(lblStopCondition, "The stop condition of the timer.")
            AddHandler textBoxStopCondition.TextChanged, AddressOf StopConditionChanged

            Controls.Add(lblDuration)
            Controls.Add(textBoxDuration)
            Controls.Add(lblStartCondition)
            Controls.Add(textBoxStartCondition)
            Controls.Add(lblStopCondition)
            Controls.Add(textBoxStopCondition)
            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub DurationChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            timer.duration = sender.Text
            VerifyNumericTextBox(textBoxDuration)
        End Sub

        Private Sub StartConditionChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            timer.startCondition = sender.Text
        End Sub

        Private Sub StopConditionChanged(ByVal sender As TextBox, ByVal e As EventArgs)
            timer.stopCondition = sender.Text
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                textBoxDuration.Text = timer.duration
                textBoxStartCondition.Text = timer.startCondition
                textBoxStopCondition.Text = timer.stopCondition
            End If
        End Sub

    End Class

    ''' <summary>
    ''' The actuator component.
    ''' </summary>
    Public Class Actuator : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Actuator.png"))
            Size = New Size(19, 19)

            Options = New ActuatorOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Actuator.png")))

            Options = New ActuatorOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the actuator component. 
    ''' </summary>
    Public Class ActuatorOptions
        Inherits Options

        Dim actuator As Actuator

        Public Sub New(ByRef newActuator As Actuator)
            MyBase.New()
            actuator = newActuator
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = actuator.Name
            textBoxType.Text = actuator.Type.ToString
            AddHandler actuator.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The sensor component.
    ''' </summary>
    Public Class Sensor : Inherits CComponent

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Sensor.png"))
            Size = New Size(19, 19)

            Options = New SensorOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\Sensor.png")))

            Options = New SensorOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the sensor component. 
    ''' </summary>
    Public Class SensorOptions
        Inherits Options

        Dim sensor As Sensor

        Public Sub New(ByRef newSensor As Sensor)
            MyBase.New()
            sensor = newSensor
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = sensor.Name
            textBoxType.Text = sensor.Type.ToString
            AddHandler sensor.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The double stop sign component.
    ''' </summary>
    Public Class StopSignDouble : Inherits CComponent
        Public standAlone As Boolean = True
        Public actuator As String

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSignDouble.png"))
            Size = New Size(43, 23)

            Options = New StopSignDoubleOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\StopSignDouble.png")))

            Options = New StopSignDoubleOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the double stop sign component. 
    ''' </summary>
    Public Class StopSignDoubleOptions
        Inherits Options

        Dim stopSignDouble As StopSignDouble
        Dim checkBoxStandAlone As CheckBox
        Dim lblActuator As Label
        Dim comboBoxActuator As ComboBox

        Public Sub New(ByRef NewStopSignDouble As StopSignDouble)
            MyBase.New()
            stopSignDouble = NewStopSignDouble
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            'Name option
            textBoxName.Text = stopSignDouble.Name
            textBoxType.Text = stopSignDouble.Type.ToString
            AddHandler stopSignDouble.NameChanged, AddressOf NameChanged

            'Stand alone option
            checkBoxStandAlone = CreateCheckBox("Standalone", New Size(80, 17), New Point(3, 64), True)
            AddHandler checkBoxStandAlone.CheckedChanged, AddressOf StandAloneChanged
            ISSDT.ToolTip.SetToolTip(checkBoxStandAlone, "Whether the stop sign has its own actuator or is controlled via another actuator.")

            'Actuator option
            Dim LblComboBoxActuator = CreateLabelAndComboBox("Actuator:", New Point(3, 88), "")
            lblActuator = LblComboBoxActuator.Label
            comboBoxActuator = LblComboBoxActuator.comboBox
            AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
            comboBoxActuator.Items.Add(stopSignDouble.Name)
            AddHandler comboBoxActuator.SelectionChangeCommitted, AddressOf ActuatorChanged
            ISSDT.ToolTip.SetToolTip(lblActuator, "The actuator that controls this stop sign.")

            If stopSignDouble.standAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            End If

            Controls.Add(checkBoxStandAlone)
            Controls.Add(lblActuator)
            Controls.Add(comboBoxActuator)

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub StandAloneChanged(ByVal sender As CheckBox, ByVal e As EventArgs)
            stopSignDouble.standAlone = sender.Checked

            If stopSignDouble.standAlone Then
                lblActuator.Hide()
                comboBoxActuator.Hide()
            Else
                lblActuator.Show()
                comboBoxActuator.Show()
            End If
        End Sub

        Private Sub ActuatorChanged(ByVal sender As ComboBox, ByVal e As EventArgs)
            stopSignDouble.actuator = sender.SelectedItem
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                checkBoxStandAlone.Checked = stopSignDouble.standAlone
                AddComponentsOfTypeToComboBox(Of StopSign, ApproachSign, Actuator)(comboBoxActuator)
                comboBoxActuator.SelectedIndex = comboBoxActuator.FindStringExact(stopSignDouble.actuator)

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class

    ''' <summary>
    ''' The draw bridge component.
    ''' </summary>
    Public Class DrawBridge : Inherits CComponent
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\DrawBridge.png"))
            Size = New Size(384, 97)

            Options = New DrawBridgeOptions(Me)
        End Sub

        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, newCanvas As CCanvas, NewSize As Size, NewRotation As Integer, NewColor As Color)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas, NewSize, NewRotation, NewColor, Image.FromFile(System.IO.Path.Combine(path, "Icons\DrawBridge.png")))

            Options = New DrawBridgeOptions(Me)
        End Sub
    End Class

    ''' <summary>
    ''' The option window for the draw bridge component.
    ''' </summary>
    Public Class DrawBridgeOptions
        Inherits Options

        Dim DrawBridge As DrawBridge

        Public Sub New(ByRef NewDrawBridge As DrawBridge)
            MyBase.New()
            DrawBridge = NewDrawBridge
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            textBoxName.Text = DrawBridge.Name
            textBoxType.Text = DrawBridge.Type.ToString
            AddHandler DrawBridge.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                'Nothing to update

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
        End Sub
    End Class






    ''' <summary>
    ''' The quay component.
    ''' </summary>
    Public Class Quay : Inherits CComponent
        Public Sub New(NewName As String, newComponentType As ComponentTypesEnum, NewLocation As Point, ByRef newCanvas As CCanvas)
            MyBase.New(NewName, newComponentType, NewLocation, newCanvas)
            Image = Image.FromFile(System.IO.Path.Combine(path, "Icons\Quay.png"))
            Size = New Size(300, 100)

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

        Dim Quay As Quay

        Public Sub New(ByRef NewQuay As Quay)
            MyBase.New()
            Quay = NewQuay
            AddHandler VisibleChanged, AddressOf UpdateOptionWindowValues

            textBoxName.Text = Quay.Name
            textBoxType.Text = Quay.Type.ToString
            textBoxWaterway.Text = Quay.Waterway
            AddHandler Quay.NameChanged, AddressOf NameChanged

            ISSDT.Controls.Add(Me)
        End Sub

        Public Sub NameChanged(Sender As CComponent, NewName As String)
            textBoxName.Text = NewName
        End Sub

        Private Sub UpdateOptionWindowValues()
            If Visible Then
                'Nothing to update

                VerifyAll()
            End If
        End Sub

        Public Sub VerifyAll()
            'Nothing to verify
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

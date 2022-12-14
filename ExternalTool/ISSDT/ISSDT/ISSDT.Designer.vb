<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ISSDT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISSDT))
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.DGVoverview = New System.Windows.Forms.DataGridView()
        Me.Waterway = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ObjectName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btRefresh = New System.Windows.Forms.Button()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Generate = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateSvgImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateJSON = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabTools = New System.Windows.Forms.TabPage()
        Me.Quay = New CTool()
        Me.CTool3 = New CTool()
        Me.CTool2 = New CTool()
        Me.CTool1 = New CTool()
        Me.ToolSensor = New CTool()
        Me.ToolActuator = New CTool()
        Me.ToolTimer = New CTool()
        Me.ToolGUIWindow = New CTool()
        Me.ToolGUIStopSign = New CTool()
        Me.ToolGUIBoomBarrier = New CTool()
        Me.ToolGUIRotatingBridge = New CTool()
        Me.ToolGUIOutgoingTrafficSign = New CTool()
        Me.ToolGUIEnteringTrafficSign = New CTool()
        Me.ToolRotatingBridge = New CTool()
        Me.ToolLeavingTrafficSign = New CTool()
        Me.ToolEnteringTrafficSign = New CTool()
        Me.ToolBoomBarrier = New CTool()
        Me.ToolQuay = New CTool()
        Me.ToolApproachSign = New CTool()
        Me.ToolStopSign = New CTool()
        Me.TabDrawings = New System.Windows.Forms.TabPage()
        Me.LblTextLabel = New CTool()
        Me.LblSquare = New CTool()
        Me.pnlColors = New System.Windows.Forms.Panel()
        Me.lblColorWhite = New System.Windows.Forms.Label()
        Me.lblColorWeg = New System.Windows.Forms.Label()
        Me.LblColorWaterweg = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.DeleteRequirement = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuItemDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogJSON = New System.Windows.Forms.SaveFileDialog()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.canvasPlant = New CCanvas()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.CToolBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.DGVoverview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.TabTools.SuspendLayout()
        Me.TabDrawings.SuspendLayout()
        Me.pnlColors.SuspendLayout()
        Me.DeleteRequirement.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        CType(Me.CToolBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.CreatePrompt = True
        Me.SaveFileDialog.DefaultExt = "txt"
        Me.SaveFileDialog.Filter = "Normal text file|*.txt"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.DefaultExt = "txt"
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        Me.OpenFileDialog.Filter = "Normal text file|*.txt"
        '
        'DGVoverview
        '
        Me.DGVoverview.AllowUserToAddRows = False
        Me.DGVoverview.AllowUserToDeleteRows = False
        Me.DGVoverview.AllowUserToResizeRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVoverview.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVoverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVoverview.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Waterway, Me.ObjectName, Me.ObjectType, Me.ObjectID})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVoverview.DefaultCellStyle = DataGridViewCellStyle4
        Me.DGVoverview.Location = New System.Drawing.Point(1355, 694)
        Me.DGVoverview.Margin = New System.Windows.Forms.Padding(4)
        Me.DGVoverview.Name = "DGVoverview"
        Me.DGVoverview.RowHeadersWidth = 51
        Me.DGVoverview.Size = New System.Drawing.Size(501, 310)
        Me.DGVoverview.TabIndex = 15
        '
        'Waterway
        '
        Me.Waterway.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Waterway.Frozen = True
        Me.Waterway.HeaderText = "Waterway"
        Me.Waterway.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.Waterway.MinimumWidth = 6
        Me.Waterway.Name = "Waterway"
        Me.Waterway.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Waterway.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Waterway.Width = 75
        '
        'ObjectName
        '
        Me.ObjectName.Frozen = True
        Me.ObjectName.HeaderText = "Name"
        Me.ObjectName.MinimumWidth = 6
        Me.ObjectName.Name = "ObjectName"
        Me.ObjectName.ReadOnly = True
        Me.ObjectName.Width = 125
        '
        'ObjectType
        '
        Me.ObjectType.Frozen = True
        Me.ObjectType.HeaderText = "Type"
        Me.ObjectType.MinimumWidth = 6
        Me.ObjectType.Name = "ObjectType"
        Me.ObjectType.ReadOnly = True
        Me.ObjectType.Width = 125
        '
        'ObjectID
        '
        Me.ObjectID.Frozen = True
        Me.ObjectID.HeaderText = "ObjectID"
        Me.ObjectID.MinimumWidth = 6
        Me.ObjectID.Name = "ObjectID"
        Me.ObjectID.ReadOnly = True
        Me.ObjectID.Width = 70
        '
        'btRefresh
        '
        Me.btRefresh.Location = New System.Drawing.Point(1355, 1012)
        Me.btRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btRefresh.Name = "btRefresh"
        Me.btRefresh.Size = New System.Drawing.Size(100, 28)
        Me.btRefresh.TabIndex = 16
        Me.btRefresh.Text = "Refresh"
        Me.btRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btRefresh.UseVisualStyleBackColor = False
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.Generate, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.SaveAsToolStripMenuItem.Text = "Save as..."
        '
        'Generate
        '
        Me.Generate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateSvgImage, Me.GenerateJSON})
        Me.Generate.Name = "Generate"
        Me.Generate.Size = New System.Drawing.Size(229, 26)
        Me.Generate.Text = "Generate"
        '
        'GenerateSvgImage
        '
        Me.GenerateSvgImage.Name = "GenerateSvgImage"
        Me.GenerateSvgImage.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.GenerateSvgImage.Size = New System.Drawing.Size(256, 26)
        Me.GenerateSvgImage.Text = "SVG Image"
        '
        'GenerateJSON
        '
        Me.GenerateJSON.Name = "GenerateJSON"
        Me.GenerateJSON.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.J), System.Windows.Forms.Keys)
        Me.GenerateJSON.Size = New System.Drawing.Size(256, 26)
        Me.GenerateJSON.Text = "JSON"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(49, 24)
        Me.InfoToolStripMenuItem.Text = "Info"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.InfoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1856, 28)
        Me.MenuStrip1.TabIndex = 17
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabTools)
        Me.TabControl.Controls.Add(Me.TabDrawings)
        Me.TabControl.Location = New System.Drawing.Point(1355, 60)
        Me.TabControl.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(491, 368)
        Me.TabControl.TabIndex = 20
        '
        'TabTools
        '
        Me.TabTools.AutoScroll = True
        Me.TabTools.BackColor = System.Drawing.SystemColors.Control
        Me.TabTools.Controls.Add(Me.Quay)
        Me.TabTools.Controls.Add(Me.CTool3)
        Me.TabTools.Controls.Add(Me.CTool2)
        Me.TabTools.Controls.Add(Me.CTool1)
        Me.TabTools.Controls.Add(Me.ToolSensor)
        Me.TabTools.Controls.Add(Me.ToolActuator)
        Me.TabTools.Controls.Add(Me.ToolTimer)
        Me.TabTools.Controls.Add(Me.ToolGUIWindow)
        Me.TabTools.Controls.Add(Me.ToolGUIStopSign)
        Me.TabTools.Controls.Add(Me.ToolGUIBoomBarrier)
        Me.TabTools.Controls.Add(Me.ToolGUIRotatingBridge)
        Me.TabTools.Controls.Add(Me.ToolGUIOutgoingTrafficSign)
        Me.TabTools.Controls.Add(Me.ToolGUIEnteringTrafficSign)
        Me.TabTools.Controls.Add(Me.ToolRotatingBridge)
        Me.TabTools.Controls.Add(Me.ToolLeavingTrafficSign)
        Me.TabTools.Controls.Add(Me.ToolEnteringTrafficSign)
        Me.TabTools.Controls.Add(Me.ToolBoomBarrier)
        Me.TabTools.Controls.Add(Me.ToolQuay)
        Me.TabTools.Controls.Add(Me.ToolApproachSign)
        Me.TabTools.Controls.Add(Me.ToolStopSign)
        Me.TabTools.Location = New System.Drawing.Point(4, 25)
        Me.TabTools.Margin = New System.Windows.Forms.Padding(4)
        Me.TabTools.Name = "TabTools"
        Me.TabTools.Padding = New System.Windows.Forms.Padding(4)
        Me.TabTools.Size = New System.Drawing.Size(483, 339)
        Me.TabTools.TabIndex = 0
        Me.TabTools.Text = "Components"
        '
        'Quay
        '
        Me.Quay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Quay.Image = CType(resources.GetObject("Quay.Image"), System.Drawing.Image)
        Me.Quay.Location = New System.Drawing.Point(280, 68)
        Me.Quay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Quay.Name = "Quay"
        Me.Quay.Size = New System.Drawing.Size(105, 49)
        Me.Quay.TabIndex = 69
        Me.ToolTip.SetToolTip(Me.Quay, "Quay")
        Me.Quay.toolType = MComponentTypes.ComponentTypesEnum.Quay
        '
        'CTool3
        '
        Me.CTool3.BackColor = System.Drawing.Color.Transparent
        Me.CTool3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CTool3.Image = CType(resources.GetObject("CTool3.Image"), System.Drawing.Image)
        Me.CTool3.Location = New System.Drawing.Point(228, 415)
        Me.CTool3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.CTool3.Name = "CTool3"
        Me.CTool3.Size = New System.Drawing.Size(117, 202)
        Me.CTool3.TabIndex = 42
        Me.ToolTip.SetToolTip(Me.CTool3, "GUI Rotating Bridge")
        Me.CTool3.toolType = MComponentTypes.ComponentTypesEnum.GUIRotatingBridge
        '
        'CTool2
        '
        Me.CTool2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CTool2.Image = CType(resources.GetObject("CTool2.Image"), System.Drawing.Image)
        Me.CTool2.Location = New System.Drawing.Point(131, 164)
        Me.CTool2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.CTool2.Name = "CTool2"
        Me.CTool2.Size = New System.Drawing.Size(200, 47)
        Me.CTool2.TabIndex = 41
        Me.ToolTip.SetToolTip(Me.CTool2, "Draw Bridge")
        Me.CTool2.toolType = MComponentTypes.ComponentTypesEnum.DrawBridge
        '
        'CTool1
        '
        Me.CTool1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CTool1.Image = CType(resources.GetObject("CTool1.Image"), System.Drawing.Image)
        Me.CTool1.Location = New System.Drawing.Point(131, 4)
        Me.CTool1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.CTool1.Name = "CTool1"
        Me.CTool1.Size = New System.Drawing.Size(73, 49)
        Me.CTool1.TabIndex = 40
        Me.ToolTip.SetToolTip(Me.CTool1, "Stop Sign Double")
        Me.CTool1.toolType = MComponentTypes.ComponentTypesEnum.StopSignDouble
        '
        'ToolSensor
        '
        Me.ToolSensor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolSensor.Image = CType(resources.GetObject("ToolSensor.Image"), System.Drawing.Image)
        Me.ToolSensor.Location = New System.Drawing.Point(277, 4)
        Me.ToolSensor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolSensor.Name = "ToolSensor"
        Me.ToolSensor.Size = New System.Drawing.Size(53, 49)
        Me.ToolSensor.TabIndex = 39
        Me.ToolTip.SetToolTip(Me.ToolSensor, "Sensor")
        Me.ToolSensor.toolType = MComponentTypes.ComponentTypesEnum.Sensor
        '
        'ToolActuator
        '
        Me.ToolActuator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolActuator.Image = CType(resources.GetObject("ToolActuator.Image"), System.Drawing.Image)
        Me.ToolActuator.Location = New System.Drawing.Point(212, 4)
        Me.ToolActuator.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolActuator.Name = "ToolActuator"
        Me.ToolActuator.Size = New System.Drawing.Size(53, 49)
        Me.ToolActuator.TabIndex = 38
        Me.ToolTip.SetToolTip(Me.ToolActuator, "Actuator")
        Me.ToolActuator.toolType = MComponentTypes.ComponentTypesEnum.Actuator
        '
        'ToolTimer
        '
        Me.ToolTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolTimer.Image = CType(resources.GetObject("ToolTimer.Image"), System.Drawing.Image)
        Me.ToolTimer.Location = New System.Drawing.Point(339, 4)
        Me.ToolTimer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolTimer.Name = "ToolTimer"
        Me.ToolTimer.Size = New System.Drawing.Size(53, 49)
        Me.ToolTimer.TabIndex = 37
        Me.ToolTip.SetToolTip(Me.ToolTimer, "Timer")
        Me.ToolTimer.toolType = MComponentTypes.ComponentTypesEnum.Timer
        '
        'ToolGUIWindow
        '
        Me.ToolGUIWindow.BackColor = System.Drawing.Color.Transparent
        Me.ToolGUIWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIWindow.Image = CType(resources.GetObject("ToolGUIWindow.Image"), System.Drawing.Image)
        Me.ToolGUIWindow.Location = New System.Drawing.Point(8, 226)
        Me.ToolGUIWindow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIWindow.Name = "ToolGUIWindow"
        Me.ToolGUIWindow.Size = New System.Drawing.Size(161, 188)
        Me.ToolGUIWindow.TabIndex = 36
        Me.ToolTip.SetToolTip(Me.ToolGUIWindow, "Bridge operator window")
        Me.ToolGUIWindow.toolType = MComponentTypes.ComponentTypesEnum.GUIBridgeWindow
        '
        'ToolGUIStopSign
        '
        Me.ToolGUIStopSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIStopSign.Image = CType(resources.GetObject("ToolGUIStopSign.Image"), System.Drawing.Image)
        Me.ToolGUIStopSign.Location = New System.Drawing.Point(181, 336)
        Me.ToolGUIStopSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIStopSign.Name = "ToolGUIStopSign"
        Me.ToolGUIStopSign.Size = New System.Drawing.Size(53, 49)
        Me.ToolGUIStopSign.TabIndex = 35
        Me.ToolTip.SetToolTip(Me.ToolGUIStopSign, "GUI Land Traffic Signs")
        Me.ToolGUIStopSign.toolType = MComponentTypes.ComponentTypesEnum.GUIStopSign
        '
        'ToolGUIBoomBarrier
        '
        Me.ToolGUIBoomBarrier.BackColor = System.Drawing.Color.Transparent
        Me.ToolGUIBoomBarrier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIBoomBarrier.Image = CType(resources.GetObject("ToolGUIBoomBarrier.Image"), System.Drawing.Image)
        Me.ToolGUIBoomBarrier.Location = New System.Drawing.Point(8, 415)
        Me.ToolGUIBoomBarrier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIBoomBarrier.Name = "ToolGUIBoomBarrier"
        Me.ToolGUIBoomBarrier.Size = New System.Drawing.Size(196, 48)
        Me.ToolGUIBoomBarrier.TabIndex = 34
        Me.ToolTip.SetToolTip(Me.ToolGUIBoomBarrier, "GUI Boom Barrier")
        Me.ToolGUIBoomBarrier.toolType = MComponentTypes.ComponentTypesEnum.GUIBoomBarrier
        '
        'ToolGUIRotatingBridge
        '
        Me.ToolGUIRotatingBridge.BackColor = System.Drawing.Color.Transparent
        Me.ToolGUIRotatingBridge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIRotatingBridge.Image = CType(resources.GetObject("ToolGUIRotatingBridge.Image"), System.Drawing.Image)
        Me.ToolGUIRotatingBridge.Location = New System.Drawing.Point(304, 226)
        Me.ToolGUIRotatingBridge.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIRotatingBridge.Name = "ToolGUIRotatingBridge"
        Me.ToolGUIRotatingBridge.Size = New System.Drawing.Size(93, 188)
        Me.ToolGUIRotatingBridge.TabIndex = 33
        Me.ToolTip.SetToolTip(Me.ToolGUIRotatingBridge, "GUI Rotating Bridge")
        Me.ToolGUIRotatingBridge.toolType = MComponentTypes.ComponentTypesEnum.GUIRotatingBridge
        '
        'ToolGUIOutgoingTrafficSign
        '
        Me.ToolGUIOutgoingTrafficSign.BackColor = System.Drawing.Color.Transparent
        Me.ToolGUIOutgoingTrafficSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIOutgoingTrafficSign.Image = CType(resources.GetObject("ToolGUIOutgoingTrafficSign.Image"), System.Drawing.Image)
        Me.ToolGUIOutgoingTrafficSign.Location = New System.Drawing.Point(243, 228)
        Me.ToolGUIOutgoingTrafficSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIOutgoingTrafficSign.Name = "ToolGUIOutgoingTrafficSign"
        Me.ToolGUIOutgoingTrafficSign.Size = New System.Drawing.Size(53, 92)
        Me.ToolGUIOutgoingTrafficSign.TabIndex = 32
        Me.ToolTip.SetToolTip(Me.ToolGUIOutgoingTrafficSign, "GUI Leaving Traffic Sign")
        Me.ToolGUIOutgoingTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.GUILeavingTrafficSign
        '
        'ToolGUIEnteringTrafficSign
        '
        Me.ToolGUIEnteringTrafficSign.BackColor = System.Drawing.Color.Transparent
        Me.ToolGUIEnteringTrafficSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolGUIEnteringTrafficSign.Image = CType(resources.GetObject("ToolGUIEnteringTrafficSign.Image"), System.Drawing.Image)
        Me.ToolGUIEnteringTrafficSign.Location = New System.Drawing.Point(181, 228)
        Me.ToolGUIEnteringTrafficSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolGUIEnteringTrafficSign.Name = "ToolGUIEnteringTrafficSign"
        Me.ToolGUIEnteringTrafficSign.Size = New System.Drawing.Size(53, 92)
        Me.ToolGUIEnteringTrafficSign.TabIndex = 31
        Me.ToolTip.SetToolTip(Me.ToolGUIEnteringTrafficSign, "GUI Entering Traffic Sign")
        Me.ToolGUIEnteringTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.GUIEnteringTrafficSign
        '
        'ToolRotatingBridge
        '
        Me.ToolRotatingBridge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolRotatingBridge.Image = CType(resources.GetObject("ToolRotatingBridge.Image"), System.Drawing.Image)
        Me.ToolRotatingBridge.Location = New System.Drawing.Point(131, 117)
        Me.ToolRotatingBridge.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolRotatingBridge.Name = "ToolRotatingBridge"
        Me.ToolRotatingBridge.Size = New System.Drawing.Size(200, 47)
        Me.ToolRotatingBridge.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.ToolRotatingBridge, "Rotating Bridge")
        Me.ToolRotatingBridge.toolType = MComponentTypes.ComponentTypesEnum.RotatingBridge
        '
        'ToolLeavingTrafficSign
        '
        Me.ToolLeavingTrafficSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolLeavingTrafficSign.Image = CType(resources.GetObject("ToolLeavingTrafficSign.Image"), System.Drawing.Image)
        Me.ToolLeavingTrafficSign.Location = New System.Drawing.Point(69, 117)
        Me.ToolLeavingTrafficSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolLeavingTrafficSign.Name = "ToolLeavingTrafficSign"
        Me.ToolLeavingTrafficSign.Size = New System.Drawing.Size(53, 92)
        Me.ToolLeavingTrafficSign.TabIndex = 29
        Me.ToolTip.SetToolTip(Me.ToolLeavingTrafficSign, "Leaving Traffic Sign")
        Me.ToolLeavingTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.LeavingTrafficSign
        '
        'ToolEnteringTrafficSign
        '
        Me.ToolEnteringTrafficSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolEnteringTrafficSign.Image = CType(resources.GetObject("ToolEnteringTrafficSign.Image"), System.Drawing.Image)
        Me.ToolEnteringTrafficSign.Location = New System.Drawing.Point(8, 117)
        Me.ToolEnteringTrafficSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolEnteringTrafficSign.Name = "ToolEnteringTrafficSign"
        Me.ToolEnteringTrafficSign.Size = New System.Drawing.Size(53, 92)
        Me.ToolEnteringTrafficSign.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.ToolEnteringTrafficSign, "Entering Traffic Sign")
        Me.ToolEnteringTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.EnteringTrafficSign
        '
        'ToolBoomBarrier
        '
        Me.ToolBoomBarrier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolBoomBarrier.Image = CType(resources.GetObject("ToolBoomBarrier.Image"), System.Drawing.Image)
        Me.ToolBoomBarrier.Location = New System.Drawing.Point(8, 68)
        Me.ToolBoomBarrier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolBoomBarrier.Name = "ToolBoomBarrier"
        Me.ToolBoomBarrier.Size = New System.Drawing.Size(253, 49)
        Me.ToolBoomBarrier.TabIndex = 27
        Me.ToolTip.SetToolTip(Me.ToolBoomBarrier, "Boom Barrier")
        Me.ToolBoomBarrier.toolType = MComponentTypes.ComponentTypesEnum.BoomBarrier
        '
        'ToolQuay
        '
        Me.ToolQuay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolQuay.Image = CType(resources.GetObject("ToolQuay.Image"), System.Drawing.Image)
        Me.ToolQuay.Location = New System.Drawing.Point(8, 68)
        Me.ToolQuay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolQuay.Name = "ToolQuay"
        Me.ToolQuay.Size = New System.Drawing.Size(150, 50)
        Me.ToolQuay.TabIndex = 69
        Me.ToolTip.SetToolTip(Me.ToolQuay, "Quay")
        Me.ToolQuay.toolType = MComponentTypes.ComponentTypesEnum.Quay
        '
        'ToolApproachSign
        '
        Me.ToolApproachSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolApproachSign.Image = CType(resources.GetObject("ToolApproachSign.Image"), System.Drawing.Image)
        Me.ToolApproachSign.Location = New System.Drawing.Point(69, 4)
        Me.ToolApproachSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolApproachSign.Name = "ToolApproachSign"
        Me.ToolApproachSign.Size = New System.Drawing.Size(53, 49)
        Me.ToolApproachSign.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.ToolApproachSign, "Approach Sign")
        Me.ToolApproachSign.toolType = MComponentTypes.ComponentTypesEnum.ApproachSign
        '
        'ToolStopSign
        '
        Me.ToolStopSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolStopSign.Image = CType(resources.GetObject("ToolStopSign.Image"), System.Drawing.Image)
        Me.ToolStopSign.Location = New System.Drawing.Point(8, 4)
        Me.ToolStopSign.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolStopSign.Name = "ToolStopSign"
        Me.ToolStopSign.Size = New System.Drawing.Size(53, 49)
        Me.ToolStopSign.TabIndex = 25
        Me.ToolTip.SetToolTip(Me.ToolStopSign, "Stop Sign")
        Me.ToolStopSign.toolType = MComponentTypes.ComponentTypesEnum.StopSign
        '
        'TabDrawings
        '
        Me.TabDrawings.BackColor = System.Drawing.SystemColors.Control
        Me.TabDrawings.Controls.Add(Me.LblTextLabel)
        Me.TabDrawings.Controls.Add(Me.LblSquare)
        Me.TabDrawings.Controls.Add(Me.pnlColors)
        Me.TabDrawings.Location = New System.Drawing.Point(4, 25)
        Me.TabDrawings.Margin = New System.Windows.Forms.Padding(4)
        Me.TabDrawings.Name = "TabDrawings"
        Me.TabDrawings.Padding = New System.Windows.Forms.Padding(4)
        Me.TabDrawings.Size = New System.Drawing.Size(483, 339)
        Me.TabDrawings.TabIndex = 1
        Me.TabDrawings.Text = "Drawing"
        '
        'LblTextLabel
        '
        Me.LblTextLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTextLabel.Image = CType(resources.GetObject("LblTextLabel.Image"), System.Drawing.Image)
        Me.LblTextLabel.Location = New System.Drawing.Point(72, 18)
        Me.LblTextLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTextLabel.Name = "LblTextLabel"
        Me.LblTextLabel.Size = New System.Drawing.Size(53, 49)
        Me.LblTextLabel.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.LblTextLabel, "Label")
        Me.LblTextLabel.toolType = MComponentTypes.ComponentTypesEnum.TextLabel
        '
        'LblSquare
        '
        Me.LblSquare.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSquare.Image = CType(resources.GetObject("LblSquare.Image"), System.Drawing.Image)
        Me.LblSquare.Location = New System.Drawing.Point(11, 18)
        Me.LblSquare.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblSquare.Name = "LblSquare"
        Me.LblSquare.Size = New System.Drawing.Size(53, 49)
        Me.LblSquare.TabIndex = 27
        Me.LblSquare.toolType = MComponentTypes.ComponentTypesEnum.Square
        '
        'pnlColors
        '
        Me.pnlColors.Controls.Add(Me.lblColorWhite)
        Me.pnlColors.Controls.Add(Me.lblColorWeg)
        Me.pnlColors.Controls.Add(Me.LblColorWaterweg)
        Me.pnlColors.Location = New System.Drawing.Point(11, 273)
        Me.pnlColors.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlColors.Name = "pnlColors"
        Me.pnlColors.Size = New System.Drawing.Size(461, 52)
        Me.pnlColors.TabIndex = 23
        '
        'lblColorWhite
        '
        Me.lblColorWhite.BackColor = System.Drawing.Color.White
        Me.lblColorWhite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColorWhite.Location = New System.Drawing.Point(81, 12)
        Me.lblColorWhite.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblColorWhite.Name = "lblColorWhite"
        Me.lblColorWhite.Size = New System.Drawing.Size(31, 28)
        Me.lblColorWhite.TabIndex = 2
        '
        'lblColorWeg
        '
        Me.lblColorWeg.BackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.lblColorWeg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColorWeg.Location = New System.Drawing.Point(43, 12)
        Me.lblColorWeg.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblColorWeg.Name = "lblColorWeg"
        Me.lblColorWeg.Size = New System.Drawing.Size(31, 28)
        Me.lblColorWeg.TabIndex = 1
        '
        'LblColorWaterweg
        '
        Me.LblColorWaterweg.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.LblColorWaterweg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblColorWaterweg.Location = New System.Drawing.Point(4, 12)
        Me.LblColorWaterweg.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblColorWaterweg.Name = "LblColorWaterweg"
        Me.LblColorWaterweg.Size = New System.Drawing.Size(31, 28)
        Me.LblColorWaterweg.TabIndex = 0
        '
        'DeleteRequirement
        '
        Me.DeleteRequirement.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.DeleteRequirement.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemDeleteRow})
        Me.DeleteRequirement.Name = "DeleteRow"
        Me.DeleteRequirement.Size = New System.Drawing.Size(208, 28)
        '
        'MenuItemDeleteRow
        '
        Me.MenuItemDeleteRow.Name = "MenuItemDeleteRow"
        Me.MenuItemDeleteRow.Size = New System.Drawing.Size(207, 24)
        Me.MenuItemDeleteRow.Text = "Delete requirement"
        '
        'SaveFileDialogJSON
        '
        Me.SaveFileDialogJSON.DefaultExt = "json"
        Me.SaveFileDialogJSON.FileName = "Configuration"
        Me.SaveFileDialogJSON.Filter = "JSON file (*.json)|*.json"
        '
        'TabPage3
        '
        Me.TabPage3.AutoScroll = True
        Me.TabPage3.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.TabPage3.Controls.Add(Me.canvasPlant)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage3.Size = New System.Drawing.Size(1339, 983)
        Me.TabPage3.TabIndex = 0
        '
        'canvasPlant
        '
        Me.canvasPlant.BackColor = System.Drawing.Color.White
        Me.canvasPlant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.canvasPlant.Location = New System.Drawing.Point(5, 5)
        Me.canvasPlant.Margin = New System.Windows.Forms.Padding(4)
        Me.canvasPlant.Name = "canvasPlant"
        Me.canvasPlant.Size = New System.Drawing.Size(1199, 971)
        Me.canvasPlant.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 33)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1347, 1012)
        Me.TabControl1.TabIndex = 22
        '
        'CToolBindingSource
        '
        Me.CToolBindingSource.DataSource = GetType(CTool)
        '
        'ISSDT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1812, 1055)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.DGVoverview)
        Me.Controls.Add(Me.btRefresh)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "ISSDT"
        Me.Text = "JSON Tool For Digital Twin Creation"
        CType(Me.DGVoverview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.TabTools.ResumeLayout(False)
        Me.TabDrawings.ResumeLayout(False)
        Me.pnlColors.ResumeLayout(False)
        Me.DeleteRequirement.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        CType(Me.CToolBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SaveFileDialog As SaveFileDialog
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents DGVoverview As DataGridView
    Friend WithEvents btRefresh As Button
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Generate As ToolStripMenuItem
    Friend WithEvents GenerateSvgImage As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents TabControl As TabControl
    Friend WithEvents TabTools As TabPage
    Friend WithEvents TabDrawings As TabPage
    Friend WithEvents pnlColors As Panel
    Friend WithEvents lblColorWeg As Label
    Friend WithEvents LblColorWaterweg As Label
    Friend WithEvents lblColorWhite As Label
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents DeleteRequirement As ContextMenuStrip
    Friend WithEvents MenuItemDeleteRow As ToolStripMenuItem
    Friend WithEvents ToolStopSign As CTool
    Friend WithEvents ToolApproachSign As CTool
    Friend WithEvents ToolBoomBarrier As CTool
    Friend WithEvents ToolQuay As CTool
    Friend WithEvents ToolEnteringTrafficSign As CTool
    Friend WithEvents ToolGUIEnteringTrafficSign As CTool
    Friend WithEvents ToolRotatingBridge As CTool
    Friend WithEvents ToolLeavingTrafficSign As CTool
    Friend WithEvents LblSquare As CTool
    Friend WithEvents ToolGUIOutgoingTrafficSign As CTool
    Friend WithEvents ToolGUIRotatingBridge As CTool
    Friend WithEvents ToolGUIBoomBarrier As CTool
    Friend WithEvents ToolGUIStopSign As CTool
    Friend WithEvents ToolGUIWindow As CTool
    Friend WithEvents CToolBindingSource As BindingSource
    Friend WithEvents ToolTimer As CTool
    Friend WithEvents ToolSensor As CTool
    Friend WithEvents ToolActuator As CTool
    Friend WithEvents CTool1 As CTool
    Friend WithEvents CTool2 As CTool
    Friend WithEvents CTool3 As CTool
    Friend WithEvents LblTextLabel As CTool
    Friend WithEvents SaveFileDialogJSON As SaveFileDialog
    Friend WithEvents Quay As CTool
    Friend WithEvents Waterway As DataGridViewComboBoxColumn
    Friend WithEvents ObjectName As DataGridViewTextBoxColumn
    Friend WithEvents ObjectType As DataGridViewTextBoxColumn
    Friend WithEvents ObjectID As DataGridViewTextBoxColumn
    Friend WithEvents GenerateJSON As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents canvasPlant As CCanvas
    Friend WithEvents TabControl1 As TabControl
End Class

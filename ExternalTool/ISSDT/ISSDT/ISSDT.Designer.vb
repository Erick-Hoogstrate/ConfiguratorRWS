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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.QuayTool = New CTool()
        Me.WaterTool = New CTool()
        Me.LockWallTool = New CTool()
        Me.MitreGateTool = New CTool()
        Me.TL_LeavingTool = New CTool()
        Me.TL_EnteringTool = New CTool()
        Me.CTool1 = New CTool()
        Me.ToolGUIOutgoingTrafficSign = New CTool()
        Me.ToolGUIEnteringTrafficSign = New CTool()
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVoverview.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVoverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVoverview.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Waterway, Me.ObjectName, Me.ObjectType, Me.ObjectID})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVoverview.DefaultCellStyle = DataGridViewCellStyle2
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
        Me.btRefresh.Location = New System.Drawing.Point(1354, 1012)
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
        Me.MenuStrip1.Size = New System.Drawing.Size(1870, 28)
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
        Me.TabControl.Size = New System.Drawing.Size(491, 388)
        Me.TabControl.TabIndex = 20
        '
        'TabTools
        '
        Me.TabTools.AutoScroll = True
        Me.TabTools.BackColor = System.Drawing.SystemColors.Control
        Me.TabTools.Controls.Add(Me.QuayTool)
        Me.TabTools.Controls.Add(Me.WaterTool)
        Me.TabTools.Controls.Add(Me.LockWallTool)
        Me.TabTools.Controls.Add(Me.MitreGateTool)
        Me.TabTools.Controls.Add(Me.TL_LeavingTool)
        Me.TabTools.Controls.Add(Me.TL_EnteringTool)
        Me.TabTools.Controls.Add(Me.CTool1)
        Me.TabTools.Controls.Add(Me.ToolGUIOutgoingTrafficSign)
        Me.TabTools.Controls.Add(Me.ToolGUIEnteringTrafficSign)
        Me.TabTools.Location = New System.Drawing.Point(4, 25)
        Me.TabTools.Margin = New System.Windows.Forms.Padding(4)
        Me.TabTools.Name = "TabTools"
        Me.TabTools.Padding = New System.Windows.Forms.Padding(4)
        Me.TabTools.Size = New System.Drawing.Size(483, 359)
        Me.TabTools.TabIndex = 0
        Me.TabTools.Text = "Components"
        '
        'QuayTool
        '
        Me.QuayTool.BackColor = System.Drawing.Color.Transparent
        Me.QuayTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.QuayTool.Image = CType(resources.GetObject("QuayTool.Image"), System.Drawing.Image)
        Me.QuayTool.Location = New System.Drawing.Point(192, 210)
        Me.QuayTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.QuayTool.Name = "QuayTool"
        Me.QuayTool.Size = New System.Drawing.Size(223, 48)
        Me.QuayTool.TabIndex = 38
        Me.ToolTip.SetToolTip(Me.QuayTool, "Quay")
        Me.QuayTool.toolType = MComponentTypes.ComponentTypesEnum.Quay
        '
        'WaterTool
        '
        Me.WaterTool.BackColor = System.Drawing.Color.Transparent
        Me.WaterTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WaterTool.Image = CType(resources.GetObject("WaterTool.Image"), System.Drawing.Image)
        Me.WaterTool.Location = New System.Drawing.Point(164, 97)
        Me.WaterTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.WaterTool.Name = "WaterTool"
        Me.WaterTool.Size = New System.Drawing.Size(265, 93)
        Me.WaterTool.TabIndex = 37
        Me.ToolTip.SetToolTip(Me.WaterTool, "Water")
        Me.WaterTool.toolType = MComponentTypes.ComponentTypesEnum.Water
        '
        'LockWallTool
        '
        Me.LockWallTool.BackColor = System.Drawing.Color.Transparent
        Me.LockWallTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LockWallTool.Image = CType(resources.GetObject("LockWallTool.Image"), System.Drawing.Image)
        Me.LockWallTool.Location = New System.Drawing.Point(175, 41)
        Me.LockWallTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LockWallTool.Name = "LockWallTool"
        Me.LockWallTool.Size = New System.Drawing.Size(96, 26)
        Me.LockWallTool.TabIndex = 36
        Me.ToolTip.SetToolTip(Me.LockWallTool, "Lock Wall")
        Me.LockWallTool.toolType = MComponentTypes.ComponentTypesEnum.LockWall
        '
        'MitreGateTool
        '
        Me.MitreGateTool.BackColor = System.Drawing.Color.Transparent
        Me.MitreGateTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MitreGateTool.Image = CType(resources.GetObject("MitreGateTool.Image"), System.Drawing.Image)
        Me.MitreGateTool.Location = New System.Drawing.Point(17, 121)
        Me.MitreGateTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MitreGateTool.Name = "MitreGateTool"
        Me.MitreGateTool.Size = New System.Drawing.Size(113, 151)
        Me.MitreGateTool.TabIndex = 35
        Me.ToolTip.SetToolTip(Me.MitreGateTool, "Mitre Gate")
        Me.MitreGateTool.toolType = MComponentTypes.ComponentTypesEnum.MitreGate
        '
        'TL_LeavingTool
        '
        Me.TL_LeavingTool.BackColor = System.Drawing.Color.Transparent
        Me.TL_LeavingTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TL_LeavingTool.Image = CType(resources.GetObject("TL_LeavingTool.Image"), System.Drawing.Image)
        Me.TL_LeavingTool.Location = New System.Drawing.Point(69, 4)
        Me.TL_LeavingTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TL_LeavingTool.Name = "TL_LeavingTool"
        Me.TL_LeavingTool.Size = New System.Drawing.Size(53, 92)
        Me.TL_LeavingTool.TabIndex = 34
        Me.ToolTip.SetToolTip(Me.TL_LeavingTool, "Leaving Traffic Sign")
        Me.TL_LeavingTool.toolType = MComponentTypes.ComponentTypesEnum.TL_Leaving
        '
        'TL_EnteringTool
        '
        Me.TL_EnteringTool.BackColor = System.Drawing.Color.Transparent
        Me.TL_EnteringTool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TL_EnteringTool.Image = CType(resources.GetObject("TL_EnteringTool.Image"), System.Drawing.Image)
        Me.TL_EnteringTool.Location = New System.Drawing.Point(8, 4)
        Me.TL_EnteringTool.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TL_EnteringTool.Name = "TL_EnteringTool"
        Me.TL_EnteringTool.Size = New System.Drawing.Size(53, 92)
        Me.TL_EnteringTool.TabIndex = 33
        Me.ToolTip.SetToolTip(Me.TL_EnteringTool, "Entering Traffic Sign" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.TL_EnteringTool.toolType = MComponentTypes.ComponentTypesEnum.TL_Entering
        '
        'CTool1
        '
        Me.CTool1.Location = New System.Drawing.Point(0, 0)
        Me.CTool1.Name = "CTool1"
        Me.CTool1.Size = New System.Drawing.Size(100, 23)
        Me.CTool1.TabIndex = 39
        Me.CTool1.toolType = MComponentTypes.ComponentTypesEnum.NoType
        '
        'ToolGUIOutgoingTrafficSign
        '
        Me.ToolGUIOutgoingTrafficSign.Location = New System.Drawing.Point(0, 0)
        Me.ToolGUIOutgoingTrafficSign.Name = "ToolGUIOutgoingTrafficSign"
        Me.ToolGUIOutgoingTrafficSign.Size = New System.Drawing.Size(100, 23)
        Me.ToolGUIOutgoingTrafficSign.TabIndex = 40
        Me.ToolGUIOutgoingTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.NoType
        '
        'ToolGUIEnteringTrafficSign
        '
        Me.ToolGUIEnteringTrafficSign.Location = New System.Drawing.Point(0, 0)
        Me.ToolGUIEnteringTrafficSign.Name = "ToolGUIEnteringTrafficSign"
        Me.ToolGUIEnteringTrafficSign.Size = New System.Drawing.Size(100, 23)
        Me.ToolGUIEnteringTrafficSign.TabIndex = 41
        Me.ToolGUIEnteringTrafficSign.toolType = MComponentTypes.ComponentTypesEnum.NoType
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
        Me.TabDrawings.Size = New System.Drawing.Size(483, 359)
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
        Me.canvasPlant.AutoScroll = True
        Me.canvasPlant.BackColor = System.Drawing.Color.White
        Me.canvasPlant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.canvasPlant.Location = New System.Drawing.Point(5, 5)
        Me.canvasPlant.Margin = New System.Windows.Forms.Padding(4)
        Me.canvasPlant.Name = "canvasPlant"
        Me.canvasPlant.Size = New System.Drawing.Size(1326, 970)
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
        Me.ClientSize = New System.Drawing.Size(1870, 1163)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.DGVoverview)
        Me.Controls.Add(Me.btRefresh)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "ISSDT"
        Me.Text = "JSON Tool For Digital Twin Creation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
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
    Friend WithEvents ToolGUIEnteringTrafficSign As CTool
    Friend WithEvents LblSquare As CTool
    Friend WithEvents ToolGUIOutgoingTrafficSign As CTool
    Friend WithEvents CToolBindingSource As BindingSource
    Friend WithEvents LblTextLabel As CTool
    Friend WithEvents SaveFileDialogJSON As SaveFileDialog
    Friend WithEvents Waterway As DataGridViewComboBoxColumn
    Friend WithEvents ObjectName As DataGridViewTextBoxColumn
    Friend WithEvents ObjectType As DataGridViewTextBoxColumn
    Friend WithEvents ObjectID As DataGridViewTextBoxColumn
    Friend WithEvents GenerateJSON As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents canvasPlant As CCanvas
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents CTool1 As CTool
    Friend WithEvents QuayTool As CTool
    Friend WithEvents WaterTool As CTool
    Friend WithEvents LockWallTool As CTool
    Friend WithEvents MitreGateTool As CTool
    Friend WithEvents TL_LeavingTool As CTool
    Friend WithEvents TL_EnteringTool As CTool
End Class

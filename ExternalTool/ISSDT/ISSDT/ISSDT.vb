Imports System.Security.Cryptography
Imports System.Text

Public Class ISSDT
    Public changedAfterSave As Boolean = False

    Public toolSelected As ComponentTypesEnum = ComponentTypesEnum.NoType
    Public colorSelected As Color = Color.White

    'Initialization at the start of the program.
    Private Sub Initialization() Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        Me.KeyPreview = True


        Dim rc1 As ResizeableControl = New ResizeableControl(canvasPlant)
        Dim rc2 As ResizeableControl = New ResizeableControl(canvasGUI)
        Dim rc3 As ResizeableControl = New ResizeableControl(TabControl1)
        Dim rc4 As ResizeableControl = New ResizeableControl(TabPage3)
        rbPlantModel.Checked = True

        MRequirements.Initialize(DGRequirements)
        MWriter.Initialize(canvasPlant, canvasGUI, DGRequirements)
        MFileOptions.Initialize(canvasPlant, canvasGUI, DGRequirements, changedAfterSave)
    End Sub


    Private Sub frmCustomerDetails_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Delete) Then
            ' When Delete is pressed, the component is deleted from the canvas.
            'deleteComponent()
        End If
    End Sub

    Private Sub lblColors_Click(sender As Object, e As EventArgs) Handles LblColorWaterweg.Click, lblColorWeg.Click, lblColorWhite.Click
        LblColorWaterweg.BorderStyle = BorderStyle.Fixed3D
        lblColorWeg.BorderStyle = BorderStyle.Fixed3D
        lblColorWhite.BorderStyle = BorderStyle.Fixed3D

        colorSelected = sender.BackColor
        sender.borderStyle = BorderStyle.FixedSingle
    End Sub

    ''' <summary>
    ''' This method collects the ccomponent data and fills the data grid.
    ''' </summary>
    Private Sub BtRefresh_Click() Handles btRefresh.Click
        Dim i = 1
        DGVoverview.Rows.Clear()

        For Each comp As CComponent In canvasPlant.Controls.OfType(Of CComponent)()
            If comp.Type <> ComponentTypesEnum.Square Then
                Dim row As String() = New String() {1, comp.Name, comp.Type.ToString, i}
                DGVoverview.Rows.Add(row)
                i += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' This method lets the user close the program. If the current canvas is not saved, a confirmation is requested.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmProgramma_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If changedAfterSave Then
            Select Case MsgBox("Do you want to save changes before closing?", MsgBoxStyle.YesNoCancel, "Confirm")
                Case MsgBoxResult.Yes
                    MFileOptions.SaveRoutine("")
                Case MsgBoxResult.Cancel
                    e.Cancel = True
                Case MsgBoxResult.No
            End Select
        End If
    End Sub

    'Temporary function for creating JSON data
    Private Sub JSONbtn_Click(sender As Object, e As EventArgs) Handles JSONbtn.Click

        MsgBox("Generate JSON file")
        MFileOptions.ExtractJSON("")

    End Sub

End Class

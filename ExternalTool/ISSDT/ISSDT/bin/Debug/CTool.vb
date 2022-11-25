Public Class CTool
    Inherits Label

    Private _toolType As ComponentTypesEnum

    <System.ComponentModel.Description("The component that this tool creates."), System.ComponentModel.Category("Misc")>
    Public Property toolType As ComponentTypesEnum
        Get
            Return _toolType
        End Get
        Set(value As ComponentTypesEnum)
            _toolType = value
        End Set
    End Property

    Private Sub Tool_Click(sender As Label, e As EventArgs) Handles Me.Click
        ISSDT.canvasPlant.ResetActive()
        ISSDT.canvasGUI.ResetActive()
        Deselect_all()

        If ISSDT.toolSelected = _toolType Then
            ISSDT.toolSelected = ComponentTypesEnum.NoType
            BorderStyle = BorderStyle.Fixed3D
        Else
            ISSDT.toolSelected = _toolType
            BorderStyle = BorderStyle.FixedSingle
        End If
    End Sub

    Private Sub Deselect_all()
        For Each tool As CTool In ISSDT.TabTools.Controls.OfType(Of CTool)().Concat(ISSDT.TabDrawings.Controls.OfType(Of CTool))
            tool.BorderStyle = BorderStyle.Fixed3D
        Next
    End Sub
End Class

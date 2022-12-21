Public Class CCanvas
    Inherits Panel

    Public startx As Integer
    Public starty As Integer
    Public endy As Integer
    Public endx As Integer
    Public mdown As Boolean
    Public valx As Boolean
    Public valy As Boolean
    Public activeComp As CComponent

    Public Sub HandleComponentMouseMove() Handles Me.MouseMove
        'Check if mouse=down
        If mdown = True And activeComp IsNot Nothing Then
            endx = (MousePosition.X - Me.Left)
            endy = (MousePosition.Y - Me.Top)

            If valy = False Then
                starty = endy - activeComp.Top
                valy = True
            End If
            If valx = False Then
                startx = endx - activeComp.Left
                valx = True
            End If
            activeComp.Left = endx - startx
            activeComp.Top = endy - starty
            ISSDT.changedAfterSave = True
        End If
    End Sub

    ''' <summary>
    ''' This method adds a component to the canvas when it is clicked when a component creation tool is active.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub pnlCanvas_Click(sender As Object, e As MouseEventArgs) Handles Me.Click
        ResetActive()

        If e.Button = MouseButtons.Left And ISSDT.toolSelected <> ComponentTypesEnum.NoType Then

            Dim comp As CComponent

            If ISSDT.toolSelected = ComponentTypesEnum.Square Then
                comp = New Square("drawing", ISSDT.toolSelected, New Point(e.X, e.Y), Me, New Size(40, 40), ISSDT.colorSelected)
                Controls.Add(comp)
                ISSDT.changedAfterSave = True
                comp.SetActive()
                Exit Sub
            End If

            If ISSDT.toolSelected = ComponentTypesEnum.TextLabel Then
                comp = New TextLabel(ISSDT.toolSelected, New Point(e.X, e.Y), Me)
                Controls.Add(comp)
                ISSDT.changedAfterSave = True
                comp.SetActive()
                Exit Sub
            End If


            Dim NewComponentName = getComponentName()
            If NewComponentName = "" Then
                Exit Sub
            End If

            If ISSDT.toolSelected = ComponentTypesEnum.MitreGate Then
                comp = New MitreGate(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            ElseIf ISSDT.toolSelected = ComponentTypesEnum.TL_Entering Then
                comp = New TL_Entering(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            ElseIf ISSDT.toolSelected = ComponentTypesEnum.TL_Leaving Then
                comp = New TL_Leaving(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            ElseIf ISSDT.toolSelected = ComponentTypesEnum.LockWall Then
                comp = New LockWall(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            ElseIf ISSDT.toolSelected = ComponentTypesEnum.Water Then
                comp = New Water(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            ElseIf ISSDT.toolSelected = ComponentTypesEnum.Quay Then
                comp = New Quay(NewComponentName, ISSDT.toolSelected, New Point(e.X, e.Y), Me)

            Else
                MsgBox("Error")
                Exit Sub
            End If

            Controls.Add(comp)
            ISSDT.changedAfterSave = True
            comp.SetActive()
        End If
    End Sub

    ''' <summary>
    ''' This method resets the active component.
    ''' </summary>
    Public Sub ResetActive()
        If activeComp Is Nothing Then
            Exit Sub
        End If

        activeComp.ResetActive()
        activeComp = Nothing
    End Sub

    ''' <summary>
    ''' This method resets the active component.
    ''' </summary>
    Public Sub ResetActive(comp As CComponent)
        If activeComp IsNot comp Then
            Exit Sub
        End If

        activeComp = Nothing
    End Sub

    ''' <summary>
    ''' Sets a new active component and resets the old active component, if there is one.
    ''' </summary>
    ''' <param name="comp"></param>
    Public Sub SetActive(comp As CComponent)
        If activeComp Is Nothing Then
            activeComp = comp
        ElseIf activeComp Is comp Then
            Exit Sub
        Else
            activeComp.ResetActive()
            activeComp = comp
        End If
    End Sub
End Class

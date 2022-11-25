Module MRequirements
    Dim rowIndex As Integer
    Dim requirements As DataGridView

    Sub Initialize(ByRef newRequirements As DataGridView)
        requirements = newRequirements

        AddHandler requirements.CellMouseUp, AddressOf DGRequirements_CellMouseUp
        AddHandler ISSDT.MenuItemDeleteRow.Click, AddressOf MenuItemDeleteRow_Click
        AddHandler ISSDT.BtnAnd.Click, AddressOf ButtonAnd_Click
        AddHandler ISSDT.btnOr.Click, AddressOf ButtonOr_Click
        AddHandler ISSDT.btnNot.Click, AddressOf ButtonNot_Click
        AddHandler ISSDT.btnLeftBracket.Click, AddressOf ButtonOpenBracket_Click
        AddHandler ISSDT.btnRightBracket.Click, AddressOf ButtonCloseBracket_Click
        AddHandler ISSDT.btnRemove.Click, AddressOf ButtonRemove_Click
        AddHandler ISSDT.btnVerify.Click, AddressOf ButtonVerify_Click

        AddHandler requirements.KeyDown, AddressOf DGRequirements_KeyDown
    End Sub

    ''' <summary>
    ''' The elements that can occur in a condition.
    ''' </summary>
    Public Enum ConditionElementEnum
        State
        LogicAnd
        LogicOr
        LogicNot
        OpenBracket
        CloseBracket
        Empty
    End Enum

    Public Sub DGRequirements_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.A Then
            ButtonAnd_Click()
        ElseIf e.KeyCode = Keys.O Then
            ButtonOr_Click()
        ElseIf e.KeyCode = Keys.N Then
            ButtonNot_Click()
        ElseIf e.KeyCode = Keys.OemOpenBrackets Then
            ButtonOpenBracket_Click()
        ElseIf e.KeyCode = Keys.OemCloseBrackets Then
            ButtonCloseBracket_Click()
        ElseIf e.KeyCode = Keys.Back Then
            ButtonRemove_Click()
        End If
    End Sub

    Public Sub AddRequirement(eventName As String)
        Dim requirement As String() = New String() {eventName, "needs", "", False}
        requirements.Rows.Add(requirement)
    End Sub

    Public Sub AddRequirement(eventName As String, reqType As String, condition As String, safety As String)
        Dim requirement As String() = New String() {eventName, reqType, condition, safety}
        requirements.Rows.Add(requirement)
    End Sub

    Private Sub DGRequirements_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.Button = MouseButtons.Right And e.RowIndex > -1 Then
            rowIndex = e.RowIndex
            requirements.Rows(rowIndex).Selected = True
            requirements.CurrentCell = requirements.Rows(rowIndex).Cells(1)
            ISSDT.DeleteRequirement.Show(Cursor.Position)
        End If
    End Sub

    ''' <summary>
    ''' Implements the delete row function.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuItemDeleteRow_Click(sender As Object, e As EventArgs)
        requirements.Rows.RemoveAt(rowIndex)
    End Sub

    ''' <summary>
    ''' Adds an 'and' as the last element of the selected requirement.
    ''' </summary>
    Private Sub ButtonAnd_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)
            If (element = ConditionElementEnum.State Or
                element = ConditionElementEnum.CloseBracket) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + "and "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds an 'or' as the last element of the selected requirement.
    ''' </summary>
    Private Sub ButtonOr_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)
            If (element = ConditionElementEnum.State Or
                element = ConditionElementEnum.CloseBracket) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + "or "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds a 'not' as the last element of the selected requirement.
    ''' </summary>
    Private Sub ButtonNot_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)
            If (element = ConditionElementEnum.LogicAnd Or
                element = ConditionElementEnum.LogicOr Or
                element = ConditionElementEnum.LogicNot Or
                element = ConditionElementEnum.OpenBracket Or
                element = ConditionElementEnum.Empty) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + "not "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds an open bracket as the last element of the selected requirement.
    ''' </summary>
    Private Sub ButtonOpenBracket_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)
            If (element = ConditionElementEnum.LogicAnd Or
                element = ConditionElementEnum.LogicOr Or
                element = ConditionElementEnum.LogicNot Or
                element = ConditionElementEnum.Empty) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + "("
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds a closing bracket as the last element of the selected requirement.
    ''' </summary>
    Private Sub ButtonCloseBracket_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)

            If getUnfinishedOpenBrackets(condition) > 0 And
                (element = ConditionElementEnum.State Or
                 element = ConditionElementEnum.CloseBracket) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value.ToString.TrimEnd(" ")
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + ") "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds a state as the last element of the selected requirement.
    ''' </summary>
    ''' <param name="state">The state to add.</param>
    Public Sub ButtonState_Click(state As String)
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = MRequirements.getLastConditionElement(condition)
            If (element <> ConditionElementEnum.State And
                element <> ConditionElementEnum.CloseBracket) Then
                requirements.CurrentRow.Cells(2).Value = requirements.CurrentRow.Cells(2).Value + state + " "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Implements the remove function button. It removes the last element in the requirement of the selected row.
    ''' </summary>
    Private Sub ButtonRemove_Click()
        If requirements.CurrentRow IsNot Nothing Then
            Dim condition As String = requirements.CurrentRow.Cells(2).Value
            Dim element As ConditionElementEnum = getLastConditionElement(condition)
            If element = ConditionElementEnum.OpenBracket Then
                condition = condition.Substring(0, condition.Length - 1)

            ElseIf element = ConditionElementEnum.LogicAnd Then
                condition = condition.Substring(0, condition.Length - 4)

            ElseIf element = ConditionElementEnum.LogicNot Then
                condition = condition.Substring(0, condition.Length - 4)

            ElseIf element = ConditionElementEnum.LogicOr Then
                condition = condition.Substring(0, condition.Length - 3)

            ElseIf element = ConditionElementEnum.CloseBracket Then
                condition = condition.Substring(0, condition.Length - 2)

            ElseIf element = ConditionElementEnum.State Then
                condition = condition.Trim
                Dim lastIndex As Integer = Math.Max(condition.LastIndexOf(" "), condition.LastIndexOf("("))
                If lastIndex = -1 Then
                    condition = ""
                Else
                    condition = condition.Substring(0, lastIndex + 1)
                End If
            End If

            requirements.CurrentRow.Cells(2).Value = condition
        End If
    End Sub

    ''' <summary>
    ''' Implements the verification function button. It checks for each requirements the validity of the state names and event names.
    ''' </summary>
    Private Sub ButtonVerify_Click()
        Dim validStates As List(Of String) = GetAllValidStates()
        Dim validEvents As List(Of String) = GetAllValidEvents()
        validStates.Add("")

        Dim expressionStates As New List(Of String)

        For Each requirement As DataGridViewRow In requirements.Rows
            'Check validity of the states in the expression.
            expressionStates = GetAllExpressionStates(requirement.Cells(2).Value)
            If Not VerifyStates(validStates, expressionStates) Then
                requirement.Cells(2).Style.BackColor = Color.Red
            Else
                If requirement.Cells(2).Style.BackColor = Color.Red Then
                    requirement.Cells(2).Style.BackColor = Color.White
                End If
            End If

            'Check validity of the event.
            If Not validEvents.Contains(requirement.Cells(0).Value) Then
                requirement.Cells(0).Style.BackColor = Color.Red
            Else
                If requirement.Cells(0).Style.BackColor = Color.Red Then
                    requirement.Cells(0).Style.BackColor = Color.White
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Returns the last elements in a conditions (e.g., and, or, bracket, state).
    ''' </summary>
    ''' <param name="condition">The conditions in which to find the last element.</param>
    ''' <returns>The last element in the conditions.</returns>
    Public Function getLastConditionElement(condition As String) As ConditionElementEnum
        If Strings.Len(condition) = 0 Then
            Return ConditionElementEnum.Empty
        ElseIf Strings.Right(condition, 2) = ") " Then
            Return ConditionElementEnum.CloseBracket
        ElseIf Strings.Right(condition, 1) = "(" Then
            Return ConditionElementEnum.OpenBracket
        ElseIf Strings.Right(condition, 4) = "and " Then
            Return ConditionElementEnum.LogicAnd
        ElseIf Strings.Right(condition, 4) = "not " Then
            Return ConditionElementEnum.LogicNot
        ElseIf Strings.Right(condition, 3) = "or " Then
            Return ConditionElementEnum.LogicOr
        Else Return ConditionElementEnum.State
        End If
    End Function

    ''' <summary>
    ''' Counts the number of open brackets that have not been closed.
    ''' </summary>
    ''' <param name="condition">The conditions in which to find the brackets.</param>
    ''' <returns>The number of open brackets that have not been closed.</returns>
    Private Function getUnfinishedOpenBrackets(condition As String) As Integer
        Return (CountCharacter(condition, "(") - CountCharacter(condition, ")"))
    End Function

    ''' <summary>
    ''' Counts the number of occurences of a character in a string.
    ''' </summary>
    ''' <param name="word">The string in which to count the character.</param>
    ''' <param name="ch">The character to count.</param>
    ''' <returns>The number of occurences of a character in a string.</returns>
    Public Function CountCharacter(ByVal word As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In word
            If c = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function

    ''' <summary>
    ''' A list of all states for all components on the canvas.
    ''' </summary>
    ''' <returns>A list of all states that can be refered to in requirements.</returns>
    Public Function GetAllValidStates() As List(Of String)
        Dim states As New List(Of String)
        For Each comp As CComponent In ISSDT.canvasPlant.Controls.OfType(Of CComponent).Concat(ISSDT.canvasGUI.Controls.OfType(Of CComponent))
            Dim compStates As List(Of String) = comp.getStates()
            For Each compState As String In compStates
                states.Add(comp.Name + "." + compState)
            Next
        Next
        Return states
    End Function

    ''' <summary>
    ''' A list of all events for all components on the canvas.
    ''' </summary>
    ''' <returns>A list of all events that can be refered used to specify requirements for.</returns>
    Public Function GetAllValidEvents() As List(Of String)
        Dim events As New List(Of String)
        For Each comp As CComponent In ISSDT.canvasPlant.Controls.OfType(Of CComponent).Concat(ISSDT.canvasGUI.Controls.OfType(Of CComponent))
            Dim compEvents As List(Of String) = comp.getEvents()
            For Each compEvent As String In compEvents
                events.Add(comp.Name + "." + compEvent)
            Next
        Next
        Return events
    End Function

    ''' <summary>
    ''' Returns a list of states that is used in an expression.
    ''' </summary>
    ''' <param name="expression">The expression for which to return the states.</param>
    ''' <returns>The list of states used in an expression.</returns>
    Public Function GetAllExpressionStates(expression As String) As List(Of String)
        expression = expression.Replace(")", " ")
        expression = expression.Replace("(", " ")
        expression = expression.Replace("not ", "")
        expression = expression.Replace(" and ", " ")
        expression = expression.Replace(" or ", " ")

        Dim states As String() = Split(expression, " ")
        Dim stateCollection As New List(Of String)

        For Each state As String In states
            stateCollection.Add(state)
        Next

        Return stateCollection
    End Function

    ''' <summary>
    ''' Checks whether all elements of a list are contained within another list.
    ''' </summary>
    ''' <param name="ValidStates">A list of valid states.</param>
    ''' <param name="ExpressionStates">A list of states to be checked.</param>
    ''' <returns>True if all expression states are contained in the valid states.</returns>
    Public Function VerifyStates(ValidStates As List(Of String), ExpressionStates As List(Of String)) As Boolean
        For Each expressionState As String In ExpressionStates
            If Not ValidStates.Contains(expressionState) Then
                Return False
            End If
        Next
        Return True
    End Function
End Module

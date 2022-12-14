Module FComponentName
    ''' <summary>
    ''' Asks the user for a component name and checks its validity.
    ''' </summary>
    ''' <returns>Valid component name</returns>
    Public Function getComponentName() As String
        Dim strAllowedChars As String = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_"
        Dim strAllowedStartingChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_"
        Dim name As String = InputBox("Component Name?", "Name", , ,)
        name = Trim(name)

        'Name must be at least 1 character.
        If name.Length = 0 Then
            Return ""
        End If

        'Name cannot start with a digit.
        If InStr(1, strAllowedStartingChars, name(0)) = 0 Then
            MsgBox("Invalid component name.")
            Return getComponentName()
        End If

        'Only a set of characters is allowed.
        For i = 1 To Len(name) - 1
            If InStr(1, strAllowedChars, name(i)) = 0 Then
                MsgBox("Invalid component name.")
                Return getComponentName()
            End If
        Next

        'Names must be unique. 
        For Each component As CComponent In ISSDT.canvasPlant.Controls.OfType(Of CComponent)()
            If name.Equals(component.Name) Then
                MsgBox("Name already exists.")
                Return getComponentName()
            End If
        Next
        Return name
    End Function
End Module

Module Expressions
    Public Enum BinaryOperator
        DISJUNCTION
        CONJUNCTION
    End Enum

    Public Enum UnaryOperator
        NEGATION
    End Enum

    Class StateVariable
        Dim name As String

        Event NameChanged()

        Function getName() As String
            Return name
        End Function

        Sub setName(newName As String)
            name = newName
            RaiseEvent NameChanged()
        End Sub
    End Class

    Class Expression

    End Class

    Class BinaryExpression : Inherits Expression
        Dim op As BinaryOperator
        Dim left As Expression
        Dim right As Expression

        Public Function getOperator() As BinaryOperator
            Return op
        End Function

        Public Sub setOperator(newOperator As BinaryOperator)
            op = newOperator
        End Sub

        Public Function getLeft() As Expression
            Return left
        End Function

        Public Sub setLeft(ByRef newLeft As Expression)
            left = newLeft
        End Sub

        Public Function getRight() As Expression
            Return right
        End Function

        Public Sub setRight(ByRef newRight As Expression)
            right = newRight
        End Sub
    End Class

    Class UnaryExpression : Inherits Expression
        Dim op
        Dim child As Expression

        Public Function getOperator() As UnaryOperator
            Return op
        End Function

        Public Sub setOperator(newOperator As UnaryOperator)
            op = newOperator
        End Sub

        Public Function getChild() As Expression
            Return child
        End Function

        Public Sub setChild(ByRef newChild As Expression)
            child = newChild
        End Sub
    End Class

    Class BoolExpression : Inherits Expression
        Dim value As Boolean

        Public Function isValue() As Boolean
            Return value
        End Function

        Public Sub setValue(newValue As Boolean)
            value = newValue
        End Sub
    End Class

    Class VariableExpression : Inherits Expression
        Dim variable As StateVariable

        Public Function getVariable() As StateVariable
            Return variable
        End Function

        Public Sub setVariable(newVariable As StateVariable)
            variable = newVariable
        End Sub
    End Class
End Module

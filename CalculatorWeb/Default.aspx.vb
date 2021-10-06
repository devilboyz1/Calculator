Imports Calculator
Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Dim strInput As String = txtInput.Text
        Dim dblResult As Double
        Dim strResult As String = ""
        'Try
        txtResult.ForeColor = Drawing.Color.Black
            Dim calc As New Calculator.Calculator()

#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            dblResult = calc.Calculate(strInput)
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

            strResult = dblResult.ToString()
        'Catch ex As Exception
        'txtResult.ForeColor = Drawing.Color.Red
        'strResult = ex.Message
        'Finally
        txtResult.Text = strResult
        'End Try

    End Sub

End Class
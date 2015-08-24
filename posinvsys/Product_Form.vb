Public Class Product_Form

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = Format(DateTimePicker1.Value, "yyyy-MM-dd")

    End Sub

    Private Sub Product_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
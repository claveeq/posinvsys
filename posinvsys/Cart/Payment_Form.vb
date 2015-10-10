Imports MySql.Data.MySqlClient
Public Class Payment_Form
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        TextBox1.Clear()
    End Sub     'MySQL


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim change As Double
        change = Val(TextBox1.Text) - Val(Label4.Text)
        Label6.Text = change
    End Sub

    Private Sub Payment_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label4.Text = Checkout_Form.Label4.Text
        TextBox1.Text = Checkout_Form.Label4.Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Checkout_Form.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label4.TextChanged
        Dim change As Double
        change = Val(TextBox1.Text) - Val(Label4.Text)
        Label6.Text = change
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "Insert into rmarquez.receipt (rec_date,rec_total,rec_cash,rec_change,rec_cog) values (DATE_FORMAT(NOW(),'%Y-%m-%d %H:%i:%s'),'" & Label4.Text & "','" & TextBox1.Text & "','" & Label6.Text & "','" & Checkout_Form.supply & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            receipt.Show()
            Me.Hide()
        End Try
    End Sub
End Class
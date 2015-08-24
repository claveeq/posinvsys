Imports MySql.Data.MySqlClient

Public Class Registration_Form
    Dim loc As Point 'to move an object usig drag and drop

    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL

    Private Sub Registration_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Male")
        ComboBox1.Items.Add("Female")

    End Sub
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub
    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub
    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label1.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub
    Private Sub Label1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label1.MouseDown
        If e.Button = MouseButtons.Left Then
            loc = e.Location
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox6.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "") Then
            MessageBox.Show("don't leave null value")

            If (TextBox3.Text <> TextBox4.Text) Then
                Label13.Text = "these passwords don't match!"
                TextBox3.Clear()
                TextBox4.Clear()
            End If

        Else
            Label13.ForeColor = Color.LimeGreen
            Label13.Text = "these passwords match!"
            Dim gender As Integer = 0
            If ComboBox1.Text = "Female" Then
                gender = 1
            End If
            MysqlConn = New MySqlConnection
            MysqlConn.ConnectionString =
                "server=localhost;userid=root;password=1234;database=rmarquez"
            Dim Reader As MySqlDataReader
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "Insert into rmarquez.account (acc_name,acc_surname,acc_pass,acc_bday,acc_gender,acc_address) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox4.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & gender & "','" & TextBox6.Text & "');"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader

                Login_Form.Label2.ForeColor = Color.LimeGreen
                Login_Form.Label2.Text = "Succefully Created"

                MysqlConn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()

            End Try
            Login_Form.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Login_Form.Show()
        Me.Hide()
    End Sub
End Class
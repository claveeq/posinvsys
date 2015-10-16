Imports MySql.Data.MySqlClient
Public Class Login_Form
    Dim loc As Point ' for movable window

    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    'for windows to move 
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Location += e.Location - loc
        End If
    End Sub
    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = MouseButtons.Left Then
            Loc = e.Location
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
    'for windows to move
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Text = Label1.Text
        TextBox1.Text = "Username"
        TextBox2.Text = "Password"

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            Dim Query As String
            MysqlConn.Open()
            Query = "INSERT INTO `rmarquez`.`receipt` (`rec_date`, `rec_items`, `rec_total`, `rec_cash`, `rec_change`, `rec_cog`) VALUES (DATE_FORMAT(NOW(),'%Y-%m-%d'), '0', '0', '0', '0', '0');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Try
            Dim Query As String
            MysqlConn.Open()
            Query = "INSERT INTO `rmarquez`.`receipt` (`rec_date`, `rec_items`, `rec_total`, `rec_cash`, `rec_change`, `rec_cog`) VALUES (DATE_FORMAT((NOW() - INTERVAL 1 DAY),'%Y-%m-%d'), '0', '0', '0', '0', '0');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub connect()
        name = TextBox1.Text
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from rmarquez.account where acc_name ='" & TextBox1.Text & "'and acc_pass = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Dim count As Integer
            count = 0
            While Reader.Read
                count = count + 1
            End While
            If count = 1 Then

                Dim newmain As New main_menu
                newmain.Show()
                Me.Hide()

            ElseIf count > 1 Then
                MessageBox.Show("Username and Password are duplicate")
            Else
                MessageBox.Show("Username and Password are incorrect")
            End If


            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        connect()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        TextBox1.Clear()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown

    End Sub

    Private Sub TextBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text = "" Then
            TextBox1.Text = "Username"
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox2.Text = "" Then
            TextBox2.Text = "Password"
            TextBox2.PasswordChar = ""
        End If
    End Sub

    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        TextBox2.Clear()
        TextBox2.PasswordChar = "*"
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            connect()
        End If

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        TextBox2.PasswordChar = "*"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class

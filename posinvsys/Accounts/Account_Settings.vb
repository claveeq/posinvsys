Imports MySql.Data.MySqlClient
Public Class Account_Settings
    Dim loc As Point ' for movable window


    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Registration_Form.Show()
        Me.Hide()
    End Sub
    'for windows to move 
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

    Private Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim main_m As New main_menu
        main_m.Show()
        Me.Hide()

    End Sub
    Public Sub combo()
        ComboBox1.Items.Clear()

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from account;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim emp_name = Reader.GetString("acc_name")
                ComboBox1.Items.Add(emp_name)
            End While

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize butto
    End Sub



    Private Sub Account_Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table
        combo()

        Try '  if accounts are disabled
            MysqlConn.Open()
            Dim Query As String
            Query = "select sum(acc_enabled) as num from account"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim enable As Integer = Reader.GetInt32("num")
                If enable > 0 Then
                    Panel2.Visible = True
                End If
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from account where acc_name = '" & ComboBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim gen As Integer = Reader.GetString("acc_gender")
                Dim gend As String
                If gen = 1 Then
                    gend = "Female"
                Else
                    gend = "Male"
                End If

                Dim adm As Integer = Reader.GetString("acc_admin")
                Dim admin As String
                If adm = 1 Then
                    admin = "Administrator"
                Else
                    admin = "Employee"
                End If
                Dim name As String = Reader.GetString("acc_name")
                Dim surname As String = Reader.GetString("acc_surname")
                Label11.Text = name + " " + surname
                Label12.Text = Reader.GetInt32("acc_age")
                Label13.Text = gend
                Label14.Text = Reader.GetString("acc_bday")
                Label15.Text = Reader.GetString("acc_address")
                Label16.Text = admin
            End While

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click, Label16.Click, Label15.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "DELETE FROM `rmarquez`.`account` WHERE `acc_name`='" & ComboBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            combo()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox1.Text = ""
            Label11.Text = ""
            Label12.Text = ""
            Label13.Text = ""
            Label14.Text = ""
            Label15.Text = ""
            Label16.Text = ""
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "Update account set acc_enabled = 0 where acc_admin = 0;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Panel2.Visible = False
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
End Class
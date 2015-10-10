Imports MySql.Data.MySqlClient
Public Class main_menu
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String
    Private Sub main_menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = ToProperCase(Login_Form.TextBox1.Text)

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"

        ComboBox1.Items.Add("Today")
        ComboBox1.Items.Add("Previous Day")
        ComboBox1.Items.Add("Week")
        ComboBox1.Items.Add("Month")
        ComboBox1.Items.Add("Year")

    End Sub
    Function ToProperCase(ByVal str As String) As String 'totitlecase function only,no need to worry
        Dim myTI As System.Globalization.TextInfo
        myTI = New System.Globalization.CultureInfo("en-US", False).TextInfo
        str = str.ToLower
        str = myTI.ToTitleCase(str)
        Return str
    End Function
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Product_Form.Show()
        Me.Hide()

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Checkout_Form.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Login_Form.Show()

        Me.Hide()
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Account_Settings.Show()
        Me.Hide()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        If ComboBox1.Text = "Today" Then
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date = DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Previous Day" Then
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date = DATE_FORMAT((NOW() - INTERVAL 1 DAY),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Week" Then
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 7 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Month" Then
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 30 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Year" Then
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 365 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        End If
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sales = Reader.GetString("sales")
                Label6.Text = sales
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub
End Class
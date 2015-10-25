Imports MySql.Data.MySqlClient
Public Class main_menu
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String


    Private Sub main_menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Chart2.Series(0).Color = Color.Red

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"

        invoice()

        Try 'Admin Rights -----------------------------------------------------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            MysqlConn.Open()
            Query = "SELECT * FROM account where acc_name = '" & Login_Form.TextBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim permission As Integer = Reader.GetInt32("acc_admin")
                Dim name As String = Reader.GetString("acc_name")
                Dim surname As String = Reader.GetString("acc_surname")

                If permission = 0 Then
                    Button5.Visible = False
                    Button7.Visible = False
                Else
                    Button5.Visible = True
                    Button7.Visible = True
                End If
                Label3.Text = name

            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

        Try 'sales today 'label 6
            MysqlConn.Open()
            Query = "SELECT SUM(rec_total) as sales FROM receipt where rec_date = DATE_FORMAT(NOW(),'%Y-%m-%d');"
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

        Try ' populating the graph
            MysqlConn.Open()
            Query = "SELECT rec_date as Day, sum(rec_cash) as Total FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 7 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d') Group by rec_date;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Chart1.Series("Weekly Sales").Points.AddXY(Reader.GetString("Day"), Reader.GetDouble("Total"))
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

        Try ' populating the graph
            MysqlConn.Open()
            Query = "SELECT * FROM inventory INNER JOIN product on inv_barcode = prod_barcode WHERE inv_stock <= inv_ropoint;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Chart2.Series("Critical Stocks").Points.AddXY(Reader.GetString("prod_name"), Reader.GetDouble("inv_stock"))
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try


        Try ' populating the graph
            MysqlConn.Open()
            Query = "SELECT * FROM invoice;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try


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
        Dim Product_Form2 As New Product_Form
        Product_Form2.Show()
        Me.Hide()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Checkout_Form.Show()
        Checkout_Form.Button5.PerformClick()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Dim newlogin As New Login_Form
        'newlogin.Show()
        Login_Form.Show()
        Me.Hide()
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Account_Settings.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Reports_Form As New Reports_Form
        Reports_Form.Show()
        Me.Hide()
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim account_settings As New Account_Settings
        account_settings.Show()
        Me.Hide()

    End Sub

    Private Sub Button5_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.MouseHover
        Label8.Text = "Reports" & vbNewLine & " Documents"
    End Sub

    Private Sub Button5_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.MouseLeave
        Label8.Text = ""
    End Sub

    Private Sub Button2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.MouseHover
        Label9.Text = "POS"
    End Sub

    Private Sub Button2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.MouseLeave
        Label9.Text = ""
    End Sub

    Private Sub Button1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.MouseHover
        Label13.Text = "Inventory"
    End Sub

    Private Sub Button1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.MouseLeave
        Label13.Text = ""
    End Sub

    Private Sub Button7_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.MouseHover
        Label14.Text = "Accounts"
    End Sub

    Private Sub Button7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.MouseLeave
        Label14.Text = ""
    End Sub
    Private Sub PictureBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseHover
        Label12.Text = "R.MARQUEZ POSINVSYS"
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Label12.Text = ""
    End Sub

    Private Sub Chart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        MessageBox.Show("R. MARQUEZ POSINVSYS (POS Inventory Managemnet System )" & vbNewLine & "is designed to work with Pharmaceutical Stores." & vbNewLine & "Developed by 3rd Year College students of Dr. Yanga's Colleges INC. ")
    End Sub
    Private Sub invoice()

        Try
            MysqlConn.Open()
            Query = "SELECT * FROM invoice"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Label15.Text = Reader.GetInt32("invc_limit")
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Try
            MysqlConn.Open()
            Query = "SELECT COUNT(*)as 'all' FROM receipt;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim count As Integer = Reader.GetInt32("all")
                Label16.Text = count - 2
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Label17.Text = Val(Label15.Text) - Val(Label16.Text)
        If Label17.Text = 0 Then
            Button2.BackgroundImage = My.Resources.Cashier_inactive
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click

    End Sub
End Class
﻿Imports MySql.Data.MySqlClient
Public Class Reports_Form
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String
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
    Private Sub Reports_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"

        ComboBox1.Items.Add("Today")
        ComboBox1.Items.Add("Previous Day")
        ComboBox1.Items.Add("Week")
        ComboBox1.Items.Add("Month")
        ComboBox1.Items.Add("Year")

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
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        If ComboBox1.Text = "Today" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date = DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Previous Day" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date = DATE_FORMAT((NOW() - INTERVAL 1 DAY),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Week" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 7 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Month" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 30 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Year" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 365 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        End If
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sales = Reader.GetString("sales")
                Dim cost = Reader.GetString("cost")
                Label6.Text = sales
                Label3.Text = cost
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub gross_calc()
        
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        Dim num1 As Double = Val(Label6.Text)
        Dim num2 As Double = Val(Label3.Text)
        Dim answer As Double = num1 - num2
        Label8.Text = answer
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

  
    Private Sub Chart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        main_menu.Show()
        Me.Hide()
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class
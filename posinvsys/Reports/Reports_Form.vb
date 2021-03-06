﻿Imports MySql.Data.MySqlClient
Public Class Reports_Form
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String

    Dim dbdataset As New DataTable  'For Datagridview 
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
        Dim date_from As String = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        Dim date_to As String = Format(DateTimePicker2.Value, "yyyy-MM-dd")

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        invoice()

        ComboBox1.Items.Add("Today")
        ComboBox1.Items.Add("Previous Day")
        ComboBox1.Items.Add("Week")
        ComboBox1.Items.Add("Month")
        ComboBox1.Items.Add("Year")


        sold_items()

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

        pop() 'populate

    End Sub
    Private Sub sold_items()
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter
        Dim bsource As New BindingSource
        dbdataset.Clear()
        Dim date_item As String = Format(DateTimePicker3.Value, "yyyy-MM-dd")

        Try
            MysqlConn.Open()
            Query =
            "SELECT it_name as Product, SUM(it_quantity) as QTY, sum(it_price) as 'Total Price' FROM rmarquez.items where it_date = '" & date_item & "' group by it_name;"
            COMMAND = New MySqlCommand(Query, MysqlConn)

            Sda.SelectCommand = COMMAND
            Sda.Fill(dbdataset)
            bsource.DataSource = dbdataset
            DataGridView1.DataSource = bsource
            Sda.Update(dbdataset)

            'default
            MysqlConn.Close() 'default
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub pop()
        ListBox1.Items.Clear()

        Try ' populate list box
            MysqlConn.Open()
            Query = "select * from rmarquez.unlisted;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim list = Reader.GetString("un_item")
                ListBox1.Items.Add(list)
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


    Private Sub Label3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        Dim num1 As Double = Val(Label6.Text)
        Dim num2 As Double = Val(Label3.Text)
        Dim answer As Double = num1 - num2
        Label8.Text = answer
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim main_m As New main_menu
        main_m.Show()
        Me.Hide()
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "SELECT * FROM receipt WHERE rec_id = '" & TextBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim id = Reader.GetString("rec_id")
                Dim rdate = Reader.GetString("rec_date")
                '       Dim items = Reader.GetString("rec_items")
                Dim rtotal = Reader.GetString("rec_total")
                Dim cash = Reader.GetString("rec_cash")
                Dim change = Reader.GetString("rec_change")
                Dim name = Reader.GetString("rec_cashier")
                '  Dim items As String = Checkout_Form.DataGridView1.

                Dim cellValues As New List(Of String)
                For Each row As DataGridViewRow In Checkout_Form.DataGridView1.Rows
                    cellValues.Add(row.Cells(3).Value.ToString() + "  " + row.Cells(1).Value.ToString() + vbNewLine + "    ......................................................Php " + row.Cells(4).Value.ToString())
                Next
                Checkout_Form.RichTextBox1.Lines = cellValues.ToArray()
                Dim items As String = Checkout_Form.RichTextBox1.Text

                Dim receipt As String =
                    "                               RMSTORE        " & vbNewLine &
                    "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" & vbNewLine & vbNewLine &
                    "Receipt " & vbNewLine &
                    "Invoice# " & id & vbNewLine &
                    "Date purchased: " & rdate & vbNewLine &
                    "Served by: " & " " & name & vbNewLine &
                    "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" & vbNewLine & vbNewLine &
                    "                               ITEMS" & vbNewLine & items & vbNewLine &
                    "________________________________________" & vbNewLine & vbNewLine &
                    "            Total.........................................Php " & rtotal & vbNewLine &
                    "            Cash.........................................Php " & cash & vbNewLine &
                    "            Change....................................Php " & change & vbNewLine &
                    "________________________________________" & vbNewLine &
                    "THIS SERVE AS AN OFFICIAL RECEIPT" & vbNewLine &
                    "Thank you COME AGAIN" & vbNewLine

                RichTextBox1.Text = receipt

            End While

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "delete  FROM unlisted WHERE un_item = '" & ListBox1.SelectedItem.ToString & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        pop()
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Product_Form.Show()
        Product_Form.TextBox1.Text = ListBox1.SelectedItem.ToString
    End Sub

    Private Sub ListBox1_MeasureItem(sender As Object, e As MeasureItemEventArgs) Handles ListBox1.MeasureItem

    End Sub
    Private Sub graph()
        Dim date_from As String = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        Dim date_to As String = Format(DateTimePicker2.Value, "yyyy-MM-dd")
        Try
            MysqlConn.Open()
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  '" & date_from & "' and '" & date_to & "'  Group by rec_date;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sales = Reader.GetString("sales")
                Dim cost = Reader.GetString("cost")
                Label6.Text = sales
                Label3.Text = cost
                Label8.Text = sales - cost
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Chart1.Series(0).Points.Clear()
        Try ' populating the graph
            MysqlConn.Open()
            Query = "SELECT rec_date as Day, sum(rec_cash) as Total FROM receipt where rec_date between  '" & date_from & "' and '" & date_to & "'  Group by rec_date limit 7;"
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
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        graph()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        graph()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
    Private Sub invoice()
        Try
            MysqlConn.Open()
            Query = "SELECT * FROM invoice"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Label19.Text = Reader.GetInt32("invc_limit")
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
                Label18.Text = count - 2
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Label20.Text = Val(Label19.Text) - Val(Label18.Text)
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            MysqlConn.Open()
            Query = " Update `rmarquez`.`invoice` Set `invc_limit`='" & TextBox2.Text & "' WHERE `invc_limit`='" & Label19.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            TextBox2.Clear()
            invoice()
        End Try
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        sold_items()
    End Sub
End Class
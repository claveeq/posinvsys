﻿Imports MySql.Data.MySqlClient
Public Class Product_Form
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
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
    Private Sub table_refresh() 'To refresh or load the data from datagridview
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim dbdataset As New DataTable  'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table
        Try
            MysqlConn.Open()
            Dim Query As String
            Query =
            "select prod_barcode as Barcode,prod_name as Product, prod_description as Description, prod_type as ""Product Type"", prod_brand as ""Product Brand"" from rmarquez.product;"
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
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        table_refresh() ' from the private sub table_refresh() to load from the beginning of the form
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "Insert into rmarquez.product (prod_name,prod_barcode,prod_description,prod_type,prod_brand) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & RichTextBox1.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            'Event na mag rurun after click ng button
            table_refresh()
            MessageBox.Show("Product Successfully Added")
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
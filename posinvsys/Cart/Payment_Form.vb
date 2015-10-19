Imports MySql.Data.MySqlClient
Public Class Payment_Form
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand
    Public Property firstname As String
    Public Property surname As String
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
        Dim rowcount As Integer = Checkout_Form.DataGridView1.Rows.Count()

        For row_num As Integer = 0 To (rowcount)
            If (row_num = (rowcount)) Then
                Exit For
            Else
                Dim brcode As String = Checkout_Form.DataGridView1.Rows(row_num).Cells(10).Value
                Try
                    MysqlConn.Open()
                    Dim Query As String
                    Dim qty As Integer = Checkout_Form.DataGridView1.Rows(row_num).Cells(3).Value
                    Query = "UPDATE `rmarquez`.`inventory` SET `inv_stock`= inv_stock - '" & qty & "' WHERE `inv_barcode`='" & brcode & "';"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    Reader = COMMAND.ExecuteReader
                    MysqlConn.Close()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                Finally
                    MysqlConn.Dispose()
                End Try
                'jj

                If brcode = "newitem" Then
                    Try
                        Dim unitem As String = Checkout_Form.DataGridView1.Rows(row_num).Cells(1).Value
                        Dim unprice As Double = Checkout_Form.DataGridView1.Rows(row_num).Cells(2).Value
                        MysqlConn.Open()
                        Dim Query As String
                        Query = "INSERT INTO `rmarquez`.`unlisted` (`un_item`, `un_price`) VALUES ('" & unitem & "', '" & unprice & "');"
                        COMMAND = New MySqlCommand(Query, MysqlConn)
                        Reader = COMMAND.ExecuteReader
                        MysqlConn.Close()
                    Catch ex As MySqlException
                    Finally
                        MysqlConn.Dispose()
                    End Try
                End If
            End If
        Next

        '  For row_num As Integer = 0 To (rowcount)
        ' If (row_num = (rowcount)) Then
        'Exit For
        'Else
        ' Dim brcode As String = Checkout_Form.DataGridView1.Rows(row_num).Cells(10).Value
        '  End If
        ' Next

        Dim cellValues As New List(Of String)
        For Each row As DataGridViewRow In Checkout_Form.DataGridView1.Rows
            cellValues.Add(row.Cells(3).Value.ToString() + "  " + row.Cells(1).Value.ToString() + vbNewLine + "    ............Php " + row.Cells(4).Value.ToString())
        Next
        Checkout_Form.RichTextBox1.Lines = cellValues.ToArray()
        Dim items As String = Checkout_Form.RichTextBox1.Text



        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "Insert into rmarquez.receipt (rec_date,rec_total,rec_cash,rec_change,rec_cog,rec_items,rec_cashier) values (DATE_FORMAT(NOW(),'%Y-%m-%d %H:%i:%s'),'" & Label4.Text & "','" & TextBox1.Text & "','" & Label6.Text & "','" & Checkout_Form.supply & "','" & items & "','" & Login_Form.TextBox1.Text & "');"
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
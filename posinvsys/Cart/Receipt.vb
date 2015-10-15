Imports MySql.Data.MySqlClient
Public Class receipt
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Reader As MySqlDataReader
    Dim Query As String

    Private Sub receipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "SELECT * FROM receipt WHERE rec_id = (select count(*) from receipt) ;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim id = Reader.GetString("rec_id")
                Dim rdate = Reader.GetString("rec_date")
                '       Dim items = Reader.GetString("rec_items")
                Dim rtotal = Reader.GetString("rec_total")
                Dim cash = Reader.GetString("rec_cash")
                Dim change = Reader.GetString("rec_change")

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
                    "Served by: " & Login_Form.nn & " " & Login_Form.qq & vbNewLine &
                    "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" & vbNewLine & vbNewLine &
                    "                               ITEMS" & vbNewLine & items & vbNewLine &
                    "________________________________________" & vbNewLine & vbNewLine &
                    "            Total.........................................Php " & rtotal & vbNewLine &
                    "            Cash.........................................Php " & cash & vbNewLine &
                    "            Change....................................Php " & change & vbNewLine &
                    "________________________________________" & vbNewLine
                RichTextBox1.Text = receipt

            End While


            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Checkout_Form.Hide()
        Dim newcheck As New Checkout_Form
        newcheck.Show()
        Me.Close()

        ' resetForm(Me)
    End Sub
    Private Sub resetForm(ByVal frmSub As Form)
        frmSub.Close()
        frmSub.Show()
    End Sub
    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class
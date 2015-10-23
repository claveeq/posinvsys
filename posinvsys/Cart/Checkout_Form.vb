Imports MySql.Data.MySqlClient
Imports CoreScanner

Public Class Checkout_Form
    Public Property total As String ' passing string to another form variable
    Public Property supply As String

    Dim loc As Point ' for movable window
    Dim num1, num2, totalcalc As Double

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL


    Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
    Dim bsource As New BindingSource 'bago tong tatlo for the table
    Dim dbdataset As New DataTable  'bago tong tatlo for the table


    Dim cCoreScannerClass As New CCoreScanner 'instantiating Barcode scanner class


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
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
    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim main_m As New main_menu
        main_m.Show()
        Me.Hide()
    End Sub
    Public Sub restart()
        'Me.Refresh()
        '  Dim Checkout_Form As New Checkout_Form
        ' Checkout_Form.Show()

    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Checkout_Form As New Checkout_Form
        Checkout_Form.Show()
        Me.Hide()

    End Sub
    Private Sub Barcode()
        Try
            'Call Open API
            Dim scannerTypes() As Short = New Short((1) - 1) {}
            'Scanner Types you are interested in
            scannerTypes(0) = 1
            ' 1 for all scanner types
            Dim numberOfScannerTypes As Short = 1
            ' Size of the scannerTypes array
            Dim status As Integer
            ' Extended API return code
            cCoreScannerClass.Open(0, scannerTypes, numberOfScannerTypes, status)
            ' Subscribe for barcode events in cCoreScannerClass
            AddHandler cCoreScannerClass.BarcodeEvent, AddressOf Me.OnBarcodeEvent
            ' Let's subscribe for events
            Dim opcode As Integer = 1001
            ' Method for Subscribe events
            Dim outXML As String
            ' XML Output
            Dim inXML As String = ("<inArgs>" + ("<cmdArgs>" + ("<arg-int>1</arg-int>" + ("<arg-int>1</arg-int>" + ("</cmdArgs>" + "</inArgs>")))))
            cCoreScannerClass.ExecCommand(opcode, inXML, outXML, status)
            Console.WriteLine(outXML)
            cCoreScannerClass.Close(cCoreScannerClass.BarcodeEvent, 0) ' haaay this line is terrible haha
        Catch exp As Exception
            Console.WriteLine(("Something wrong please check... " + exp.Message))
        End Try

    End Sub
    Private Sub OnBarcodeEvent(ByVal eventType As Short, ByRef pscanData As String) ' eventfunction for barcode_scanner
        Dim barcode As String = ""
        barcode = pscanData
        Me.Invoke(DirectCast(Sub() Textbox1.Text = barcode, MethodInvoker))

    End Sub
    Private Sub trim_rawdata() 'rawdata ng barcode label
        Dim Bar As String = "" 'raw data from the scanner
        Dim raw As String

        raw = Textbox1.Lines(9).ToString()
        Bar = raw.Trim
        Bar = Bar.Replace("<datalabel>", "")
        Bar = Bar.Replace("</datalabel>", "")
        Bar = Bar.Replace("0x20", " ")
        Bar = Bar.Replace("0x21 ", "!")
        Bar = Bar.Replace("0x22 ", """")
        Bar = Bar.Replace("0x23 ", "#")
        Bar = Bar.Replace("0x24 ", "$")
        Bar = Bar.Replace("0x25 ", "%")
        Bar = Bar.Replace("0x26 ", "&")
        Bar = Bar.Replace("0x27", "'")
        Bar = Bar.Replace("0x28", "(")
        Bar = Bar.Replace("0x29", ")")
        Bar = Bar.Replace("0x2A", "*")
        Bar = Bar.Replace("0x2B", "+")
        Bar = Bar.Replace("0x2C", ",")
        Bar = Bar.Replace("0x2D", "-")
        Bar = Bar.Replace("0x2E", ".")
        Bar = Bar.Replace("0x2F", "/")
        Bar = Bar.Replace("0x30", "0")
        Bar = Bar.Replace("0x31", "1")
        Bar = Bar.Replace("0x32", "2")
        Bar = Bar.Replace("0x33", "3")
        Bar = Bar.Replace("0x34", "4")
        Bar = Bar.Replace("0x35", "5")
        Bar = Bar.Replace("0x36", "6")
        Bar = Bar.Replace("0x37", "7")
        Bar = Bar.Replace("0x38", "8")
        Bar = Bar.Replace("0x39", "9")
        Bar = Bar.Replace("0x3A", ":")
        Bar = Bar.Replace("0x3B", ";")
        Bar = Bar.Replace("0x3C", "<")
        Bar = Bar.Replace("0x3D", "=")
        Bar = Bar.Replace("0x3E", ">")
        Bar = Bar.Replace("0x3F", "?")
        Bar = Bar.Replace("0x40", "@")
        Bar = Bar.Replace("0x41", "A")
        Bar = Bar.Replace("0x42", "B")
        Bar = Bar.Replace("0x43", "C")
        Bar = Bar.Replace("0x44", "D")
        Bar = Bar.Replace("0x45", "E")
        Bar = Bar.Replace("0x46", "F")
        Bar = Bar.Replace("0x47", "G")
        Bar = Bar.Replace("0x48", "H")
        Bar = Bar.Replace("0x49", "I")
        Bar = Bar.Replace("0x4A", "J")
        Bar = Bar.Replace("0x4B", "K")
        Bar = Bar.Replace("0x4C", "L")
        Bar = Bar.Replace("0x4D", "M")
        Bar = Bar.Replace("0x4E", "N")
        Bar = Bar.Replace("0x4F", "O")
        Bar = Bar.Replace("0x50", "P")
        Bar = Bar.Replace("0x51", "Q")
        Bar = Bar.Replace("0x52", "R")
        Bar = Bar.Replace("0x53", "S")
        Bar = Bar.Replace("0x54", "T")
        Bar = Bar.Replace("0x55", "U")
        Bar = Bar.Replace("0x56", "V")
        Bar = Bar.Replace("0x57", "W")
        Bar = Bar.Replace("0x58", "X")
        Bar = Bar.Replace("0x59", "Y")
        Bar = Bar.Replace("0x5A", "Z")
        Bar = Bar.Replace("0x5B", "[")
        Bar = Bar.Replace("0x5C", "\")
        Bar = Bar.Replace("0x5D", "]")
        Bar = Bar.Replace("0x5E", "^")
        Bar = Bar.Replace("0x5F", "_")
        Bar = Bar.Replace("0x60", "`")
        Bar = Bar.Replace("0x61", "a")
        Bar = Bar.Replace("0x62", "b")
        Bar = Bar.Replace("0x63", "c")
        Bar = Bar.Replace("0x64", "d")
        Bar = Bar.Replace("0x65", "e")
        Bar = Bar.Replace("0x66", "f")
        Bar = Bar.Replace("0x67", "g")
        Bar = Bar.Replace("0x68", "h")
        Bar = Bar.Replace("0x69", "i")
        Bar = Bar.Replace("0x6A", "j")
        Bar = Bar.Replace("0x6B", "k")
        Bar = Bar.Replace("0x6C", "l")
        Bar = Bar.Replace("0x6D", "m")
        Bar = Bar.Replace("0x6E", "n")
        Bar = Bar.Replace("0x6F", "o")
        Bar = Bar.Replace("0x70", "p")
        Bar = Bar.Replace("0x71", "q")
        Bar = Bar.Replace("0x72", "r")
        Bar = Bar.Replace("0x73", "s")
        Bar = Bar.Replace("0x74", "t")
        Bar = Bar.Replace("0x75", "u")
        Bar = Bar.Replace("0x76", "v")
        Bar = Bar.Replace("0x77", "w")
        Bar = Bar.Replace("0x78", "x")
        Bar = Bar.Replace("0x79", "y")
        Bar = Bar.Replace("0x7A", "z")
        Bar = Bar.Replace("0x7B", "{")
        Bar = Bar.Replace("0x7C", "|")
        Bar = Bar.Replace("0x7D", "}")
        Bar = Bar.Replace("0x7E", "~")
        Bar = Bar.Replace(" ", "")
        TextBox.Text = Bar

    End Sub
    Public Sub Checkout_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Barcode()
        TextBox2.Text = "Product Name"


    End Sub
    Public Sub popoulate()


        Dim cart_img As Image = My.Resources.cart
        Dim btn_img As Image = My.Resources.Trash

        'Dim butcol As DataGridViewButtonColumn

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim reader As MySqlDataReader

        'DataGridView1.Columns.Insert(0, imgcol)
        ' DataGridView1.Columns.Insert(2, butcol)
        Try
            MysqlConn.Open()
            Dim Query As String
            Query =
            "select * FROM product INNER JOIN pricing ON product.prod_barcode = pricing.price_barcode INNER JOIN inventory ON inventory.inv_barcode = pricing.price_barcode  where prod_barcode = '" & TextBox.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            reader = COMMAND.ExecuteReader
            While reader.Read
                Dim qty As Integer = 1

                Dim brcode = reader.GetString("prod_barcode")
                Dim name = reader.GetString("prod_name")
                Dim price = reader.GetString("price_price")
                Dim psupply = reader.GetString("price_supply")
                Dim stock = reader.GetString("inv_stock")

                total = qty * price
                supply = qty * psupply
                Dim currentstock As Integer = stock - qty

                Dim row As Object() = New Object() {cart_img, name, price, qty, total, btn_img, psupply, supply, stock, currentstock, brcode}
                DataGridView1.Rows.Add(row)

                Dim num1 As Double
                Dim num2 As Double
                Dim add As Double
                num1 = (1 * price)
                num2 = Val(Label4.Text)
                add = num1 + num2
                Label4.Text = add
            End While
            'default
            MysqlConn.Close() 'default
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub

    Private Sub Textbox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Textbox1.TextChanged
        trim_rawdata()

    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox.TextChanged
        popoulate()
    End Sub
    Public Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex = 5 Then  'delete the entire row of a product
            DataGridView1.Rows.Remove(DataGridView1.Rows(e.RowIndex))
                    total_price_computation()
        End If

    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If e.RowIndex >= 0 And e.ColumnIndex = 3 Then  'edit totals of quantity,supply and stock of the product in data grid view
            Dim num_qty As Double = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            Dim price_qty As Double = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            Dim total_qty As Double = num_qty * price_qty
            DataGridView1.Rows(e.RowIndex).Cells(4).Value = total_qty

            Dim num_sup As Double = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            Dim price_sup As Double = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            Dim total_sup As Double = num_sup * price_sup
            DataGridView1.Rows(e.RowIndex).Cells(7).Value = total_sup


            Dim num_stock As Double = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            Dim price_stock As Double = DataGridView1.Rows(e.RowIndex).Cells(8).Value
            Dim total_stock As Double = price_stock - num_stock
            DataGridView1.Rows(e.RowIndex).Cells(9).Value = total_stock
        End If
    End Sub
    Private Sub total_price_computation()
        Dim totalValue As Double
        Dim totalValue_cod As Double
        For Each dgvRow As DataGridViewRow In DataGridView1.Rows
            If Not dgvRow.IsNewRow Then
                totalValue += CDbl(dgvRow.Cells(4).Value)
                totalValue_cod += CDbl(dgvRow.Cells(7).Value)
            End If
        Next
        Label4.Text = totalValue
        supply = totalValue_cod
    End Sub
    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        total_price_computation()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Payment_Form.Show()
        Payment_Form.Label4.Text = Label4.Text
        Payment_Form.TextBox1.Text = Label4.Text
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox3.Text = TextBox3.Text + "6"
    End Sub
    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        TextBox2.Clear()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox3.Text = TextBox3.Text + "1"
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox3.Text = TextBox3.Text + "2"
    End Sub

    Public Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        num2 = Val(TextBox3.Text)
        If Label6.Text = "+" Then
            totalcalc = num1 + num2
        ElseIf (Label6.Text = "-")
            totalcalc = num1 - num2
        ElseIf (Label6.Text = "x")
            totalcalc = num1 * num2
        ElseIf (Label6.Text = "/")
            totalcalc = num1 / num2
        End If
        TextBox3.Clear()
        TextBox3.Text = totalcalc
        Label6.Text = "="
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox3.Text = TextBox3.Text + "3"
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox3.Text = TextBox3.Text + "4"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox3.Text = TextBox3.Text + "5"
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox3.Text = TextBox3.Text + "7"
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        TextBox3.Text = TextBox3.Text + "8"
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        TextBox3.Text = TextBox3.Text + "9"
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        num1 = Val(TextBox3.Text)
        TextBox3.Clear()
        Label6.Text = "-"
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        num1 = Val(TextBox3.Text)
        TextBox3.Clear()
        Label6.Text = "x"
    End Sub
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        num1 = Val(TextBox3.Text)
        TextBox3.Clear()
        Label6.Text = "/"
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        num1 = 0
        num2 = 0
        totalcalc = 0
        TextBox3.Clear()
        Label6.Text = ""
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        TextBox3.Text = TextBox3.Text + "0"
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If TextBox3.Text = "" Then
            MessageBox.Show("Please enter a price in a Custom item.")
        Else
            Dim cart_img As Image = My.Resources.cart
            Dim btn_img As Image = My.Resources.Trash
            Dim name = TextBox2.Text
            Dim price = TextBox3.Text
            Dim qty As Integer = 1
            Dim brcode As String = "newitem"
            Dim psupply = 1
            Dim stock = 1
            total = qty * price
            supply = qty * psupply
            Dim currentstock As Integer = stock - qty

            Dim row As Object() = New Object() {cart_img, name, price, qty, total, btn_img, psupply, supply, stock, currentstock, brcode}
            DataGridView1.Rows.Add(row)

            Dim num1 As Double
            Dim num2 As Double
            Dim add As Double
            num1 = (1 * price)
            num2 = Val(Label4.Text)
            add = num1 + num2
            Label4.Text = add

            TextBox2.Text = "Product Name"
            TextBox3.Clear()
            Label6.Text = ""
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Public Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        num1 = Val(TextBox3.Text)
        TextBox3.Clear()
        Label6.Text = "+"
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As EventArgs) Handles TextBox2.LostFocus
        If TextBox2.Text = "" Then
            TextBox2.Text = "Product Name"
        End If
    End Sub

    Private Sub Checkout_Form_MarginChanged(sender As Object, e As EventArgs) Handles Me.MarginChanged

    End Sub
End Class

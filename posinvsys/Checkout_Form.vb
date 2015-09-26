Imports MySql.Data.MySqlClient
Imports CoreScanner
Public Class Checkout_Form

    Dim loc As Point ' for movable window


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
        main_menu.Show()
        Me.Hide()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button5.Click
        DataGridView1.Rows.Clear()

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
            CCoreScannerClass.Open(0, scannerTypes, numberOfScannerTypes, status)
            ' Subscribe for barcode events in cCoreScannerClass
            AddHandler CCoreScannerClass.BarcodeEvent, AddressOf Me.OnBarcodeEvent
            ' Let's subscribe for events
            Dim opcode As Integer = 1001
            ' Method for Subscribe events
            Dim outXML As String
            ' XML Output
            Dim inXML As String = ("<inArgs>" + ("<cmdArgs>" + ("<arg-int>1</arg-int>" + ("<arg-int>1</arg-int>" + ("</cmdArgs>" + "</inArgs>")))))
            CCoreScannerClass.ExecCommand(opcode, inXML, outXML, status)
            Console.WriteLine(outXML)
            cCoreScannerClass.Close(cCoreScannerClass.BarcodeEvent, 0) ' this line is terrible haha

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

   
    Private Sub dfsdf()
    


        Dim img As Image = Image.FromFile("D:\Clash\Doucuments\Clave\SAD\Proj\posinvsys\posinvsys\Images\cart.png")

        Dim imgcol As DataGridViewImageColumn = New DataGridViewImageColumn()
        With imgcol
            .Image = img
        End With


        DataGridView1.ColumnCount = 3
        DataGridView1.Columns(0).Name = "barcode"
        DataGridView1.Columns(1).Name = "Stock"
        DataGridView1.Columns(2).Name = "mage"


        Dim row As Object() = New Object() {"xcvxc", "wrer", "sdfsd"}
        DataGridView1.Rows.Add(row)

        Dim row2 As Object() = New Object() {"xcvxc", "wrer", "sdfsd"}
        DataGridView1.Rows.Add(row2)
        DataGridView1.Columns.Insert(0, imgcol)

    End Sub
    Private Sub Checkout_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Barcode()

        DataGridView1.ColumnCount = 6
        DataGridView1.Columns(0).Name = "Barcode"
        DataGridView1.Columns(0).Width = 90
        DataGridView1.Columns(1).Name = "Product"
        DataGridView1.Columns(1).Width = 130
        DataGridView1.Columns(2).Name = "Stock"
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(3).Name = "Price"
        DataGridView1.Columns(4).Name = "QTY"
        DataGridView1.Columns(5).Name = "Delete"
    End Sub
    Private Sub popoulate()

        Dim img As Image = Image.FromFile("D:\Clash\Doucuments\Clave\SAD\Proj\posinvsys\posinvsys\Images\cart.png")

        Dim imgcol As DataGridViewImageColumn = New DataGridViewImageColumn()
        With imgcol
            .Image = img
        End With

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim reader As MySqlDataReader


        Try
            MysqlConn.Open()
            Dim Query As String
            Query =
            "select * FROM product INNER JOIN pricing ON product.prod_barcode = pricing.price_barcode where prod_barcode = '" & TextBox.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            reader = COMMAND.ExecuteReader
            While reader.Read

                Dim name = reader.GetString("prod_name")
                Dim barc = reader.GetString("prod_barcode")
                Dim locs = reader.GetString("prod_loc")
                Dim price = reader.GetString("price_price")
                Dim row As Object() = New Object() {barc, name, "NA", price, "QTY", ""}
                DataGridView1.Rows.Add(row)
                DataGridView1.Columns.Insert(0, imgcol)
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

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dfsdf()
    End Sub
End Class

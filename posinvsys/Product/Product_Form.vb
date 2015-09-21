Imports MySql.Data.MySqlClient
Imports CoreScanner

Public Class Product_Form
    Dim loc As Point ' to move windows

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String

    Dim dbdataset As New DataTable  'bago tong tatlo for the table
    Dim DV As New DataView(dbdataset) 'for search filter

    Dim cCoreScannerClass As New CCoreScanner 'instantiating Barcode scanner class



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
  
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Public Sub combobox_type()
        'for populating the type,brand,location combobox
        ComboBox1.Items.Clear() 'para reset all items
        ComboBox4.Items.Clear()
        Try
            MysqlConn.Open()
            Query = "select * from rmarquez.type;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim stype = Reader.GetString("type_name")
                ComboBox1.Items.Add(stype)
                ComboBox4.Items.Add(stype)
            End While
            ComboBox1.Items.Add("Add Type")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Public Sub combobox_brand()
        ComboBox2.Items.Clear()
        ComboBox5.Items.Clear()
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from brand;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sbrand = Reader.GetString("brand_name")
                ComboBox2.Items.Add(sbrand)
                ComboBox5.Items.Add(sbrand)
            End While
            ComboBox2.Items.Add("Add Brand")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Public Sub combobox_loc()
        ComboBox3.Items.Clear()
        ComboBox6.Items.Clear()
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from location;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sloc = Reader.GetString("loc_name")
                ComboBox3.Items.Add(sloc)
                ComboBox6.Items.Add(sloc)
            End While
            ComboBox3.Items.Add("Add Loc")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub table_refresh() 'To refresh or load the data from datagridview
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table

        dbdataset.Clear()
        Try
            MysqlConn.Open()
            Query =
            "select prod_barcode as Barcode,prod_name as Product, prod_description as Description, prod_type as Type, prod_brand as Brand, prod_loc as Location from rmarquez.product;"
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
    Private Sub Product_Form_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        table_refresh() ' from the private sub table_refresh() to load from the beginning of the form

        combobox_type()
        combobox_brand()
        combobox_loc()

        Barcode()


    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "Insert into rmarquez.product (prod_name,prod_barcode,prod_description,prod_type,prod_brand,prod_loc) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & RichTextBox1.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader

            'Event na mag rurun after click ng button
            MessageBox.Show("Product Successfully Added")
       

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            table_refresh()
            Barcode()
        End Try
    End Sub


    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox1.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox1.SelectedIndex = lastitem - 1 Then
            addtype.Show()
        End If
    End Sub

    Private Sub ComboBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox2.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox2.SelectedIndex = lastitem - 1 Then
            addbrand.Show()
        End If
    End Sub

    Private Sub ComboBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox3.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox3.SelectedIndex = lastitem - 1 Then
            addloc.Show()
        End If
    End Sub

    Private Sub TextBox9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.Click
        TextBox2.Clear()
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        DV.RowFilter = String.Format("Product Like '%" & TextBox9.Text & "%'")
        DataGridView1.DataSource = DV
        If (TextBox9.Text = "") Then
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        DV.RowFilter = String.Format("Type Like '%" & ComboBox4.Text & "%'")
        DataGridView1.DataSource = DV
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        DV.RowFilter = String.Format("Brand Like '%" & ComboBox5.Text & "%'")
        DataGridView1.DataSource = DV
    End Sub
    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        DV.RowFilter = String.Format("Location Like '%" & ComboBox6.Text & "%'")
        DataGridView1.DataSource = DV
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

            trim_rawdata()
            cCoreScannerClass.Close(cCoreScannerClass.BarcodeEvent, 0) ' haaay this line is terrible haha
        Catch exp As Exception
            Console.WriteLine(("Something wrong please check... " + exp.Message))
        End Try

    End Sub
    Public Sub OnBarcodeEvent(ByVal eventType As Short, ByRef pscanData As String) ' eventfunction for barcode_scanner
        Dim barcode As String = pscanData
        Me.Invoke(DirectCast(Sub() TextBox10.Text = barcode, MethodInvoker))

    End Sub
    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        Barcode()
        TextBox2.Clear()
        table_refresh()

        TextBox1.Clear()
        RichTextBox1.Clear()
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""

    End Sub
    Private Sub trim_rawdata() 'rawdata ng barcode label
        Dim Bar As String 'raw data from the scanner
        Dim raw As String
        raw = TextBox10.Lines(9).ToString()
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
        TextBox2.Text = Bar
        DV.RowFilter = String.Format("Barcode Like '%" & TextBox2.Text & "%'")
        DataGridView1.DataSource = DV
   
        'to know if the row index exist
        Dim row As DataGridViewRow
        Dim selectedCellCount As Integer = DataGridView1.GetCellCount(DataGridViewElementStates.Selected)

        row = Me.DataGridView1.Rows(0)
        If selectedCellCount = 1 Then
       
            TextBox1.Text = row.Cells("Product").Value.ToString
            RichTextBox1.Text = row.Cells("Description").Value.ToString
            ComboBox1.Text = row.Cells("type").Value.ToString
            ComboBox2.Text = row.Cells("brand").Value.ToString
            ComboBox3.Text = row.Cells("Location").Value.ToString
        Else
            TextBox1.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
        End If


    End Sub
    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        trim_rawdata()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex >= 0 Then  'retrieve cell data from datagridview to textbox
            Dim row As DataGridViewRow

            row = Me.DataGridView1.Rows(e.RowIndex)

            TextBox1.Text = row.Cells("Product").Value.ToString
            RichTextBox1.Text = row.Cells("Description").Value.ToString
            ComboBox1.Text = row.Cells("type").Value.ToString
            ComboBox2.Text = row.Cells("brand").Value.ToString
            ComboBox3.Text = row.Cells("Location").Value.ToString
            TextBox2.Text = row.Cells("Barcode").Value.ToString
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "delete from rmarquez.product where prod_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader

            'Event na mag rurun after click ng button
            MessageBox.Show("Product Successfully Deleted")
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            table_refresh()
            MysqlConn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "update rmarquez.product set prod_name='" & TextBox1.Text & "',prod_description='" & RichTextBox1.Text & "',prod_type='" & ComboBox1.Text & "',prod_brand='" & ComboBox2.Text & "',prod_loc='" & ComboBox3.Text & "' where prod_barcode =  '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader

            'Event na mag rurun after click ng button
            MessageBox.Show("Product Successfully Updated")
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            table_refresh()

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Barcode()
    End Sub
End Class
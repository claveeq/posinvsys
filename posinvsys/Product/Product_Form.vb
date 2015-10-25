Imports MySql.Data.MySqlClient
Imports CoreScanner

Imports System.IO
Imports System.Data
Imports System.Reflection
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing

Public Class Product_Form
    Dim loc As Point ' to move windows

    Dim Reader As MySqlDataReader 'MySQL Commands
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Dim Query As String

    Dim dbdataset As New DataTable  'For Datagridview 
    Dim DV As New DataView(dbdataset) 'For Search Filter

    Dim cCoreScannerClass As New CCoreScanner 'Instantiating Barcode scanner class

    'for windows to move
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
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub refresh_all()
        Controls.Clear()
        InitializeComponent()
        combobox_brand()
        combobox_loc()
        combobox_type()
        retail_visible()
        table_refresh()
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        refresh_all()
        Barcode()
    End Sub
    Public Sub combobox_type()       'for populating the type,brand,location combobox
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'For the table
        Dim bsource As New BindingSource 'For the table

        ComboBox1.Items.Clear() 'Product 
        ComboBox4.Items.Clear() 'Search Filter
        variants.ComboBox1.Items.Clear() 'Edit Variants

        Try
            MysqlConn.Open()
            Query = "select * from rmarquez.type;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            ComboBox4.Items.Add("")
            While Reader.Read
                Dim stype = Reader.GetString("type_name")
                ComboBox1.Items.Add(stype)
                ComboBox4.Items.Add(stype)
                variants.ComboBox1.Items.Add(stype)
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
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter
        Dim bsource As New BindingSource

        ComboBox2.Items.Clear()
        ComboBox5.Items.Clear()
        variants.ComboBox2.Items.Clear()

        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from brand;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            ComboBox5.Items.Add("")
            While Reader.Read
                Dim sbrand = Reader.GetString("brand_name")
                ComboBox2.Items.Add(sbrand)
                ComboBox5.Items.Add(sbrand)
                variants.ComboBox2.Items.Add(sbrand)
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
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table

        ComboBox3.Items.Clear()
        ComboBox6.Items.Clear()
        variants.ComboBox3.Items.Clear()
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from location;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            ComboBox6.Items.Add("")
            While Reader.Read
                Dim sloc = Reader.GetString("loc_name")
                ComboBox3.Items.Add(sloc)
                ComboBox6.Items.Add(sloc)
                variants.ComboBox3.Items.Add(sloc)
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
        Dim Sda As New MySqlDataAdapter
        Dim bsource As New BindingSource
        dbdataset.Clear()
        Try
            MysqlConn.Open()
            Query =
            "SELECT product.prod_barcode AS Barcode,product.prod_name AS Product, product.prod_description as Description, product.prod_type AS Type, product.prod_brand AS Brand, product.prod_loc as Location, pricing.price_price as Price FROM product INNER JOIN pricing ON product.prod_barcode = pricing.price_barcode ORDER BY product.prod_name ASC;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Sda.SelectCommand = COMMAND
            Sda.Fill(dbdataset)
            bsource.DataSource = dbdataset
            DataGridView1.DataSource = bsource
            Sda.Update(dbdataset)
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub retail_visible()
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table
        Try 'Admin Rights -----------------------------------------------------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            MysqlConn.Open()
            Query = "SELECT * FROM account where acc_name = '" & Login_Form.TextBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim permission As Integer = Reader.GetInt32("acc_admin")
                If permission = 0 Then
                    Panel5.Location = New Point(0, 505)
                    Button1.Visible = False
                    Button2.Visible = False
                    Button5.Visible = False
                    Button8.Visible = False
                    Label12.Visible = False
                    Label16.Visible = False
                    Label23.Visible = False
                    Label24.Visible = False
                    TextBox1.Enabled = False
                    RichTextBox1.Enabled = False
                    TextBox6.Enabled = False
                    TextBox7.Enabled = False
                    TextBox3.Enabled = False
                    TextBox4.Enabled = False

                    ComboBox1.DropDownStyle = ComboBoxStyle.Simple
                    ComboBox2.DropDownStyle = ComboBoxStyle.Simple
                    ComboBox3.DropDownStyle = ComboBoxStyle.Simple
                    ComboBox1.Enabled = False
                    ComboBox2.Enabled = False
                    ComboBox3.Enabled = False

                    Button9.Visible = False
                    Button10.Visible = False
                    Button11.Visible = False
                Else
                    Panel5.Location = New Point(203, 505)
                End If
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or
        ComboBox1.SelectedIndex = -1 Or
        ComboBox2.SelectedIndex = -1 Or
        ComboBox3.SelectedIndex = -1 Or
        TextBox6.Text = "" Or
        TextBox7.Text = "" Or
        TextBox3.Text = "" Or
        TextBox4.Text = "" Then
            MessageBox.Show("Please don't leave an empty value!")
        Else
            Try 'product table
                MysqlConn.Open()
                Dim Query As String
                Query = "Insert into rmarquez.product (prod_name,prod_barcode,prod_description,prod_type,prod_brand,prod_loc) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & RichTextBox1.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "');"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader
                MysqlConn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
            Try 'pricing table
                MysqlConn.Open()
                Dim Query As String
                Query = "Insert into rmarquez.pricing (`price_barcode`, `price_supply`, `price_markup`, `price_price`) VALUES ('" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "', '" & Label3.Text & "');"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader
                MysqlConn.Close()
            Catch ex As MySqlException
            Finally
                MysqlConn.Dispose()
            End Try
            Try 'inventory table
                MysqlConn.Open()
                Dim Query As String
                Query = "Insert into rmarquez.inventory (`inv_barcode`, `inv_stock`, `inv_ropoint`, `inv_roamount`) VALUES ('" & TextBox2.Text & "', '" & TextBox6.Text & "', '" & TextBox7.Text & "', '0');"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader
                MessageBox.Show("Product Successfully Added")
                MysqlConn.Close()
            Catch ex As MySqlException
            Finally
                MysqlConn.Dispose()
                TextBox1.Clear()
                RichTextBox1.Clear()
                ComboBox1.SelectedIndex = -1
                ComboBox2.SelectedIndex = -1
                ComboBox3.SelectedIndex = -1
                TextBox3.Clear()
                TextBox4.Clear()
                Label3.Text = ""
                TextBox6.Clear()
                TextBox7.Clear()
                TextBox2.Clear()

                refresh_all()
            End Try
        End If

    End Sub


    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox1.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox1.SelectedIndex = lastitem - 1 Then
            variants.Show()
            ComboBox1.SelectedIndex = -1
        End If


    End Sub

    Private Sub ComboBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox2.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox2.SelectedIndex = lastitem - 1 Then
            variants.Show()
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Private Sub ComboBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.TextChanged
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox3.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox3.SelectedIndex = lastitem - 1 Then
            variants.Show()
            ComboBox3.SelectedIndex = -1
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
    Private Sub OnBarcodeEvent(ByVal eventType As Short, ByRef pscanData As String) ' eventfunction for barcode_scanner
        Dim barcode As String = pscanData
        Me.Invoke(DirectCast(Sub() TextBox10.Text = barcode, MethodInvoker))
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

        get_value()

    End Sub
    Private Sub get_value()


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
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            ComboBox3.SelectedIndex = -1
        End If
        Try
            MysqlConn.Open()
            Query =
            "SELECT * FROM pricing WHERE price_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                TextBox3.Text = Reader.GetString("price_supply")
                TextBox4.Text = Reader.GetString("price_markup")
                Label3.Text = Reader.GetString("price_price")
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Try
            MysqlConn.Open()
            Query =
            "SELECT * FROM inventory WHERE inv_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                TextBox6.Text = Reader.GetString("inv_stock")
                TextBox7.Text = Reader.GetString("inv_ropoint")
            End While
            MysqlConn.Close() 'default
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click

        TextBox2.Clear()


        TextBox1.Clear()
        RichTextBox1.Clear()

        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1

        TextBox6.Clear()
        TextBox7.Clear()

        TextBox3.Clear()
        TextBox4.Clear()
        Label3.Text = "Total Price"

        table_refresh()
        Barcode()
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
            If TextBox1.Text = "" Then
                ComboBox1.SelectedIndex = -1
                ComboBox2.SelectedIndex = -1
                ComboBox3.SelectedIndex = -1
            End If

            Try
                MysqlConn.Open()
                Query =
                "SELECT * FROM pricing WHERE price_barcode = '" & TextBox2.Text & "';"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader
                While Reader.Read
                    TextBox3.Text = Reader.GetString("price_supply")
                    TextBox4.Text = Reader.GetString("price_markup")
                    Label3.Text = Reader.GetString("price_price")
                End While
                MysqlConn.Close() 'default
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
            Try
                MysqlConn.Open()
                Query =
                "SELECT * FROM inventory WHERE inv_barcode = '" & TextBox2.Text & "';"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                Reader = COMMAND.ExecuteReader
                While Reader.Read
                    TextBox6.Text = Reader.GetString("inv_stock")
                    TextBox7.Text = Reader.GetString("inv_ropoint")
                End While
                MysqlConn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
        End If

        ' record_edit()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        main_menu.Show()
        Me.Hide()

    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim main_m As New main_menu
        main_m.Show()
        Me.Hide()
    End Sub
    Private Sub retail()
        Dim markup As Double = (Val(TextBox3.Text) * (Val(TextBox4.Text) / 100))
        Dim retailprice As Double = Val(TextBox3.Text) + markup
        Label3.Text = Format(retailprice, "0.00")
        If TextBox3.Text = "" And TextBox4.Text = "" Then
            Label3.Text = "Final Price"
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        retail()

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        retail()

    End Sub


    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        refresh_all()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        If TextBox1.Text = "" Then
            TextBox3.Clear()
            TextBox4.Clear()
            Label3.Text = "Finale Price"
            TextBox6.Clear()
            TextBox7.Clear()
        End If
        '  record_edit()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "update product set prod_name='" & TextBox1.Text & "',prod_description='" & RichTextBox1.Text & "',prod_type='" & ComboBox1.Text & "',prod_brand='" & ComboBox2.Text & "',prod_loc='" & ComboBox3.Text & "' where prod_barcode =  '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            ComboBox3.SelectedIndex = -1
        End Try
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "update inventory set inv_stock='" & TextBox6.Text & "',inv_ropoint='" & TextBox7.Text & "' where inv_barcode =  '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            TextBox6.Clear()
            TextBox7.Clear()
        End Try
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "update pricing set price_supply='" & TextBox3.Text & "',price_markup='" & TextBox4.Text & "',price_price='" & Label3.Text & "' where price_barcode =  '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MessageBox.Show("Product Successfully Updated")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            TextBox3.Clear()
            TextBox4.Clear()
            Label3.Text = "Final Price"

            table_refresh()
            Barcode()
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "delete from product where prod_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "delete from inventory where inv_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "delete from pricing where price_barcode = '" & TextBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Label3.Text = "Final Price"
            MessageBox.Show("Product Successfully Deleted")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            ComboBox3.SelectedIndex = -1
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox3.Clear()
            TextBox4.Clear()

            refresh_all()
        End Try
    End Sub
    Private Sub record_edit() 'Enable / Disable 
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * FROM product INNER JOIN inventory ON prod_barcode = inv_barcode INNER JOIN pricing ON price_barcode = prod_barcode where prod_barcode = '" & TextBox2.Text & "';"
            ' Query = "select * From product"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim name As String = Reader.GetString("prod_name")
                Dim des_data As String = Reader.GetString("prod_description")
                Dim type_data As String = Reader.GetString("prod_type")
                Dim brand_data As String = Reader.GetString("prod_brand")
                Dim loc_data As String = Reader.GetString("prod_loc")
                '  Dim stock_data As String = Reader.GetString("inv_stock")
                '  Dim ropoint_data As String = Reader.GetString("inv_ropoint")
                '  Dim supply_data As String = Reader.GetString("price_supply")
                '  Dim markup_data As String = Reader.GetString("price_markup")
                If TextBox1.Text <> name Or
                   RichTextBox1.Text <> des_data Or
                    ComboBox1.Text <> type_data Or
                    ComboBox2.Text <> brand_data Or
                    ComboBox3.Text <> loc_data Then
                    Button5.Enabled = True
                    Button5.BackgroundImage = My.Resources.edit_prod
                Else
                    Button5.Enabled = False
                    Button5.BackgroundImage = My.Resources.edit_prod_inactive
                End If
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        variants.Show()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        '   record_edit()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        '   record_edit()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        '  record_edit()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        '   record_edit()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        TextBox1.Clear()
        RichTextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        Label3.Text = ""
        table_refresh()

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Numbers Only!")
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Numbers Only!")
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Numbers Only!")
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Numbers Only!")
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        Dim num1 As Double = Val(TextBox6.Text)
        Dim num2 As Double = Val(TextBox7.Text)
        If num1 < num2 Then
            MessageBox.Show("Please enter a value below the Current Stock!")
            TextBox7.Clear()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        combobox_type()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        combobox_brand()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        combobox_loc()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        table_refresh()

        'Creating iTextSharp Table from the DataTable data
        Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
        pdfTable.DefaultCell.Padding = 3
        pdfTable.WidthPercentage = 100
        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT
        pdfTable.DefaultCell.BorderWidth = 1

        'Adding Header row
        For Each column As DataGridViewColumn In DataGridView1.Columns
            Dim FontColour = New BaseColor(240, 240, 240)
            Dim MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour)
            Dim cell As New PdfPCell(New Phrase(column.HeaderText, MyFont))
            pdfTable.AddCell(cell)
        Next

        'Adding DataRow
        For Each row As DataGridViewRow In DataGridView1.Rows
            For Each cell As DataGridViewCell In row.Cells
                If (Not (cell.Value) Is Nothing) Then
                    pdfTable.AddCell(cell.Value.ToString)
                End If
            Next
        Next

        'Exporting to PDF
        Dim folderPath As String = "C:\Users\EQ\Desktop\"
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Using stream As New FileStream(folderPath & "Inventory_" & Now.Year & "-" & Now.Month & "-" & Now.Day & ".pdf", FileMode.Create)
            Dim pdfDoc As New Document(PageSize.A2, 10.0F, 10.0F, 10.0F, 0.0F)
            PdfWriter.GetInstance(pdfDoc, stream)
            pdfDoc.Open()
            pdfDoc.Add(pdfTable)
            pdfDoc.Close()
            stream.Close()
        End Using

        MessageBox.Show("Successfully Exported. Please Go to Desktop.")
    End Sub
End Class
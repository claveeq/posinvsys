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
    Dim Bar As String

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
        Barcode()

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
            TextBox1.Clear()
            TextBox2.Clear()
            RichTextBox1.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""

            table_refresh()
            Barcode()

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
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
        Catch exp As Exception
            Console.WriteLine(("Something wrong please check... " + exp.Message))
        End Try
    End Sub
    Public Sub OnBarcodeEvent(ByVal eventType As Short, ByRef pscanData As String) ' eventfunction for barcode_scanner
        Dim barcode As String = pscanData
        Me.Invoke(DirectCast(Sub() TextBox10.Text = barcode, MethodInvoker))

    End Sub

    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        TextBox2.Clear()
    End Sub
    Public Sub trim_rawdata() 'rawdata ng barcode label
        Dim raw As String
        raw = TextBox10.Lines(9).ToString()
        Bar = raw.Replace("0x3", "")
        Bar = Bar.Replace("<datalabel>", "")
        Bar = Bar.Replace("</datalabel>", "")
        Bar = Bar.Replace(" ", "")
        Bar = Bar.Trim()
        TextBox2.Text = Bar
    End Sub
    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        trim_rawdata()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        table_refresh()
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        DV.RowFilter = String.Format("Barcode Like '%" & TextBox2.Text & "%'")
        DataGridView1.DataSource = DV
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Bar = TextBox2.Text
        TextBox3.Text = Bar
        table_refresh()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
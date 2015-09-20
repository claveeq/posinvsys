Imports MySql.Data.MySqlClient
Imports CoreScanner
Public Class Add_Product_Form

    Dim loc As Point ' to move windows

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String

    Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
    Dim bsource As New BindingSource 'bago tong tatlo for the table
    Dim dbdataset As New DataTable  'bago tong tatlo for the table

    Dim DV As New DataView(dbdataset) 'for search filter


    Dim Bar As String 'raw data
    Private Sub Add_Product_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        combobox_type()
        combobox_brand()
        combobox_loc()

    End Sub
    Public Sub combobox_type()

        Dim Sda As New MySqlDataAdapter 'bago tong tatlo for the table
        Dim bsource As New BindingSource 'bago tong tatlo for the table
        Dim dbdataset As New DataTable  'bago tong tatlo for the table
        'for populating the type,brand,location combobox
        ComboBox1.Items.Clear() 'para reset all items
        Product_Form.ComboBox4.Items.Clear()
        Try
            MysqlConn.Open()
            Query = "select * from rmarquez.type;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim stype = Reader.GetString("type_name")
                ComboBox1.Items.Add(stype)
                Product_Form.ComboBox4.Items.Add(stype)
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
        Product_Form.ComboBox5.Items.Clear()
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from brand;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sbrand = Reader.GetString("brand_name")
                ComboBox2.Items.Add(sbrand)
                Product_Form.ComboBox5.Items.Add(sbrand)
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
        Product_Form.ComboBox6.Items.Clear()
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "select * from location;"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sloc = Reader.GetString("loc_name")
                ComboBox3.Items.Add(sloc)
                Product_Form.ComboBox6.Items.Add(sloc)
            End While
            ComboBox3.Items.Add("Add Loc")
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
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

            Call Product_Form.table_refresh()
            Call Product_Form.Barcode()

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox1.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox1.SelectedIndex = lastitem - 1 Then
            addtype.Show()
        End If
    End Sub

    Private Sub ComboBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox2.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox2.SelectedIndex = lastitem - 1 Then
            addbrand.Show()
        End If
    End Sub

    Private Sub ComboBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lastitem As Integer = 0 'para malaman ung last item/index value ng mga items sa combo box 1
        lastitem = ComboBox3.Items.Count
        'para mag show ung addtype na from pag click ung add type sa combobox
        If ComboBox3.SelectedIndex = lastitem - 1 Then
            addloc.Show()
        End If
    End Sub

    Private Sub Panel6_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel6.Paint

    End Sub
End Class
Imports MySql.Data.MySqlClient
Public Class variants
    Dim loc As Point ' for movable window
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Private Sub variants_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "insert into rmarquez.type (type_name) values('" & ComboBox1.Text & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Call Product_Form.combobox_type()
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "insert into rmarquez.brand (brand_name) values('" & ComboBox2.Text & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Call Product_Form.combobox_brand()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox2.Text = ""
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "insert into rmarquez.location (loc_name) values('" & ComboBox3.Text & "');"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Call Product_Form.combobox_loc()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox3.Text = ""
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "DELETE FROM `rmarquez`.`type` WHERE `type_name`='" & ComboBox1.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Call Product_Form.combobox_type()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox1.Text = ""
        End Try
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "DELETE FROM `rmarquez`.`brand` WHERE `brand_name`='" & ComboBox2.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Call Product_Form.combobox_brand()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox2.Text = ""
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim Query As String
            Query = "DELETE FROM `rmarquez`.`location` WHERE `loc_name`='" & ComboBox3.Text & "';"
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            Call Product_Form.combobox_loc()
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            ComboBox3.Text = ""
        End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call Product_Form.combobox_type()
        Me.Hide()
    End Sub
End Class
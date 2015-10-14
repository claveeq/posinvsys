Imports MySql.Data.MySqlClient
Public Class Reports_Form
    Dim loc As Point ' for movable window

    Dim Reader As MySqlDataReader
    Dim MysqlConn As MySqlConnection 'MySQL
    Dim COMMAND As MySqlCommand     'MySQL
    Dim Query As String
    Private Sub Reports_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=localhost;userid=root;password=1234;database=rmarquez"

        ComboBox1.Items.Add("Today")
        ComboBox1.Items.Add("Previous Day")
        ComboBox1.Items.Add("Week")
        ComboBox1.Items.Add("Month")
        ComboBox1.Items.Add("Year")
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        If ComboBox1.Text = "Today" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date = DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Previous Day" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date = DATE_FORMAT((NOW() - INTERVAL 1 DAY),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Week" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 7 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Month" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 30 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        ElseIf ComboBox1.Text = "Year" Then
            Query = "SELECT SUM(rec_total) as sales,SUM(rec_cog) as cost FROM receipt where rec_date between  DATE_FORMAT((NOW() - INTERVAL 365 DAY),'%Y-%m-%d') and DATE_FORMAT(NOW(),'%Y-%m-%d');"
        End If
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Query, MysqlConn)
            Reader = COMMAND.ExecuteReader
            While Reader.Read
                Dim sales = Reader.GetString("sales")
                Dim cost = Reader.GetString("cost")
                Label6.Text = sales
                Label3.Text = cost
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
    Private Sub gross_calc()
        
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        Dim num1 As Double = Val(Label6.Text)
        Dim num2 As Double = Val(Label3.Text)
        Dim answer As Double = num1 - num2
        Label8.Text = answer
    End Sub
End Class
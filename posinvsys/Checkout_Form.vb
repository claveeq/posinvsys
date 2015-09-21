Public Class Checkout_Form
    Dim loc As Point ' for movable window
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
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized 'minimize button
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Checkout_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListView1.View = System.Windows.Forms.View.Details
        ' Adding ListView Columns
        ListView1.Columns.Add("Product", 500, HorizontalAlignment.Left)
        ListView1.Columns.Add("Price", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("QTY", 40, HorizontalAlignment.Left)
        ListView1.Columns.Add("Remove", 55, HorizontalAlignment.Left)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        main_menu.Show()
        Me.Hide()
    End Sub
End Class
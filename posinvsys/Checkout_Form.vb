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



    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        main_menu.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button5.Click
        populate()

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button5.Click
        DataGridView1.Rows.Clear()
    End Sub
    Private Sub populate()
        DataGridView1.Rows.Clear()

        DataGridView1.ColumnCount = 3
        DataGridView1.Columns(0).Name = "Product"
        DataGridView1.Columns(1).Name = "QTY"
        DataGridView1.Columns(2).Name = "Price"



        Dim imgcoll As DataGridViewImageColumn = New DataGridViewImageColumn()
        imgcoll.HeaderText = "photo"
        imgcoll.Name = "image"
        DataGridView1.Columns.Add(imgcoll)

        Dim img As Image = Image.FromFile("D:\Clash\Doucuments\Clave\SAD\posinvsys\posinvsys\Image\cart.png")
        Dim row As Object() = New Object() {"dfsdf", "wer", "wer", img}
        DataGridView1.Rows.Add(row)



    End Sub
  
End Class
Public Class NFlvl1

    Private Sub NFlvl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Charges = 2 Then
            charg.Text = "2"
        End If

        If Charges = 1 Then
            charg.Text = "1"
        End If

        If Charges = 0 Then
            charg.Text = "0"
            Replay.Visible = False
        End If
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub

    Private Sub Menu_Click(sender As Object, e As EventArgs) Handles Menu.Click
        Me.Close()
        MenuLvl.Show()
    End Sub

    Private Sub Replay_Click(sender As Object, e As EventArgs) Handles Replay.Click
        Me.Close()
        Lvl1.Show()
    End Sub
End Class
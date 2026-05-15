Public Class Flvl9
    Private Sub Menu_Click(sender As Object, e As EventArgs) Handles Menu.Click
        Me.Close()
        MenuLvl.Show()
    End Sub

    Private Sub Replay_Click(sender As Object, e As EventArgs) Handles Replay.Click
        Me.Close()
        Lvl9.Show()
    End Sub

    Private Sub Flvl9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub
End Class
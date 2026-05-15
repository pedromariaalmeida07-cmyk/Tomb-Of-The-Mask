Public Class FinishSM
    Private Sub FinishSM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
        SRMODE.Show()
    End Sub

    Private Sub Replay_Click(sender As Object, e As EventArgs) Handles Replay.Click
        Me.Close()
        SpeedRunMode.Show()
    End Sub

    Private Sub FCoins_Click(sender As Object, e As EventArgs) Handles FCoins.Click

    End Sub
End Class
Public Class NFlvl2

    Private Sub NFlvl2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateCharges()

        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        Select Case Charges
            Case 0
                charg.Text = "0"
                Replay.Visible = False
                Replay.Enabled = False
            Case 1
                charg.Text = "1"
            Case 2
                charg.Text = "2"
        End Select

        If Charges <= 1 Then
            charg.Text = "0"
            Replay.Visible = False
            Replay.Enabled = False
        End If

    End Sub

    Private Sub Menu_Click(sender As Object, e As EventArgs) Handles Menu.Click
        Me.Close()
        MenuLvl.Show()
    End Sub

    Private Sub Replay_Click(sender As Object, e As EventArgs) Handles Replay.Click
        Me.Close()
        Lvl2.Show()
    End Sub
    Public Sub UpdateCharges()
        charg.Text = $"{Charges}"

        ' Oculta o botão Replay se Charges for 0
        Replay.Visible = (Charges > 0)
        Replay.Enabled = (Charges > 0)
    End Sub
End Class
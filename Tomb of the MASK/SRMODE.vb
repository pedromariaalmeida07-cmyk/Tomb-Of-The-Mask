Public Class SRMODE

    Private Sub Coins_Click(sender As Object, e As EventArgs) Handles Coins.Click

    End Sub

    Private Sub SRMODE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        ' Exibir valor inicial de Coins
        Coins.Text = $"{NCoins}"
        Lblshields.Text = $"{Shield}"

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click 'jogar
        Me.Hide()
        SpeedRunMode.Show()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click ' opções
        Me.Hide()
        Borring.Show()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click ' defenições
        Settings.Show()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click 'niveis
        Me.Hide()
        MenuLvl.Show()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click ' loja 
        Me.Hide()
        Options.Show()
    End Sub
End Class
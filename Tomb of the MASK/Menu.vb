Imports System.Media
Imports System.Threading

Public Class Menu

    Dim music As New SoundPlayer()

    ' Variáveis para a primeira nuvem (movendo-se da direita para a esquerda)
    Dim velocidade As Integer = 5
    Dim direcao As Integer = -1

    ' Variáveis para a segunda nuvem (movendo-se da esquerda para a direita)
    Dim velocidade1 As Integer = 5
    Dim direcao1 As Integer = 1 ' Começa indo para a direita
    Dim velocidade2 As Integer = 5
    Dim direcao2 As Integer = 1 ' Começa indo para a direita

    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuração da música
        music.SoundLocation = Application.StartupPath & "\imgM\sprites\music.wav"
        Try
            music.PlayLooping()
        Catch ex As Exception
            MessageBox.Show("Erro ao carregar música: " & ex.Message)
        End Try

        ' Configuração da janela
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.KeyPreview = True

        ' Configuração das imagens das nuvens
        Try
            Nuvem.Image = Image.FromFile(Application.StartupPath & "\imgM\sprites\cloud2.png")
            Nuvem1.Image = Image.FromFile(Application.StartupPath & "\imgM\sprites\cloud3.png")


        Catch ex As Exception
            MessageBox.Show("Erro ao carregar as imagens das nuvens: " & ex.Message)
        End Try

        ' Configuração dos PictureBox
        Nuvem.SizeMode = PictureBoxSizeMode.StretchImage
        Nuvem.Anchor = AnchorStyles.None

        Nuvem1.SizeMode = PictureBoxSizeMode.StretchImage
        Nuvem1.Anchor = AnchorStyles.None



        ' Configuração do Timer
        TimerUM.Interval = 50
        TimerUM.Enabled = True
        TimerUM.Start()
        AddHandler TimerUM.Tick, AddressOf TimerUM_Tick
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Hide()
        MenuLvl.Show()
    End Sub

    Private Sub Menu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Me.Hide()
        MenuLvl.Show()
    End Sub

    ' Movimento das nuvens
    Private Sub TimerUM_Tick(sender As Object, e As EventArgs) Handles TimerUM.Tick
        ' Movimento da primeira nuvem (da direita para a esquerda)
        Nuvem.Left += velocidade * direcao
        Debug.Print("Posição da Nuvem: " & Nuvem.Left)

        ' Limites da primeira nuvem
        If Nuvem.Left <= 1275 Then
            direcao = 1
        ElseIf Nuvem.Left >= Me.Width - Nuvem.Width - 50 Then
            direcao = -1
        End If

        ' Movimento da segunda nuvem (da esquerda para a direita entre 0 e 773)
        Nuvem1.Left += velocidade1 * direcao1
        Debug.Print("Posição da Nuvem1: " & Nuvem1.Left)

        ' Limites fixos da segunda nuvem
        If Nuvem1.Left <= 0 Then
            direcao1 = 1 ' Muda para a direita
        ElseIf Nuvem1.Left >= 531 Then
            direcao1 = -1 ' Muda para a esquerda
        End If

    End Sub
End Class

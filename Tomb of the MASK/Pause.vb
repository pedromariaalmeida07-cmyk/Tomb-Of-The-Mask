Imports System.Media
Imports System.Net.Security

Public Class Pause
    Private Music As Boolean = True
    Private Sound As Boolean = True

    Private WithEvents CountDown As New Timer()
    Private countdownCounter As Integer = 1
    Private WithEvents CountAnimation As New Timer()

    Private countStart As New SoundPlayer("imgM\sounds\startCD.wav")
    Private countEnd As New SoundPlayer("imgM\sounds\endCD.wav")

    Private Sub Pause_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            countdownCounter = 1
            picCount.Visible = True
            picCount.Image = Image.FromFile("imgM\animations\components\count\3.png")
            CountDown.Start()
        End If
    End Sub

    Private Sub BttExit_Click(sender As Object, e As EventArgs) Handles BttExit.Click
        Me.Close()
        For i As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            Dim frm As Form = Application.OpenForms(i)
            ' Garante que o form esteja visível
            frm.Visible = True
            ' Fecha o formulário
            frm.Hide()
            MenuLvl.Show()
        Next
    End Sub

    Private Sub BttContinue_Click(sender As Object, e As EventArgs) Handles BttContinue.Click
        countdownCounter = 1
        picCount.Visible = True
        picCount.Image = Image.FromFile("imgM\animations\components\count\3.png")
        CountDown.Start()
    End Sub

    Private Sub BttX_Click(sender As Object, e As EventArgs) Handles BttX.Click
        countdownCounter = 1
        picCount.Visible = True
        picCount.Image = Image.FromFile("imgM\animations\components\count\3.png")
        CountDown.Start()
    End Sub
    '-------------------------------------------------------------------------------------------------------
    Private Sub Pause_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.KeyPreview = True
    End Sub

    Public Sub New()
        InitializeComponent()
        picCount.SizeMode = PictureBoxSizeMode.CenterImage
        CountDown.Interval = 1000

        picCount.BringToFront()
    End Sub

    Private Sub CountDown_Tick(sender As Object, e As EventArgs) Handles CountDown.Tick
        countdownCounter += 1

        Select Case countdownCounter
            Case 1
                picCount.Visible = True
            Case 2
                picCount.Image = Image.FromFile("imgM\animations\components\count\2.png")
                'countStart.Play()
            Case 3
                picCount.Image = Image.FromFile("imgM\animations\components\count\1.png")
                'countStart.Play()

            Case 4
                picCount.Visible = False
                CountDown.Stop()
                ' Inicia a animação de fade
                Me.Close()
        End Select
    End Sub


End Class
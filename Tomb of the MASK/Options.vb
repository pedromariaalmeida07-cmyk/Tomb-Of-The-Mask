Public Class Options
    Dim originalImage As Image
    Dim fadeAlpha As Single = 0.0F
    Dim fadeStep As Single = 0.05F   ' Incremento a cada tick (ajuste para suavizar)
    Dim fadeIn As Boolean = True      ' Define se a animação está em fade in (True) ou fade out (False)

    Private WithEvents tmrFade As New Timer()
    Private WithEvents TimerDisplay As New Timer()

    Private Sub Options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Coins.Text = $"{NCoins}"
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        tmrFade.Interval = 50
        TimerDisplay.Interval = 2000

        If NCoins.ToString().Length = 2 Then
            Coins.Location = New Point(1549, 48)
        ElseIf NCoins.ToString().Length = 1 Then
            Coins.Location = New Point(1596, 48)
        End If

    End Sub

    Public Function SetImageOpacity(ByVal image As Image, ByVal opacity As Single) As Image
        Dim bmp As New Bitmap(image.Width, image.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            Dim cm As New Imaging.ColorMatrix()
            cm.Matrix33 = opacity
            Dim ia As New Imaging.ImageAttributes()
            ia.SetColorMatrix(cm)
            g.DrawImage(image, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ia)
        End Using
        Return bmp
    End Function

    Private Sub BuyCHARGES_Click(sender As Object, e As EventArgs) Handles BuyCHARGES.Click
        If Charges >= 3 Then
            originalImage = Image.FromFile("imgM\sprites\limit charges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()

            Exit Sub
        End If

        If NCoins >= 500 Then
            NCoins -= 500
            Coins.Text = $"{NCoins}"
            Charges += 1
            MenuLvl.Coins.Text = $"{NCoins}"
            SRMODE.Coins.Text = $"{NCoins}"
        Else
            originalImage = Image.FromFile("imgM\sprites\no money charges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    Private Sub BuySHIELDS_Click(sender As Object, e As EventArgs) Handles BuySHIELDS.Click
        If Shield >= 3 Then
            originalImage = Image.FromFile("imgM\sprites\limit shields.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()

            Exit Sub
        End If

        If NCoins >= 200 Then
            NCoins -= 200
            Coins.Text = $"{NCoins}"
            MenuLvl.Coins.Text = $"{NCoins}"
            SRMODE.Coins.Text = $"{NCoins}"
            Shield += 1
        Else
            originalImage = Image.FromFile("imgM\sprites\no money shields.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    Private Sub BuyLVL_Click(sender As Object, e As EventArgs) Handles BuyLVL.Click
        If SpecialMode >= 1 Then
            originalImage = Image.FromFile("imgM\sprites\limit sm.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()

            Exit Sub
        End If

        If NCoins >= 100 Then
            NCoins -= 100
            Coins.Text = $"{NCoins}"
            MenuLvl.Coins.Text = $"{NCoins}"
            SRMODE.Coins.Text = $"{NCoins}"
            SpecialMode = 1
            BttMasks.Image = Image.FromFile("imgM\sprites\Masks.png")
            sm = True
            MenuLvl.BttMasks.Image = Image.FromFile("imgM\sprites\Masks.png")
        Else
            originalImage = Image.FromFile("imgM\sprites\no money sm.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Settings.Show()
    End Sub

    Private Sub BttMasks_Click(sender As Object, e As EventArgs) Handles BttMasks.Click
        If sm = False Then
            originalImage = Image.FromFile("imgM\sprites\need sm.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)

            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        Else
            Me.Hide()
            SRMODE.Show()
        End If

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Me.Hide()
        MenuLvl.Show()
    End Sub

    Private Sub PicOPP_Click(sender As Object, e As EventArgs) Handles PicOPP.Click
        Me.Hide()
        Borring.Show()
    End Sub

    Private Sub tmrFade_Tick(sender As Object, e As EventArgs) Handles tmrFade.Tick
        If fadeIn Then
            ' Aumenta a opacidade (fade in)
            fadeAlpha += fadeStep
            If fadeAlpha >= 1.0F Then
                fadeAlpha = 1.0F
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
                tmrFade.Stop()
                ' Inicia o timer para manter a mensagem visível por 2 segundos
                TimerDisplay.Start()
            Else
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
            End If
        Else
            ' Diminui a opacidade (fade out)
            fadeAlpha -= fadeStep
            If fadeAlpha <= 0.0F Then
                fadeAlpha = 0.0F
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
                tmrFade.Stop()
                mesage.Visible = False
            Else
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
            End If
        End If
    End Sub

    ' Evento do Timer que mantém a mensagem visível por 2 segundos
    Private Sub TimerDisplay_Tick(sender As Object, e As EventArgs) Handles TimerDisplay.Tick
        TimerDisplay.Stop()
        ' Após os 2 segundos, inicia a animação de fade out
        fadeIn = False
        tmrFade.Start()
    End Sub

End Class
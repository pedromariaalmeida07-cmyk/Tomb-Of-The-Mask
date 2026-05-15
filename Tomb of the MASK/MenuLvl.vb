Public Class MenuLvl

    Dim originalImage As Image
    Dim fadeAlpha As Single = 0.0F
    Dim fadeStep As Single = 0.05F   ' Incremento a cada tick (ajuste para suavizar)
    Dim fadeIn As Boolean = True      ' True para fade in, False para fade out

    Private WithEvents tmrFade As New Timer()
    Private WithEvents TimerDisplay As New Timer()

    ' Observação: As variáveis globais Charges, NCoins e sm devem estar definidas em outro módulo ou serem propriedades globais.
    ' Também se assume que os controles Lvl1, Lvl2, …, Lvl9, Coins, Mcharges, mesage, BttMasks, Carrocel, PictureBox1, Def, etc. existem no form.

    Private Sub MenuLvl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuração inicial: torna visíveis os botões de nível e outros elementos


        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Lvl1.Enabled = True

        ' Exibir valor inicial de Coins
        Coins.Text = $"{NCoins}"

        If sm = True Then
            BttMasks.Image = Image.FromFile("imgM\sprites\Masks.png")
        End If

        Select Case Charges
            Case 0
                Mcharges.Text = "0"
            Case 1
                Mcharges.Text = "1"
            Case 2
                Mcharges.Text = "2"
            Case 3
                Mcharges.Text = "3"
        End Select
        Mcharges.Text = Charges
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

    ' Botão para abrir o nível 1 (Lvl0)
    Private Sub Lvl0_Click(sender As Object, e As EventArgs) Handles Lvl0.Click
        If Charges > 0 Then
            Dim lvl1Form As New Lvl1
            lvl1Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 2 (a partir do Lvl1)
    Private Sub Lvl1_Click(sender As Object, e As EventArgs) Handles Lvl1.Click
        If Charges > 0 Then
            Dim lvl2Form As New Lvl2
            lvl2Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 3 (a partir do Lvl2)
    Private Sub Lvl2_Click(sender As Object, e As EventArgs) Handles Lvl2.Click
        If Charges > 0 Then
            Dim lvl3Form As New Lvl3
            lvl3Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 4 (a partir do Lvl3)
    Private Sub Lvl3_Click(sender As Object, e As EventArgs) Handles Lvl3.Click
        If Charges > 0 Then
            Dim lvl4Form As New Lvl4
            lvl4Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 5 (a partir do Lvl4)
    Private Sub Lvl4_Click(sender As Object, e As EventArgs) Handles Lvl4.Click
        If Charges > 0 Then
            Dim lvl5Form As New Lvl5
            lvl5Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 6 (a partir do Lvl5)
    Private Sub Lvl5_Click(sender As Object, e As EventArgs) Handles Lvl5.Click
        If Charges > 0 Then
            Dim lvl6Form As New Lvl6
            lvl6Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 7 (a partir do Lvl6)
    Private Sub Lvl6_Click(sender As Object, e As EventArgs) Handles Lvl6.Click
        If Charges > 0 Then
            Dim lvl7Form As New Lvl7
            lvl7Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 8 (a partir do Lvl7)
    Private Sub Lvl7_Click(sender As Object, e As EventArgs) Handles Lvl7.Click
        If Charges > 0 Then
            Dim lvl8Form As New Lvl8
            lvl8Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 9 (a partir do Lvl8)
    Private Sub Lvl8_Click(sender As Object, e As EventArgs) Handles Lvl8.Click
        If Charges > 0 Then
            Dim lvl9Form As New Lvl9
            lvl9Form.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Botão para abrir o nível 10 (a partir do Lvl9)
    Private Sub Lvl9_Click(sender As Object, e As EventArgs) Handles Lvl9.Click
        If Charges > 0 Then
            Lvl10.Show()
            Me.Hide()
        Else
            originalImage = Image.FromFile("imgM\sprites\ECharges.png")
            mesage.Image = SetImageOpacity(originalImage, 0.0F)
            fadeAlpha = 0.0F
            fadeIn = True
            mesage.Visible = True
            tmrFade.Start()
        End If
    End Sub

    ' Outros botões do MenuLvl

    Private Sub Carrocel_Click(sender As Object, e As EventArgs) Handles Carrocel.Click
        Me.Hide()
        Borring.Show()
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Hide()
        Options.Show()
    End Sub

    Private Sub Def_Click(sender As Object, e As EventArgs) Handles Def.Click
        Settings.Show()
    End Sub

    ' Animação de fade para a mensagem de erro
    Private Sub tmrFade_Tick(sender As Object, e As EventArgs) Handles tmrFade.Tick
        If fadeIn Then
            fadeAlpha += fadeStep
            If fadeAlpha >= 1.0F Then
                fadeAlpha = 1.0F
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
                tmrFade.Stop()
                TimerDisplay.Start()
            Else
                mesage.Image = SetImageOpacity(originalImage, fadeAlpha)
            End If
        Else
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

    ' Timer que mantém a mensagem visível por 2 segundos antes de iniciar o fade out
    Private Sub TimerDisplay_Tick(sender As Object, e As EventArgs) Handles TimerDisplay.Tick
        TimerDisplay.Stop()
        fadeIn = False
        tmrFade.Start()
    End Sub

    ' Ajusta a posição do label de Coins conforme o valor (opcional)
    Private Sub PosCoins()
        If NCoins > 9 Then
            Coins.Location = New Point(1600, 43)
        End If
        If NCoins > 99 Then
            Coins.Location = New Point(1545, 43)
        End If
        If NCoins > 999 Then
            Coins.Location = New Point(1530, 43)
        End If
    End Sub

End Class
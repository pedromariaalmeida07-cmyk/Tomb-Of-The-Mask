Imports System.Threading

Public Class Form1
    Private WithEvents timer As New System.Windows.Forms.Timer()

    Private currentOpacity As Double = 1.0 ' 100% de opacidade

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

        Thread.Sleep(2000)

        timer.Interval = 10
        timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        currentOpacity -= 0.01 ' Reduz 1% por tick


        If currentOpacity > 0.8 Then

            Me.BackgroundImage = AdjustImageOpacity(Me.BackgroundImage, currentOpacity)
        Else

            timer.Stop()
            Me.BackColor = Color.Black
            Me.Hide()
            Menu.Show()
        End If
    End Sub

    ' Função para ajustar a opacidade de uma imagem (PEDIR AO CHAT GPT PARA EXPLICAR)
    Private Function AdjustImageOpacity(image As Image, opacity As Double) As Image
        Dim bmp As New Bitmap(image.Width, image.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            Dim matrix As New Imaging.ColorMatrix()
            matrix.Matrix33 = CSng(opacity) ' Define a opacidade
            Dim attributes As New Imaging.ImageAttributes()
            attributes.SetColorMatrix(matrix, Imaging.ColorMatrixFlag.Default, Imaging.ColorAdjustType.Bitmap)
            g.DrawImage(image, New Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes)
        End Using
        Return bmp
    End Function
End Class

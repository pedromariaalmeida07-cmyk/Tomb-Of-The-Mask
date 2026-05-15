Imports System.Drawing.Drawing2D
Imports System.Media
Imports System.Net.Security
Imports System.IO

Public Class Lvl2
    Inherits Form

    Private gridVisible As Boolean = False

    ' Constantes de grid e velocidade
    Private Const gridSize As Integer = 64
    Private Const gridWidth As Integer = 31
    Private Const gridHeight As Integer = 17
    Private Const moveSpeed As Integer = 60
    Private dot As Integer = 0
    Private collisionDirection As Keys = Keys.S
    Private Fcoin As Integer = 0
    Private Fstar As Integer = 0




    ' Mapas e variantes
    Private wallVariants(,) As Integer
    Private map(,) As Integer

    ' Posição do player e destino (em pixels)
    Private playerX As Integer = 2 * gridSize
    Private playerY As Integer = 2 * gridSize
    Private targetX As Integer
    Private targetY As Integer
    '23,12
    Private direction As Keys
    Private moving As Boolean = False

#Region "Timers"
    ' Timers para diversas lógicas
    Private WithEvents moveTimer As New Timer()
    Private WithEvents idleTimer As New Timer()
    Private WithEvents tailRemovalTimer As New Timer()
    Private WithEvents batAnimationTimer As New Timer()
    Private WithEvents dartAnimationTimer As New Timer()
    Private WithEvents dartMoveTimer As New Timer()
    Private WithEvents batMoveTimer As New Timer()
    Private dartActiveTimer As Boolean = True
    Private lastDartGenerationTime As DateTime = DateTime.MinValue
    Private dartGenerationDelay As Integer = 3000 ' 2000 ms = 2 segundos

    Private WithEvents strawAnimationTimer As New Timer()
    Private WithEvents strawMoveTimer As New Timer()
    Private WithEvents PortalAnimationTimer As New Timer()
    Private WithEvents HPAnimationTimer As New Timer()
    Private WithEvents spikeAnimationTimer As New Timer()
    Private WithEvents snakeAnimationTimer As New Timer()
    Private WithEvents snakeMoveTimer As New Timer()
    Private WithEvents snakeAlertTimer As New Timer()
    Private WithEvents snakeRemovalTimer As New Timer()
    Private WithEvents PlatformAnimationTimer As New Timer()
    Private WithEvents PlatformTimer As New Timer()
    Private WithEvents StarTimer As New Timer()
    Private WithEvents DotTimer As New Timer()
    Private WithEvents EndTimer As New Timer()
    Private WithEvents CoinTimer As New Timer()
    Private WithEvents CountDown As New Timer()
    Private countdownCounter As Integer = 0
    Private WithEvents CountAnimation As New Timer()

    Private WithEvents PlatSequenceTimer As New Timer()
    ' Contador para os ticks da sequência (cada tick = 0,5 s)
    Private platformSequenceTickCount As Integer = 0

    Private WithEvents platformFlashTimer As New Timer()
    Private WithEvents NoShieldsTimer As New Timer()
    Private WithEvents YesShieldsTimer As New Timer()
    Private WithEvents FinishTimer As New Timer()

    Private WithEvents NormalPlatformAnimationTimer As New Timer()






#End Region

#Region "Imagens e Variáveis de Animação"
    Private wallImages As New Dictionary(Of Integer, (Image, Image))
    Private floorImage As Image ', starImage As Image, DotImage As Image, coinImage As Image
    Private rand As New Random()

    Private spikesImage As Image
    Private spikesImage1 As Image

    'elementos do mapa
    'star
    Private StarImage() As Image
    Private StarImageIndex As Integer = 0
    Private currentStarImage As Image

    'dot
    Private DotImage() As Image
    Private DotImageIndex As Integer = 0
    Private currentDotImage As Image

    'coin
    Private CoinImage() As Image
    Private CoinImageIndex As Integer = 0
    Private currentCoinImage As Image

    'end
    Private EndImage() As Image
    Private EndImageIndex As Integer = 0
    Private currentEndImage As Image

    ' Imagens do bat
    Private batImage() As Image
    Private batImageIndex As Integer = 0
    Private currentBatImage As Image

    ' Imagens do dart trap (para animação do trap)
    Private dartImage() As Image
    Private dartImageIndex As Integer = 0
    Private currentdartImage As Image

    ' Imagem do projétil (dardo disparado) – caso seja diferente
    Private projectileImage As Image ' opcional se desejar uma imagem distinta
    Private projectileX As Integer = -1
    Private projectileY As Integer = -1
    Private projectileActive As Boolean = False

    Private lastProjectileTime As DateTime = DateTime.MinValue
    Private cooldown As Integer = 10000 ' tempo de espera em milissegundos (1 segundo)

    'Para a straw
    Private strawImage() As Image
    Private strawImageIndex As Integer = 0
    Private currentStrawImage As Image

    'Portal
    Private PortalImage() As Image
    Private PortalImageIndex As Integer = 0
    Private currentPortalImage As Image

    'hiden spikes 
    Private HPImage() As Image
    Private spikeImage As Image
    Private currentHPImage As Image
    Private spikesVisible As Boolean = False ' Controle para os spikes
    Private HPImageIndex As Integer = 0

    Private spikesActive As Boolean = False
    Private spikeAnimationPlaying As Boolean = False
    Private spikeAnimationFrame As Integer = 0   ' 0 = spike1, 1 = spike2, 2 = spike3, 3 = spike4
    Private WithEvents spikeAnimTimer As New Timer()
    Private spikeFrameIndex As Integer = 0   ' 0 = spikes1, 1 = spikes2, 2 = spikes3, 3 = spikes4
    Private spikeAnimationActive As Boolean = False

    'Temp Plat
    Private PlatImage() As Image
    Private PlatImageIndex As Integer = 0
    Private currentPlatImage As Image

    Private platformRemovalScheduled As Boolean = False
    Private platformRemovalX As Integer
    Private platformRemovalY As Integer

    Private storedTargetX As Integer
    Private storedTargetY As Integer

    Private platformTickCount As Integer = 0
    Private PlatImagesStage2() As Image
    Private PlatImagesStage3() As Image

    Private fadeOpacity As Integer = 255 ' Começa com tela totalmente preta
    Private WithEvents fadeTimer As New Timer()
    Private fadePanel As Panel

    ' Players de som para a plataforma
    Private spPlat1 As New SoundPlayer("imgM\animations\traps\platform\p1.wav")
    Private spPlat2 As New SoundPlayer("imgM\animations\traps\platform\p2.wav")
    Private spPlat3 As New SoundPlayer("imgM\animations\traps\platform\p3.wav")

    Private activatedPlatformGroup As New List(Of Point)

    Private platformFlashActive As Boolean = False
    Private platformFlashAlpha As Integer = 200 ' Transparência inicial (0 a 255)


    Private countStart As New SoundPlayer("imgM\sounds\startCD.wav")
    Private countEnd As New SoundPlayer("imgM\sounds\endCD.wav")

#End Region
    Private isPaused As Boolean = False



    Private SnakeIndex As Integer = 1

#Region "Tail Segment"
    Private Class TailSegment
        Public Property X As Integer
        Public Property Y As Integer
        Public Property TailType As Integer  ' 1 = tail1, 2 = tail2
        Public Property TimeStamp As DateTime
        ' Imagem já rotacionada para este segmento
        Public Property RotatedImage As Image
    End Class

    Private tailSegments As New List(Of TailSegment)
    Private lastCellX As Integer = playerX \ gridSize
    Private lastCellY As Integer = playerY \ gridSize
#End Region

#Region "shield"
    Private altTextureActive As Boolean = False
    Private WithEvents altTextureTimer As New Timer()

    ' Variáveis para guardar as texturas originais
    Private originalPlayerSkins() As Image
    Private originalTail1Image As Image
    Private originalTail2Image As Image
    Private originalBall1Image As Image
    Private originalBall2Image As Image

    ' Arrays e imagens alternativas
    ' Alternativas do player (S1.png a S11.png)
    Private alternativePlayerSkins(10) As Image  ' Índices de 0 a 10 correspondem a S1.png a S11.png
    ' Alternativas da tail (tail3.png e tail4.png)
    Private alternativeTail1Image As Image
    Private alternativeTail2Image As Image
    ' Alternativas da ball (ball3.png e ball4.png)
    Private alternativeBall1Image As Image
    Private alternativeBall2Image As Image
#End Region
#Region "portal"
    Private teleported As Boolean = False

    Private flashActive As Boolean = False
    Private flashAlpha As Integer = 200 ' valor inicial para a transparência (0 a 255)
    Private WithEvents flashTimer As New Timer()

#End Region
    Private health As Integer = 1
    Private currentOpacity As Double = 1.0 ' 100% de opacidade
    Private platformAnimationStages As New Dictionary(Of Point, Integer)
    Private platformStages As New Dictionary(Of Point, Integer)
    Private normalPlatformIndex As Integer = 0


#Region "Construtor e Inicialização"
    Public Sub New()
        InitializeComponent()
        Count.SizeMode = PictureBoxSizeMode.CenterImage

        Me.DoubleBuffered = True
        Me.Width = gridWidth * gridSize + 16
        Me.Height = gridHeight * gridSize + 39
        Me.Text = "Level 1"
        Me.KeyPreview = True

        InitializeMap()
        LoadImages()

        ' Posição destino inicial
        targetX = playerX
        targetY = playerY

        ' Configuração dos timers
        moveTimer.Interval = 15
        idleTimer.Interval = 150       ' 150 ms para idle
        tailRemovalTimer.Interval = 20 ' 20 ms para remover tail segments
        batAnimationTimer.Interval = 100 ' 100 ms para bat animation
        batMoveTimer.Interval = 1
        dartAnimationTimer.Interval = 100 ' inicia com 500 ms
        dartMoveTimer.Interval = 1        ' ajuste conforme desejado (15 ms ~66 FPS)
        strawAnimationTimer.Interval = 1
        strawMoveTimer.Interval = 300
        PortalAnimationTimer.Interval = 150
        flashTimer.Interval = 15
        HPAnimationTimer.Interval = 10000  ' Alterna entre block1 e block2 a cada 0,5 segundos
        spikeAnimationTimer.Interval = 1500
        snakeAnimationTimer.Interval = 250
        snakeMoveTimer.Interval = 100
        snakeAlertTimer.Interval = 300
        snakeRemovalTimer.Interval = 50 ' Intervalo em milissegundos para cada remoção
        PlatformAnimationTimer.Interval = 150
        PlatSequenceTimer.Interval = 500
        platformFlashTimer.Interval = 5
        StarTimer.Interval = 150
        DotTimer.Interval = 150
        CoinTimer.Interval = 150
        EndTimer.Interval = 150
        CountDown.Interval = 1000
        NoShieldsTimer.Interval = 2000
        YesShieldsTimer.Interval = 10000


        ' Inicia os timers
        Count.BringToFront()
        CountDown.Start()

        originalPlayerSkins = idleSkins.Clone()
        originalTail1Image = tail1Image
        originalTail2Image = tail2Image
        originalBall1Image = playerImg1
        originalBall2Image = playerImg2

        ' Carrega as imagens alternativas para o player
        alternativePlayerSkins(0) = Image.FromFile("imgM\animations\player\S1.png")
        alternativePlayerSkins(1) = Image.FromFile("imgM\animations\player\S2.png")
        alternativePlayerSkins(2) = Image.FromFile("imgM\animations\player\S3.png")
        alternativePlayerSkins(3) = Image.FromFile("imgM\animations\player\S4.png")
        alternativePlayerSkins(4) = Image.FromFile("imgM\animations\player\S5.png")
        alternativePlayerSkins(5) = Image.FromFile("imgM\animations\player\S6.png")
        alternativePlayerSkins(6) = Image.FromFile("imgM\animations\player\S7.png")
        alternativePlayerSkins(7) = Image.FromFile("imgM\animations\player\S8.png")
        alternativePlayerSkins(8) = Image.FromFile("imgM\animations\player\S9.png")
        alternativePlayerSkins(9) = Image.FromFile("imgM\animations\player\S10.png")
        alternativePlayerSkins(10) = Image.FromFile("imgM\animations\player\S11.png")

        ' Carrega as imagens alternativas para a tail
        alternativeTail1Image = Image.FromFile("imgM\animations\player\tail3.png")
        alternativeTail2Image = Image.FromFile("imgM\animations\player\tail4.png")

        ' Carrega as imagens alternativas para a ball
        alternativeBall1Image = Image.FromFile("imgM\animations\player\ball3.png")
        alternativeBall2Image = Image.FromFile("imgM\animations\player\ball4.png")

        ' Configura o timer para 10 segundos (10000 ms)
        altTextureTimer.Interval = 10000

        NormalPlatformAnimationTimer.Interval = 200 ' Tempo entre frames (ajuste se necessário)
        NormalPlatformAnimationTimer.Start()

    End Sub
    Private Sub IniciarJogo()


    End Sub

#End Region
#Region "mov Straw"


    Public Class Node
        Public Property X As Integer
        Public Property Y As Integer
        Public Property G As Integer ' custo do caminho percorrido
        Public Property H As Integer ' heurística (distância até o destino)
        Public Property F As Integer ' G + H
        Public Property Parent As Node
    End Class
#End Region
#Region "Snake"
    'snake
    Private snakeImage() As Image
    Private currentsnakeImage As Image
    Private snakeHeadFrames() As Image
    Private snakeHeadFrameIndex As Integer = 0
    Dim hiddenSnakeImage As Image

    Private trapActivated As Boolean = False
    Private snakeSpeed As Integer = 40

    Private trapActivationZone As Rectangle = New Rectangle(2, 2, 2, 3)   ' área onde o player ativa a trap
    Private snakeInitialPos As Point = New Point(2, 8)                      ' posição inicial da snake
    Private snakeHeadPos As Point                                           ' posição atual da cabeça
    Private snakeBody As New List(Of Point)                                 ' lista de posições dos segmentos do corpo

    ' Variáveis para a direção e sentido da snake
    ' Conforme a combinação:
    '   snakeDirection = False e snakeSense = False: direita e horizontal (default)
    '   snakeDirection = True  e snakeSense = False: esquerda e horizontal 
    '   snakeDirection = True  e snakeSense = True:  de baixo para cima e vertical
    '   snakeDirection = False e snakeSense = True:  de cima para baixo e vertical
    Private snakeDirection As Boolean = False   ' false = direita (default)
    Private snakeSense As Boolean = False         ' false = horizontal (default)
    Private snakeLength As Integer = 25

    Private snakeRemovalStarted As Boolean = False
    Private headRemoved As Boolean = False
    Private snakeActive As Boolean = False   ' Indica se a snake trap está ativa
    Private alertFrameCount As Integer = 0     ' Contador dos ticks de alerta

    Dim headFrames As Integer() = {0, 1, 2} ' Índices correspondentes às imagens snake1, snake11 e snake12
    Dim headFrameIndex As Integer = 0 ' Índice atual da animação

    ' Imagens e skins do player
    Private playerImg1 As Image, playerImg2 As Image
    Private idleSkins() As Image
    Private idleSkinIndex As Integer = 0
    Private currentPlayerImage As Image

    Private playerAngle As Integer = 0
    Private tailAngle As Single = 0

    ' Variáveis de tempo para movimento do player
    Private movementStartTime As DateTime
    Private totalMovementTime As Double

    ' Imagens da tail
    Private tail1Image As Image, tail2Image As Image

    ' Para o bat



#End Region



#Region "Métodos Auxiliares"
    ' Rotaciona uma imagem em um ângulo dado
    Private Function RotateImage(img As Image, angle As Single) As Image
        Dim rotatedBmp As New Bitmap(img.Width, img.Height)
        rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution)
        Using g As Graphics = Graphics.FromImage(rotatedBmp)
            g.TranslateTransform(img.Width / 2, img.Height / 2)
            g.RotateTransform(angle)
            g.TranslateTransform(-img.Width / 2, -img.Height / 2)
            g.DrawImage(img, New Point(0, 0))
        End Using
        Return rotatedBmp

        rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution)
        Using g As Graphics = Graphics.FromImage(rotatedBmp)
            g.TranslateTransform(img.Width / 2, img.Height / 2)
            g.RotateTransform(angle)
            g.TranslateTransform(-img.Width / 2, -img.Height / 2)
            g.DrawImage(img, New Point(0, 0))
        End Using
        Return rotatedBmp
    End Function
#End Region

#Region "Carregamento de Imagens e Mapa"
    Private Sub LoadImages()
        Try
            ' Carrega paredes e variantes
            wallImages(1) = (Image.FromFile("imgM\walls\wall1.png"), Image.FromFile("imgM\walls\wall11.png"))
            wallImages(2) = (Image.FromFile("imgM\walls\wall2.png"), Image.FromFile("imgM\walls\wall22.png"))
            wallImages(3) = (Image.FromFile("imgM\walls\wall3.png"), Image.FromFile("imgM\walls\wall33.png"))
            wallImages(4) = (Image.FromFile("imgM\walls\wall4.png"), Image.FromFile("imgM\walls\wall44.png"))
            wallImages(5) = (Image.FromFile("imgM\walls\wall5.png"), Image.FromFile("imgM\walls\wall55.png"))
            wallImages(6) = (Image.FromFile("imgM\walls\wall6.png"), Image.FromFile("imgM\walls\wall66.png"))
            wallImages(7) = (Image.FromFile("imgM\walls\wall7.png"), Image.FromFile("imgM\walls\wall77.png"))
            wallImages(8) = (Image.FromFile("imgM\walls\wall8.png"), Image.FromFile("imgM\walls\wall88.png"))
            wallImages(20) = (Image.FromFile("imgM\walls\wall9.png"), Image.FromFile("imgM\walls\wall99.png"))
            wallImages(21) = (Image.FromFile("imgM\walls\wall10.png"), Image.FromFile("imgM\walls\wall1010.png"))
            wallImages(22) = (Image.FromFile("imgM\walls\wall111.png"), Image.FromFile("imgM\walls\wall111.png"))
            wallImages(23) = (Image.FromFile("imgM\walls\wall112.png"), Image.FromFile("imgM\walls\wall112.png"))
            wallImages(24) = (Image.FromFile("imgM\walls\wall113.png"), Image.FromFile("imgM\walls\wall113.png"))
            wallImages(25) = (Image.FromFile("imgM\walls\wall114.png"), Image.FromFile("imgM\walls\wall114.png"))
            wallImages(26) = (Image.FromFile("imgM\walls\wall9999.png"), Image.FromFile("imgM\walls\wall9999.png"))
            wallImages(69) = (Image.FromFile("imgM\walls\wall69.png"), Image.FromFile("imgM\walls\wall69.png"))


            floorImage = Image.FromFile("imgM\walls\blanc.png")

            ' Player imagens
            playerImg1 = Image.FromFile("imgM\animations\player\ball1.png")
            playerImg2 = Image.FromFile("imgM\animations\player\ball2.png")

            ' Idle skins
            idleSkins = New Image(10) {}
            idleSkins(0) = Image.FromFile("imgM\animations\player\skin1.png")
            idleSkins(1) = Image.FromFile("imgM\animations\player\skin2.png")
            idleSkins(2) = Image.FromFile("imgM\animations\player\skin3.png")
            idleSkins(3) = Image.FromFile("imgM\animations\player\skin4.png")
            idleSkins(4) = Image.FromFile("imgM\animations\player\skin5.png")
            idleSkins(5) = Image.FromFile("imgM\animations\player\skin6.png")
            idleSkins(6) = Image.FromFile("imgM\animations\player\skin7.png")
            idleSkins(7) = Image.FromFile("imgM\animations\player\skin8.png")
            idleSkins(8) = Image.FromFile("imgM\animations\player\skin9.png")
            idleSkins(9) = Image.FromFile("imgM\animations\player\skin10.png")
            idleSkins(10) = Image.FromFile("imgM\animations\player\skin11.png")
            currentPlayerImage = idleSkins(0)

            ' Tail imagens
            tail1Image = Image.FromFile("imgM\animations\player\tail1.png")
            tail2Image = Image.FromFile("imgM\animations\player\tail2.png")

            ' Itens
            ' starImage = Image.FromFile("imgM\animations\.gif\star.gif")
            ' DotImage = Image.FromFile("imgM\animations\dot.png")
            ' CoinImage = Image.FromFile("imgM\animations\.gif\coin.gif")
            'dot
            DotImage = New Image(1) {}
            DotImage(0) = Image.FromFile("imgM\animations\components\dot\dot1.png")
            DotImage(1) = Image.FromFile("imgM\animations\components\dot\dot2.png")
            currentDotImage = DotImage(0)

            StarImage = New Image(4) {}
            StarImage(0) = Image.FromFile("imgM\animations\components\star\star1.png")
            StarImage(1) = Image.FromFile("imgM\animations\components\star\star2.png")
            StarImage(2) = Image.FromFile("imgM\animations\components\star\star3.png")
            StarImage(3) = Image.FromFile("imgM\animations\components\star\star4.png")
            StarImage(4) = Image.FromFile("imgM\animations\components\star\star5.png")
            currentStarImage = StarImage(0)

            CoinImage = New Image(3) {}
            CoinImage(0) = Image.FromFile("imgM\animations\components\coin\coin1.png")
            CoinImage(1) = Image.FromFile("imgM\animations\components\coin\coin2.png")
            CoinImage(2) = Image.FromFile("imgM\animations\components\coin\coin3.png")
            CoinImage(3) = Image.FromFile("imgM\animations\components\coin\coin4.png")
            currentCoinImage = CoinImage(0)

            EndImage = New Image(2) {}
            EndImage(0) = Image.FromFile("imgM\animations\components\end\e1.png")
            EndImage(1) = Image.FromFile("imgM\animations\components\end\e2.png")
            EndImage(2) = Image.FromFile("imgM\animations\components\end\e3.png")
            currentEndImage = EndImage(0)


            ' Bat imagens
            batImage = New Image(7) {}
            batImage(0) = Image.FromFile("imgM\animations\traps\bat\bat1.png")
            batImage(1) = Image.FromFile("imgM\animations\traps\bat\bat2.png")
            batImage(2) = Image.FromFile("imgM\animations\traps\bat\bat3.png")
            batImage(3) = Image.FromFile("imgM\animations\traps\bat\bat4.png")
            batImage(4) = Image.FromFile("imgM\animations\traps\bat\bat5.png")
            batImage(5) = Image.FromFile("imgM\animations\traps\bat\bat6.png")
            batImage(6) = Image.FromFile("imgM\animations\traps\bat\bat7.png")
            batImage(7) = Image.FromFile("imgM\animations\traps\bat\bat8.png")
            currentBatImage = batImage(0)

            ' Dart trap imagens 
            dartImage = New Image(1) {}
            dartImage(0) = Image.FromFile("imgM\animations\traps\dartTrap\dart1.png")
            dartImage(1) = Image.FromFile("imgM\animations\traps\dartTrap\dart2.png")
            currentdartImage = dartImage(0)
            projectileImage = Image.FromFile("imgM\animations\traps\dartTrap\dart.png")

            'Straw images
            strawImage = New Image(8) {}
            strawImage(0) = Image.FromFile("imgM\animations\traps\straw\straw1.png")
            strawImage(1) = Image.FromFile("imgM\animations\traps\straw\straw2.png")
            strawImage(2) = Image.FromFile("imgM\animations\traps\straw\straw3.png")
            strawImage(3) = Image.FromFile("imgM\animations\traps\straw\straw4.png")
            strawImage(4) = Image.FromFile("imgM\animations\traps\straw\straw5.png")
            strawImage(5) = Image.FromFile("imgM\animations\traps\straw\straw6.png")
            strawImage(6) = Image.FromFile("imgM\animations\traps\straw\straw7.png")
            strawImage(7) = Image.FromFile("imgM\animations\traps\straw\straw8.png")
            strawImage(8) = Image.FromFile("imgM\animations\traps\straw\straw9.png")

            currentStrawImage = strawImage(0)

            'Portal images
            PortalImage = New Image(9) {}
            PortalImage(0) = Image.FromFile("imgM\animations\traps\teleport\t1.png")
            PortalImage(1) = Image.FromFile("imgM\animations\traps\teleport\t2.png")
            PortalImage(2) = Image.FromFile("imgM\animations\traps\teleport\t3.png")
            PortalImage(3) = Image.FromFile("imgM\animations\traps\teleport\t4.png")
            PortalImage(4) = Image.FromFile("imgM\animations\traps\teleport\t5.png")
            PortalImage(5) = Image.FromFile("imgM\animations\traps\teleport\t6.png")
            PortalImage(6) = Image.FromFile("imgM\animations\traps\teleport\t7.png")
            PortalImage(7) = Image.FromFile("imgM\animations\traps\teleport\t8.png")
            PortalImage(8) = Image.FromFile("imgM\animations\traps\teleport\t9.png")
            PortalImage(9) = Image.FromFile("imgM\animations\traps\teleport\t10.png")
            currentPortalImage = PortalImage(0)

            spikesImage = Image.FromFile("imgM\sprites\traps\spikes.png")
            spikesImage1 = Image.FromFile("imgM\sprites\traps\spikes1.png")

            'hiden spike
            HPImage = New Image(5) {}
            HPImage(0) = Image.FromFile("imgM\animations\traps\HP\block1.png")
            HPImage(1) = Image.FromFile("imgM\animations\traps\HP\block2.png")
            HPImage(2) = Image.FromFile("imgM\animations\traps\HP\spike1.png")
            HPImage(3) = Image.FromFile("imgM\animations\traps\HP\spike2.png")
            HPImage(4) = Image.FromFile("imgM\animations\traps\HP\spike3.png")
            HPImage(5) = Image.FromFile("imgM\animations\traps\HP\spike4.png")
            currentHPImage = HPImage(0)

            'snake
            snakeImage = New Image(5) {}
            snakeImage(0) = Image.FromFile("imgM\animations\traps\snake\snake1.png")
            snakeImage(1) = Image.FromFile("imgM\animations\traps\snake\snake11.png")
            snakeImage(2) = Image.FromFile("imgM\animations\traps\snake\snake12.png")
            snakeImage(3) = Image.FromFile("imgM\animations\traps\snake\snake2.png")
            snakeImage(4) = Image.FromFile("imgM\animations\traps\snake\block1.png")
            snakeImage(5) = Image.FromFile("imgM\animations\traps\snake\block2.png")
            currentsnakeImage = snakeImage(1)
            hiddenSnakeImage = Image.FromFile("imgM\walls\wall7.png")

            'Temp Plat
            PlatImage = New Image(2) {}
            PlatImage(0) = Image.FromFile("imgM\animations\traps\platform\p1.png")
            PlatImage(1) = Image.FromFile("imgM\animations\traps\platform\p2.png")
            PlatImage(2) = Image.FromFile("imgM\animations\traps\platform\p3.png")
            currentPlatImage = PlatImage(0)

            ' Carregar imagens para o estágio 2 (após 0,5 s – p4, p5, p6)
            PlatImagesStage2 = New Image(2) {}
            PlatImagesStage2(0) = Image.FromFile("imgM\animations\traps\platform\p4.png")
            PlatImagesStage2(1) = Image.FromFile("imgM\animations\traps\platform\p5.png")
            PlatImagesStage2(2) = Image.FromFile("imgM\animations\traps\platform\p6.png")

            ' Carregar imagens para o estágio 3 (após 1,0 s – p7, p8, p9)
            PlatImagesStage3 = New Image(2) {}
            PlatImagesStage3(0) = Image.FromFile("imgM\animations\traps\platform\p7.png")
            PlatImagesStage3(1) = Image.FromFile("imgM\animations\traps\platform\p8.png")
            PlatImagesStage3(2) = Image.FromFile("imgM\animations\traps\platform\p9.png")


        Catch ex As Exception
            MessageBox.Show("Erro ao carregar imagens: " & ex.Message)
        End Try
    End Sub

    Private Sub InitializeMap()
        ReDim map(gridHeight - 1, gridWidth - 1)
        Dim tempMap(,) As Integer = {
  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 18, 2, 0, 0, 0, 0, 0},
    {0, 3, 10, 10, 10, 10, 10, 5, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 9, 2, 0, 0, 0, 0, 0},
    {0, 3, 10, 10, 10, 10, 10, 11, 10, 10, 10, 10, 10, 10, 9, 2, 0, 0, 0, 0, 1, 1, 1, 8, 10, 2, 0, 0, 0, 0, 0},
    {0, 3, 10, 10, 10, 10, 10, 10, 6, 4, 4, 4, 4, 7, 10, 5, 1, 1, 0, 8, 10, 10, 10, 10, 10, 5, 1, 1, 1, 1, 0},
    {0, 0, 7, 10, 10, 10, 10, 10, 2, 0, 0, 0, 0, 8, 10, 10, 10, 11, 21, 0, 10, 10, 10, 10, 10, 10, 10, 10, 11, 19, 0},
    {0, 0, 0, 4, 4, 4, 4, 4, 0, 0, 0, 0, 3, 0, 0, 0, 10, 0, 21, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 19, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 10, 10, 0, 21, 0, 10, 0, 0, 0, 26, 0, 6, 4, 4, 4, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 10, 10, 6, 0, 7, 10, 10, 10, 10, 10, 10, 2, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 7, 0, 10, 2, 0, 0, 4, 4, 4, 7, 10, 0, 2, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 22, 10, 5, 1, 1, 1, 1, 1, 8, 10, 9, 2, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19, 0, 10, 0, 10, 6, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19, 0, 10, 10, 10, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
}


        Array.Copy(tempMap, map, tempMap.Length)

        ReDim wallVariants(gridHeight - 1, gridWidth - 1)
        For y As Integer = 0 To gridHeight - 1
            For x As Integer = 0 To gridWidth - 1
                If map(y, x) >= 1 AndAlso map(y, x) <= 8 Or (map(y, x) >= 20 AndAlso map(y, x) <= 25) Then
                    wallVariants(y, x) = rand.Next(2)
                End If
            Next
        Next
    End Sub
#End Region

#Region "Desenho"
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics
        Dim cellSize As Integer = gridSize
        ' Calcula os offsets para centralizar a grid
        Dim offsetX As Integer = (ClientSize.Width - (gridWidth * cellSize)) \ 2
        Dim offsetY As Integer = (ClientSize.Height - (gridHeight * cellSize)) \ 2

        If Not gridVisible Then
            g.Clear(Color.Black)
        Else
            ' Desenha o mapa com os offsets aplicados
            For y As Integer = 0 To gridHeight - 1
                Dim py As Integer = offsetY + y * cellSize
                For x As Integer = 0 To gridWidth - 1
                    Dim px As Integer = offsetX + x * cellSize
                    Select Case map(y, x)
                        Case 1 To 8
                            If wallImages.ContainsKey(map(y, x)) Then
                                Dim images = wallImages(map(y, x))
                                Dim selectedImage As Image = If(wallVariants(y, x) = 0, images.Item1, images.Item2)
                                g.DrawImage(selectedImage, px, py, cellSize, cellSize)
                            End If
                        Case 0
                            g.DrawImage(floorImage, px, py, cellSize, cellSize)
                        Case 9
                            g.DrawImage(currentStarImage, px, py, cellSize, cellSize)
                        Case 10
                            g.DrawImage(currentDotImage, px, py, cellSize, cellSize)
                        Case 11
                            g.DrawImage(currentCoinImage, px, py, cellSize, cellSize)
                        Case 12
                            g.DrawImage(currentdartImage, px, py, cellSize, cellSize)
                            ' Desenha todos os projéteis gerados pela dart trap
                            For Each proj As Projectile In projectiles
                                g.DrawImage(projectileImage, offsetX + proj.X, offsetY + proj.Y, cellSize, cellSize)
                            Next

                        Case 13
                            g.DrawImage(spikesImage, px, py, cellSize, cellSize)
                        Case 14
                            g.DrawImage(currentHPImage, px, py, cellSize, cellSize)
                            'If (x >= 13 And x <= 16 And y = 1) And (HPImageIndex = 0) Then
            'g.DrawImage(spikeImage, px, py, cellSize, cellSize)
        'End If
                        Case 15
                            ' Durante o alerta (player entrou na zona e a snake ainda não foi iniciada)
                            If trapActivated AndAlso Not snakeActive Then
                                g.DrawImage(currentsnakeImage, px, py, cellSize, cellSize)
                            Else
                                ' Quando a snake já está ativa ou antes da ativação, exibe a imagem original do tile
                                g.DrawImage(hiddenSnakeImage, px, py, cellSize, cellSize)
                            End If

                        Case 16, 17
                            Dim platPoint As New Point(x, y)

                            If map(y, x) = 16 Then
                                ' Plataformas normais (não ativadas) alternam entre p1, p2, p3
                                g.DrawImage(PlatImage(normalPlatformIndex), px, py, cellSize, cellSize)

                            ElseIf map(y, x) = 17 AndAlso platformStages.ContainsKey(platPoint) Then
                                Dim stage As Integer = platformStages(platPoint)
                                Select Case stage
                                    Case 0
                                        ' Estágio inicial da destruição (usa p1-p3)
                                        g.DrawImage(PlatImage(normalPlatformIndex), px, py, cellSize, cellSize)
                                    Case 1
                                        ' Primeiro estágio da destruição (usa p4-p6)
                                        g.DrawImage(PlatImagesStage2(PlatImageIndex Mod PlatImagesStage2.Length), px, py, cellSize, cellSize)
                                    Case Else
                                        ' Segundo estágio da destruição (usa p7-p9)
                                        g.DrawImage(PlatImagesStage3(PlatImageIndex Mod PlatImagesStage3.Length), px, py, cellSize, cellSize)
                                End Select
                            End If
                        Case 18
                            g.DrawImage(currentEndImage, px, py, cellSize, cellSize)
                        Case 19
                            g.DrawImage(spikesImage1, px, py, cellSize, cellSize)
                        Case 20 To 26
                            If wallImages.ContainsKey(map(y, x)) Then
                                Dim images = wallImages(map(y, x))
                                Dim selectedImage As Image = If(wallVariants(y, x) = 0, images.Item1, images.Item2)
                                g.DrawImage(selectedImage, px, py, cellSize, cellSize)
                            End If

                    End Select
                Next
            Next

            ' Desenha os tail segments aplicando os offsets
            For Each tail In tailSegments
                Dim tailRect As New Rectangle(offsetX + tail.X * cellSize, offsetY + tail.Y * cellSize, cellSize, cellSize)
                If tail.RotatedImage IsNot Nothing Then
                    g.DrawImage(tail.RotatedImage, tailRect)
                End If
            Next

            ' Desenha o player com rotação, aplicando os offsets
            Dim playerCenterX As Single = offsetX + playerX + cellSize / 2
            Dim playerCenterY As Single = offsetY + playerY + cellSize / 2
            Dim statePlayer As GraphicsState = g.Save()
            g.TranslateTransform(playerCenterX, playerCenterY)
            g.RotateTransform(playerAngle)
            g.TranslateTransform(-playerCenterX, -playerCenterY)
            g.DrawImage(currentPlayerImage, New Rectangle(offsetX + playerX, offsetY + playerY, cellSize, cellSize))
            g.Restore(statePlayer)

            ' Desenha outros elementos, por exemplo, o bat

            ' Desenha a straw


            ' Desenha os portais

        End If

        ' Se houver flash, ele ocupa toda a tela (não precisa de offset)
        If flashActive Then
            Dim flashColor As Color = Color.FromArgb(flashAlpha, Color.Yellow)
            Using flashBrush As New SolidBrush(flashColor)
                g.FillRectangle(flashBrush, Me.ClientRectangle)
            End Using
        End If

        ' Desenha a snake (aplicando os offsets, se necessário)
        ' Desenha a Snake 1 (posição inicial em (3,1))
        If snakeActive Then
            ' Desenha os segmentos do corpo
            For Each segment As Point In snakeBody
                g.DrawImage(snakeImage(3), segment.X, segment.Y, gridSize, gridSize)
            Next

            ' Desenha a cabeça somente se ainda não foi removida
            If Not headRemoved Then
                Dim stateSnake As GraphicsState = g.Save()
                Dim snakeCenterX As Single = snakeHeadPos.X + gridSize / 2
                Dim snakeCenterY As Single = snakeHeadPos.Y + gridSize / 2

                ' Calcula o ângulo de rotação (ajuste conforme sua lógica de direção)
                Dim snakeAngle As Single = 0
                If snakeSense = False Then
                    If snakeDirection = True Then snakeAngle = 180
                Else
                    If snakeDirection = True Then
                        snakeAngle = 270
                    Else
                        snakeAngle = 90
                    End If
                End If

                g.TranslateTransform(snakeCenterX, snakeCenterY)
                g.RotateTransform(snakeAngle)
                g.TranslateTransform(-snakeCenterX, -snakeCenterY)
                g.DrawImage(currentsnakeImage, snakeHeadPos.X, snakeHeadPos.Y, gridSize, gridSize)
                g.Restore(stateSnake)
            End If
        End If

        ' Desenha o flash branco da plataforma (ocupando toda a tela)
        If platformFlashActive Then
            Dim flashColor As Color = Color.FromArgb(platformFlashAlpha, Color.White)
            Using flashBrush As New SolidBrush(flashColor)
                g.FillRectangle(flashBrush, Me.ClientRectangle)
            End Using
        End If


        If spikesActive AndAlso currentHPImage Is HPImage(1) Then
            For y As Integer = 0 To gridHeight - 1
                For x As Integer = 0 To gridWidth - 1
                    ' Verifica se este tile é o HP (valor 14)
                    If map(y, x) = 14 Then
                        ' Armazena a posição do tile HP para cálculo relativo
                        Dim hpX As Integer = x
                        Dim hpY As Integer = y

                        ' Define as células vizinhas (cima, direita, baixo, esquerda)
                        Dim neighbors As New List(Of Point) From {
                            New Point(x, y - 1),  ' Acima
                            New Point(x + 1, y),  ' À direita
                            New Point(x, y + 1),  ' Abaixo
                            New Point(x - 1, y)   ' À esquerda
                        }

                        ' Seleciona a imagem de spike de acordo com o frame da animação
                        Dim spikeImg As Image
                        If spikeAnimationPlaying Then
                            spikeImg = HPImage(2 + spikeAnimationFrame)
                        Else
                            spikeImg = HPImage(5) ' Mantém o último frame (spike4.png) após a animação
                        End If

                        ' Para cada célula vizinha, aplica a rotação respectiva
                        For Each n As Point In neighbors
                            If n.X >= 0 AndAlso n.X < gridWidth AndAlso n.Y >= 0 AndAlso n.Y < gridHeight Then
                                ' Desenha o spike apenas se o tile vizinho estiver vazio (0) ou for do tipo 10
                                If map(n.Y, n.X) = 0 OrElse map(n.Y, n.X) = 10 Then
                                    Dim rotation As Single = 0
                                    Dim dx As Integer = n.X - hpX
                                    Dim dy As Integer = n.Y - hpY

                                    If dx = 0 AndAlso dy = -1 Then
                                        rotation = 180    ' Para a célula acima, gira 180°
                                    ElseIf dx = 1 AndAlso dy = 0 Then
                                        rotation = 270    ' Para a célula à direita, gira 270° (ou -90°)
                                    ElseIf dx = -1 AndAlso dy = 0 Then
                                        rotation = 90     ' Para a célula à esquerda, gira 90°
                                    ElseIf dx = 0 AndAlso dy = 1 Then
                                        rotation = 0      ' Para a célula abaixo, mantém a rotação (0°)
                                    End If

                                    ' Aplica a rotação utilizando a função RotateImage (definida no seu código)
                                    Dim rotatedSpike As Image = RotateImage(spikeImg, rotation)
                                    Dim cellRect As New Rectangle(offsetX + n.X * cellSize, offsetY + n.Y * cellSize, cellSize, cellSize)
                                    g.DrawImage(rotatedSpike, cellRect)
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If

        If currentHPImage Is HPImage(1) Then ' Somente se o tile HP estiver com block2.png
            For y As Integer = 0 To gridHeight - 1
                For x As Integer = 0 To gridWidth - 1
                    If map(y, x) = 14 Then  ' Tile correspondente ao HP
                        Dim hpX As Integer = x
                        Dim hpY As Integer = y

                        ' Define os vizinhos: acima, à direita, abaixo e à esquerda
                        Dim neighbors As New List(Of Point) From {
                    New Point(x, y - 1),  ' Acima
                    New Point(x + 1, y),  ' À direita
                    New Point(x, y + 1),  ' Abaixo
                    New Point(x - 1, y)   ' À esquerda
                }

                        ' Se a animação está ativa, utiliza o frame atual; caso contrário, mantém o último frame (spikes4.png)
                        Dim spikeImg As Image
                        If spikeAnimationActive Then
                            spikeImg = HPImage(2 + spikeFrameIndex)
                        Else
                            spikeImg = HPImage(5)
                        End If

                        For Each n As Point In neighbors
                            If n.X >= 0 AndAlso n.X < gridWidth AndAlso n.Y >= 0 AndAlso n.Y < gridHeight Then
                                ' Desenha os spikes apenas se o tile vizinho estiver vazio (0) ou for do tipo 10
                                If map(n.Y, n.X) = 0 OrElse map(n.Y, n.X) = 10 Then
                                    Dim rotation As Single = 0
                                    Dim dx As Integer = n.X - hpX
                                    Dim dy As Integer = n.Y - hpY

                                    ' Define a rotação conforme a posição relativa:
                                    If dx = 0 AndAlso dy = -1 Then
                                        rotation = 180   ' Para a célula acima, gira 180°
                                    ElseIf dx = 1 AndAlso dy = 0 Then
                                        rotation = 270   ' Para a célula à direita, gira 270° (ou -90°)
                                    ElseIf dx = -1 AndAlso dy = 0 Then
                                        rotation = 90    ' Para a célula à esquerda, gira 90°
                                    ElseIf dx = 0 AndAlso dy = 1 Then
                                        rotation = 0     ' Para a célula abaixo, mantém 0°
                                    End If

                                    ' Aplica a rotação utilizando a função RotateImage (já definida no seu código)
                                    Dim rotatedSpike As Image = RotateImage(spikeImg, rotation)
                                    Dim cellRect As New Rectangle(offsetX + n.X * cellSize, offsetY + n.Y * cellSize, cellSize, cellSize)
                                    g.DrawImage(rotatedSpike, cellRect)
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub DrawSnake(g As Graphics, snakeBody As List(Of Point), snakeHeadPos As Point, offsetX As Integer, offsetY As Integer)
        ' Desenha os segmentos do corpo da snake com rotação aplicada
        For Each segment As Point In snakeBody
            g.DrawImage(snakeImage(3), offsetX + segment.X, offsetY + segment.Y + gridSize, gridSize, gridSize)
        Next

        Dim stateSnake As GraphicsState = g.Save()
        Dim snakeCenterX As Single = offsetX + snakeHeadPos.X + gridSize / 2
        Dim snakeCenterY As Single = offsetY + snakeHeadPos.Y + gridSize / 2
        Dim snakeAngle As Single = 0  ' Ajuste a rotação conforme necessário
        g.TranslateTransform(snakeCenterX, snakeCenterY)
        g.RotateTransform(snakeAngle)
        g.TranslateTransform(-snakeCenterX, -snakeCenterY)
        g.DrawImage(currentsnakeImage, offsetX + snakeHeadPos.X, offsetY + snakeHeadPos.Y, gridSize, gridSize)
        g.Restore(stateSnake)
    End Sub

#End Region

#Region "Eventos de Input e Atualização do Player"
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If Count.Visible Then
            e.SuppressKeyPress = True
        End If

        If e.KeyCode = Keys.Escape Then
            TogglePause()
            Return
        End If

        ' Código existente para movimentação (WASD, etc.)
        If Not moving Then
            teleported = False
            direction = e.KeyCode
            FindTarget()
            storedTargetX = targetX
            storedTargetY = targetY
            currentPlayerImage = playerImg1
            movementStartTime = DateTime.Now
            Dim distance As Integer = Math.Abs(targetX - playerX) + Math.Abs(targetY - playerY)
            totalMovementTime = (distance / moveSpeed) * (moveTimer.Interval / 1000.0)
            tailSegments.Clear()
            lastCellX = playerX \ gridSize
            lastCellY = playerY \ gridSize
            idleTimer.Stop()
            tailRemovalTimer.Stop()
            moveTimer.Start()

            Select Case e.KeyCode
                Case Keys.W, Keys.Up
                    tailAngle = 270
                Case Keys.S, Keys.Down
                    tailAngle = 90
                Case Keys.A, Keys.Left
                    tailAngle = 180
                Case Keys.D, Keys.Right
                    tailAngle = 0
            End Select
        End If

        If e.KeyCode = Keys.F2 Then
            If Debugger.IsAttached Then
                MessageBox.Show("Pontos atuais: " & dot)
            End If
        End If



    End Sub

    Private Sub TogglePause()
        If Not isPaused Then
            PauseGame()
        Else
            ResumeGame()
        End If
    End Sub

    Private Sub PauseGame()
        isPaused = True
        ' Parar os timers para congelar o jogo
        moveTimer.Stop()
        idleTimer.Stop()
        tailRemovalTimer.Stop()
        batAnimationTimer.Stop()
        batMoveTimer.Stop()
        dartAnimationTimer.Stop()
        dartMoveTimer.Stop()
        strawAnimationTimer.Stop()
        strawMoveTimer.Stop()
        PortalAnimationTimer.Stop()
        flashTimer.Stop()
        HPAnimationTimer.Stop()
        spikeAnimationTimer.Stop()
        snakeAnimationTimer.Stop()
        snakeMoveTimer.Stop()
        snakeAlertTimer.Stop()
        snakeRemovalTimer.Stop()
        PlatformAnimationTimer.Stop()
        PlatSequenceTimer.Stop()
        platformFlashTimer.Stop()
        CountDown.Stop()

        ' Pausar o cronômetro

        ' Instancia e exibe o formulário de pausa já criado no designer
        Pause.ShowDialog()

        ' Ao fechar o form de pausa, retoma o jogo
        ResumeGame()
    End Sub

    Private Sub ResumeGame()
        isPaused = False
        ' Retomar os timers necessários
        moveTimer.Start()
        idleTimer.Start()
        tailRemovalTimer.Start()
        batAnimationTimer.Start()
        batMoveTimer.Start()
        dartAnimationTimer.Start()
        dartMoveTimer.Start()
        strawAnimationTimer.Start()
        strawMoveTimer.Start()
        PortalAnimationTimer.Start()
        HPAnimationTimer.Start()
        spikeAnimationTimer.Start()
        snakeAnimationTimer.Start()
        PlatformAnimationTimer.Start()

        ' Retomar o cronômetro
    End Sub

    Private Sub FindTarget()
        Dim newX As Integer = playerX \ gridSize
        Dim newY As Integer = playerY \ gridSize

        Select Case direction
            Case Keys.W, Keys.Up
                While newY > 0 AndAlso (map(newY - 1, newX) = 0 OrElse
                                      map(newY - 1, newX) = 9 OrElse
                                      map(newY - 1, newX) = 10 OrElse
                                      map(newY - 1, newX) = 18 OrElse
                                      map(newY - 1, newX) = 11)
                    newY -= 1
                End While
            Case Keys.S, Keys.Down
                While newY < gridHeight - 1 AndAlso (map(newY + 1, newX) = 0 OrElse
                                                  map(newY + 1, newX) = 9 OrElse
                                                  map(newY + 1, newX) = 10 OrElse
                                                  map(newY + 1, newX) = 18 OrElse
                                                  map(newY + 1, newX) = 11)
                    newY += 1
                End While
            Case Keys.A, Keys.Left
                While newX > 0 AndAlso (map(newY, newX - 1) = 0 OrElse
                                     map(newY, newX - 1) = 9 OrElse
                                     map(newY, newX - 1) = 10 OrElse
                                     map(newY, newX - 1) = 18 OrElse
                                     map(newY, newX - 1) = 11)
                    newX -= 1
                End While
            Case Keys.D, Keys.Right
                While newX < gridWidth - 1 AndAlso (map(newY, newX + 1) = 0 OrElse
                                                 map(newY, newX + 1) = 9 OrElse
                                                 map(newY, newX + 1) = 10 OrElse
                                                 map(newY, newX + 1) = 18 OrElse
                                                 map(newY, newX + 1) = 11)
                    newX += 1
                End While
        End Select

        targetX = newX * gridSize
        targetY = newY * gridSize
        moving = True
    End Sub




    Private Sub moveTimer_Tick(sender As Object, e As EventArgs) Handles moveTimer.Tick
        Dim dx As Integer = Math.Sign(targetX - playerX) * moveSpeed
        Dim dy As Integer = Math.Sign(targetY - playerY) * moveSpeed

        If Math.Abs(targetX - playerX) < moveSpeed Then dx = targetX - playerX
        If Math.Abs(targetY - playerY) < moveSpeed Then dy = targetY - playerY

        playerX += dx
        playerY += dy

        ' Verifica se o player entrou no portal (compara as posições em células)
        Dim currentCellX As Integer = playerX \ gridSize
        Dim currentCellY As Integer = playerY \ gridSize


        Dim elapsedTime As Double = (DateTime.Now - movementStartTime).TotalSeconds
        Dim timeRemaining As Double = totalMovementTime - elapsedTime

        If elapsedTime < 0.2 Then
            currentPlayerImage = playerImg1
        ElseIf timeRemaining <= 0.2 Then
            currentPlayerImage = playerImg1
        Else
            currentPlayerImage = playerImg2
        End If

        ' Atualiza a tail (código já existente)
        currentCellX = playerX \ gridSize
        currentCellY = playerY \ gridSize
        If currentCellX <> lastCellX OrElse currentCellY <> lastCellY Then
            Dim ts As New TailSegment()
            ts.X = lastCellX
            ts.Y = lastCellY
            ts.TailType = If(tailSegments.Count = 0, 1, 2)
            ts.TimeStamp = DateTime.Now
            If ts.TailType = 1 Then
                ts.RotatedImage = RotateImage(tail1Image, tailAngle)
            Else
                ts.RotatedImage = RotateImage(tail2Image, tailAngle)
            End If
            tailSegments.Add(ts)
            lastCellX = currentCellX
            lastCellY = currentCellY
        End If

        ' Se o player atingiu o alvo, para o movimento
        If playerX = targetX AndAlso playerY = targetY Then
            moveTimer.Stop()
            moving = False
            Select Case direction
                Case Keys.W, Keys.Up
                    playerAngle = 180
                Case Keys.S, Keys.Down
                    playerAngle = 0
                Case Keys.A, Keys.Left
                    playerAngle = 90
                Case Keys.D, Keys.Right
                    playerAngle = 270
            End Select
            tailAngle = 0
            tailRemovalTimer.Start()
            idleTimer.Start()
            currentPlayerImage = idleSkins(idleSkinIndex)

            CheckCollisionWithSpikes()
        End If

        Dim playerGridPos As New Point(playerX \ gridSize, playerY \ gridSize)
        If Not trapActivated AndAlso trapActivationZone.Contains(playerGridPos) Then
            trapActivated = True
            alertFrameCount = 0
            currentsnakeImage = snakeImage(4)
            snakeAlertTimer.Start()
        End If


        ' Verifica se o player está em contato com alguma plataforma (tile 16) e ativa o grupo conectado
        Dim playerCellX As Integer = playerX \ gridSize
        Dim playerCellY As Integer = playerY \ gridSize

        For offsetY As Integer = -1 To 1
            For offsetX As Integer = -1 To 1
                Dim checkX As Integer = playerCellX + offsetX
                Dim checkY As Integer = playerCellY + offsetY
                If checkX >= 0 AndAlso checkX < gridWidth AndAlso checkY >= 0 AndAlso checkY < gridHeight Then
                    If map(checkY, checkX) = 16 AndAlso Not platformRemovalScheduled Then
                        platformRemovalScheduled = True
                        ' Ativa todas as plataformas conectadas (grupo)
                        ActivatePlatformGroup(checkX, checkY)
                        platformSequenceTickCount = 0
                        PlatSequenceTimer.Interval = 500 ' 0,5 s por etapa
                        PlatSequenceTimer.Start()
                        Exit Sub
                    End If
                End If
            Next
        Next

        If map(playerCellY, playerCellX) = 10 Then
            map(playerCellY, playerCellX) = 0
            Experience += 1
            dot += 1
        End If

        If map(playerCellY, playerCellX) = 9 Then
            ' Remove a star
            map(playerCellY, playerCellX) = 0

            ' Inicia a animação do flash amarelo
            flashActive = True
            flashAlpha = 200
            flashTimer.Start()
            Fstar += 1

            ' Garante que todas as estrelas estejam ocultas antes de definir quais devem aparecer
            Flvl1.s1.Visible = True
            Flvl1.s2.Visible = True
            Flvl1.s3.Visible = True

            Select Case Fstar
                Case 1
                    Flvl2.s1.Visible = False
                    MenuLvl.Star2.Image = Image.FromFile("imgM\levels\1stars.png")
                Case 2
                    Flvl2.s1.Visible = False
                    Flvl2.s2.Visible = False
                    MenuLvl.Star2.Image = Image.FromFile("imgM\levels\2stars.png")
                Case 3
                    Flvl2.s1.Visible = False
                    Flvl2.s2.Visible = False
                    Flvl2.s3.Visible = False
                    MenuLvl.Star2.Image = Image.FromFile("imgM\levels\3stars.png")
            End Select
        End If

        If map(playerCellY, playerCellX) = 10 Then

            map(playerCellY, playerCellX) = 11
        End If



        ' Reseta a flag teleported apenas quando o player sai da área de qualquer portal




        If map(playerCellY, playerCellX) = 11 Then
            map(playerCellY, playerCellX) = 0
            NCoins += 50
            MenuLvl.Coins.Text = $"{NCoins}"
            SRMODE.Coins.Text = $"{NCoins}"
            Options.Coins.Text = $"{NCoins}"
            Fcoin += 50
        End If

        CheckCollisionWithBat()
        CheckCollisionWithHiddenSpikes()
        CheckCollisionWithDart()

        If map(playerCellY, playerCellX) = 18 Then
            Me.Close()
            Flvl2.Show()
            Flvl2.FCoins.Text = ("+" & Fcoin)
            MenuLvl.Coins.Text = $"{NCoins}"
            Options.Coins.Text = $"{NCoins}"
            SRMODE.Coins.Text = $"{NCoins}"
            MenuLvl.Lvl2.Visible = True
            MenuLvl.Star3.Visible = True

        End If

        Me.Invalidate()
    End Sub



    Private Sub tailRemovalTimer_Tick(sender As Object, e As EventArgs) Handles tailRemovalTimer.Tick
        If tailSegments.Count > 0 Then
            tailSegments.RemoveAt(0)
        Else
            tailRemovalTimer.Stop()
        End If
        Me.Invalidate()
    End Sub

    Private Sub idleTimer_Tick(sender As Object, e As EventArgs) Handles idleTimer.Tick
        CheckCollisionWithBat()
        If Not moving Then
            currentPlayerImage = idleSkins(idleSkinIndex)
            idleSkinIndex = (idleSkinIndex + 1) Mod idleSkins.Length

            CheckCollisionWithHiddenSpikes()
            CheckCollisionWithDart()
            If health <= 0 Then
                Me.Close()
                NFlvl2.Show()
                Charges -= 1
                MenuLvl.Mcharges.Text = Charges

                ' Garante que o formulário NFlvl2 está carregado antes de atualizar a label
                If Application.OpenForms.OfType(Of NFlvl2).Any() Then
                    NFlvl2.charg.Text = $"{Charges}"
                End If

                moveTimer.Stop()
                idleTimer.Stop()
                tailRemovalTimer.Stop()
                batAnimationTimer.Stop()
                dartAnimationTimer.Stop()
                dartMoveTimer.Stop()
                strawAnimationTimer.Stop()
                strawMoveTimer.Stop()
                PortalAnimationTimer.Stop()
                HPAnimationTimer.Stop()
                spikeAnimationTimer.Stop()
                snakeAnimationTimer.Stop()
                snakeMoveTimer.Stop()
                snakeAlertTimer.Stop()
                snakeRemovalTimer.Stop()
                PlatformAnimationTimer.Stop()
                PlatSequenceTimer.Stop()
                platformFlashTimer.Stop()
                NoShieldsTimer.Stop()
                YesShieldsTimer.Stop()
                FinishTimer.Stop()
                NormalPlatformAnimationTimer.Stop()

                moveTimer.Stop()
                ' Primeiro, para o cronómetro
            End If

        End If
        Me.Invalidate()
    End Sub
#End Region
#Region "game elements"
    Private Sub DotTimer_Tick(sender As Object, e As EventArgs) Handles DotTimer.Tick
        DotImageIndex = (DotImageIndex + 1) Mod DotImage.Length
        currentDotImage = DotImage(DotImageIndex)
        Me.Invalidate()
    End Sub

    Private Sub StarTimer_Tick(sender As Object, e As EventArgs) Handles StarTimer.Tick
        StarImageIndex = (StarImageIndex + 1) Mod StarImage.Length
        currentStarImage = StarImage(StarImageIndex)
        Me.Invalidate()
    End Sub

    Private Sub CoinTimer_Tick(sender As Object, e As EventArgs) Handles CoinTimer.Tick
        CoinImageIndex = (CoinImageIndex + 1) Mod CoinImage.Length
        currentCoinImage = CoinImage(CoinImageIndex)
        Me.Invalidate()
    End Sub

    Private Sub EndTimer_Tick(sender As Object, e As EventArgs) Handles EndTimer.Tick
        EndImageIndex = (EndImageIndex + 1) Mod EndImage.Length
        currentEndImage = EndImage(EndImageIndex)
        Me.Invalidate()
    End Sub
#End Region

#Region "Traps"
    Private Sub batAnimationTimer_Tick(sender As Object, e As EventArgs) Handles batAnimationTimer.Tick
        batImageIndex = (batImageIndex + 1) Mod batImage.Length
        currentBatImage = batImage(batImageIndex)
        Me.Invalidate()
    End Sub



    Private Sub dartAnimationTimer_Tick(sender As Object, e As EventArgs) Handles dartAnimationTimer.Tick
        ' Atualiza o índice da imagem para alternar entre dart1.png e dart2.png
        dartImageIndex = (dartImageIndex + 1) Mod dartImage.Length
        currentdartImage = dartImage(dartImageIndex)

        ' Alterna o intervalo para dar efeito de animação
        If dartActiveTimer Then
            dartAnimationTimer.Interval = 500 ' 0,2 s
        Else
            dartAnimationTimer.Interval = 5000 ' 0,5 s
        End If
        dartActiveTimer = Not dartActiveTimer

        ' Se a imagem atual for "dart2.png" (índice 1) e o tempo de geração foi ultrapassado, gera um novo projétil
        Dim dartTrapRow As Integer = 7   ' ajuste conforme necessário
        Dim dartTrapCol As Integer = 13  ' ajuste conforme necessário
        If dartImageIndex = 1 AndAlso (DateTime.Now - lastDartGenerationTime).TotalMilliseconds > dartGenerationDelay Then
            Dim newProj As New Projectile
            ' O novo projétil é criado na célula imediatamente à esquerda do trap
            newProj.X = (dartTrapCol - 1) * gridSize
            newProj.Y = dartTrapRow * gridSize
            projectiles.Add(newProj)
            lastDartGenerationTime = DateTime.Now
        End If

        Me.Invalidate()
    End Sub

    Private Sub dartMoveTimer_Tick(sender As Object, e As EventArgs) Handles dartMoveTimer.Tick
        Dim stepSize As Integer = 15
        ' Usamos uma lista auxiliar para armazenar os projéteis que deverão ser removidos
        Dim toRemove As New List(Of Projectile)

        For Each proj As Projectile In projectiles
            ' Move o projétil para a esquerda
            proj.X -= stepSize

            Dim col As Integer = proj.X \ gridSize
            Dim row As Integer = proj.Y \ gridSize

            ' Se o projétil sair da grade ou atingir uma parede, agenda sua remoção
            If col < 0 OrElse col >= gridWidth OrElse row < 0 OrElse row >= gridHeight OrElse
           ((map(row, col) >= 1 AndAlso map(row, col) <= 8) OrElse (map(row, col) >= 20 AndAlso map(row, col) <= 25)) Then
                toRemove.Add(proj)
            End If
        Next

        ' Remove os projéteis que colidiram ou saíram da grade
        For Each proj In toRemove
            projectiles.Remove(proj)
        Next

        Me.Invalidate()
    End Sub

    Private Class Projectile
        Public Property X As Integer
        Public Property Y As Integer
    End Class

    ' Lista de projéteis ativos
    Private projectiles As New List(Of Projectile)



    Private Sub strawMoveTimer_Tick(sender As Object, e As EventArgs) Handles strawMoveTimer.Tick


        ' Obtém a posição do player em células
        Dim playerCellX As Integer = playerX \ gridSize
        Dim playerCellY As Integer = playerY \ gridSize

        ' Calcula o caminho da straw até o player

        ' Se houver caminho e não estiver na mesma célula, move para o próximo passo


        strawImageIndex = (strawImageIndex + 1) Mod strawImage.Length
        currentStrawImage = strawImage(strawImageIndex)

        Me.Invalidate()
    End Sub


    Private Function GetPath(startX As Integer, startY As Integer, goalX As Integer, goalY As Integer) As List(Of Point)
        Dim openList As New List(Of Node)
        Dim closedList As New List(Of Node)

        Dim startNode As New Node() With {
        .X = startX,
        .Y = startY,
        .G = 0,
        .H = Math.Abs(goalX - startX) + Math.Abs(goalY - startY)
    }
        startNode.F = startNode.G + startNode.H
        openList.Add(startNode)

        While openList.Count > 0
            ' Obtém o nó com menor F
            Dim currentNode As Node = openList.OrderBy(Function(n) n.F).First()

            ' Se chegou ao destino, reconstrói o caminho
            If currentNode.X = goalX AndAlso currentNode.Y = goalY Then
                Dim path As New List(Of Point)
                While currentNode IsNot Nothing
                    path.Insert(0, New Point(currentNode.X, currentNode.Y))
                    currentNode = currentNode.Parent
                End While
                Return path
            End If

            openList.Remove(currentNode)
            closedList.Add(currentNode)

            ' Vizinhos (cima, baixo, esquerda, direita)
            Dim neighbors As New List(Of Point) From {
            New Point(currentNode.X - 1, currentNode.Y),
            New Point(currentNode.X + 1, currentNode.Y),
            New Point(currentNode.X, currentNode.Y - 1),
            New Point(currentNode.X, currentNode.Y + 1)
        }

            For Each neighbor In neighbors
                ' Verifica se está dentro dos limites
                If neighbor.X < 0 Or neighbor.X >= gridWidth Or neighbor.Y < 0 Or neighbor.Y >= gridHeight Then Continue For

                ' Verifica se a célula é um obstáculo (no seu caso, paredes são tiles 1 a 8)
                If map(neighbor.Y, neighbor.X) <> 0 AndAlso map(neighbor.Y, neighbor.X) <> 10 Then Continue For

                ' Se já está na closedList, pula
                If closedList.Any(Function(n) n.X = neighbor.X AndAlso n.Y = neighbor.Y) Then Continue For

                Dim tentativeG = currentNode.G + 1
                Dim existingNode = openList.FirstOrDefault(Function(n) n.X = neighbor.X AndAlso n.Y = neighbor.Y)

                If existingNode Is Nothing Then
                    Dim neighborNode As New Node()
                    neighborNode.X = neighbor.X
                    neighborNode.Y = neighbor.Y
                    neighborNode.G = tentativeG
                    neighborNode.H = Math.Abs(goalX - neighbor.X) + Math.Abs(goalY - neighbor.Y)
                    neighborNode.F = neighborNode.G + neighborNode.H
                    neighborNode.Parent = currentNode
                    openList.Add(neighborNode)
                ElseIf tentativeG < existingNode.G Then
                    existingNode.G = tentativeG
                    existingNode.F = existingNode.G + existingNode.H
                    existingNode.Parent = currentNode
                End If
            Next
        End While

        ' Se não encontrar caminho, retorna uma lista vazia
        Return New List(Of Point)
    End Function ' starw
    Private Sub PortalAnimationTimer_Tick(sender As Object, e As EventArgs) Handles PortalAnimationTimer.Tick
        PortalImageIndex = (PortalImageIndex + 1) Mod PortalImage.Length
        currentPortalImage = PortalImage(PortalImageIndex)
        Me.Invalidate()
    End Sub

    Private Sub flashTimer_Tick(sender As Object, e As EventArgs) Handles flashTimer.Tick
        flashAlpha -= 20
        If flashAlpha <= 0 Then
            flashAlpha = 0
            flashActive = False
            flashTimer.Stop()
        End If
        Me.Invalidate()
    End Sub

    Private Sub spikeAnimationTimer_Tick(sender As Object, e As EventArgs) Handles spikeAnimationTimer.Tick
        ' Se estiver no estado normal (block1.png), muda para block2.png e ativa os spikes
        If currentHPImage Is HPImage(0) Then
            currentHPImage = HPImage(1)   ' Muda para block2.png
            spikesActive = True           ' Ativa a animação dos spikes nas células adjacentes
            spikeFrameIndex = 0           ' Reinicia o índice de frames (se for necessário para animação)
        Else
            currentHPImage = HPImage(0)   ' Retorna para block1.png
            spikesActive = False          ' Desativa os spikes
        End If

        ' Se os spikes estiverem ativos, atualiza o frame (exemplo para animação)
        If spikesActive Then
            spikeFrameIndex = (spikeFrameIndex + 1) Mod 4
        End If

        Me.Invalidate()
    End Sub



    Private Sub PlatformTimer_Tick(sender As Object, e As EventArgs) Handles PlatformTimer.Tick
        PlatformTimer.Stop()
        FloodFillPlatform(platformRemovalX, platformRemovalY)
        platformRemovalScheduled = False

        ' RETOME o movimento usando o target armazenado
        moving = True
        targetX = storedTargetX
        targetY = storedTargetY
        moveTimer.Start()

        Me.Invalidate()
    End Sub

    Private Sub FloodFillPlatform(x As Integer, y As Integer)
        ' Verifica se a posição está dentro dos limites
        If x < 0 Or x >= gridWidth Or y < 0 Or y >= gridHeight Then Exit Sub
        ' Se o tile não for 16, sai

        ' Chama recursivamente para os vizinhos
        FloodFillPlatform(x + 1, y)
        FloodFillPlatform(x - 1, y)
        FloodFillPlatform(x, y + 1)
        FloodFillPlatform(x, y - 1)
    End Sub
    Private Sub PlatSequenceTimer_Tick(sender As Object, e As EventArgs) Handles PlatSequenceTimer.Tick
        platformSequenceTickCount += 1

        ' Atualiza o estágio de animação para cada plataforma ativada
        For Each pt As Point In activatedPlatformGroup
            platformStages(pt) += 1
        Next

        Select Case platformSequenceTickCount
            Case 1
                spPlat1.Play()
            Case 2
                spPlat2.Play()
            Case 3
                ' Para cada plataforma ativada que atingiu ou ultrapassou o estágio 3, remova-a
                Dim toRemove As New List(Of Point)
                For Each pt As Point In activatedPlatformGroup
                    If platformStages(pt) >= 3 Then
                        map(pt.Y, pt.X) = 0 ' Remove a plataforma do mapa
                        CheckPlayerProximityToExplodingPlatform(pt.X, pt.Y)
                        toRemove.Add(pt)
                    End If
                Next
                For Each pt As Point In toRemove
                    activatedPlatformGroup.Remove(pt)
                    platformStages.Remove(pt)
                Next
                spPlat3.Play()
                PlatSequenceTimer.Interval = 10
            Case 4
                platformFlashActive = True
                platformFlashAlpha = 200
                platformFlashTimer.Start()
                PlatSequenceTimer.Stop()
                platformSequenceTickCount = 0
                platformRemovalScheduled = False
                PlatSequenceTimer.Interval = 500
                moving = True
                moveTimer.Start()
        End Select
        Me.Invalidate()
    End Sub
    Private Sub ActivatePlatformGroup(x As Integer, y As Integer)
        activatedPlatformGroup.Clear()
        platformStages.Clear()
        FloodFillActivate(x, y)
    End Sub

    Private Sub FloodFillActivate(x As Integer, y As Integer)
        ' Verifica os limites
        If x < 0 Or x >= gridWidth Or y < 0 Or y >= gridHeight Then Exit Sub
        ' Apenas processa se for uma plataforma não ativada (valor 16)
        If map(y, x) <> 16 Then Exit Sub
        ' Marca como ativada (valor 17)
        map(y, x) = 17
        Dim pt As New Point(x, y)
        activatedPlatformGroup.Add(pt)
        ' Inicializa o estágio de animação para esta plataforma
        platformStages(pt) = 0
        FloodFillActivate(x + 1, y)
        FloodFillActivate(x - 1, y)
        FloodFillActivate(x, y + 1)
        FloodFillActivate(x, y - 1)
    End Sub
    Private Sub platformFlashTimer_Tick(sender As Object, e As EventArgs) Handles platformFlashTimer.Tick
        platformFlashAlpha -= 20 ' Reduz a intensidade do flash
        If platformFlashAlpha <= 0 Then
            platformFlashAlpha = 0
            platformFlashActive = False
            platformFlashTimer.Stop()
        End If
        Me.Invalidate()
    End Sub
#End Region
    Private Sub CountDown_Tick(sender As Object, e As EventArgs) Handles CountDown.Tick
        countdownCounter += 1

        Select Case countdownCounter

            Case 1
                Count.Image = Image.FromFile("imgM\animations\components\count\2.png")
                'countStart.Play()

            Case 2
                Count.Image = Image.FromFile("imgM\animations\components\count\1.png")
                'countStart.Play()

            Case 3
                Count.Visible = False
                CountDown.Stop()
                ' Inicia a animação de fade
                ' countEnd.Play()
                FadeAnim()
        End Select

    End Sub

    Private Sub FadeAnim()
        ' Cria um painel preto que cobre toda a tela
        fadePanel = New Panel() With {
        .Size = Me.ClientSize,
        .BackColor = Color.Black
    }
        Me.Controls.Add(fadePanel)
        fadePanel.BringToFront()

        ' Configura o timer para reduzir a opacidade
        fadeTimer.Interval = 50 ' Define a velocidade do fade
        fadeTimer.Start()
    End Sub

    Private Sub fadeTimer_Tick(sender As Object, e As EventArgs) Handles fadeTimer.Tick
        fadeOpacity -= 15 ' Reduz a opacidade a cada tick
        If fadeOpacity <= 0 Then
            fadeTimer.Stop()
            fadePanel.Visible = False
            MostrarGrelhaEAtivarJogo() ' Exibe a grelha do jogo
        Else
            fadePanel.BackColor = Color.FromArgb(fadeOpacity, Color.Black)
        End If
    End Sub

    Private Sub MostrarGrelhaEAtivarJogo()
        gridVisible = True
        Me.Invalidate()
        ' Agora que as animações terminaram, inicie os timers do jogo

        IniciarJogo()
        moveTimer.Start()
        idleTimer.Start()
        tailRemovalTimer.Start()
        batAnimationTimer.Start()
        batMoveTimer.Start()
        dartAnimationTimer.Start()
        dartMoveTimer.Start()
        'strawAnimationTimer.Start()
        ' strawMoveTimer.Start()
        PortalAnimationTimer.Start()
        HPAnimationTimer.Start()
        spikeAnimationTimer.Start()
        PlatformAnimationTimer.Start()
        StarTimer.Start()
        DotTimer.Start()
        CoinTimer.Start()
        EndTimer.Start()
    End Sub

    Private Sub SpeedRunMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None

    End Sub


    Private Sub StartSpikeAnimation()
        currentHPImage = HPImage(1)    ' Muda para block2.png
        spikesActive = True
        spikeAnimationPlaying = True
        spikeAnimationFrame = 0
        spikeAnimationTimer.Interval = 150 ' Define o intervalo entre os frames (ajuste conforme necessário)
        spikeAnimationTimer.Start()
        ' Verifica se o HP está ativo (block2.png)
        If currentHPImage Is HPImage(1) Then
            spikeFrameIndex = 0
            spikeAnimationActive = True
            spikeAnimTimer.Interval = 150  ' Intervalo em milissegundos entre os frames (ajuste conforme necessário)
            spikeAnimTimer.Start()
        End If
    End Sub
    Private Sub spikeAnimTimer_Tick(sender As Object, e As EventArgs) Handles spikeAnimTimer.Tick
        If spikeAnimationActive Then
            spikeFrameIndex += 1
            If spikeFrameIndex > 3 Then
                ' Quando chegar ao último frame, a animação para
                spikeFrameIndex = 3
                spikeAnimationActive = False
                spikeAnimTimer.Stop()
            End If
            Me.Invalidate()  ' Solicita redessínio para atualizar os spikes
        End If
    End Sub
#Region "shield"


    Private Sub SpeedRunMode_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        ' Verifica se o player ainda tem shields disponíveis antes de reduzir
        If Shield > 0 Then
            Shield -= 1  ' Reduz 1 shield apenas se for maior que zero

            ' Ativa a textura alternativa
            altTextureActive = True

            ' Substitui o array de skins do player pelas alternativas
            idleSkins = alternativePlayerSkins.Clone()
            currentPlayerImage = idleSkins(0)

            ' Substitui as imagens da tail pelas alternativas
            tail1Image = alternativeTail1Image
            tail2Image = alternativeTail2Image

            ' Substitui as texturas da ball pelas alternativas
            playerImg1 = alternativeBall1Image
            playerImg2 = alternativeBall2Image

            ' Reinicia o timer para restaurar as texturas originais após 10 segundos
            altTextureTimer.Stop()
            altTextureTimer.Start()

            ' Atualiza a imagem do número de shields restantes
            Select Case Shield
                Case 2
                    AtualizarMensagem("imgM\sprites\ShAct2.png", YesShieldsTimer)
                Case 1
                    AtualizarMensagem("imgM\sprites\ShAct1.png", YesShieldsTimer)
                Case 0
                    AtualizarMensagem("imgM\sprites\ShAct0.png", YesShieldsTimer)
            End Select

        Else
            ' Caso o jogador não tenha mais shields, exibe a mensagem apropriada
            AtualizarMensagem("imgM\sprites\no shields.png", NoShieldsTimer)
        End If

        If altTextureActive = True Then
            health += 1000000
            UpdateHealthDisplay()
        End If

    End Sub

    ' Função para atualizar a mensagem do PictureBox e iniciar o timer correto
    Private Sub AtualizarMensagem(ByVal caminhoImagem As String, ByVal timer As Timer)
        Try
            If IO.File.Exists(caminhoImagem) Then
                Message.Image = Image.FromFile(caminhoImagem)
            Else
                Message.Image = Nothing ' Se a imagem não existir, não carrega nada
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao carregar imagem: " & ex.Message)
            Message.Image = Nothing
        End Try

        Message.Visible = True
        timer.Stop()
        timer.Interval = 2000 ' Garante que o timer dure pelo menos 2 segundos
        timer.Start()
    End Sub

    ' Evento do timer para restaurar as texturas originais após 10 segundos
    Private Sub altTextureTimer_Tick(sender As Object, e As EventArgs) Handles altTextureTimer.Tick
        altTextureTimer.Stop()
        altTextureActive = False

        ' Restaura as texturas originais do player e da tail
        idleSkins = originalPlayerSkins.Clone()
        currentPlayerImage = idleSkins(0)
        tail1Image = originalTail1Image
        tail2Image = originalTail2Image

        ' Restaura as texturas originais da ball
        playerImg1 = originalBall1Image
        playerImg2 = originalBall2Image

        health = 1
        UpdateHealthDisplay()

        ' Atualiza a tela
        Me.Invalidate()
    End Sub

    ' Esconde a mensagem após o tempo do timer (sem bloquear o programa)
    Private Sub NoShieldsTimer_Tick(sender As Object, e As EventArgs) Handles NoShieldsTimer.Tick
        EsconderMensagem(NoShieldsTimer)
    End Sub

    Private Sub YesShieldsTimer_Tick(sender As Object, e As EventArgs) Handles YesShieldsTimer.Tick
        EsconderMensagem(YesShieldsTimer)
    End Sub

    ' Função para esconder a mensagem do PictureBox e parar o timer
    Private Sub EsconderMensagem(ByVal timer As Timer)
        Message.Visible = False
        timer.Stop()
        Me.Invalidate()
    End Sub
#End Region

#Region "Matança"
    Private Sub CheckPlayerProximityToExplodingPlatform(x As Integer, y As Integer)
        Dim playerCellX As Integer = playerX \ gridSize
        Dim playerCellY As Integer = playerY \ gridSize

        ' Verifica se o jogador está exatamente na célula da plataforma
        If playerCellX = x AndAlso playerCellY = y Then
            health -= 1
            UpdateHealthDisplay()
            Exit Sub
        End If

        ' Define as células vizinhas (acima, à esquerda, à direita e abaixo)
        Dim neighbors As New List(Of Point) From {
         New Point(x, y - 1),
         New Point(x - 1, y),
         New Point(x + 1, y),
         New Point(x, y + 1)
    }

        ' Se o jogador estiver em qualquer uma das células vizinhas, diminui a vida
        For Each n As Point In neighbors
            If n.X = playerCellX AndAlso n.Y = playerCellY Then
                health -= 1
                UpdateHealthDisplay()
                Exit For
            End If
        Next
    End Sub
    Private Sub CheckCollisionWithBat()
        ' Obtém as coordenadas do player em pixels (tamanho total da grade)
        Dim playerRect As New Rectangle(playerX, playerY, gridSize, gridSize)

        ' Define a redução do tamanho do bat (90% da grade)
        Dim batSize As Integer = CInt(gridSize * 0.9)
        Dim offset As Integer = (gridSize - batSize) \ 2 ' Centraliza o bat na grade

        ' Obtém as coordenadas do bat ajustado

        ' Verifica se há interseção entre os retângulos (colisão parcial ou total)


    End Sub

    Private Sub CheckCollisionWithHiddenSpikes()
        If Not (currentHPImage Is HPImage(1)) Then Exit Sub

        Dim playerCellX As Integer = playerX \ gridSize
        Dim playerCellY As Integer = playerY \ gridSize

        Dim playerRect As New Rectangle(playerX, playerY, gridSize, gridSize)

        ' Percorre o mapa procurando tiles que representam os hidden spikes (valor 14)
        For y As Integer = 0 To gridHeight - 1
            For x As Integer = 0 To gridWidth - 1
                If map(y, x) = 14 Then
                    ' Se quiser considerar apenas células acima, abaixo, à esquerda ou à direita (sem diagonais), 
                    ' utilize: If Math.Abs(playerCellX - x) + Math.Abs(playerCellY - y) <= 1 Then
                    ' 6    ' Para incluir diagonais, utilize:
                    If Math.Abs(playerCellX - x) + Math.Abs(playerCellY - y) <= 1 Then
                        health -= 1
                        UpdateHealthDisplay()
                        Exit Sub  ' Aplica o dano uma única vez por verificação
                    End If
                End If
            Next
        Next

    End Sub
    Private Sub CheckCollisionWithSpikes()
        Dim collisionCell As Point = GetCollisionCell()
        ' Se a célula "base" (obtida conforme a direção) contém um spike (tile 13 ou 19), reduz a vida
        If map(collisionCell.Y, collisionCell.X) = 13 OrElse map(collisionCell.Y, collisionCell.X) = 19 Then
            health -= 1
            UpdateHealthDisplay()
        End If
    End Sub

    Private Function GetCollisionCell() As Point
        Dim finalCellX As Integer = targetX \ gridSize
        Dim finalCellY As Integer = targetY \ gridSize
        Select Case direction
            Case Keys.W, Keys.Up
                ' Se estiver se movendo para cima, a célula base é a célula imediatamente acima
                Return New Point(finalCellX, Math.Max(finalCellY - 1, 0))
            Case Keys.S, Keys.Down
                ' Se estiver se movendo para baixo, a célula base é a célula imediatamente abaixo
                Return New Point(finalCellX, Math.Min(finalCellY + 1, gridHeight - 1))
            Case Keys.A, Keys.Left
                ' Se estiver se movendo para a esquerda, a célula base é a célula imediatamente à esquerda
                Return New Point(Math.Max(finalCellX - 1, 0), finalCellY)
            Case Keys.D, Keys.Right
                ' Se estiver se movendo para a direita, a célula base é a célula imediatamente à direita
                Return New Point(Math.Min(finalCellX + 1, gridWidth - 1), finalCellY)
            Case Else
                ' Caso padrão: usa o destino atual
                Return New Point(finalCellX, finalCellY)
        End Select
    End Function

    Private Sub UpdateHealthDisplay()
    End Sub
    Private Sub ActivateShield()
        altTextureActive = True

        currentPlayerImage = alternativePlayerSkins(0)
        If altTextureActive = True Then

            health += 1000000
            UpdateHealthDisplay()

        End If

        altTextureTimer.Start()
    End Sub

    Private Sub CheckCollisionWithDart()
        Dim playerRect As New Rectangle(playerX, playerY, gridSize, gridSize)

        ' Definir a colisão do dardo (80% do tamanho da célula)
        Dim dartWidth As Integer = CInt(gridSize * 0.8)
        Dim dartHeight As Integer = CInt(gridSize * 0.8)

        ' Verificar colisão com projéteis ativos (dardos)
        For Each proj As Projectile In projectiles
            Dim dartRect As New Rectangle(proj.X, proj.Y, dartWidth, dartHeight)

            If playerRect.IntersectsWith(dartRect) Then
                health -= 1
                UpdateHealthDisplay()
                ' Opcional: pode adicionar uma animação de dano ou som de impacto
                Exit For ' Sai do loop após detectar uma colisão
            End If
        Next
    End Sub

#End Region

    Private Sub Lvl2_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Para a contagem decrescente temporariamente para que não seja exibida
        CountDown.Stop()
        ' Executa a animação de fade imediatamente
        FadeAnim()
        ' Se for necessário retomar a contagem depois da animação,
        ' você pode iniciá-la novamente (por exemplo, com um Delay ou ao finalizar o fade)
        ' CountDown.Start()
    End Sub

    Private Sub NormalPlatformAnimationTimer_Tick(sender As Object, e As EventArgs) Handles NormalPlatformAnimationTimer.Tick
        ' Alterna entre p1, p2, p3 para plataformas normais
        normalPlatformIndex = (normalPlatformIndex + 1) Mod PlatImage.Length
        Me.Invalidate()
    End Sub

End Class





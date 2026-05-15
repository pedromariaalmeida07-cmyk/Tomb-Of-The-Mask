<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Flvl10
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Flvl10))
        FCoins = New Label()
        s3 = New PictureBox()
        s1 = New PictureBox()
        s2 = New PictureBox()
        Replay = New PictureBox()
        Menu = New PictureBox()
        CType(s3, ComponentModel.ISupportInitialize).BeginInit()
        CType(s1, ComponentModel.ISupportInitialize).BeginInit()
        CType(s2, ComponentModel.ISupportInitialize).BeginInit()
        CType(Replay, ComponentModel.ISupportInitialize).BeginInit()
        CType(Menu, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' FCoins
        ' 
        FCoins.AutoSize = True
        FCoins.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        FCoins.Font = New Font("Tomb of the Mask", 12F)
        FCoins.ForeColor = SystemColors.ControlText
        FCoins.Location = New Point(894, 566)
        FCoins.Name = "FCoins"
        FCoins.Size = New Size(54, 24)
        FCoins.TabIndex = 14
        FCoins.Text = "+ 0"
        FCoins.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' s3
        ' 
        s3.Image = CType(resources.GetObject("s3.Image"), Image)
        s3.Location = New Point(1013, 442)
        s3.Name = "s3"
        s3.Size = New Size(84, 90)
        s3.TabIndex = 13
        s3.TabStop = False
        ' 
        ' s1
        ' 
        s1.Image = CType(resources.GetObject("s1.Image"), Image)
        s1.Location = New Point(829, 442)
        s1.Name = "s1"
        s1.Size = New Size(84, 90)
        s1.TabIndex = 12
        s1.TabStop = False
        ' 
        ' s2
        ' 
        s2.Image = CType(resources.GetObject("s2.Image"), Image)
        s2.Location = New Point(921, 442)
        s2.Name = "s2"
        s2.Size = New Size(84, 90)
        s2.TabIndex = 11
        s2.TabStop = False
        ' 
        ' Replay
        ' 
        Replay.Image = CType(resources.GetObject("Replay.Image"), Image)
        Replay.Location = New Point(755, 804)
        Replay.Name = "Replay"
        Replay.Size = New Size(416, 100)
        Replay.TabIndex = 10
        Replay.TabStop = False
        ' 
        ' Menu
        ' 
        Menu.Image = CType(resources.GetObject("Menu.Image"), Image)
        Menu.Location = New Point(755, 692)
        Menu.Name = "Menu"
        Menu.Size = New Size(416, 100)
        Menu.TabIndex = 9
        Menu.TabStop = False
        ' 
        ' Flvl10
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1924, 1055)
        Controls.Add(FCoins)
        Controls.Add(s3)
        Controls.Add(s1)
        Controls.Add(s2)
        Controls.Add(Replay)
        Controls.Add(Menu)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Flvl10"
        Text = "Tomb Of The Mask"
        CType(s3, ComponentModel.ISupportInitialize).EndInit()
        CType(s1, ComponentModel.ISupportInitialize).EndInit()
        CType(s2, ComponentModel.ISupportInitialize).EndInit()
        CType(Replay, ComponentModel.ISupportInitialize).EndInit()
        CType(Menu, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents FCoins As Label
    Friend WithEvents s3 As PictureBox
    Friend WithEvents s1 As PictureBox
    Friend WithEvents s2 As PictureBox
    Friend WithEvents Replay As PictureBox
    Friend WithEvents Menu As PictureBox
End Class

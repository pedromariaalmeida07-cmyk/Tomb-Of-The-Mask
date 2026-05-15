<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotFinishSM
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NotFinishSM))
        FCoins = New Label()
        Replay = New PictureBox()
        PictureBox1 = New PictureBox()
        CType(Replay, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' FCoins
        ' 
        FCoins.AutoSize = True
        FCoins.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        FCoins.Font = New Font("Tomb of the Mask", 24F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FCoins.ForeColor = SystemColors.ControlText
        FCoins.Location = New Point(815, 507)
        FCoins.Name = "FCoins"
        FCoins.Size = New Size(188, 48)
        FCoins.TabIndex = 7
        FCoins.Text = "+ 250"
        FCoins.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Replay
        ' 
        Replay.Image = CType(resources.GetObject("Replay.Image"), Image)
        Replay.Location = New Point(756, 799)
        Replay.Name = "Replay"
        Replay.Size = New Size(416, 101)
        Replay.TabIndex = 6
        Replay.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(756, 688)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(416, 101)
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' NotFinishSM
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1924, 1055)
        Controls.Add(FCoins)
        Controls.Add(Replay)
        Controls.Add(PictureBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "NotFinishSM"
        Text = "Tomb Of The Mask"
        CType(Replay, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents FCoins As Label
    Friend WithEvents Replay As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
End Class

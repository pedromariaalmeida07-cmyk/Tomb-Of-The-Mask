<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinishSM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FinishSM))
        PictureBox1 = New PictureBox()
        Replay = New PictureBox()
        FCoins = New Label()
        FTime = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(Replay, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(754, 688)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(416, 101)
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        ' 
        ' Replay
        ' 
        Replay.Image = CType(resources.GetObject("Replay.Image"), Image)
        Replay.Location = New Point(754, 799)
        Replay.Name = "Replay"
        Replay.Size = New Size(416, 101)
        Replay.TabIndex = 3
        Replay.TabStop = False
        ' 
        ' FCoins
        ' 
        FCoins.AutoSize = True
        FCoins.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        FCoins.Font = New Font("Tomb of the Mask", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FCoins.ForeColor = SystemColors.ControlText
        FCoins.Location = New Point(895, 566)
        FCoins.Name = "FCoins"
        FCoins.Size = New Size(54, 24)
        FCoins.TabIndex = 4
        FCoins.Text = "+ 0"
        FCoins.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' FTime
        ' 
        FTime.AutoSize = True
        FTime.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        FTime.Font = New Font("Tomb of the Mask", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FTime.ForeColor = SystemColors.ControlText
        FTime.Location = New Point(917, 476)
        FTime.Name = "FTime"
        FTime.Size = New Size(98, 24)
        FTime.TabIndex = 5
        FTime.Text = "00:00"
        FTime.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' FinishSM
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1924, 1055)
        Controls.Add(FTime)
        Controls.Add(FCoins)
        Controls.Add(Replay)
        Controls.Add(PictureBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "FinishSM"
        Text = "Tomb Of The Mask"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(Replay, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtTime As TextBox
    Friend WithEvents FinalCoins As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Replay As PictureBox
    Friend WithEvents FCoins As Label
    Friend WithEvents FTime As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NFlvl2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NFlvl2))
        charg = New Label()
        Menu = New PictureBox()
        Replay = New PictureBox()
        CType(Menu, ComponentModel.ISupportInitialize).BeginInit()
        CType(Replay, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' charg
        ' 
        charg.AutoSize = True
        charg.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        charg.Font = New Font("Tomb of the Mask", 24F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        charg.Location = New Point(995, 553)
        charg.Name = "charg"
        charg.Size = New Size(60, 48)
        charg.TabIndex = 0
        charg.Text = "3"
        ' 
        ' Menu
        ' 
        Menu.Image = CType(resources.GetObject("Menu.Image"), Image)
        Menu.Location = New Point(756, 688)
        Menu.Name = "Menu"
        Menu.Size = New Size(416, 100)
        Menu.TabIndex = 1
        Menu.TabStop = False
        ' 
        ' Replay
        ' 
        Replay.Image = CType(resources.GetObject("Replay.Image"), Image)
        Replay.Location = New Point(756, 794)
        Replay.Name = "Replay"
        Replay.Size = New Size(416, 100)
        Replay.TabIndex = 2
        Replay.TabStop = False
        ' 
        ' NFlvl2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1924, 1055)
        Controls.Add(Replay)
        Controls.Add(Menu)
        Controls.Add(charg)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "NFlvl2"
        Text = "Tomb Of The Mask"
        CType(Menu, ComponentModel.ISupportInitialize).EndInit()
        CType(Replay, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents charg As Label
    Friend WithEvents Menu As PictureBox
    Friend WithEvents Replay As PictureBox
End Class

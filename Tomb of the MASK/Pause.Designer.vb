<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pause
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pause))
        BttExit = New PictureBox()
        BttContinue = New PictureBox()
        BttX = New PictureBox()
        picCount = New PictureBox()
        CType(BttExit, ComponentModel.ISupportInitialize).BeginInit()
        CType(BttContinue, ComponentModel.ISupportInitialize).BeginInit()
        CType(BttX, ComponentModel.ISupportInitialize).BeginInit()
        CType(picCount, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' BttExit
        ' 
        BttExit.BackgroundImage = CType(resources.GetObject("BttExit.BackgroundImage"), Image)
        BttExit.Location = New Point(774, 461)
        BttExit.Margin = New Padding(3, 2, 3, 2)
        BttExit.Name = "BttExit"
        BttExit.Size = New Size(136, 100)
        BttExit.TabIndex = 2
        BttExit.TabStop = False
        ' 
        ' BttContinue
        ' 
        BttContinue.BackgroundImage = CType(resources.GetObject("BttContinue.BackgroundImage"), Image)
        BttContinue.Location = New Point(774, 311)
        BttContinue.Margin = New Padding(3, 2, 3, 2)
        BttContinue.Name = "BttContinue"
        BttContinue.Size = New Size(136, 100)
        BttContinue.TabIndex = 3
        BttContinue.TabStop = False
        ' 
        ' BttX
        ' 
        BttX.BackgroundImage = CType(resources.GetObject("BttX.BackgroundImage"), Image)
        BttX.Location = New Point(1045, 230)
        BttX.Margin = New Padding(3, 2, 3, 2)
        BttX.Name = "BttX"
        BttX.Size = New Size(46, 39)
        BttX.TabIndex = 4
        BttX.TabStop = False
        ' 
        ' picCount
        ' 
        picCount.Image = CType(resources.GetObject("picCount.Image"), Image)
        picCount.Location = New Point(-8, -30)
        picCount.Margin = New Padding(3, 2, 3, 2)
        picCount.Name = "picCount"
        picCount.Size = New Size(1680, 810)
        picCount.TabIndex = 5
        picCount.TabStop = False
        picCount.Visible = False
        ' 
        ' Pause
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1684, 791)
        Controls.Add(picCount)
        Controls.Add(BttX)
        Controls.Add(BttContinue)
        Controls.Add(BttExit)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Pause"
        Text = "Tomb Of The Mask"
        CType(BttExit, ComponentModel.ISupportInitialize).EndInit()
        CType(BttContinue, ComponentModel.ISupportInitialize).EndInit()
        CType(BttX, ComponentModel.ISupportInitialize).EndInit()
        CType(picCount, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents BttExit As PictureBox
    Friend WithEvents BttContinue As PictureBox
    Friend WithEvents BttX As PictureBox
    Friend WithEvents picCount As PictureBox
End Class

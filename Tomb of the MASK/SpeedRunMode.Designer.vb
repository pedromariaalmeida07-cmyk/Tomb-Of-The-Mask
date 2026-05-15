<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpeedRunMode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpeedRunMode))
        Count = New PictureBox()
        Message = New PictureBox()
        CType(Count, ComponentModel.ISupportInitialize).BeginInit()
        CType(Message, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Count
        ' 
        Count.Image = CType(resources.GetObject("Count.Image"), Image)
        Count.Location = New Point(0, 0)
        Count.Name = "Count"
        Count.Size = New Size(1920, 1080)
        Count.TabIndex = 0
        Count.TabStop = False
        Count.Visible = False
        ' 
        ' Message
        ' 
        Message.Image = CType(resources.GetObject("Message.Image"), Image)
        Message.Location = New Point(873, 2)
        Message.Name = "Message"
        Message.Size = New Size(191, 98)
        Message.TabIndex = 1
        Message.TabStop = False
        Message.Visible = False
        ' 
        ' SpeedRunMode
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        ClientSize = New Size(1924, 1055)
        Controls.Add(Message)
        Controls.Add(Count)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "SpeedRunMode"
        Text = "Tomb Of The Mask"
        CType(Count, ComponentModel.ISupportInitialize).EndInit()
        CType(Message, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Count As PictureBox
    Friend WithEvents Message As PictureBox
End Class

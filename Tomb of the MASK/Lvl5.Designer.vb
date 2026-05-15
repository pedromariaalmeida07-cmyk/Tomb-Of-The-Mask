<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lvl5
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lvl5))
        Count = New PictureBox()
        Message = New PictureBox()
        CType(Count, ComponentModel.ISupportInitialize).BeginInit()
        CType(Message, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Count
        ' 
        Count.Image = CType(resources.GetObject("Count.Image"), Image)
        Count.Location = New Point(2, -13)
        Count.Name = "Count"
        Count.Size = New Size(1920, 1080)
        Count.TabIndex = 4
        Count.TabStop = False
        Count.Visible = False
        ' 
        ' Message
        ' 
        Message.Image = CType(resources.GetObject("Message.Image"), Image)
        Message.Location = New Point(866, -13)
        Message.Name = "Message"
        Message.Size = New Size(191, 98)
        Message.TabIndex = 5
        Message.TabStop = False
        Message.Visible = False
        ' 
        ' Lvl5
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1055)
        Controls.Add(Message)
        Controls.Add(Count)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Lvl5"
        Text = "Tomb Of The Mask"
        CType(Count, ComponentModel.ISupportInitialize).EndInit()
        CType(Message, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Count As PictureBox
    Friend WithEvents Message As PictureBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NFlvl10
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NFlvl10))
        Replay = New PictureBox()
        Menu = New PictureBox()
        charg = New Label()
        CType(Replay, ComponentModel.ISupportInitialize).BeginInit()
        CType(Menu, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Replay
        ' 
        Replay.Image = CType(resources.GetObject("Replay.Image"), Image)
        Replay.Location = New Point(758, 798)
        Replay.Name = "Replay"
        Replay.Size = New Size(416, 100)
        Replay.TabIndex = 5
        Replay.TabStop = False
        ' 
        ' Menu
        ' 
        Menu.Image = CType(resources.GetObject("Menu.Image"), Image)
        Menu.Location = New Point(758, 686)
        Menu.Name = "Menu"
        Menu.Size = New Size(416, 100)
        Menu.TabIndex = 4
        Menu.TabStop = False
        ' 
        ' charg
        ' 
        charg.AutoSize = True
        charg.BackColor = Color.FromArgb(CByte(255), CByte(246), CByte(0))
        charg.Font = New Font("Tomb of the Mask", 24F)
        charg.Location = New Point(997, 551)
        charg.Name = "charg"
        charg.Size = New Size(60, 48)
        charg.TabIndex = 3
        charg.Text = "3"
        ' 
        ' NFlvl10
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1924, 1055)
        Controls.Add(Replay)
        Controls.Add(Menu)
        Controls.Add(charg)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "NFlvl10"
        Text = "Tomb Of The Mask"
        CType(Replay, ComponentModel.ISupportInitialize).EndInit()
        CType(Menu, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Replay As PictureBox
    Friend WithEvents Menu As PictureBox
    Friend WithEvents charg As Label
End Class

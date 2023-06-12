<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ltxtWriteLength = New System.Windows.Forms.TextBox()
        Me.utxtWriteData = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.grbRead = New System.Windows.Forms.GroupBox()
        Me.ltxtReadLength = New System.Windows.Forms.TextBox()
        Me.ltxtReadAddress = New System.Windows.Forms.TextBox()
        Me.utxtReadData = New System.Windows.Forms.TextBox()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.cmbReadBlock = New System.Windows.Forms.ComboBox()
        Me.lblReadBlock = New System.Windows.Forms.Label()
        Me.lblReadMark = New System.Windows.Forms.Label()
        Me.lblReadAddress = New System.Windows.Forms.Label()
        Me.lblReadData = New System.Windows.Forms.Label()
        Me.lblReadLength = New System.Windows.Forms.Label()
        Me.grbIdentify = New System.Windows.Forms.GroupBox()
        Me.utxtCard = New System.Windows.Forms.TextBox()
        Me.lblCard = New System.Windows.Forms.Label()
        Me.btnIdentify = New System.Windows.Forms.Button()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.btnGetConfig = New System.Windows.Forms.Button()
        Me.btnInitPassive = New System.Windows.Forms.Button()
        Me.btnInitActive = New System.Windows.Forms.Button()
        Me.btnInformation = New System.Windows.Forms.Button()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtport = New System.Windows.Forms.TextBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label13 = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.ltxtWriteAddress = New System.Windows.Forms.TextBox()
        Me.btnWrite = New System.Windows.Forms.Button()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.txtRes = New System.Windows.Forms.TextBox()
        Me.grbWrite = New System.Windows.Forms.GroupBox()
        Me.cmbWriteBlock = New System.Windows.Forms.ComboBox()
        Me.lblWriteBlock = New System.Windows.Forms.Label()
        Me.lblWriteMark = New System.Windows.Forms.Label()
        Me.lblWriteAddress = New System.Windows.Forms.Label()
        Me.lblWriteLength = New System.Windows.Forms.Label()
        Me.lblWriteData = New System.Windows.Forms.Label()
        Me.grbRead.SuspendLayout()
        Me.grbIdentify.SuspendLayout()
        Me.pnlTitle.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.pnlBody.SuspendLayout()
        Me.grbWrite.SuspendLayout()
        Me.SuspendLayout()
        '
        'ltxtWriteLength
        '
        Me.ltxtWriteLength.Location = New System.Drawing.Point(424, 18)
        Me.ltxtWriteLength.Name = "ltxtWriteLength"
        Me.ltxtWriteLength.Size = New System.Drawing.Size(100, 21)
        Me.ltxtWriteLength.TabIndex = 13
        Me.ltxtWriteLength.Text = "12"
        '
        'utxtWriteData
        '
        Me.utxtWriteData.Location = New System.Drawing.Point(100, 49)
        Me.utxtWriteData.Name = "utxtWriteData"
        Me.utxtWriteData.Size = New System.Drawing.Size(528, 21)
        Me.utxtWriteData.TabIndex = 10
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.Location = New System.Drawing.Point(300, 12)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(125, 12)
        Me.label12.TabIndex = 5
        Me.label12.Text = "IP Port or Baudrate:"
        '
        'grbRead
        '
        Me.grbRead.Controls.Add(Me.ltxtReadLength)
        Me.grbRead.Controls.Add(Me.ltxtReadAddress)
        Me.grbRead.Controls.Add(Me.utxtReadData)
        Me.grbRead.Controls.Add(Me.btnRead)
        Me.grbRead.Controls.Add(Me.cmbReadBlock)
        Me.grbRead.Controls.Add(Me.lblReadBlock)
        Me.grbRead.Controls.Add(Me.lblReadMark)
        Me.grbRead.Controls.Add(Me.lblReadAddress)
        Me.grbRead.Controls.Add(Me.lblReadData)
        Me.grbRead.Controls.Add(Me.lblReadLength)
        Me.grbRead.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbRead.Location = New System.Drawing.Point(0, 52)
        Me.grbRead.Name = "grbRead"
        Me.grbRead.Size = New System.Drawing.Size(770, 82)
        Me.grbRead.TabIndex = 4
        Me.grbRead.TabStop = False
        Me.grbRead.Text = "EPC(GEN 2) Read"
        '
        'ltxtReadLength
        '
        Me.ltxtReadLength.Location = New System.Drawing.Point(424, 17)
        Me.ltxtReadLength.Name = "ltxtReadLength"
        Me.ltxtReadLength.Size = New System.Drawing.Size(100, 21)
        Me.ltxtReadLength.TabIndex = 12
        Me.ltxtReadLength.Text = "12"
        '
        'ltxtReadAddress
        '
        Me.ltxtReadAddress.Location = New System.Drawing.Point(250, 18)
        Me.ltxtReadAddress.Name = "ltxtReadAddress"
        Me.ltxtReadAddress.Size = New System.Drawing.Size(100, 21)
        Me.ltxtReadAddress.TabIndex = 11
        Me.ltxtReadAddress.Text = "02"
        '
        'utxtReadData
        '
        Me.utxtReadData.Location = New System.Drawing.Point(100, 49)
        Me.utxtReadData.Name = "utxtReadData"
        Me.utxtReadData.Size = New System.Drawing.Size(528, 21)
        Me.utxtReadData.TabIndex = 10
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(634, 45)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(120, 30)
        Me.btnRead.TabIndex = 4
        Me.btnRead.TabStop = False
        Me.btnRead.Text = "&Read"
        Me.btnRead.UseVisualStyleBackColor = True
        '
        'cmbReadBlock
        '
        Me.cmbReadBlock.FormattingEnabled = True
        Me.cmbReadBlock.Items.AddRange(New Object() {"0-Reserved", "1-EPC", "2-TID", "3-User"})
        Me.cmbReadBlock.Location = New System.Drawing.Point(100, 18)
        Me.cmbReadBlock.Name = "cmbReadBlock"
        Me.cmbReadBlock.Size = New System.Drawing.Size(86, 20)
        Me.cmbReadBlock.TabIndex = 0
        Me.cmbReadBlock.TabStop = False
        Me.cmbReadBlock.Text = "1-EPC"
        '
        'lblReadBlock
        '
        Me.lblReadBlock.AutoSize = True
        Me.lblReadBlock.Location = New System.Drawing.Point(12, 22)
        Me.lblReadBlock.Name = "lblReadBlock"
        Me.lblReadBlock.Size = New System.Drawing.Size(41, 12)
        Me.lblReadBlock.TabIndex = 5
        Me.lblReadBlock.Text = "Block:"
        '
        'lblReadMark
        '
        Me.lblReadMark.AutoSize = True
        Me.lblReadMark.Location = New System.Drawing.Point(549, 22)
        Me.lblReadMark.Name = "lblReadMark"
        Me.lblReadMark.Size = New System.Drawing.Size(125, 12)
        Me.lblReadMark.TabIndex = 8
        Me.lblReadMark.Text = "(Length not more 16)"
        '
        'lblReadAddress
        '
        Me.lblReadAddress.AutoSize = True
        Me.lblReadAddress.Location = New System.Drawing.Point(194, 22)
        Me.lblReadAddress.Name = "lblReadAddress"
        Me.lblReadAddress.Size = New System.Drawing.Size(53, 12)
        Me.lblReadAddress.TabIndex = 6
        Me.lblReadAddress.Text = "Address:"
        '
        'lblReadData
        '
        Me.lblReadData.AutoSize = True
        Me.lblReadData.Location = New System.Drawing.Point(12, 52)
        Me.lblReadData.Name = "lblReadData"
        Me.lblReadData.Size = New System.Drawing.Size(35, 12)
        Me.lblReadData.TabIndex = 9
        Me.lblReadData.Text = "Data:"
        '
        'lblReadLength
        '
        Me.lblReadLength.AutoSize = True
        Me.lblReadLength.Location = New System.Drawing.Point(371, 22)
        Me.lblReadLength.Name = "lblReadLength"
        Me.lblReadLength.Size = New System.Drawing.Size(47, 12)
        Me.lblReadLength.TabIndex = 7
        Me.lblReadLength.Text = "Length:"
        '
        'grbIdentify
        '
        Me.grbIdentify.Controls.Add(Me.utxtCard)
        Me.grbIdentify.Controls.Add(Me.lblCard)
        Me.grbIdentify.Controls.Add(Me.btnIdentify)
        Me.grbIdentify.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbIdentify.Location = New System.Drawing.Point(0, 0)
        Me.grbIdentify.Name = "grbIdentify"
        Me.grbIdentify.Size = New System.Drawing.Size(770, 52)
        Me.grbIdentify.TabIndex = 3
        Me.grbIdentify.TabStop = False
        Me.grbIdentify.Text = "EPC(GEN 2) Identify"
        '
        'utxtCard
        '
        Me.utxtCard.Location = New System.Drawing.Point(100, 19)
        Me.utxtCard.Name = "utxtCard"
        Me.utxtCard.Size = New System.Drawing.Size(528, 21)
        Me.utxtCard.TabIndex = 3
        '
        'lblCard
        '
        Me.lblCard.AutoSize = True
        Me.lblCard.Location = New System.Drawing.Point(12, 22)
        Me.lblCard.Name = "lblCard"
        Me.lblCard.Size = New System.Drawing.Size(53, 12)
        Me.lblCard.TabIndex = 2
        Me.lblCard.Text = "Card No:"
        '
        'btnIdentify
        '
        Me.btnIdentify.Location = New System.Drawing.Point(634, 15)
        Me.btnIdentify.Name = "btnIdentify"
        Me.btnIdentify.Size = New System.Drawing.Size(120, 30)
        Me.btnIdentify.TabIndex = 1
        Me.btnIdentify.TabStop = False
        Me.btnIdentify.Text = "&Identify"
        Me.btnIdentify.UseVisualStyleBackColor = True
        '
        'pnlTitle
        '
        Me.pnlTitle.AutoSize = True
        Me.pnlTitle.Controls.Add(Me.btnGetConfig)
        Me.pnlTitle.Controls.Add(Me.btnInitPassive)
        Me.pnlTitle.Controls.Add(Me.btnInitActive)
        Me.pnlTitle.Controls.Add(Me.btnInformation)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 36)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(770, 36)
        Me.pnlTitle.TabIndex = 7
        '
        'btnGetConfig
        '
        Me.btnGetConfig.Location = New System.Drawing.Point(661, 3)
        Me.btnGetConfig.Name = "btnGetConfig"
        Me.btnGetConfig.Size = New System.Drawing.Size(95, 30)
        Me.btnGetConfig.TabIndex = 6
        Me.btnGetConfig.TabStop = False
        Me.btnGetConfig.Text = "GetConfig"
        Me.btnGetConfig.UseVisualStyleBackColor = True
        '
        'btnInitPassive
        '
        Me.btnInitPassive.Location = New System.Drawing.Point(560, 3)
        Me.btnInitPassive.Name = "btnInitPassive"
        Me.btnInitPassive.Size = New System.Drawing.Size(95, 30)
        Me.btnInitPassive.TabIndex = 5
        Me.btnInitPassive.TabStop = False
        Me.btnInitPassive.Text = "Init Passive"
        Me.btnInitPassive.UseVisualStyleBackColor = True
        '
        'btnInitActive
        '
        Me.btnInitActive.Location = New System.Drawing.Point(459, 3)
        Me.btnInitActive.Name = "btnInitActive"
        Me.btnInitActive.Size = New System.Drawing.Size(95, 30)
        Me.btnInitActive.TabIndex = 4
        Me.btnInitActive.TabStop = False
        Me.btnInitActive.Text = "Init Active"
        Me.btnInitActive.UseVisualStyleBackColor = True
        '
        'btnInformation
        '
        Me.btnInformation.Location = New System.Drawing.Point(358, 3)
        Me.btnInformation.Name = "btnInformation"
        Me.btnInformation.Size = New System.Drawing.Size(95, 30)
        Me.btnInformation.TabIndex = 3
        Me.btnInformation.TabStop = False
        Me.btnInformation.Text = "Information"
        Me.btnInformation.UseVisualStyleBackColor = True
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(155, 9)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(139, 21)
        Me.txtIP.TabIndex = 1
        Me.txtIP.Text = "COM1"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(431, 9)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(74, 21)
        Me.txtport.TabIndex = 2
        Me.txtport.Text = "9600"
        '
        'panel1
        '
        Me.panel1.AutoSize = True
        Me.panel1.Controls.Add(Me.label12)
        Me.panel1.Controls.Add(Me.label13)
        Me.panel1.Controls.Add(Me.btnConnect)
        Me.panel1.Controls.Add(Me.txtIP)
        Me.panel1.Controls.Add(Me.txtport)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(770, 36)
        Me.panel1.TabIndex = 8
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Location = New System.Drawing.Point(12, 12)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(137, 12)
        Me.label13.TabIndex = 4
        Me.label13.Text = "IP Address or Comport:"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(560, 3)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(95, 30)
        Me.btnConnect.TabIndex = 0
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'ltxtWriteAddress
        '
        Me.ltxtWriteAddress.Location = New System.Drawing.Point(253, 19)
        Me.ltxtWriteAddress.Name = "ltxtWriteAddress"
        Me.ltxtWriteAddress.Size = New System.Drawing.Size(100, 21)
        Me.ltxtWriteAddress.TabIndex = 12
        Me.ltxtWriteAddress.Text = "02"
        '
        'btnWrite
        '
        Me.btnWrite.Location = New System.Drawing.Point(634, 45)
        Me.btnWrite.Name = "btnWrite"
        Me.btnWrite.Size = New System.Drawing.Size(120, 30)
        Me.btnWrite.TabIndex = 4
        Me.btnWrite.TabStop = False
        Me.btnWrite.Text = "Writ&e"
        Me.btnWrite.UseVisualStyleBackColor = True
        '
        'pnlBody
        '
        Me.pnlBody.Controls.Add(Me.txtRes)
        Me.pnlBody.Controls.Add(Me.grbWrite)
        Me.pnlBody.Controls.Add(Me.grbRead)
        Me.pnlBody.Controls.Add(Me.grbIdentify)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(0, 72)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Size = New System.Drawing.Size(770, 511)
        Me.pnlBody.TabIndex = 6
        '
        'txtRes
        '
        Me.txtRes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRes.Location = New System.Drawing.Point(0, 216)
        Me.txtRes.Multiline = True
        Me.txtRes.Name = "txtRes"
        Me.txtRes.Size = New System.Drawing.Size(770, 295)
        Me.txtRes.TabIndex = 9
        '
        'grbWrite
        '
        Me.grbWrite.Controls.Add(Me.ltxtWriteLength)
        Me.grbWrite.Controls.Add(Me.ltxtWriteAddress)
        Me.grbWrite.Controls.Add(Me.utxtWriteData)
        Me.grbWrite.Controls.Add(Me.btnWrite)
        Me.grbWrite.Controls.Add(Me.cmbWriteBlock)
        Me.grbWrite.Controls.Add(Me.lblWriteBlock)
        Me.grbWrite.Controls.Add(Me.lblWriteMark)
        Me.grbWrite.Controls.Add(Me.lblWriteAddress)
        Me.grbWrite.Controls.Add(Me.lblWriteLength)
        Me.grbWrite.Controls.Add(Me.lblWriteData)
        Me.grbWrite.Dock = System.Windows.Forms.DockStyle.Top
        Me.grbWrite.Location = New System.Drawing.Point(0, 134)
        Me.grbWrite.Name = "grbWrite"
        Me.grbWrite.Size = New System.Drawing.Size(770, 82)
        Me.grbWrite.TabIndex = 5
        Me.grbWrite.TabStop = False
        Me.grbWrite.Text = "EPC(GEN 2) Write"
        '
        'cmbWriteBlock
        '
        Me.cmbWriteBlock.FormattingEnabled = True
        Me.cmbWriteBlock.Items.AddRange(New Object() {"0-Reserved", "1-EPC", "2-TID", "3-User"})
        Me.cmbWriteBlock.Location = New System.Drawing.Point(100, 18)
        Me.cmbWriteBlock.Name = "cmbWriteBlock"
        Me.cmbWriteBlock.Size = New System.Drawing.Size(86, 20)
        Me.cmbWriteBlock.TabIndex = 0
        Me.cmbWriteBlock.TabStop = False
        Me.cmbWriteBlock.Text = "1-EPC"
        '
        'lblWriteBlock
        '
        Me.lblWriteBlock.AutoSize = True
        Me.lblWriteBlock.Location = New System.Drawing.Point(12, 22)
        Me.lblWriteBlock.Name = "lblWriteBlock"
        Me.lblWriteBlock.Size = New System.Drawing.Size(41, 12)
        Me.lblWriteBlock.TabIndex = 5
        Me.lblWriteBlock.Text = "Block:"
        '
        'lblWriteMark
        '
        Me.lblWriteMark.AutoSize = True
        Me.lblWriteMark.Location = New System.Drawing.Point(549, 22)
        Me.lblWriteMark.Name = "lblWriteMark"
        Me.lblWriteMark.Size = New System.Drawing.Size(221, 12)
        Me.lblWriteMark.TabIndex = 8
        Me.lblWriteMark.Text = "(Address start 2,Length not more 16)"
        '
        'lblWriteAddress
        '
        Me.lblWriteAddress.AutoSize = True
        Me.lblWriteAddress.Location = New System.Drawing.Point(194, 22)
        Me.lblWriteAddress.Name = "lblWriteAddress"
        Me.lblWriteAddress.Size = New System.Drawing.Size(53, 12)
        Me.lblWriteAddress.TabIndex = 6
        Me.lblWriteAddress.Text = "Address:"
        '
        'lblWriteLength
        '
        Me.lblWriteLength.AutoSize = True
        Me.lblWriteLength.Location = New System.Drawing.Point(371, 22)
        Me.lblWriteLength.Name = "lblWriteLength"
        Me.lblWriteLength.Size = New System.Drawing.Size(47, 12)
        Me.lblWriteLength.TabIndex = 7
        Me.lblWriteLength.Text = "Length:"
        '
        'lblWriteData
        '
        Me.lblWriteData.AutoSize = True
        Me.lblWriteData.Location = New System.Drawing.Point(12, 52)
        Me.lblWriteData.Name = "lblWriteData"
        Me.lblWriteData.Size = New System.Drawing.Size(35, 12)
        Me.lblWriteData.TabIndex = 9
        Me.lblWriteData.Text = "Data:"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 583)
        Me.Controls.Add(Me.pnlBody)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.panel1)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Passive Quick Demo(VB)"
        Me.grbRead.ResumeLayout(False)
        Me.grbRead.PerformLayout()
        Me.grbIdentify.ResumeLayout(False)
        Me.grbIdentify.PerformLayout()
        Me.pnlTitle.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.pnlBody.ResumeLayout(False)
        Me.pnlBody.PerformLayout()
        Me.grbWrite.ResumeLayout(False)
        Me.grbWrite.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents ltxtWriteLength As TextBox
    Private WithEvents utxtWriteData As TextBox
    Private WithEvents label12 As Label
    Private WithEvents grbRead As GroupBox
    Private WithEvents ltxtReadLength As TextBox
    Private WithEvents ltxtReadAddress As TextBox
    Private WithEvents utxtReadData As TextBox
    Private WithEvents btnRead As Button
    Private WithEvents cmbReadBlock As ComboBox
    Private WithEvents lblReadBlock As Label
    Private WithEvents lblReadMark As Label
    Private WithEvents lblReadAddress As Label
    Private WithEvents lblReadData As Label
    Private WithEvents lblReadLength As Label
    Private WithEvents grbIdentify As GroupBox
    Private WithEvents utxtCard As TextBox
    Private WithEvents lblCard As Label
    Private WithEvents btnIdentify As Button
    Private WithEvents pnlTitle As Panel
    Private WithEvents btnGetConfig As Button
    Private WithEvents btnInitPassive As Button
    Private WithEvents btnInitActive As Button
    Private WithEvents btnInformation As Button
    Private WithEvents txtIP As TextBox
    Private WithEvents txtport As TextBox
    Private WithEvents panel1 As Panel
    Private WithEvents label13 As Label
    Private WithEvents btnConnect As Button
    Private WithEvents ltxtWriteAddress As TextBox
    Private WithEvents btnWrite As Button
    Private WithEvents pnlBody As Panel
    Private WithEvents txtRes As TextBox
    Private WithEvents grbWrite As GroupBox
    Private WithEvents cmbWriteBlock As ComboBox
    Private WithEvents lblWriteBlock As Label
    Private WithEvents lblWriteMark As Label
    Private WithEvents lblWriteAddress As Label
    Private WithEvents lblWriteLength As Label
    Private WithEvents lblWriteData As Label
End Class

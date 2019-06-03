<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.txtDay = New System.Windows.Forms.TextBox()
        Me.txtBaktun = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtMonth = New System.Windows.Forms.TextBox()
        Me.txtKin = New System.Windows.Forms.TextBox()
        Me.txtUinal = New System.Windows.Forms.TextBox()
        Me.txtTun = New System.Windows.Forms.TextBox()
        Me.txtKatun = New System.Windows.Forms.TextBox()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtDay
        '
        Me.txtDay.Location = New System.Drawing.Point(52, 10)
        Me.txtDay.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDay.Name = "txtDay"
        Me.txtDay.Size = New System.Drawing.Size(76, 20)
        Me.txtDay.TabIndex = 0
        '
        'txtBaktun
        '
        Me.txtBaktun.Enabled = False
        Me.txtBaktun.Location = New System.Drawing.Point(9, 41)
        Me.txtBaktun.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBaktun.Name = "txtBaktun"
        Me.txtBaktun.Size = New System.Drawing.Size(76, 20)
        Me.txtBaktun.TabIndex = 1
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(290, 10)
        Me.txtYear.Margin = New System.Windows.Forms.Padding(2)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(76, 20)
        Me.txtYear.TabIndex = 3
        '
        'txtMonth
        '
        Me.txtMonth.Location = New System.Drawing.Point(168, 10)
        Me.txtMonth.Margin = New System.Windows.Forms.Padding(2)
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.Size = New System.Drawing.Size(76, 20)
        Me.txtMonth.TabIndex = 2
        '
        'txtKin
        '
        Me.txtKin.Enabled = False
        Me.txtKin.Location = New System.Drawing.Point(327, 41)
        Me.txtKin.Margin = New System.Windows.Forms.Padding(2)
        Me.txtKin.Name = "txtKin"
        Me.txtKin.Size = New System.Drawing.Size(76, 20)
        Me.txtKin.TabIndex = 4
        '
        'txtUinal
        '
        Me.txtUinal.Enabled = False
        Me.txtUinal.Location = New System.Drawing.Point(248, 41)
        Me.txtUinal.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUinal.Name = "txtUinal"
        Me.txtUinal.Size = New System.Drawing.Size(76, 20)
        Me.txtUinal.TabIndex = 5
        '
        'txtTun
        '
        Me.txtTun.Enabled = False
        Me.txtTun.Location = New System.Drawing.Point(168, 41)
        Me.txtTun.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTun.Name = "txtTun"
        Me.txtTun.Size = New System.Drawing.Size(76, 20)
        Me.txtTun.TabIndex = 6
        '
        'txtKatun
        '
        Me.txtKatun.Enabled = False
        Me.txtKatun.Location = New System.Drawing.Point(88, 41)
        Me.txtKatun.Margin = New System.Windows.Forms.Padding(2)
        Me.txtKatun.Name = "txtKatun"
        Me.txtKatun.Size = New System.Drawing.Size(76, 20)
        Me.txtKatun.TabIndex = 7
        '
        'btnConvert
        '
        Me.btnConvert.Location = New System.Drawing.Point(88, 67)
        Me.btnConvert.Margin = New System.Windows.Forms.Padding(2)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(234, 25)
        Me.btnConvert.TabIndex = 8
        Me.btnConvert.Text = "Convert"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 102)
        Me.Controls.Add(Me.btnConvert)
        Me.Controls.Add(Me.txtKatun)
        Me.Controls.Add(Me.txtTun)
        Me.Controls.Add(Me.txtUinal)
        Me.Controls.Add(Me.txtKin)
        Me.Controls.Add(Me.txtMonth)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.txtBaktun)
        Me.Controls.Add(Me.txtDay)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainForm"
        Me.Text = "Julian to Mayan Calendar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDay As System.Windows.Forms.TextBox
    Friend WithEvents txtBaktun As System.Windows.Forms.TextBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtMonth As System.Windows.Forms.TextBox
    Friend WithEvents txtKin As System.Windows.Forms.TextBox
    Friend WithEvents txtUinal As System.Windows.Forms.TextBox
    Friend WithEvents txtTun As System.Windows.Forms.TextBox
    Friend WithEvents txtKatun As System.Windows.Forms.TextBox
    Friend WithEvents btnConvert As System.Windows.Forms.Button

End Class

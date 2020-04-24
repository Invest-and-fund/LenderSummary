<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.frmAccountName = New System.Windows.Forms.Label()
        Me.frmAccountID = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Go = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.frmLoanID = New System.Windows.Forms.TextBox()
        Me.frmLHID = New System.Windows.Forms.TextBox()
        Me.frmLoanName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.frmLoanExtr = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(63, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Account"
        '
        'frmAccountName
        '
        Me.frmAccountName.AutoSize = True
        Me.frmAccountName.Location = New System.Drawing.Point(231, 9)
        Me.frmAccountName.Name = "frmAccountName"
        Me.frmAccountName.Size = New System.Drawing.Size(39, 13)
        Me.frmAccountName.TabIndex = 1
        Me.frmAccountName.Text = "Label2"
        '
        'frmAccountID
        '
        Me.frmAccountID.Location = New System.Drawing.Point(115, 6)
        Me.frmAccountID.Name = "frmAccountID"
        Me.frmAccountID.Size = New System.Drawing.Size(100, 20)
        Me.frmAccountID.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(8, 113)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1306, 500)
        Me.DataGridView1.TabIndex = 3
        '
        'Go
        '
        Me.Go.Location = New System.Drawing.Point(1239, 5)
        Me.Go.Name = "Go"
        Me.Go.Size = New System.Drawing.Size(75, 68)
        Me.Go.TabIndex = 4
        Me.Go.Text = "Go"
        Me.Go.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(78, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Loan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Loan Holding"
        '
        'frmLoanID
        '
        Me.frmLoanID.Location = New System.Drawing.Point(115, 30)
        Me.frmLoanID.Name = "frmLoanID"
        Me.frmLoanID.Size = New System.Drawing.Size(100, 20)
        Me.frmLoanID.TabIndex = 7
        '
        'frmLHID
        '
        Me.frmLHID.Location = New System.Drawing.Point(115, 54)
        Me.frmLHID.Name = "frmLHID"
        Me.frmLHID.Size = New System.Drawing.Size(100, 20)
        Me.frmLHID.TabIndex = 8
        '
        'frmLoanName
        '
        Me.frmLoanName.AutoSize = True
        Me.frmLoanName.Location = New System.Drawing.Point(231, 33)
        Me.frmLoanName.Name = "frmLoanName"
        Me.frmLoanName.Size = New System.Drawing.Size(39, 13)
        Me.frmLoanName.TabIndex = 9
        Me.frmLoanName.Text = "Label4"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "part of Loan Name"
        '
        'frmLoanExtr
        '
        Me.frmLoanExtr.Location = New System.Drawing.Point(115, 78)
        Me.frmLoanExtr.Name = "frmLoanExtr"
        Me.frmLoanExtr.Size = New System.Drawing.Size(239, 20)
        Me.frmLoanExtr.TabIndex = 11
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1321, 621)
        Me.Controls.Add(Me.frmLoanExtr)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.frmLoanName)
        Me.Controls.Add(Me.frmLHID)
        Me.Controls.Add(Me.frmLoanID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Go)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.frmAccountID)
        Me.Controls.Add(Me.frmAccountName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Lender Summary"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents frmAccountName As Label
    Friend WithEvents frmAccountID As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Go As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents frmLoanID As TextBox
    Friend WithEvents frmLHID As TextBox
    Friend WithEvents frmLoanName As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents frmLoanExtr As TextBox
End Class

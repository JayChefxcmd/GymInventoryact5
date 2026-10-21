<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.cboBrand = New System.Windows.Forms.ComboBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnViewAll = New System.Windows.Forms.Button()
        Me.dgvInventory = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblRecord = New System.Windows.Forms.Label()
        Me.btnNewEntry = New System.Windows.Forms.Button()
        Me.btnIn = New System.Windows.Forms.Button()
        Me.btnOut = New System.Windows.Forms.Button()
        Me.btnUnitCost = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrintDO = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.dgvRecords = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTitle)
        Me.GroupBox1.Controls.Add(Me.lblBrand)
        Me.GroupBox1.Controls.Add(Me.cboBrand)
        Me.GroupBox1.Controls.Add(Me.lblCategory)
        Me.GroupBox1.Controls.Add(Me.cboCategory)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.btnViewAll)
        Me.GroupBox1.Controls.Add(Me.dgvInventory)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(976, 370)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(6, 18)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(964, 52)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Gym Equipment Inventory System"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBrand.Location = New System.Drawing.Point(40, 92)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(55, 21)
        Me.lblBrand.TabIndex = 1
        Me.lblBrand.Text = "Brand"
        '
        'cboBrand
        '
        Me.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBrand.Location = New System.Drawing.Point(120, 91)
        Me.cboBrand.Name = "cboBrand"
        Me.cboBrand.Size = New System.Drawing.Size(200, 23)
        Me.cboBrand.TabIndex = 2
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblCategory.Location = New System.Drawing.Point(380, 92)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(80, 21)
        Me.lblCategory.TabIndex = 3
        Me.lblCategory.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Location = New System.Drawing.Point(480, 91)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(200, 23)
        Me.cboCategory.TabIndex = 4
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(740, 84)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 36)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "Search"
        '
        'btnViewAll
        '
        Me.btnViewAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewAll.Location = New System.Drawing.Point(850, 84)
        Me.btnViewAll.Name = "btnViewAll"
        Me.btnViewAll.Size = New System.Drawing.Size(100, 36)
        Me.btnViewAll.TabIndex = 6
        Me.btnViewAll.Text = "View All"
        '
        'dgvInventory
        '
        Me.dgvInventory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInventory.Location = New System.Drawing.Point(40, 140)
        Me.dgvInventory.Name = "dgvInventory"
        Me.dgvInventory.Size = New System.Drawing.Size(896, 214)
        Me.dgvInventory.TabIndex = 7
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblRecord)
        Me.GroupBox2.Controls.Add(Me.btnNewEntry)
        Me.GroupBox2.Controls.Add(Me.btnIn)
        Me.GroupBox2.Controls.Add(Me.btnOut)
        Me.GroupBox2.Controls.Add(Me.btnUnitCost)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Controls.Add(Me.btnPrintDO)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.dgvRecords)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 384)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(976, 384)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'lblRecord
        '
        Me.lblRecord.AutoSize = True
        Me.lblRecord.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblRecord.Location = New System.Drawing.Point(20, 30)
        Me.lblRecord.Name = "lblRecord"
        Me.lblRecord.Size = New System.Drawing.Size(240, 28)
        Me.lblRecord.TabIndex = 0
        Me.lblRecord.Text = "Stock In and Out Record"
        '
        'btnNewEntry
        '
        Me.btnNewEntry.Location = New System.Drawing.Point(290, 26)
        Me.btnNewEntry.Name = "btnNewEntry"
        Me.btnNewEntry.Size = New System.Drawing.Size(110, 36)
        Me.btnNewEntry.TabIndex = 1
        Me.btnNewEntry.Text = "New Entry"
        '
        'btnIn
        '
        Me.btnIn.Location = New System.Drawing.Point(430, 26)
        Me.btnIn.Name = "btnIn"
        Me.btnIn.Size = New System.Drawing.Size(66, 36)
        Me.btnIn.TabIndex = 2
        Me.btnIn.Text = "In"
        '
        'btnOut
        '
        Me.btnOut.Location = New System.Drawing.Point(502, 26)
        Me.btnOut.Name = "btnOut"
        Me.btnOut.Size = New System.Drawing.Size(66, 36)
        Me.btnOut.TabIndex = 3
        Me.btnOut.Text = "Out"
        '
        'btnUnitCost
        '
        Me.btnUnitCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUnitCost.Location = New System.Drawing.Point(610, 26)
        Me.btnUnitCost.Name = "btnUnitCost"
        Me.btnUnitCost.Size = New System.Drawing.Size(90, 36)
        Me.btnUnitCost.TabIndex = 4
        Me.btnUnitCost.Text = "Unit Cost"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(706, 26)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 36)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'btnPrintDO
        '
        Me.btnPrintDO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintDO.Location = New System.Drawing.Point(792, 26)
        Me.btnPrintDO.Name = "btnPrintDO"
        Me.btnPrintDO.Size = New System.Drawing.Size(86, 36)
        Me.btnPrintDO.TabIndex = 6
        Me.btnPrintDO.Text = "Print DO"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(884, 26)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(76, 36)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        '
        'dgvRecords
        '
        Me.dgvRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRecords.Location = New System.Drawing.Point(20, 78)
        Me.dgvRecords.Name = "dgvRecords"
        Me.dgvRecords.Size = New System.Drawing.Size(936, 288)
        Me.dgvRecords.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1000, 780)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.ForeColor = System.Drawing.Color.Black
        Me.MinimumSize = New System.Drawing.Size(1016, 819)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gym Equipment Inventory System"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents cboBrand As System.Windows.Forms.ComboBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnViewAll As System.Windows.Forms.Button
    Friend WithEvents dgvInventory As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRecord As System.Windows.Forms.Label
    Friend WithEvents btnNewEntry As System.Windows.Forms.Button
    Friend WithEvents btnIn As System.Windows.Forms.Button
    Friend WithEvents btnOut As System.Windows.Forms.Button
    Friend WithEvents btnUnitCost As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnPrintDO As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dgvRecords As System.Windows.Forms.DataGridView

End Class

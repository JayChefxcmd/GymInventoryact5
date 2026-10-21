Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports MySqlConnector

Public Class Form1

    Private dtInv As DataTable
    Private dtRec As DataTable
    Private viewInv As DataView
    Private viewRec As DataView
    Private italicFont As Font

    Private printRows As DataRow()
    Private printIndex As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    ' ------------------------------------------------------------------
    ' Startup
    ' ------------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildTables()

        viewInv = New DataView(dtInv) With {.Sort = "ID ASC"}
        viewRec = New DataView(dtRec) With {.Sort = "ID DESC"}
        dgvInventory.DataSource = viewInv
        dgvRecords.DataSource = viewRec

        FormatInventoryGrid()
        FormatRecordGrid()
        ApplyTheme()
    End Sub

    ' Connect to XAMPP once the window is visible so errors can be shown properly.
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Do
            Try
                Db.EnsureSchema()
                LoadInventory()
                LoadRecords()
                FillFilters()
                Text = "Gym Equipment Inventory System  -  MySQL (" & New MySqlConnectionStringBuilder(Db.ConnectionString).Server & ")"
                Return
            Catch ex As Exception
                Dim answer = MessageBox.Show(
                    "Could not connect to MySQL." & vbCrLf & vbCrLf &
                    "1. Open the XAMPP Control Panel." & vbCrLf &
                    "2. Press Start next to MySQL." & vbCrLf &
                    "3. Press Retry." & vbCrLf & vbCrLf &
                    "Details: " & ex.Message,
                    "Database Connection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                If answer = DialogResult.Cancel Then
                    Close()
                    Return
                End If
            End Try
        Loop
    End Sub

    Private Sub BuildTables()
        dtInv = New DataTable("Inventory")
        dtInv.Columns.Add("ID", GetType(Integer))
        dtInv.Columns.Add("Brand", GetType(String))
        dtInv.Columns.Add("Category", GetType(String))
        dtInv.Columns.Add("Product", GetType(String))
        dtInv.Columns.Add("Quantity", GetType(Integer))
        dtInv.Columns.Add("UnitCost", GetType(Decimal))

        dtRec = New DataTable("Records")
        dtRec.Columns.Add("ID", GetType(Integer))
        dtRec.Columns.Add("Product", GetType(String))
        dtRec.Columns.Add("Brand", GetType(String))
        dtRec.Columns.Add("Category", GetType(String))
        dtRec.Columns.Add("InOut", GetType(String))
        dtRec.Columns.Add("Quantity", GetType(Integer))
        dtRec.Columns.Add("UnitCost", GetType(Decimal))
        dtRec.Columns.Add("TotalCost", GetType(Decimal))
        dtRec.Columns.Add("EntryDate", GetType(String))
        dtRec.Columns.Add("Saved", GetType(Boolean))
        dtRec.Columns.Add("ProductID", GetType(Integer))

    End Sub

    Private Sub LoadInventory()
        Dim keep As Object = Nothing
        Dim current = SelectedProduct()
        If current IsNot Nothing Then keep = current("ID")

        dtInv.Rows.Clear()
        Using c As MySqlConnection = Db.OpenConnection()
            Using cmd As New MySqlCommand("SELECT id, brand, category, product, quantity, unit_cost FROM equipment ORDER BY id", c)
                Using rd As MySqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        dtInv.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetInt32(4), rd.GetDecimal(5))
                    End While
                End Using
            End Using
        End Using

        If keep IsNot Nothing Then
            For Each gridRow As DataGridViewRow In dgvInventory.Rows
                Dim drv = TryCast(gridRow.DataBoundItem, DataRowView)
                If drv IsNot Nothing AndAlso CInt(drv("ID")) = CInt(keep) Then
                    gridRow.Selected = True
                    Exit For
                End If
            Next
        End If
    End Sub

    ' Loads saved movements from MySQL. Unsaved (pending) rows stay untouched.
    Private Sub LoadRecords()
        For Each r As DataRow In dtRec.Select("Saved = True")
            r.Delete()
        Next
        dtRec.AcceptChanges()

        Using c As MySqlConnection = Db.OpenConnection()
            Dim sql As String = "SELECT id, product, brand, category, direction, quantity, unit_cost, total_cost, " &
                                "LOWER(DATE_FORMAT(entry_date, '%d/%m/%Y %l:%i %p')), equipment_id " &
                                "FROM stock_records ORDER BY id"
            Using cmd As New MySqlCommand(sql, c)
                Using rd As MySqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        dtRec.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4),
                                       rd.GetInt32(5), rd.GetDecimal(6), rd.GetDecimal(7), rd.GetString(8), True, rd.GetInt32(9))
                    End While
                End Using
            End Using
        End Using
    End Sub

    ' ------------------------------------------------------------------
    ' Black and white theme
    ' ------------------------------------------------------------------
    Private Sub ApplyTheme()
        For Each b As Button In New Button() {btnSearch, btnViewAll, btnNewEntry, btnIn, btnOut, btnUnitCost, btnSave, btnPrintDO, btnExit}
            b.FlatStyle = FlatStyle.Flat
            b.FlatAppearance.BorderColor = Color.Black
            b.FlatAppearance.BorderSize = 1
            b.FlatAppearance.MouseOverBackColor = Color.Gainsboro
            b.FlatAppearance.MouseDownBackColor = Color.Silver
            b.BackColor = Color.White
            b.ForeColor = Color.Black
            b.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
            b.Cursor = Cursors.Hand
        Next

        btnNewEntry.BackColor = Color.Black
        btnNewEntry.ForeColor = Color.White
        btnNewEntry.FlatAppearance.MouseOverBackColor = Color.DimGray
        btnNewEntry.FlatAppearance.MouseDownBackColor = Color.Gray

        For Each c As ComboBox In New ComboBox() {cboBrand, cboCategory}
            c.FlatStyle = FlatStyle.Flat
            c.BackColor = Color.White
            c.ForeColor = Color.Black
        Next

        For Each g As GroupBox In New GroupBox() {GroupBox1, GroupBox2}
            g.ForeColor = Color.Black
            g.BackColor = Color.White
        Next

        For Each grid As DataGridView In New DataGridView() {dgvInventory, dgvRecords}
            grid.EnableHeadersVisualStyles = False
            grid.BackgroundColor = Color.White
            grid.BorderStyle = BorderStyle.FixedSingle
            grid.GridColor = Color.Black
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Black
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
            grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            grid.DefaultCellStyle.BackColor = Color.White
            grid.DefaultCellStyle.ForeColor = Color.Black
            grid.DefaultCellStyle.SelectionBackColor = Color.Black
            grid.DefaultCellStyle.SelectionForeColor = Color.White
            grid.RowHeadersVisible = False
            grid.AllowUserToAddRows = False
            grid.AllowUserToDeleteRows = False
            grid.ReadOnly = True
            grid.MultiSelect = False
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Next

        italicFont = New Font(dgvRecords.Font, FontStyle.Italic)
    End Sub

    Private Sub FormatInventoryGrid()
        dgvInventory.Columns("UnitCost").HeaderText = "Unit Cost"
        dgvInventory.Columns("ID").FillWeight = 30
        dgvInventory.Columns("Product").FillWeight = 140
        For Each name As String In {"ID", "Quantity", "UnitCost"}
            dgvInventory.Columns(name).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvInventory.Columns(name).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Next
        dgvInventory.Columns("UnitCost").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub FormatRecordGrid()
        dgvRecords.Columns("Saved").Visible = False
        dgvRecords.Columns("ProductID").Visible = False
        dgvRecords.Columns("InOut").HeaderText = "IN/OUT"
        dgvRecords.Columns("UnitCost").HeaderText = "Unit Cost"
        dgvRecords.Columns("TotalCost").HeaderText = "Total Cost"
        dgvRecords.Columns("EntryDate").HeaderText = "Date"
        dgvRecords.Columns("ID").FillWeight = 30
        dgvRecords.Columns("InOut").FillWeight = 50
        dgvRecords.Columns("Quantity").FillWeight = 60
        dgvRecords.Columns("EntryDate").FillWeight = 120
        For Each name As String In {"ID", "Quantity", "UnitCost", "TotalCost"}
            dgvRecords.Columns(name).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvRecords.Columns(name).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Next
        dgvRecords.Columns("UnitCost").DefaultCellStyle.Format = "N2"
        dgvRecords.Columns("TotalCost").DefaultCellStyle.Format = "N2"
    End Sub

    ' Unsaved entries are shown in italics until Save is pressed.
    Private Sub dgvRecords_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvRecords.CellFormatting
        If e.RowIndex < 0 OrElse italicFont Is Nothing Then Return
        Dim drv = TryCast(dgvRecords.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv IsNot Nothing AndAlso Not CBool(drv("Saved")) Then e.CellStyle.Font = italicFont
    End Sub

    ' ------------------------------------------------------------------
    ' Brand / Category search
    ' ------------------------------------------------------------------
    Private Sub FillFilters()
        cboBrand.Items.Clear()
        cboBrand.Items.Add("All Brands")
        For Each b In dtInv.AsEnumerable().Select(Function(r) r.Field(Of String)("Brand")).Distinct().OrderBy(Function(s) s)
            cboBrand.Items.Add(b)
        Next
        cboBrand.SelectedIndex = 0

        cboCategory.Items.Clear()
        cboCategory.Items.Add("All Categories")
        For Each c In dtInv.AsEnumerable().Select(Function(r) r.Field(Of String)("Category")).Distinct().OrderBy(Function(s) s)
            cboCategory.Items.Add(c)
        Next
        cboCategory.SelectedIndex = 0
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim parts As New List(Of String)
        If cboBrand.SelectedIndex > 0 Then parts.Add("Brand = '" & cboBrand.Text.Replace("'", "''") & "'")
        If cboCategory.SelectedIndex > 0 Then parts.Add("Category = '" & cboCategory.Text.Replace("'", "''") & "'")
        viewInv.RowFilter = String.Join(" AND ", parts)

        If viewInv.Count = 0 Then
            MessageBox.Show("No equipment matches this brand and category." & vbCrLf & "Choose different filters or press View All.",
                            "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        Try
            LoadInventory()   ' picks up changes made in phpMyAdmin
            FillFilters()
        Catch ex As Exception
            ShowDbError(ex)
        End Try
        viewInv.RowFilter = ""
    End Sub

    ' ------------------------------------------------------------------
    ' Stock in / out
    ' ------------------------------------------------------------------
    Private Function SelectedProduct() As DataRow
        If dgvInventory.SelectedRows.Count = 0 Then Return Nothing
        Dim drv = TryCast(dgvInventory.SelectedRows(0).DataBoundItem, DataRowView)
        If drv Is Nothing Then Return Nothing
        Return drv.Row
    End Function

    Private Function RequireProduct() As DataRow
        Dim row = SelectedProduct()
        If row Is Nothing Then
            MessageBox.Show("Please select a product first.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Return row
    End Function

    Private Function NextRecordId() As Integer
        Dim max As Object = dtRec.Compute("MAX(ID)", "")
        If IsDBNull(max) Then Return 1
        Return CInt(max) + 1
    End Function

    Private Function Stamp() As String
        Return Now.ToString("dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture).ToLower()
    End Function

    Private Sub btnNewEntry_Click(sender As Object, e As EventArgs) Handles btnNewEntry.Click
        dgvInventory.ClearSelection()
        MessageBox.Show("New entry is ready." & vbCrLf & "Select a product and choose IN or OUT.",
                        "New Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnIn_Click(sender As Object, e As EventArgs) Handles btnIn.Click
        AddMovement("IN")
    End Sub

    Private Sub btnOut_Click(sender As Object, e As EventArgs) Handles btnOut.Click
        AddMovement("OUT")
    End Sub

    Private Sub AddMovement(direction As String)
        Dim product = RequireProduct()
        If product Is Nothing Then Return

        Dim answer As String = Microsoft.VisualBasic.Interaction.InputBox(
            "Stock " & direction & " selected: " & CStr(product("Product")) & vbCrLf & "Enter quantity:",
            "Stock " & direction, "1")
        If answer = "" Then Return   ' Cancel

        Dim qty As Integer
        If Not Integer.TryParse(answer, qty) OrElse qty <= 0 Then
            MessageBox.Show("Enter a whole number greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim productId As Integer = CInt(product("ID"))

        If direction = "OUT" Then
            Dim available As Integer = CInt(product("Quantity"))
            For Each r As DataRow In dtRec.Select("Saved = False AND ProductID = " & productId)
                available += If(CStr(r("InOut")) = "IN", CInt(r("Quantity")), -CInt(r("Quantity")))
            Next
            If qty > available Then
                MessageBox.Show("Only " & available & " of " & CStr(product("Product")) & " available." & vbCrLf & "Enter a smaller quantity.",
                                "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        Dim cost As Decimal = CDec(product("UnitCost"))
        dtRec.Rows.Add(NextRecordId(), product("Product"), product("Brand"), product("Category"),
                       direction, qty, cost, cost * qty, Stamp(), False, productId)
    End Sub

    Private Sub btnUnitCost_Click(sender As Object, e As EventArgs) Handles btnUnitCost.Click
        Dim product = RequireProduct()
        If product Is Nothing Then Return

        Dim answer As String = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter new unit cost for " & CStr(product("Product")) & ":", "Unit Cost",
            CDec(product("UnitCost")).ToString("0.00", CultureInfo.InvariantCulture))
        If answer = "" Then Return

        Dim cost As Decimal
        If Not Decimal.TryParse(answer, NumberStyles.Number, CultureInfo.InvariantCulture, cost) OrElse cost < 0 Then
            MessageBox.Show("Enter a number of 0 or more.", "Invalid Cost", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using c As MySqlConnection = Db.OpenConnection()
                Using cmd As New MySqlCommand("UPDATE equipment SET unit_cost = @cost WHERE id = @id", c)
                    cmd.Parameters.AddWithValue("@cost", cost)
                    cmd.Parameters.AddWithValue("@id", CInt(product("ID")))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ShowDbError(ex)
            Return
        End Try
        product("UnitCost") = cost
        MessageBox.Show("Unit cost updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim unsaved As DataRow() = dtRec.Select("Saved = False", "ID ASC")
        If unsaved.Length = 0 Then
            MessageBox.Show("Add a stock IN or OUT entry first.", "Nothing to Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using c As MySqlConnection = Db.OpenConnection()
                Using tx As MySqlTransaction = c.BeginTransaction()
                    Try
                        For Each r As DataRow In unsaved
                            Dim delta As Integer = CInt(r("Quantity"))
                            If CStr(r("InOut")) = "OUT" Then delta = -delta

                            Using upd As New MySqlCommand("UPDATE equipment SET quantity = quantity + @d WHERE id = @id AND quantity + @d >= 0", c, tx)
                                upd.Parameters.AddWithValue("@d", delta)
                                upd.Parameters.AddWithValue("@id", CInt(r("ProductID")))
                                If upd.ExecuteNonQuery() <> 1 Then
                                    Throw New InvalidOperationException("Stock for " & CStr(r("Product")) & " changed and is no longer enough for this entry.")
                                End If
                            End Using

                            Using ins As New MySqlCommand("INSERT INTO stock_records (equipment_id, product, brand, category, direction, quantity, unit_cost, total_cost) " &
                                                          "VALUES (@eq, @pr, @br, @ca, @di, @qt, @uc, @tc)", c, tx)
                                ins.Parameters.AddWithValue("@eq", CInt(r("ProductID")))
                                ins.Parameters.AddWithValue("@pr", CStr(r("Product")))
                                ins.Parameters.AddWithValue("@br", CStr(r("Brand")))
                                ins.Parameters.AddWithValue("@ca", CStr(r("Category")))
                                ins.Parameters.AddWithValue("@di", CStr(r("InOut")))
                                ins.Parameters.AddWithValue("@qt", CInt(r("Quantity")))
                                ins.Parameters.AddWithValue("@uc", CDec(r("UnitCost")))
                                ins.Parameters.AddWithValue("@tc", CDec(r("TotalCost")))
                                ins.ExecuteNonQuery()
                            End Using
                        Next
                        tx.Commit()
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            dtRec.Rows.Clear()
            LoadInventory()
            LoadRecords()
            MessageBox.Show("Transaction saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ShowDbError(ex)
        End Try
    End Sub

    Private Sub ShowDbError(ex As Exception)
        MessageBox.Show("The database request failed and nothing was changed." & vbCrLf & vbCrLf & ex.Message,
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub

    ' ------------------------------------------------------------------
    ' Print delivery order
    ' ------------------------------------------------------------------
    Private Sub btnPrintDO_Click(sender As Object, e As EventArgs) Handles btnPrintDO.Click
        printRows = dtRec.Select("Saved = True", "ID DESC")
        If printRows.Length = 0 Then
            MessageBox.Show("There are no saved stock movements to print.", "Nothing to Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        printIndex = 0
        Using doc As New PrintDocument()
            AddHandler doc.PrintPage, AddressOf PrintDoPage
            Using preview As New PrintPreviewDialog()
                preview.Document = doc
                preview.WindowState = FormWindowState.Maximized
                preview.ShowDialog(Me)
            End Using
        End Using
    End Sub

    Private Sub PrintDoPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim x As Single = e.MarginBounds.Left
        Dim y As Single = e.MarginBounds.Top
        Dim colX As Single() = {0, 35, 190, 285, 335, 385, 455, 530}

        Using titleFont As New Font("Segoe UI", 18.0!, FontStyle.Bold),
              headFont As New Font("Segoe UI", 9.0!, FontStyle.Bold),
              bodyFont As New Font("Segoe UI", 9.0!)

            g.DrawString("Delivery Order", titleFont, Brushes.Black, x, y)
            y += 34
            g.DrawString("Gym Equipment Inventory System   -   Printed " & Stamp(), bodyFont, Brushes.Black, x, y)
            y += 30

            Dim heads As String() = {"ID", "Product", "Brand", "In/Out", "Qty", "Unit Cost", "Total", "Date"}
            For i As Integer = 0 To heads.Length - 1
                g.DrawString(heads(i), headFont, Brushes.Black, x + colX(i), y)
            Next
            y += 20
            g.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y)
            y += 6

            While printIndex < printRows.Length AndAlso y + 20 < e.MarginBounds.Bottom
                Dim r As DataRow = printRows(printIndex)
                Dim cells As String() = {
                    CStr(r("ID")), CStr(r("Product")), CStr(r("Brand")), CStr(r("InOut")), CStr(r("Quantity")),
                    CDec(r("UnitCost")).ToString("N2"), CDec(r("TotalCost")).ToString("N2"), CStr(r("EntryDate"))}
                For i As Integer = 0 To cells.Length - 1
                    g.DrawString(cells(i), bodyFont, Brushes.Black, x + colX(i), y)
                Next
                y += 20
                printIndex += 1
            End While
        End Using

        e.HasMorePages = printIndex < printRows.Length
    End Sub

End Class

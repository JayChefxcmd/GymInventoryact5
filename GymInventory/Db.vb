Imports MySqlConnector

''' <summary>
''' XAMPP MySQL/MariaDB connection. Defaults match a fresh XAMPP install
''' (user root, empty password, port 3306). Change them here if yours differ.
''' </summary>
Module Db

    Public ConnectionString As String =
        "Server=localhost;Port=3306;Database=gym_inventory;User ID=root;Password=;CharSet=utf8mb4;"

    Public Function OpenConnection() As MySqlConnection
        Dim c As New MySqlConnection(ConnectionString)
        c.Open()
        Return c
    End Function

    ''' <summary>Creates the database, tables and sample equipment when they do not exist yet.</summary>
    Public Sub EnsureSchema()
        Dim csb As New MySqlConnectionStringBuilder(ConnectionString)
        Dim dbName As String = csb.Database
        csb.Database = ""

        Using server As New MySqlConnection(csb.ConnectionString)
            server.Open()
            Run(server, "CREATE DATABASE IF NOT EXISTS `" & dbName & "` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci")
        End Using

        Using c As MySqlConnection = OpenConnection()
            Run(c, "CREATE TABLE IF NOT EXISTS equipment (" &
                   "id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, " &
                   "brand VARCHAR(60) NOT NULL, " &
                   "category VARCHAR(60) NOT NULL, " &
                   "product VARCHAR(100) NOT NULL, " &
                   "quantity INT NOT NULL DEFAULT 0, " &
                   "unit_cost DECIMAL(10,2) NOT NULL DEFAULT 0) ENGINE=InnoDB")

            Run(c, "CREATE TABLE IF NOT EXISTS stock_records (" &
                   "id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, " &
                   "equipment_id INT NOT NULL, " &
                   "product VARCHAR(100) NOT NULL, " &
                   "brand VARCHAR(60) NOT NULL, " &
                   "category VARCHAR(60) NOT NULL, " &
                   "direction ENUM('IN','OUT') NOT NULL, " &
                   "quantity INT NOT NULL, " &
                   "unit_cost DECIMAL(10,2) NOT NULL, " &
                   "total_cost DECIMAL(12,2) NOT NULL, " &
                   "entry_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, " &
                   "FOREIGN KEY (equipment_id) REFERENCES equipment(id)) ENGINE=InnoDB")

            Using count As New MySqlCommand("SELECT COUNT(*) FROM equipment", c)
                If Convert.ToInt32(count.ExecuteScalar()) = 0 Then
                    Run(c, "INSERT INTO equipment (brand, category, product, quantity, unit_cost) VALUES " &
                           "('Life Fitness','Cardio','Treadmill T5',8,2400.00)," &
                           "('Concept2','Cardio','RowErg Rower',12,990.00)," &
                           "('Rogue','Free Weights','Olympic Barbell 20kg',40,295.00)," &
                           "('Rogue','Strength','Adjustable Bench',15,450.00)," &
                           "('Bowflex','Free Weights','Adjustable Dumbbells',20,549.00)," &
                           "('Technogym','Strength','Cable Crossover',4,3200.00)," &
                           "('Rep Fitness','Strength','Power Rack PR-4000',6,780.00)," &
                           "('Rep Fitness','Accessories','Kettlebell 16kg',60,85.00)," &
                           "('Rep Fitness','Accessories','Resistance Band Set',75,32.00)")
                End If
            End Using
        End Using
    End Sub

    Private Sub Run(c As MySqlConnection, sql As String)
        Using cmd As New MySqlCommand(sql, c)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Module

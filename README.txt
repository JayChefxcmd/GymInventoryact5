Gym Equipment Inventory System  (VB.NET WinForms + XAMPP MySQL, black and white)

1. Start XAMPP: open the XAMPP Control Panel and press Start next to MySQL
   (Apache is only needed if you want phpMyAdmin at http://localhost/phpmyadmin).
2. Open GymInventory.slnx in Visual Studio 2022 (17.13 or newer) or Visual Studio 2026.
   Needs Windows and the ".NET desktop development" workload (.NET 8 SDK).
3. Press F5. Visual Studio downloads the MySqlConnector package automatically.

On first run the app creates the database "gym_inventory", its two tables and the sample
equipment by itself. If you prefer, import gym_inventory.sql in phpMyAdmin first.

Connection settings are in GymInventory/Db.vb (ConnectionString). The defaults match a fresh
XAMPP install: server localhost, port 3306, user root, empty password.
If MySQL in XAMPP runs on another port or has a password, edit that one line.

Tables
  equipment       id, brand, category, product, quantity, unit_cost
  stock_records   id, equipment_id, product, brand, category, direction (IN/OUT),
                  quantity, unit_cost, total_cost, entry_date

How it works
  - In / Out add an italic (unsaved) row to the record grid.
  - Save writes every unsaved row to MySQL in one transaction and updates the stock counts.
    If stock changed underneath you (for example in phpMyAdmin), the save is rolled back.
  - Unit Cost updates the price in MySQL straight away.
  - View All reloads the equipment from MySQL, so edits made in phpMyAdmin show up.

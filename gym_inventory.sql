-- Gym Equipment Inventory System - XAMPP (MySQL / MariaDB)
-- Optional: the app creates all of this by itself on first run.
-- To import by hand: phpMyAdmin > Import > choose this file > Go.

CREATE DATABASE IF NOT EXISTS gym_inventory CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE gym_inventory;

CREATE TABLE IF NOT EXISTS equipment (
  id        INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  brand     VARCHAR(60)  NOT NULL,
  category  VARCHAR(60)  NOT NULL,
  product   VARCHAR(100) NOT NULL,
  quantity  INT NOT NULL DEFAULT 0,
  unit_cost DECIMAL(10,2) NOT NULL DEFAULT 0
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS stock_records (
  id           INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  equipment_id INT NOT NULL,
  product      VARCHAR(100) NOT NULL,
  brand        VARCHAR(60)  NOT NULL,
  category     VARCHAR(60)  NOT NULL,
  direction    ENUM('IN','OUT') NOT NULL,
  quantity     INT NOT NULL,
  unit_cost    DECIMAL(10,2) NOT NULL,
  total_cost   DECIMAL(12,2) NOT NULL,
  entry_date   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (equipment_id) REFERENCES equipment(id)
) ENGINE=InnoDB;

INSERT INTO equipment (brand, category, product, quantity, unit_cost)
SELECT * FROM (
  SELECT 'Life Fitness','Cardio','Treadmill T5',8,2400.00 UNION ALL
  SELECT 'Concept2','Cardio','RowErg Rower',12,990.00 UNION ALL
  SELECT 'Rogue','Free Weights','Olympic Barbell 20kg',40,295.00 UNION ALL
  SELECT 'Rogue','Strength','Adjustable Bench',15,450.00 UNION ALL
  SELECT 'Bowflex','Free Weights','Adjustable Dumbbells',20,549.00 UNION ALL
  SELECT 'Technogym','Strength','Cable Crossover',4,3200.00 UNION ALL
  SELECT 'Rep Fitness','Strength','Power Rack PR-4000',6,780.00 UNION ALL
  SELECT 'Rep Fitness','Accessories','Kettlebell 16kg',60,85.00 UNION ALL
  SELECT 'Rep Fitness','Accessories','Resistance Band Set',75,32.00
) AS seed
WHERE NOT EXISTS (SELECT 1 FROM equipment);

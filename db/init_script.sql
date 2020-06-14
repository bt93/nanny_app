-- Make sure not using other db's
USE master
GO

-- Drop the existing db if it already exists
DROP DATABASE IF EXISTS NannyDB

-- Create new database
CREATE DATABASE NannyDB
GO

USE NannyDB
GO

CREATE TABLE address (
	address_id INT IDENTITY PRIMARY KEY,
	street_number INT NOT NULL,
	streed_name NVARCHAR(25) NOT NULL,
	city NVARCHAR(25) NOT NULL,
	state NVARCHAR(25) NOT NULL,
	zip INT,
	county NVARCHAR(25),
	country NVARCHAR(25),
)

CREATE TABLE diaper (
	diaper_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	time DATETIME NOT NULL,
	notes TEXT
)

CREATE TABLE meal (
	meal_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	time DATETIME NOT NULL,
	type NVARCHAR(20),
	notes TEXT
)

CREATE TABLE nap (
	nap_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	notes TEXT
)
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

CREATE TABLE caretaker (
	caretaker_id INT IDENTITY PRIMARY KEY,
	address_id INT NOT NULL,
	first_name NVARCHAR(80) NOT NULL,
	last_name NVARCHAR(80) NOT NULL,
	email NVARCHAR(120) NOT NULL,
	password NVARCHAR(60) NOT NULL,
	phone_number NVARCHAR(10)
	CONSTRAINT fk_caretaker_address FOREIGN KEY (address_id) REFERENCES address(address_id)
)

CREATE TABLE parent (
	parent_id INT IDENTITY PRIMARY KEY,
	address_id INT NOT NULL,
	first_name NVARCHAR(80),
	last_name NVARCHAR(80),
	email NVARCHAR(120),
	phone_number NVARCHAR(10)
	CONSTRAINT fk_parent_address FOREIGN KEY (address_id) REFERENCES address(address_id)
)


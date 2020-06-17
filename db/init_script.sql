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
	street_name NVARCHAR(25) NOT NULL,
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
	email_address NVARCHAR(120) NOT NULL,
	password NVARCHAR (60) NOT NULL,
	phone_number NVARCHAR(20) NOT NULL,
	CONSTRAINT fk_caretaker_address FOREIGN KEY (address_id) REFERENCES address(address_id)
)

CREATE TABLE parent (
	parent_id INT IDENTITY PRIMARY KEY,
	address_id INT NOT NULL,
	first_name NVARCHAR(80) NOT NULL,
	last_name NVARCHAR(80) NOT NULL,
	email_address NVARCHAR(120) NOT NULL,
	phone_number NVARCHAR(20) NOT NULL
	CONSTRAINT fk_parent_address FOREIGN KEY (address_id) REFERENCES address(address_id)
)

CREATE TABLE child (
	child_id INT IDENTITY PRIMARY KEY,
	first_name NVARCHAR(80) NOT NULL,
	last_name NVARCHAR(80) NOT NULL,
	gender NVARCHAR(11),
	date_of_birth DATE,
	rate_per_hour MONEY NOT NULL,
	needs_diapers BIT NOT NULL,
)

CREATE TABLE child_parent (
	child_id INT NOT NULL,
	parent_id INT NOT NULL,
	CONSTRAINT pk_child_parent PRIMARY KEY (child_id, parent_id),
	CONSTRAINT fk_child FOREIGN KEY (child_id) REFERENCES child(child_id),
	CONSTRAINT fk_parent FOREIGN KEY (parent_id) REFERENCES parent(parent_id)
)

CREATE TABLE session (
	session_id INT IDENTITY PRIMARY KEY,
	child_id INT NOT NULL,
	drop_off DATETIME,
	pick_up DATETIME,
	notes TEXT
	CONSTRAINT fk_session_child FOREIGN KEY (child_id) REFERENCES child(child_id)
)

CREATE TABLE session_caretaker (
	session_id INT NOT NULL,
	caretaker_id INT NOT NULL
	CONSTRAINT pk_session_caretaker PRIMARY KEY (session_id, caretaker_id),
	CONSTRAINT fk_session FOREIGN KEY (session_id) REFERENCES session(session_id),
	CONSTRAINT fk_caretaker FOREIGN KEY (caretaker_id) REFERENCES caretaker(caretaker_id)
)

CREATE TABLE diaper (
	diaper_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	time DATETIME NOT NULL,
	notes TEXT
	CONSTRAINT fk_diaper_session FOREIGN KEY (session_id) REFERENCES session(session_id)
)

CREATE TABLE meal (
	meal_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	time DATETIME NOT NULL,
	type NVARCHAR(20),
	notes TEXT
	CONSTRAINT fk_meal_session FOREIGN KEY (session_id) REFERENCES session(session_id)
)

CREATE TABLE nap (
	nap_id INT IDENTITY PRIMARY KEY,
	session_id INT NOT NULL,
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	notes TEXT
	CONSTRAINT fk_nap_session FOREIGN KEY (session_id) REFERENCES session(session_id)
)
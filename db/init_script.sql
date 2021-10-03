-- Make sure not using other db's
USE master
GO

-- Drop the existing db if it already exists
DROP DATABASE IF EXISTS NannyDB
GO
-- Create new database
CREATE DATABASE NannyDB
GO

USE NannyDB
GO

CREATE TABLE address (
	address_id INT IDENTITY PRIMARY KEY,
	street NVARCHAR(25) NOT NULL,
	city NVARCHAR(25) NOT NULL,
	state NVARCHAR(25) NOT NULL,
	zip INT,
	county NVARCHAR(25),
	country NVARCHAR(25) NOT NULL,
)

CREATE TABLE caretaker (
	caretaker_id INT IDENTITY PRIMARY KEY,
	address_id INT NOT NULL,
	first_name NVARCHAR(80) NOT NULL,
	last_name NVARCHAR(80) NOT NULL,
	email_address NVARCHAR(120) NOT NULL,
	password NVARCHAR (60) NOT NULL,
	phone_number NVARCHAR(20) NOT NULL,
	salt NVARCHAR(20) NOT NULL,
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
	gender NVARCHAR(1),
	date_of_birth DATE,
	rate_per_hour MONEY NOT NULL,
	needs_diapers BIT NOT NULL,
	active BIT NOT NULL,
	image_url NVARCHAR(100)
)

CREATE TABLE child_caretaker (
	child_id INT NOT NULL,
	caretaker_id INT NOT NULL,
	CONSTRAINT pk_child_caretaker PRIMARY KEY (child_id, caretaker_id),
	CONSTRAINT fk_child_to_caretaker FOREIGN KEY (child_id) REFERENCES child(child_id),
	CONSTRAINT fk_caretaker_to_child FOREIGN KEY (caretaker_id) REFERENCES caretaker(caretaker_id)
)

CREATE TABLE child_parent (
	child_id INT NOT NULL,
	parent_id INT NOT NULL,
	CONSTRAINT pk_child_parent PRIMARY KEY (child_id, parent_id),
	CONSTRAINT fk_child_to_parent FOREIGN KEY (child_id) REFERENCES child(child_id),
	CONSTRAINT fk_parent_to_child FOREIGN KEY (parent_id) REFERENCES parent(parent_id)
)

CREATE TABLE session (
	session_id INT IDENTITY PRIMARY KEY,
	child_id INT NOT NULL,
	drop_off DATETIME NOT Null,
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
	start_time DATETIME,
	end_time DATETIME,
	notes TEXT
	CONSTRAINT fk_nap_session FOREIGN KEY (session_id) REFERENCES session(session_id)
)

-- Stored Procedures
GO

-- Procedure for adding caretaker
CREATE PROCEDURE addCareTaker
@street NVARCHAR(25),
@city NVARCHAR(25),
@state NVARCHAR(25),
@zip INT,
@county NVARCHAR(25),
@country NVARCHAR(25),
@first_name NVARCHAR(80),
@last_name NVARCHAR(80),
@email_address NVARCHAR(120),
@password NVARCHAR(60),
@salt NVARCHAR(20),
@phone_number NVARCHAR(20)
AS
BEGIN TRANSACTION
INSERT INTO address (street, city, state, zip, county, country)
	VALUES (@street, @city, @state, @zip, @county, @country)
	SELECT @@Identity
INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (@@Identity, @first_name, @last_name, @email_address, @password, @phone_number, @salt)
	SELECT @@Identity
COMMIT TRANSACTION

GO

-- Stored Procedure for Update caretaker
CREATE PROCEDURE updateCareTaker
@caretaker_id INT,
@street NVARCHAR(25),
@city NVARCHAR(25),
@state NVARCHAR(25),
@zip INT,
@county NVARCHAR(25),
@country NVARCHAR(25),
@first_name NVARCHAR(80),
@last_name NVARCHAR(80),
@email_address NVARCHAR(120),
@phone_number NVARCHAR(20)
AS
BEGIN TRANSACTION
UPDATE address
	SET street = @street,
	city = @city,
	state = @state,
	zip = @zip,
	county = @county,
    country = @country
    OUTPUT INSERTED.address_id
	WHERE address_id = (SELECT address_id FROM caretaker WHERE caretaker_id = @caretaker_id)
UPDATE caretaker
	SET first_name = @first_name,
	last_name = @last_name,
	email_address = @email_address,
    phone_number = @phone_number
    OUTPUT INSERTED.caretaker_id
	WHERE caretaker_id = @caretaker_id
COMMIT TRANSACTION

GO

-- Procedure for deleting a caretaker
CREATE PROCEDURE deleteCareTaker
@caretaker_id INT,
@address_id INT
AS
BEGIN TRANSACTION
DELETE FROM child_caretaker
	WHERE caretaker_id = @caretaker_id
DELETE FROM session_caretaker
    WHERE caretaker_id = @caretaker_id
DELETE FROM caretaker
    WHERE caretaker_id = @caretaker_id
DELETE FROM address
	WHERE address_id = @address_id
COMMIT TRANSACTION

GO

-- Procedure for adding a new parent
CREATE PROCEDURE addParent
@street NVARCHAR(25),
@city NVARCHAR(25),
@state NVARCHAR(25),
@zip INT,
@county NVARCHAR(25),
@country NVARCHAR(25),
@first_name NVARCHAR(80),
@last_name NVARCHAR(80),
@email_address NVARCHAR(120),
@phone_number NVARCHAR(20),
@child_id INT
AS
BEGIN TRANSACTION
INSERT INTO address(street, city, state, zip, county, country)
	VALUES(@street, @city, @state, @zip, @county, @country)
	SELECT @@Identity
INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (@@Identity, @first_name, @last_name, @email_address, @phone_number);
	DECLARE @parent_id INT
    SELECT @parent_id = @@Identity
INSERT INTO child_parent (child_id, parent_id)
    VALUES (@child_id, @parent_id)
COMMIT TRANSACTION

GO

-- Procedure for Updating parent info
CREATE PROCEDURE updateParent
@parent_id INT,
@street NVARCHAR(25),
@city NVARCHAR(25),
@state NVARCHAR(25),
@zip INT,
@county NVARCHAR(25),
@country NVARCHAR(25),
@first_name NVARCHAR(80),
@last_name NVARCHAR(80),
@email_address NVARCHAR(120),
@phone_number NVARCHAR(20)
AS
BEGIN TRANSACTION
UPDATE address
	SET street = @street,
	city = @city,
	state = @state,
	zip = @zip,
	county = @county,
    country = @country
	WHERE address_id = (SELECT address_id FROM parent WHERE parent_id = @parent_id)
UPDATE parent
	SET first_name = @first_name,
	last_name = @last_name,
	email_address = @email_address,
	phone_number = @phone_number
    OUTPUT INSERTED.parent_id
	WHERE parent_id = @parent_id
COMMIT TRANSACTION

GO

-- Procedure for adding new session
CREATE PROCEDURE createNewSession
@child_id INT,
@drop_off DATETIME,
@notes TEXT,
@caretaker_id INT
AS
BEGIN TRANSACTION
INSERT INTO session (child_id, drop_off, notes)
	VALUES (@child_id, @drop_off, @notes)
	DECLARE @session_id INT
    SELECT @@Identity
INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@@IDENTITY, @caretaker_id)
	SELECT @session_id
COMMIT TRANSACTION

GO

-- Procedure for deleting a session
CREATE PROCEDURE deleteSession
@session_id INT,
@caretaker_id INT
AS
BEGIN TRANSACTION
DELETE FROM session_caretaker
	WHERE session_id = @session_id
    AND caretaker_id = @caretaker_id
DELETE FROM meal
    WHERE session_id = @session_id
DELETE FROM diaper
    WHERE session_id = @session_id
DELETE FROM nap
    WHERE session_id = @session_id
DELETE FROM session
    WHERE session_id = @session_id
COMMIT TRANSACTION

GO
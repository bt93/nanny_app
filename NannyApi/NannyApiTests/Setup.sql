-- Start a transaction
--BEGIN TRANSACTION

-- REMOVE data from the database
DELETE FROM nap;
DELETE FROM meal;
DELETE FROM diaper;
DELETE FROM session_caretaker;
DELETE FROM session;
DELETE FROM caretaker;
DELETE FROM child_parent;
DELETE FROM child;
DELETE FROM parent;
DELETE FROM address;

-- INSERT into address
INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('1282 Berkshire Dr', 'Macedonia', 'Ohio', 44056, 'Summit', 'United States of America');
DECLARE @berkshire int
SELECT @berkshire = @@IDENTITY;

INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('8882 Fleet Dr', 'Streetsboro', 'Ohio', 44422, 'Portage', 'United States of America');
DECLARE @fleet int
SELECT @fleet = @@IDENTITY;

INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('3892 Street St', 'Hudson', 'Ohio', 44056, 'Summit', 'United States of America');
DECLARE @street int
SELECT @street = @@IDENTITY;

INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('1111 Mayfield Rd', 'Mayfield Heights', 'Ohio', 44136, 'Cuyahoga', 'United States of America');
DECLARE @mayfield int
SELECT @mayfield = @@IDENTITY;

-- Caretaker tests
INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	VALUES (@berkshire, 'Ruth', 'Howie', 'ruth@ruth.com', 'pass', '216-222-0123');

INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	VALUES (@fleet, 'Person', 'Guy', 'person@guy.com', 'word', '216-111-2243');

INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	VALUES (@berkshire, 'Person', 'Thing', 'person@Thing.com', 'word', '216-111-2243');

-- Test if they work
--SELECT * FROM address
--SELECT * FROM caretaker

-- Return data to the caller
SELECT @berkshire AS berkshire, @fleet AS fleet, @street AS street, @mayfield as mayfield

-- Rollback Transaction
--ROLLBACK TRANSACTION
﻿-- Start a transactio
--BEGIN TRANSACTION

-- REMOVE data from the database
DELETE FROM nap;
DELETE FROM meal;
DELETE FROM diaper;
DELETE FROM session_caretaker;
DELETE FROM session;
DELETE FROM child_caretaker;
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
INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (@berkshire, 'Ruth', 'Howie', 'ruth@ruth.com', 'pass', '216-222-0123', 'asfed');
DECLARE @ruth int
SELECT @ruth = @@IDENTITY;

INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (@fleet, 'Person', 'Guy', 'person@guy.com', 'word', '216-111-2243', 'asf');
DECLARE @person int
SELECT @person = @@IDENTITY;

INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (@street, 'Person', 'Thing', 'person@Thing.com', 'word', '216-111-2243', 'safgg');

-- Parent Tests
INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (@mayfield, 'Megan', 'Kwecien', 'Megan@megan.com', '216-222-5555')
DECLARE @megan int
SELECT @megan = @@IDENTITY;

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (@mayfield, 'Matt', 'Kwecien', 'Matt@matt.com', '216-222-5555')
DECLARE @matt int
SELECT @matt = @@IDENTITY;

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (@street, 'Jane', 'Doe', 'jane@doe.com', '216-222-5555')
DECLARE @jane int
SELECT @jane = @@IDENTITY;

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (@street, 'John', 'Doe', 'jane@doe.com', '216-222-5555')
DECLARE @john int
SELECT @john = @@IDENTITY;

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, active)
	VALUES ('Ellie', 'Kwecien', 'F', '2018-08-25', 6.50, 1, 1);
DECLARE @ellie int
SELECT @ellie = @@IDENTITY;

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, active)
	VALUES ('Bobby', 'Doe', 'M', '2020-01-01', 7.50, 1, 1);
DECLARE @bobby int
SELECT @bobby = @@IDENTITY;

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, active)
	VALUES ('Joey', 'Parentless', 'M', '2020-01-01', 7.50, 1, 0);
DECLARE @joey int
SELECT @joey = @@IDENTITY;

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, active)
	VALUES ('Johnny', 'Parentless', 'M', '2020-01-01', 7.50, 1, 0);
DECLARE @johnny int
SELECT @johnny = @@IDENTITY;

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, active)
	VALUES ('Jinny', 'Parentless', 'M', '2020-01-01', 7.50, 1, 0);
DECLARE @jinny int
SELECT @jinny = @@IDENTITY;

INSERT INTO child_caretaker (child_id, caretaker_id)
	VALUES (@ellie, @ruth)

INSERT INTO child_caretaker (child_id, caretaker_id)
	VALUES (@bobby, @ruth)

INSERT INTO child_caretaker (child_id, caretaker_id)
	VALUES (@joey, @person)


INSERT INTO child_caretaker (child_id, caretaker_id)
	VALUES (@johnny, @ruth)

INSERT INTO child_caretaker (child_id, caretaker_id)
	VALUES (@jinny, @ruth)

INSERT INTO child_parent (child_id, parent_id)
	VALUES (@ellie, @megan);
INSERT INTO child_parent (child_id, parent_id)
	VALUES (@ellie, @matt);
INSERT INTO child_parent (child_id, parent_id)
	VALUES (@bobby, @jane);
INSERT INTO child_parent (child_id, parent_id)
	VALUES (@joey, @john);

INSERT INTO session (child_id, drop_off, pick_up, notes)
	VALUES (@ellie, '2020-10-20', '2020-10-20', 'notes')
DECLARE @session1 int
SELECT @session1 = @@IDENTITY;

INSERT INTO session (child_id, drop_off, pick_up, notes)
	VALUES (@ellie, '2020-10-20', '2020-10-20', 'notes')
DECLARE @session2 int
SELECT @session2 = @@IDENTITY;

INSERT INTO session (child_id, drop_off, notes)
	VALUES (@ellie, '2020-10-20', 'notes')
DECLARE @session3 int
SELECT @session3 = @@IDENTITY;

INSERT INTO session (child_id, drop_off)
	VALUES (@bobby, '2020-10-20')
DECLARE @session4 int
SELECT @session4 = @@IDENTITY;

INSERT INTO session (child_id, drop_off, pick_up, notes)
	VALUES (@joey, '2020-10-20', '2020-10-20', 'notes')
DECLARE @session5 int
SELECT @session5 = @@IDENTITY;

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@session1, @ruth)

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@session2, @ruth)

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@session3, @ruth)

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@session4, @ruth)

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (@session5, @person)

INSERT INTO nap (session_id, start_time, end_time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', '2020-10-20 11:25:00', 'Didnt sleep good')
DECLARE @nap1 int
SELECT @nap1 = @@IDENTITY

INSERT INTO nap (session_id, start_time, end_time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', '2020-10-20 11:25:00', 'ssleep good')

INSERT INTO meal (session_id, time, type, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Breakfast', 'Ate it good')
DECLARE @meal1 int
SELECT @meal1 = @@IDENTITY;

INSERT INTO meal (session_id, time, type, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Lunch', 'Ate it good')

INSERT INTO meal (session_id, time, type, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Dinner', 'Ate it good')

INSERT INTO meal (session_id, time, type, notes)
	VALUES (@session2, '2020-10-20 11:20:00', 'Breakfast', 'Ate it good')

INSERT INTO diaper (session_id, time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Nothing Unusal')
DECLARE @diaper1 int
SELECT @diaper1 = @@IDENTITY

INSERT INTO diaper (session_id, time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Nothing Unusal')


INSERT INTO diaper (session_id, time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Nothing Unusal')


INSERT INTO diaper (session_id, time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Nothing Unusal')

INSERT INTO diaper (session_id, time, notes)
	VALUES (@session1, '2020-10-20 11:20:00', 'Nothing Unusal')

INSERT INTO child_allergies (child_id, allergy_id)
	VALUES (@ellie, (SELECT allergy_id FROM allergies WHERE name = 'Banana'))
-- Test if they work
--SELECT * FROM address
--SELECT * FROM caretaker
--SELECT * FROM session
--SELECT * FROM child
--SELECT * FROM session_caretaker
--SELECT * FROM child_allergies

-- Return data to the caller
SELECT @berkshire AS berkshire, @fleet AS fleet, @street AS street, @mayfield as mayfield,
@ruth AS ruth, @megan AS megan, @matt AS matt, @jane AS jane, @john as john,
@ellie AS ellie, @bobby AS bobby,@joey AS joey, @johnny AS johnny, @jinny AS jinny,
@session1 as session1, @meal1 AS meal1, @nap1 AS nap1, @diaper1 AS diaper1;

-- Rollback Transaction
--ROLLBACK TRANSACTION
INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('1282 Berkshire Drive', 'Macedonia', 'Ohio', 44056, 'Summit', 'United States of America')

SELECT * from address

INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (1, 'Ruth', 'Howie', 'rudih@windstream.net', 'password', '216-262-9355', 'asfojopewqf')

	INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (1, 'Jason', 'Howie', 'jason@windstream.net', 'password', '216-262-9355', 'asfd')
	INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (1, 'Jay', 'Howie', 'rudih@windstream.net', 'password', '216-262-9355', 'asdf')
	INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (1, 'John', 'Doe', 'rudih@windstream.net', 'password', '216-262-9355', 'sasfa')
	INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number, salt)
	VALUES (1, 'Jane', 'Doe', 'rudih@windstream.net', 'password', '216-262-9355', 'asdadee')

SELECT *
	FROM caretaker c
	JOIN address a ON c.address_id = a.address_id

SELECT *
	FROM caretaker
	JOIN address ON caretaker.address_id = address.address_id
	WHERE first_name = 'Jason'
	AND last_name = 'Howie';


INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers) 
	VALUES ('Ellie', 'Kwecien', 'F', '2018-08-25', 6.30, 1)
DECLARE @ellie int
SELECT @ellie = @@IDENTITY

SELECT *
	FROM child

SELECT * 
	FROM parent
	JOIN child_parent ON child_parent.parent_id = parent.parent_id
	JOIN child ON child_parent.child_id = child.child_id


DECLARE @Existingdate datetime
SET @Existingdate=GETDATE()

INSERT INTO session (child_id, drop_off, pick_up, notes)
	VALUES (3, @Existingdate, @Existingdate, 'Ellie was a sweetheart!')

SELECT * FROM session
	JOIN child ON session.child_id = child.child_id

INSERT INTO session_caretaker (session_id, caretaker_id)
	VALUES (5,2)

SELECT child.*
	FROM child
	JOIN session ON child.child_id = session.child_id
	JOIN session_caretaker ON session.session_id = session_caretaker.session_id
	JOIN caretaker ON session_caretaker.caretaker_id = caretaker.caretaker_id
	WHERE caretaker.caretaker_id = 1
	GROUP BY child.child_id, child.first_name, child.last_name, 
	child.gender, child.date_of_birth, child.needs_diapers,
	child.rate_per_hour, child.image_url;


INSERT INTO diaper (session_id, time, notes)
	VALUES (1, @Existingdate, 'Big ONE!')

SELECT *
	FROM diaper
	JOIN session ON diaper.session_id = session.session_id


INSERT INTO meal (session_id, time, type, notes)
	VALUES (1, @Existingdate, 'Lunch', 'Carrots and Peas')

SELECT *
	FROM meal
	JOIN session ON meal.session_id = session.session_id


INSERT INTO nap (session_id, start_time, end_time, notes)
	VALUES (1, @Existingdate, @Existingdate, 'She slept very well')

SELECT *
	FROM nap
	JOIN session ON nap.session_id = session.session_id

INSERT INTO address (street, city, state, zip, county, country)
	VALUES ('2211 Mayfield Ridge Road', 'Mayfield Heights', 'Ohio', 44124, 'Cuyahoga', 'United States of America')

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (2, 'Megan', 'Kwecien', 'meganhowie.1@gmail.com', '330-222-2222')

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (2, 'Matt', 'Kwecien', 'mat.1@gmail.com', '330-222-2222')

INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	VALUES (1, 'Bob', 'Boob', 'mat.1@gmail.com', '330-222-2222')

	INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers) 
	VALUES ('Bobby', 'Boob', 'M', '2018-08-25', 6.30, 1)
	INSERT INTO child_parent (child_id, parent_id)
	VALUES (2, 3)

SELECT * FROM parent

INSERT INTO child_parent (child_id, parent_id)
	VALUES (1, 1)

INSERT INTO child_parent (child_id, parent_id)
	VALUES (1, 2)

SELECT * FROM child
	JOIN child_parent ON child.child_id = child_parent.child_id
	JOIN parent ON child_parent.parent_id = parent.parent_id

DECLARE @address int
SELECT address = (SELECT address_id FROM caretaker WHERE caretaker_id = 45)
DELETE FROM caretaker WHERE caretaker_id = 45;
DELETE FROM address WHERE address_id = @address;

SELECT *
	FROM parent
	JOIN child_parent ON parent.parent_id = child_parent.parent_id
	WHERE child_id = 2;


INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	VALUES (1, 'Bob', 'Person', 'bob@bob.com', 'bob', '555-555-5555');

UPDATE caretaker
	SET address_id = 1,
	first_name = 'Ruth',
	last_name = 'Howie',
	email_address = 'Ruth@ruth.com',
	password = 'pass'
	WHERE caretaker_id = 1;

	DELETE FROM caretaker
   WHERE caretaker_id = 2
  DELETE FROM address
   WHERE address_id = 3
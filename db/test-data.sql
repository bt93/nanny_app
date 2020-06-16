INSERT INTO address (street_number, streed_name, city, state, zip, county, country)
	VALUES (1282, 'Berkshire Drive', 'Macedonia', 'Ohio', 44056, 'Summit', 'United States of America')

SELECT * from address

INSERT INTO caretaker (address_id, first_name, last_name, email, password, phone_number)
	VALUES (1, 'Ruth', 'Howie', 'rudih@windstream.net', 'password', '216-262-9355')

SELECT *
	FROM caretaker c
	JOIN address a ON c.address_id = c.address_id

INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers) 
	VALUES ('Ellie', 'Kwecien', 'Female', '2018-08-25', 6.30, 1)

SELECT *
	FROM child

DECLARE @Existingdate datetime
SET @Existingdate=GETDATE()

INSERT INTO session (child_id, drop_off, pick_up, notes)
	VALUES (1, @Existingdate, @Existingdate, 'Ellie was a sweetheart!')

SELECT * FROM session
	JOIN child ON session.child_id = child.child_id


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

INSERT INTO address (street_number, streed_name, city, state, zip, county, country)
	VALUES (2221, 'Mayfield Ridge Road', 'Mayfield Heights', 'Ohio', 44124, 'Cuyahoga', 'United States of America')

INSERT INTO parent (address_id, first_name, last_name, email, phone_number)
	VALUES (2, 'Megan', 'Kwecien', 'meganhowie.1@gmail.com', '330-222-2222')

INSERT INTO parent (address_id, first_name, last_name, email, phone_number)
	VALUES (2, 'Matt', 'Kwecien', 'mat.1@gmail.com', '330-222-2222')

SELECT * FROM parent

INSERT INTO child_parent (child_id, parent_id)
	VALUES (1, 1)

INSERT INTO child_parent (child_id, parent_id)
	VALUES (1, 2)

SELECT * FROM child
	JOIN child_parent ON child.child_id = child_parent.child_id
	JOIN parent ON child_parent.parent_id = parent.parent_id
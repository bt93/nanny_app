INSERT INTO address (street_number, streed_name, city, state, zip, county, country)
	VALUES (1282, 'Berkshire Drive', 'Macedonia', 'Ohio', 44056, 'Summit', 'United States of America')

SELECT * from address

INSERT INTO caretaker (address_id, first_name, last_name, email, password, phone_number)
	VALUES (1, 'Ruth', 'Howie', 'rudih@windstream.net', 'password', '216-262-9355')

SELECT *
	FROM caretaker c
	JOIN address a ON c.address_id = c.address_id

INSERT INTO child (caretaker_id, first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers) 
	VALUES (2, 'Ellie', 'Kwecien', 'Female', '2018-08-25', 6.30, 1)

SELECT *
	FROM child
	JOIN caretaker ON child.caretaker_id = caretaker.caretaker_id
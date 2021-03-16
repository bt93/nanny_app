
-- Creates the table for allergy types
CREATE TABLE allergy_type(
	allergy_type_id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(80) NOT NULL
)

-- Creates the table for an allergy
CREATE TABLE allergies (
	allergy_id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(80) NOT NULL,
	allergy_type_id INT NOT NULL,
	CONSTRAINT fk_allergy_allergy_type FOREIGN KEY (allergy_type_id) REFERENCES allergy_type(allergy_type_id)
)

-- Creates the table that references the allergy to the child
CREATE TABLE child_allergies (
	child_id INT NOT NULL,
	allergy_id INT NOT NULL,
	CONSTRAINT pk_child_allergies PRIMARY KEY (child_id, allergy_id),
	CONSTRAINT fk_child_to_allergy FOREIGN KEY (child_id) REFERENCES child(child_id),
	CONSTRAINT fk_allergy_to_child FOREIGN KEY (allergy_id) REFERENCES allergies(allergy_id)
);

-- Insert common types of allergies
INSERT INTO allergy_type (name) VALUES ('Drug');
INSERT INTO allergy_type (name) VALUES ('Food');
INSERT INTO allergy_type (name) VALUES ('Insect');
INSERT INTO allergy_type (name) VALUES ('Mold');
INSERT INTO allergy_type (name) VALUES ('Pet');
INSERT INTO allergy_type (name) VALUES ('Pollen');
INSERT INTO allergy_type (name) VALUES ('Other');

-- Insert common allergies
INSERT INTO allergies (name, allergy_type_id) VALUES ('Penicillin', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Amoxicillin', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Tetracycline', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Ibuprofen', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Naproxen', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Asprin', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cetuximab', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Rituximab', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Insulin', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Carbamazepine', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Lamotrigine', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Phenytoin', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Atracurium', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Succinycholine', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Vecuronium', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Drug'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cow''s Milk', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Eggs', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Tree Nuts', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Peanuts', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Shellfish', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Fish', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Soy', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Wheat', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Linseed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Sesame Seed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Peach', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Banana', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Avodcado', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Kiwi', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Passion Fruit', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Celery', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Garlic', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Mustard Seed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Aniseed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Chamomile', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Food'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Bees', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Wasps', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Hornets', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Yellow-Jackets', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Fire Ants', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Mosquitoes', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Kissing Bugs', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Bedbugs', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Fleas', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Flies', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cockroaches', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Dust Mites', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Insect'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Aspergillus', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Mold'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Penicillium', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Mold'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cladosporium', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Mold'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Alternaria', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Mold'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Stachybotrys', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Mold'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cats', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Dogs', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Mice', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Gerbils', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Hamsters', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Guinea Pigs', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Rabits', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pet'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Birch', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Oak', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Grass', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Ragweed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Elm', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Cedar', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Pine', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Poplar', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Walnut', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Nettle', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Sagebrush', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Tumbleweed', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Lamb''s Quarters', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('English Plantain', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Pollen'));
INSERT INTO allergies (name, allergy_type_id) VALUES ('Latex', (SELECT allergy_type_id FROM allergy_type WHERE name = 'Other'));
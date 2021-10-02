-- Stored Procedure to add a new allergy. Check allergy type ID before adding.
EXEC AddNewAllergy @name = 'Wolf', @allergy_type_id = 5;

-- Stored procedure for a new allergy type
EXEC AddAllergyType @name = 'Car';
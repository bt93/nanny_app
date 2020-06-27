# Nanny Tracking App

An app that allows a nanny or caretaker to keep track of dates, times, and other data regarding all the children under his or her responsibility. Will include nap times, meal times, diaper changes, pick up and drop off times, and amounts owed to caretaker.

## Endpoints

### Caretakers

**Get all Caretakers**

GET - `/api/caretakers` - Will return a list of all the caretakers in the database

```
{
    "careTakerId": 1,
    "addressId": 1,
    "firstName": "Jane",
    "lastName": "Doe",
    "emailAddress": "jane@doe.com",
    "password": "mypassword",
    "phoneNumber": "xxx-xxx-xxxx",
    "address": {
        "street": "1111 Example BlVD",
        "city": "New York",
        "state": "NY",
        "zip": 10004,
        "county": "New York",
        "country": "United States of America"
    }
},
{
    "careTakerId": 2,
    "addressId": 2,
    "firstName": "Bob",
    "lastName": "Person",
    "emailAddress": "bob@bob.com",
    "password": "bob",
    "phoneNumber": "xxx-xxx-xxxx",
    "address": {
        "street": "1232 Street ST",
        "city": "Kansas City",
        "state": "MO",
        "zip": 30231,
        "county": "Example",
        "country": "United States of America"
    }
}
```

**Get a single Caretaker by id**

GET - `/api/caretakers/{id}` - Returns a single caretaker given an id. If id doesn't exist, will return `404`.

```
{
    "careTakerId": 2,
    "addressId": 2,
    "firstName": "Bob",
    "lastName": "Person",
    "emailAddress": "bob@bob.com",
    "password": "bob",
    "phoneNumber": "xxx-xxx-xxxx",
    "address": {
        "street": "1232 Street ST",
        "city": "Kansas City",
        "state": "MO",
        "zip": 30231,
        "county": "Example",
        "country": "United States of America"
    }
}
```

**Add a Caretaker**

POST - `/api/caretakers` - Creates a new Caretaker and if successful returns that data in JSON and `201`. 

**Update Caretaker**

PUT - `/api/caretakers/{id}` - Will update the caretaker given and return that data in JSON and `201`. If caretaker doesn't exist, returns a `404`.

**Delete Caretaker**

DELETE - `/api/caretakers/{id}` - Will delete the caretaker info at that id and return a `204`. If caretaker doesn't exist, returns a `404`.

### Parents

**Get all Parents**

GET - `/api/parents` - Returns a list of all parents in the database.

```
{
        "parentId": 1,
        "addressId": 3,
        "firstName": "Matt",
        "lastName": "Guy",
        "emailAddress": "mat@mat.com",
        "phoneNumber": "xxx-xxx-xxxx",
        "address": {
            "street": "12312 Example RD",
            "city": "Someplace",
            "state": "OH",
            "zip": 44231,
            "county": "Medina",
            "country": "United States of America"
        }
    },
    {
        "parentId": 2,
        "addressId": 4,
        "firstName": "Bob",
        "lastName": "Example",
        "emailAddress": "myemail@email.com",
        "phoneNumber": "xxx-xxx-xxxx",
        "address": {
            "street": "2314 Some RD",
            "city": "Another Place",
            "state": "TN",
            "zip": 2342,
            "county": "Place",
            "country": "United States of America"
        }
    }
```

**Get all Parents by Child**

GET - `/api/parents/child/{child-id}` - Returns a list of any parent connected to a child.

**Get parent by id**

GET - `/api/parents/{id}` - Returns a single parent by id.

**Add new Parent**

POST - `/api/parents` - Creates a new parent in the database and returns it back.
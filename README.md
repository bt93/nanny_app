# Nanny Tracking App

An app that allows a nanny or caretaker to keep track of dates, times, and other data regarding all the children under his or her responsibility. Will include nap times, meal times, diaper changes, pick up and drop off times, and amounts owed to caretaker.

## Endpoints

### Login and Register

This API uses [JSON Web Tokens](https://www.jsonwebtoken.io/) (JWT) to authenticate the main users, which will be the caretaker.

**Login**

POST - `/api/login` - Takes an email address and password and checks if the hashed password is equal to what is on the database. If the user doesn't exist or the email address and/or password are incorrect, returns `400`. If email and password are a match will return a JWT.

*Request Body*
```
{
    "emailAddress": "example@example.com",
    "password": "password"
}
```
*Response Body*
```
{
    "careTakerId": 1,
    "emailAddress": "example@example.com",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6Imhvd2llamFzb245M0BnbWFpbC5jb20iLCJuYmYiOjE1OTM3OTkyMzIsImV4cCI6MTU5NDQwNDAzMiwiaWF0IjoxNTkzNzk5MjMyfQ.x-LtX2cTPSqdFKXklC9MDPkuz_bNbE676kgSN0cvTAY"
}
```

**Register**

POST - `/api/login/register` - Takes in a new caretaker object, and hashes the password, and returns a `201`.

*Request Body*
```
{
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
}
```

### Caretakers

## NOTE: This endpoint is currently not being considered for the final product
**Get all Caretakers**

GET - `/api/caretakers` - Will return a list of all the caretakers in the database

```
{
    "careTakerId": 1,
    "addressId": 1,
    "firstName": "Jane",
    "lastName": "Doe",
    "emailAddress": "jane@doe.com",
    "password": "H2tL5RqyydUVbwTx9j1MUQ4hLmM=",
    "phoneNumber": "xxx-xxx-xxxx",
    "address": {
        "street": "1111 Example BlVD",
        "city": "New York",
        "state": "NY",
        "zip": 10004,
        "county": "New York",
        "country": "United States of America"
    },
    "salt": "vG/qDBiBzvA=",
},
{
    "careTakerId": 2,
    "addressId": 2,
    "firstName": "Bob",
    "lastName": "Person",
    "emailAddress": "bob@bob.com",
    "password": "Djp/qTSsnNeibLwE7n33Q+bzv70=",
    "phoneNumber": "xxx-xxx-xxxx",
    "address": {
        "street": "1232 Street ST",
        "city": "Kansas City",
        "state": "MO",
        "zip": 30231,
        "county": "Example",
        "country": "United States of America"
    },
    "salt": "0zRlA9sK/KY="
}
```
**Get the current caretaker**

GET - `/api/caretakers/current` - Returns a single caretaker that is logged in. If id doesn't exist, will return `404`.

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

**Update Caretaker**

PUT - `/api/caretakers` - Will update the current caretaker and return that data in JSON and `201`. If caretaker doesn't exist, returns a `404`.

**Delete Caretaker**

DELETE - `/api/caretakers` - Will delete the current caretaker info and return a `204`. If caretaker doesn't exist, returns a `404`.

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

**Update a Parent**

PUT - `/api/parents/{id}` - Updates a parent given a parent object. If parent doesn't exist, returns `404`. If it does, returns `201` and updated parent.

### Children

**Get all children for caretaker**

Get - `/api/children` - Gets a list of all the children attributed to the current caretaker logged in

```
[
    {
        "childId": 1,
        "firstName": "Bobby",
        "lastName": "Example",
        "gender": "M",
        "dateOfBirth": "2018-08-25T00:00:00",
        "ratePerHour": 6.3000,
        "needsDiapers": true,
        "imageUrl": "",
        "parents": [
            {
            "parentId": 1,
            "addressId": 3,
            "firstName": "Sally",
            "lastName": "Example",
            "emailAddress": "Sally@example.com",
            "phoneNumber": "xxx-xxx-xxxx",
            "address": {
                "street": "12312 Example RD",
                "city": "Someplace",
                "state": "TN",
                "zip": 44231,
                "county": "Place",
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
        ]
    },
    {
        "childId": 2,
        "firstName": "Tina",
        "lastName": "Person",
        "gender": "F",
        "dateOfBirth": "2016-08-25T00:00:00",
        "ratePerHour": 9.2000,
        "needsDiapers": false,
        "imageUrl": "",
        "parents": [
            {
                "parentId": 3,
                "addressId": 1,
                "firstName": "Jerry",
                "lastName": "Person",
                "emailAddress": "person@gmail.com",
                "phoneNumber": "xxx-xxx-xxxx",
                "address": {
                    "street": "2135 Our Road",
                    "city": "This Place",
                    "state": "TN",
                    "zip": 44056,
                    "county": "Summit",
                    "country": "United States of America"
                }
            }
        ]
    }
]
```
# Nanny Tracking API

An app that allows a nanny or caretaker to keep track of dates, times, and other data regarding all the children under his or her responsibility. Will include nap times, meal times, diaper changes, pick up and drop off times, and amounts owed to caretaker.

## Endpoints

### Login and Register

This API uses [JSON Web Tokens](https://www.jsonwebtoken.io/) (JWT) to authenticate the main users, which will be the caretaker.

**Login**

POST - `/api/login` - Takes an email address and password and checks if the hashed password is equal to what is on the database. If the user doesn't exist or the email address and/or password are incorrect, returns `400`. If email and password are a match will return a JWT.

*Request Body*
```json
{
    "emailAddress": "example@example.com",
    "password": "password"
}
```
*Response Body*
```json
{
    "careTakerId": 1,
    "emailAddress": "example@example.com",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6Imhvd2llamFzb245M0BnbWFpbC5jb20iLCJuYmYiOjE1OTM3OTkyMzIsImV4cCI6MTU5NDQwNDAzMiwiaWF0IjoxNTkzNzk5MjMyfQ.x-LtX2cTPSqdFKXklC9MDPkuz_bNbE676kgSN0cvTAY"
}
```

**Register**

POST - `/api/login/register` - Takes in a new caretaker object, and hashes the password, and returns a `201`.

*Request Body*
```json
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

```json
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

```json
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

GET - `/api/parents` - Returns a list of all parents related to the current caretaker in the database.

```json
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

GET - `/api/parents/child/{childId}` - Returns a list of any parent connected to a child and current caretaker.

**Get parent by id**

GET - `/api/parents/{id}` - Returns a single parent by id that is also connected to the parent.

**Add new Parent**

POST - `/api/parents/child/{childId}` - Creates a new parent and assigns it to a child in the database and returns it back.

**Add Existing Parent**

POST - `/api/parents/{id}/child/{childId}` - Adds an already existing parent to a child

**Update a Parent**

PUT - `/api/parents/{id}` - Updates a parent given a parent object. If parent doesn't exist, returns `404`. If it does, returns `201` and updated parent.

### Children

**Get all children for caretaker**

GET - `/api/children` - Gets a list of all the children attributed to the current caretaker logged in. Along with the child's parents.

```json
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

**Get a child by id for caretaker**

GET - `/api/children/{childId}` - Gets a single child by their id attributed to the current caretaker logged in. Along with the child's parents.

**Get Deactivated Children**

GET `/api/children/{childId}` - Gets all the children who have been deactivated

**Create a new child**

POST - `/api/children` -  Creates a new child given a child object, and returns that object back.

**Update Existing Child**

PUT - `/api/children/{childId}` - Updates an existing child given an id and child object. If child isn't in the database, will return `404`.


**Reinstate Child**

PUT - `/api/reinstate/{childId}` - Will change the child back to active

**Delete Existing child**

DELETE - `/api/children/{childId}` - Will delete a child. Doesn't actually delete from the database but will turn the child to inactive. If successful returns `204`, if not returns `404`.

### Sessions

**Get Current Sessions**

GET - `/api/sessions` - Will get a full list of the current session in progress now

```json
[
    {
        "sessionId": 2,
        "childId": 2,
        "dropOff": "2020-10-02T08:10:22",
        "pickUp": null,
        "notes": "Was Very cranky today.",
        "child": {
            "childId": 2,
            "firstName": "Molly",
            "lastName": "Silly",
            "gender": "M",
            "dateOfBirth": "2016-06-25T00:00:00",
            "ratePerHour": 6.2300,
            "needsDiapers": true,
            "active": true,
            "imageUrl": "",
            "parents": [
                {
                    "parentId": 1,
                    "addressId": 2,
                    "firstName": "Martha",
                    "lastName": "Silly",
                    "emailAddress": "place@palce.com",
                    "phoneNumber": "330-222-2222",
                    "address": {
                        "street": "2202 Rpad Rd",
                        "city": "Someplace",
                        "state": "Ohio",
                        "zip": 44124,
                        "county": "Cuyahoga",
                        "country": "United States of America"
                    }
                }
            ]
        },
        "diapers": [],
        "meals": [],
        "naps": [
            {
                "napId": 3,
                "sessionId": 2,
                "startTime": "2020-10-02T10:23:00",
                "endTime": null,
                "notes": ""
            }
        ]
    }
]
```

**Get Session By Id**

GET - `/api/sessions/{sessionId}` - Gets all the sessions by id. If it doesn't exist, returns a `404`

***Get All Sessions By Child**

GET - `/api/sessions/child/{childId}` - Gets a list of the sessions by child.

**Get All Sessions**

GET `/api/sessions/all` - Returns a list of all the sessions in by caretaker

**Get All Naps**

GET `/api/sessions/{sessionId}/naps` - Gets a list of all the naps per session

```json
[
    {
        "napId": 3,
        "sessionId": 2,
        "startTime": "2020-10-02T10:23:00",
        "endTime": null,
        "notes": ""
    }
]
```

**Get Nap By Id**

GET - `/api/sessions/naps/{napId}` - Gets a Nap by the Id

**Get All Meals**

GET - `/api/sessions/{sessionId}/meals` - Gets a list of all the meals

**Get Meal By Id**

GET - `/api/sessions/{sessionId}/meals/{mealId}` - Gets a meal by its id

**Get All Diapers**

GET - `/api/sessions/{sessionId}/diapers` - Gets a list of all the diapers

**Get Diapers By Id**

GET - `/api/sessions/{sessionId}/diapers/{diaperId}` - Gets a diaper by its id

**Create Session**

POST - `/api/sessions/child/{childId}` - Creates a new session

**Create Nap**

POST - `/api/sessions/{sessionId}/naps` - Post a new nap given a session

**Create Meal**

POST - `/api/sessions/{sessionId}/meals` - Creates a new meal given a session

**Create Diaper**

POST - `/api/sessions/{sessionId/diapers` - Creates a new diaper change

**End Session**

PUT - `/api/sessions/end/{sessionId}` - Ends a session given an id and session

**Update Current Session**

PUT - `/api/sessions/{sessionId}` - Updates a session in progress

**Update Closed Session**

PUT - `/api/sessions/closed/{sessionId}` - Updates a closed session

**Update Nap**

PUT - `/api/sessions/{sessionId}/naps/{napId}` - Updates a nap

**Update Nap**

PUT - `/api/sessions/{sessionId}/meals/{mealId}` - Updates a meal

**Update Nap**

PUT - `/api/sessions/{sessionId}/diaper/{diaperId}` - Updates a diaper

**Delete Session**

DELETE - `/api/sessions/{sessionId}` - Deletes a session

**Delete Nap**

DELETE - `/api/sessions/{sessionId}/naps/{napId}` - Deletes a nap

**Delete Meal**

DELETE - `/api/sessions/{sessionId}/meals/{mealId}` - Deletes a meal

**Delete Diaper**

DELETE - `/api/sessions/{sessionId}/diapers/{diaperId}` - Deletes a diaper
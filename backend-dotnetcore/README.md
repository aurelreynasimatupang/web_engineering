# Backend Report

### Group 17, WebEng

## Introduction

This is the report for the backend API made using dotnetcore. The backend should run when the following command is run:

```
docker-compose up
```

In the case that docker does not deploy correctly, this should be run in the root directory:

```
docker-compose down
docker-compose up -d
docker-compose up mariadb
```

Then in `backend-dotnetcore` terminal, we have to install Newtonsoft because it was used in the database seeding. (this should have been done in docker-compose up):

```
dotnet add package Newtonsoft.Json --version 13.0.1
```

Then, run the backend:

```
dotnet watch run
```

## ApiDesign

To fulfill the Api requirements, most of the requirements are query parameters under the property endpoint. Because of this, some of the requirements can be mix-queried.

To obtain one extra point, we added a User endpoint `/user` and another User endpoint `user/{id}`.

We followed the TA's feedback and made statistics an endpoint `/statistics` and `/statistics/{city}`, all calculations that changes a statistics object is done in the backend, which we will see later. Statistics object also includes a list of properties for ready calculation of rent and deposit data.

We also follow reccommended directions in the tutorial to add `PropertySummary` and `UserSummary` schema in our database and api models.

## Database Design & Repository

We chose to follow the tutorial and use docker and mariadb to run our database software. The database follows that of the api shema, where 3 main tables properties, user, and statistics, are made and connected.

At each properties object in `properties.json`, we take all the values needed as specificed in our spec.yaml to create the property object, statistics object, and user object. The user will be checked by id if they already exist and is added to the database. The statistics will be checked by city, then properties will be appended to the statistics' list of properties. The properties list would be used to calculate all the values in statistics. The database context will then connect the user and statistics to the properties, because they both have a list of properties.

The `properties.json` file given by Kaggle is not an array of objects, but of a type of json called line-delimited json, so we cannot parse this by what the TA has done. Upon research, we found out we can parse this type of json file by installing `Newtonsoft`. Then, we can parse it into an array of properties object and append them to the database context.

With this database, we can create a repository implementing IRepository. With 3 endpoints, there were 3 repositories for each of them.

## MVC Pattern

We followed the tutorial and used the MVC pattern.

### View

Eventhough the api specification allows json and csv, the view now only returns json files.

### Model

We created the models for `Property`, `User` and `Statistics`, and `PlaceInfo` and `MatchTenant` are sub-objects in Property as per the api design schema.

We also created api models for them which implements `PropertySummary` and `UserSummary` for returning json file through the api more easily. If we returned the `Models.Property` or `Models.User`, it would be a recursive mess, since `Property` contains `User`, which contains a list of properties.

We kept the `LastLoggedOn` and `MemberSince` attribute for user, and let it have a list of properties.

`Statistics` has data on sum, number, mean, median and sd for both rent and deposit to make it easy to calculate in the backend.

### Controller

All controllers are implemented from the given Abstract Controller file, controllers does vastly different functions, so we have to split them in sections.

#### Property

All methods in the specifications is fulfilled in property. They are `GET /`, `POST /`, `PUT /`, `DELETE /`,`GET /{id}`,

To fulfil the parameters, we have `order-dir` and `order-by `in `RequestModel.Order`, and lattitude, longitude, city, isActive, min (rent), and max (rent) in `RequestModel.Filter`. Querying them in the repository is not very difficult.

For the Post function, the creation of a new unique id will be handled by the frontend since the request body requires an id.

Because property affect statistics, all POST, PUT, and DELETE endpoints edits correspopnding statistics and edits its data, then calls `UpdateSync` of statistics repository to edit changes.

#### Statistics

It has only GET methods for `/statistics` and `/statistics/{city}`.

### Users

Only GET methods for `/users` and `/users/{id}`.

## Bugs and Improvement

- Our Swagger api cannot work, but we confirmed that POST, DELETE and PUT does work in the frontend.
- If we are able to, we will also add the choice to return a csv file.

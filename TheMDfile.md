# WebEng Gr17 D4

## For Easy run , see README.txt


## Introduction

We are Lazos, Abel, Aurel and Ravi from group 17 of the 2021-2022 WebEng course. Documentation and report for backend is written as a README.md in the `backend-dotnetcore` directory. Documentation of frontend is written in the README.md of `react-file-forntend` directory as well as embedded in the webpage, but the webpage also contains contributions and work process.

We followed mostly TA directions as it is our first time using Docker, mariadb and C#. Some group members have more experience with react.js so we chose to do the frontend with it. We have made the necessary files to deploy them, including the `.env` file, the `docker-compose.yml`, and `Dockerfile` in backend and frontend. The backend runs in port 3001, mariaDB in port 3006, and frontend in 3000.

## How to run

To run the project, first paste the `properties.json` file from Kaggle inside the data folder (since it's too big for us to push to git) It has to be the type of a line-limited json file (an array of objects without the arry square brackets) because that's how we programmed the backend to parse it. Next, switch the `#if` block where database seeding happen in `program.cs` to true. If the database is successfully set up, switch it back to false for everytime you run it again. If you want to reset the database, make sure to delete the db folder in the `data` folder, and that there is no `2021-GROUP17` docker container running yet. Running the following command in the root directory should run the entire project smoothly:

```
docker-compose up
```

### MariaDB Error

In the case that you get the error that mariadb seems to not be working for some reason, quit docker in all three servers and close all docker containers with this command:

```
docker-compose down mariadb
```

Then, delete db from data folder and run docker again.

### Docker can't deploy

In the case that docker does not deploy correctly, run the following in the root directory:

```
docker-compose down
docker-compose up -d
docker-compose up mariadb
```

Then in `backend-dotnetcore` terminal, we have to manually install Newtonsoft because it was used in the database seeding:

```
dotnet-sdk should be installed as well for windows to run
```

```
dotnet add package Newtonsoft.Json --version 13.0.1
```

Then, run the backend:

```
dotnet watch run
```

The json view of the backend can be seen in `localhost:3001`

Now to run the frontend, first install `node_modules`:

```
npm ci
```

There are a few things specified in the `package-lock` file that needs to be installed beforehand or else the program can't run. This ideally should already be installed upon deploying with docker. In the case that it doesnt, run the command `npm install X` in the frontend directory, for X being:

- react-bootstrap
- bootstrap
- react-router
- react-router-dom
- google-map-react

To run the project, run this in the frontend folder:
`npm start`

If an error occurs because it can't recognize the react script, be it by running with docker or running frontend manually, run the following in the frontend directory, then try to start the web app again.

```
npm install
npm start
```

# WebEng Gr17 Frontend

We chose to do this project with React and bootstrap because of its advanced functionalities and easier unit testing. Our frontend is a multipage project with different URI. This README.md documents only how to navigate it, the full report is inside the frontend webpage.

## Introduction

To run this project, docker should already install all the scripts needed when running the following in the root directory::

```
docker-compose up
```

In the case that docker does not do so and you had to manually run mariadb and the dotnet backend, first create the `node_modules` needed by our frontend by running the following command. (this ideally should already be done by docker):

```
npm ci
```

There are a few things specified in the `package-lock` file, needs to be installed beforehand or else the program can't run. This ideally should already be installed upon deploying with docker. In the case that it doesnt, run the command `npm install X` in the frontend directory, for X being:

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

Then you can see the frontend webpage by browsing [http://localhost:3000](http://localhost:3000).

## URI

This section explains the different URI you can access in our project, how to access them, the javascript file responsible for them and which api was involved. We used the `react-router` and `react-router-dom` to make navigating between webpages possible. `app.js` contains all the possible routings while the corresponding file builds the website with the routes.

### main page

The root directory, http://localhost:3000, is built by the file `home.js` and contains a search bar with query parameters `city` (with a default of Groningen), `Min Price` (minimum rent, default 0), `Max Price` (maximum rent, default 500000), `Max Number` (limit number of properties, default 10), `order-by` (ascending or descending, default asc), `order-dir`(the values to order properties by, default areasqm) and `isActive` (default true). There is also search by id box in this case the query values will be ignored. Once query parameters are set, click the search button and an api call with be made to `/` retrive properties object according to the queries and display them, then the objects will be passed to the `properties.js` file and create a page in `/properties` directory.

If the id box is being filled, then the api call to `/{id)` will be made and also go to `/properties` directory, showing one property and still showing the statistic.

The header of this main page can also toggle between the "Home", "about us", and "Search by Log/Lat", which links to `/`, `/about` and `/logLat` respectively.

### /about

This page introduces our group and tells you that the documentation is in the README.md.

### /properties & /propUpdate

The top of the page shows statistics information of the city being quried, with its mean, median and standard deviation of rent and deposit. This is made with an api call to the `/statistics/{city}` endpoint inside the `properties.js` file. Directly calling the api and displaying elements at the same time causes bugs, so we made it possible for this section to display "Loading..." while the api call was being made, then display something if there's an object retreived by the api call.

Below the page is a list of properties according to the queries being made in boxes, with the boxes showing some informations of the properties. Each box will have two buttons, the delete and edit button. The delete button will make the delete call to the `properties/{id}` endpoint and delete one property according to its id in that box.

The edit button will take user to the `/propUpdate` directory (`propUpdate.js`). This will turn the data of properties into textboxes and change the 2 buttons into one button. The user can edit the values of (.....) of a properties. We didn't make all all values editable for simplicity, unless requirements specifically requires that. This is only to show that the `UPDATE` method works. After making changes, the button will call the update method to `/properties/{id}` endpoint.

### /logLat, /propertiesLogLat & /propUpdateMulti

Clicking "Search by Log/Lat" in the header bar takes user to the `/logLat` directory made by `logLat.js`. It looks almost the same as the home page, except in here, the search bar in the middle of the screen only allows querying by longitude and latitude. Upon filling in information and clicking search, the frontend will make an api call to `/properties` with the query `?longitude={long}&latitude={lat}` and retrieve all properties with the matching long and latitude and pass it to `propertiesLogLat.js`, which builds the `/propertiesLogLat` page.

`/propertiesLogLat` directory is similar to `/properties`, except that there will not be a city statistics. Instead, there will be a google map with a landmark showing the point of latitude longitude searched. The way this works is that we used the `google-map-react` package that contains the component `<GoogleMapReact>` and make an api call to google to create this component. The parameters includes the latitude and longitude we just queried, the apiKey created in Google Developers (made with Aurel's google email), and how much to zoom into this point. This is the 3rd party api we chose to use for the project. Below the map looks almost the same, with boxes of properties.

The only thing different from `/properties` directory is that on the left down corner there is a delete all and update all button. The delete all button calls the delete method on `/properties` and take the query parameters longitude and latitude to delete all properties with this same coordinates.

The update all button takes us to the `/propUpdateMulti` directory built by the `propUpdateMulti.js` files, in here only one box will be available to edit the same parameters in the properties, except the changes will be applied to all properties of this latititude and longitude. Clicking the button makes an put api call to `/properties` and updates corressponding properties to the same value.

### \*

This is the page displayed when the api call returns the code 404, which is built by `notFound.js`.

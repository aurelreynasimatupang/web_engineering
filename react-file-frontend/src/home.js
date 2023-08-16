import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Home() {
  const [inputData, setInputValue] = useState({
    id: "",
    city: "Groningen",
    min: 1,
    max: 50000,
    limit: 10,
    orderBy: "Rent",
    orderDir: "Asc",
    isActive: "True",
  });

  const navigate = useNavigate();

  function toAddNew() {
    navigate("/newProperty");
  }

  function DisplayInfo() {
    if (inputData.id !== "") {
      fetch(`http://localhost:3001/${inputData.id}`)
        .then((response) => {
          return response.json();
        })
        .then((data) => {
          navigate("/properties", { state: [data] });
        });
    } else {
      fetch(`http://localhost:3001/?city=${inputData.city}&limit=${inputData.limit}&min=${inputData.min}&max=${inputData.max}
            &order-by=${inputData.orderBy}&order-dir=${inputData.orderDir}&isActive=${inputData.isActive}`)
        .then((response) => {
          return response.json();
        })
        .then((data) => {
          navigate("/properties", { state: data });
        });
    }
  }

  function Footer() {
    return (
      <div className="Footer">
        <p className="center">Scroll down for documentation!</p>
        <h1 className="center">Web Engineering Project</h1>
        <p className="center">Team 17</p>
        <div className="aboutus">
          <h2>Welcome to our Project!</h2>
          <p>
            This is our main page, built using <code>home.js</code> with the
            directory <strong>http://localhost:3000/</strong>. In here we
            explain the different URI you can access in our project, how to
            access them, the javascript file responsible for them and which api
            was involved. We used the <strong>react-router</strong> and{" "}
            <strong>react-router-dom</strong> to make navigating between
            webpages possible. <code>app.js</code> contains all the possible
            routings while the corresponding file builds the website with the
            routes. All api calls is made using the react function{" "}
            <code>fetch</code>, which takes the json form of parameters methods,
            headers, body, parameters, and so on to make appropriate api calls.
          </p>
          <p>
            We chose to do this project with React and bootstrap because of its
            advanced functionalities and easier unit testing. Our frontend is a
            multipage project with different URI. How to navigate them is
            documented in the webpage and in the <code>README.md</code> inside
            of the frontend folder.
          </p>
          <p>
            Firstly, you can see that this main page contains a navigation bar
            and a search query. The navgiation bar is build with{" "}
            <code>NavBar.js</code> and contains 4 directories.{" "}
            <span className="text-white">Home</span> is the current site,{" "}
            <span className="text-white">About Us</span> introduces our group
            and working process,{" "}
            <span className="text-white">Search by Lat/Long</span> allows
            editing of properties in the same house, and{" "}
            <span className="text-white">Search User</span> searches a user that
            may or may not sell a property.{" "}
          </p>
          <p>
            The center of our home page contains a query card. Each parameter is
            a query parameter avilable in the backend, kept track in the
            frontend using a <code>useState</code> object. They are query
            parameters <code>city</code> (with a default of Groningen),{" "}
            <code>Min Price</code>(minimum rent, default 0),{" "}
            <code>Max Price</code>(maximum rent, default 500000),{" "}
            <code>Limit</code>
            (limit number of properties, default 10), <code>order-by</code>{" "}
            (ascending or descending, default asc), <code>order-dir</code>
            (the values to order properties by, default areasqm) and{" "}
            <code>isActive</code> (default true). Once query parameters are set,
            click the search button and an api call{" "}
            <span className="text-white">GET</span> will be made to{" "}
            <strong>/</strong> to retrive properties object according to the
            queries. Then, the object returned will be passed to the{" "}
            <strong>http://localhost:3000/properties</strong> site. There is
            also search by <code>id</code> box, in this case the query values
            will be ignored and the api call{" "}
            <span className="text-white">GET</span> will be made to{" "}
            <strong>/[id]</strong>.
          </p>
          <p>
            The "Add New" button takes you to{" "}
            <strong>http://localhost:3000/newProperty</strong>, where you can
            add a new property to the data base.
          </p>
          <p>
            If in any case the backend server response with code 404,{" "}
            <code>notFound.js</code> will build and take you to{" "}
            <strong>http://localhost:3000/*</strong>
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="Search">
      <section className="main py-5">
        <div className="container-fluid py-5">
          <div className="row py-5 mx-5">
            <h1 className="text-white center">Find the Property you desire</h1>
            <p className="text-white center">
              {" "}
              Vast number of properties waiting to be found
            </p>

            <div className="card mt-4">
              <div className="card-body">
                <div className="row">
                  <div className="col-md-1">
                    <span>City</span>
                    <input
                      type="text"
                      id="form1"
                      className="form-control"
                      placeholder="enter a city"
                      aria-label="text"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          city: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-md-1">
                    <span>Min Price</span>
                    <input
                      type="number"
                      id="form2"
                      className="form-control"
                      placeholder="min price"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          min: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-md-1">
                    <span>Max price</span>
                    <input
                      type="number"
                      id="form3"
                      className="form-control"
                      placeholder="max price"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          max: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-md-1">
                    <span>Limit</span>
                    <input
                      type="number"
                      id="form3"
                      className="form-control"
                      placeholder="limit"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          limit: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-md-1">
                    <span>order-By</span>
                    <select
                      className="mt-1"
                      name=""
                      id="orderBy"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          orderBy: event.target.value,
                        })
                      }
                    >
                      <option value="Rent"> rent</option>
                      <option value="Costsqm"> costsqm</option>
                    </select>
                  </div>

                  <div className="col-md-1">
                    <span>order-Dir</span>
                    <select
                      className="mt-1"
                      name=""
                      id="orderDir"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          orderDir: event.target.value,
                        })
                      }
                    >
                      <option value="Asc"> asc</option>
                      <option value="Desc"> desc</option>
                    </select>
                  </div>

                  <div className="col-md-1">
                    <span>isActive</span>
                    <select
                      className="mt-1"
                      name=""
                      id="isAvailable"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          isActive: event.target.value,
                        })
                      }
                    >
                      <option value="true">Yes</option>
                      <option value="false">No</option>
                    </select>
                  </div>

                  <div className="col-md-1">
                    <span id="uniqueId">uniqueId*</span>
                    <input
                      type="text"
                      id="unique"
                      className="form-control"
                      placeholder="enter an id"
                      aria-label="text"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          id: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-md-2">
                    <button className="btn1" onClick={() => DisplayInfo()}>
                      Search
                    </button>
                    <button className="btn2" onClick={toAddNew}>
                      Add New
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>{" "}
      </section>
      <Footer />
    </div>
  );
}

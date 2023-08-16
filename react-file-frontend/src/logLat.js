import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function LogLat() {
  const navigate = useNavigate();

  const [inputData, setInputValue] = useState({
    longitude: 1,
    latitude: 1,
  });

  function DisplayInfo() {
    fetch(
      `http://localhost:3001/?longitude=${inputData.longitude}&latitude=${inputData.latitude}`
    )
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        navigate("/propertiesLogLat", { state: data });
      });
  }

  function Footer() {
    return (
      <div className="Footer">
        <p className="center">Scroll down for documentation!</p>
        <h1 className="center">Web Engineering Project</h1>
        <p className="center">Team 17</p>
        <div className="aboutus">
          <p>
            Welcome to the <strong>http://localhost:3000/logLat</strong>{" "}
            directory made by <code>logLat.js</code>. It looks almost the same
            as the home page, except in here, the search bar in the middle of
            the screen only allows querying by longitude and latitude. Let's get
            you started with some coordinates you can use: (longitude, latitude)
            <ul>
              <li>(6.127975,51.938433)</li>
              <li>(6,913927,53.013933)</li>
              <li>(5.800954,51.843076)</li>
            </ul>
            Upon filling in information and clicking search, the frontend will
            make an api call to <strong>/</strong> with the query{" "}
            <strong>/?longitude=[long]&latitude=[lat]</strong> and retrieve all
            properties with the matching long and latitude, then passing it to{" "}
            <code>propertiesLogLat.js</code>, which builds the{" "}
            <strong>http://localhost:3000/propertiesLogLat</strong> page.
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
            <h1 className="text-white text-center">
              Search by longitude/ latitude
            </h1>
            <p className="text-white text-center">
              {" "}
              Vast number of properties waiting to be found
            </p>

            <div className="card mt-4">
              <div className="card-body">
                <div className="row px-5">
                  <div className="col-lg-1">
                    <span>Longitude</span>
                    <input
                      type="number"
                      id="form4"
                      className="form-control"
                      placeholder="longitude"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          longitude: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-lg-1">
                    <span>Latitude</span>
                    <input
                      type="number"
                      id="form4"
                      className="form-control"
                      placeholder="latitude"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          latitude: event.target.value,
                        })
                      }
                    />
                  </div>

                  <div className="col-lg-2">
                    <button className="btn1" onClick={DisplayInfo}>
                      Search
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

import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Users() {
  const navigate = useNavigate();
  const [inputData, setInputValue] = useState({
    userId: "",
  });

  function DisplayInfo() {
    fetch(`http://localhost:3001/users/${inputData.userId}`)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        navigate("/usersDisplay", { state: data });
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
            Welcome to the <strong>http://localhost:3000/users</strong>{" "}
            directory made by <code>User.js</code>. It looks almost the same as
            the home page, except in here, the search bar in the middle of the
            screen only allows querying by user id. Upon filling in information
            and clicking search, the frontend will make an{" "}
            <span className="text-white">GET</span> api call to{" "}
            <strong>/users[id]</strong> with the specific id and retrieve the
            user you are looking for. It takes you to the{" "}
            <strong>http://localhost:3000/userDisplay</strong> page built by{" "}
            <code>usersDisplay.js</code>.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="Search">
      <section className="main py-5">
        <div className="container-fluid py-5 ">
          <div className="row py-5 mx-5 px-5">
            <h1 className="text-white text-center">Search by User</h1>
            <p className="text-white text-center">
              {" "}
              Vast number of Users waiting to be found
            </p>

            <div className="card mt-4">
              <div className="card-body">
                <div className="row">
                  <div className="col-lg-1">
                    <span>UserId</span>
                    <input
                      type="number"
                      id="form4"
                      className="form-control"
                      placeholder="userId"
                      aria-label="Search"
                      onChange={(event) =>
                        setInputValue({
                          ...inputData,
                          userId: event.target.value,
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

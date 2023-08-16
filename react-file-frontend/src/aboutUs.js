import React from "react";

export default function AboutUs() {
  return (
    <div className="aboutus">
      <h1 className="title text-white">About us</h1>
      <p className="center">
        We are Group 17 of the 2021-2022 Web Engineering course. Our members are
        Ábel (s4460669), Lazos(s4109651), Aurel(s4363787) and Ravi (s4062760).{" "}
      </p>
      <div className="gallery">
        <div className="row">
          <div className="col-3 center">
            <img src={require("./img/abel.png")} alt="abel" />
            <code>Ábel</code>
          </div>
          <div className="col-3 center">
            <img src={require("./img/lazos.png")} alt="lazos" />
            <code>Lazos</code>
          </div>
          <div className="col-3 center">
            <img src={require("./img/aurel.png")} alt="aurel" />
            <code>Aurel</code>
          </div>
          <div className="col-3 center">
            <img src={require("./img/ravi.png")} alt="ravi" />
            <code>Ravi</code>
          </div>
        </div>
      </div>
      <br />
      <p>
        First, everyone contirbuted to the design of the api and the creation of
        its documentation.{" "}
      </p>
      <p>
        The backend is mainly created by Abel and Aurel using dotnetcore, most
        of its functionatlities are taken from the TA's tutorial, but some parts
        that's specific to our database and api are tested and debugged and is
        in perfect condition.
      </p>
      <p>
        Lazos and Ravi created the base framework of the frontend using react,
        then styled the webpages and created most of the javascript
        functionalities in the frontend, including conditional rendering, api
        calls and state management. Aurel and Abel also helped debugging and
        testing the frontend.{" "}
      </p>
      <p>
        Aurel wrote all the README.md and all documentarions in the frontend.
      </p>
      <h3>M2</h3>
      <p>
        We didn't make a PR for M2 due to being too close to the original D4
        deadline (a week before). There were difficulties in database creation,
        firstly because <code>properties.json</code> was of an uncommon type of
        json file. Secondly, mariadb and docker was very difficult to work with
        because we were very new to it, and it frequently runs into development
        errors. The process of debugging this took over half of the total time
        taken in developing the backend. . Once Aurel successfully created the
        database and we programmed the controllers, it is able to do GET request
        for properties, users, and statistics.{" "}
        <span className="text-white">GET</span>,{" "}
        <span className="text-white">PUT</span> and{" "}
        <span className="text-white">DELETE</span> works for{" "}
        <strong>/[id]</strong> but not <span className="text-white">POST</span>{" "}
        yet. Statistics have not yet been made to modify when properties modify
        (it was done later) But it was sufficient for frontend to start
        implementing fetch methods. Most debugging was done by Abel.{" "}
      </p>
      <h3>M3</h3>
      <p>
        Frontend developed at the same pace as the backend. First Lazos created
        the home page was made to test the query parameters and{" "}
        <span className="text-white">GET</span> requests, as well as efforts
        into styling. Then more components are made as endpoints are being
        developed. Lat/long page is made after{" "}
        <span className="text-white">DELTE</span> and{" "}
        <span className="text-white">PUT</span> to <strong>/</strong> works.
        Then lastly, we implemented POST for properties and users page. Most
        difficulties came in state management and api calls, which sometimes can
        make calls repeatedly to the backend. Lazos is able to fix and debug
        them.{" "}
      </p>
      <h3>M4</h3>
      <p>
        We finalized all the codes, including adding functionatlities that were
        still lacking in backend and frontend and tidied up frontend's style,
        then improved documentations and the report. We are able to deploy
        everything in <strong>docker-compose up</strong>.
      </p>
    </div>
  );
}

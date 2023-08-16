import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";

export default function Properties() {
  const { state } = useLocation();
  const navigate = useNavigate();
  const [stateUpdated, setUpdatedState] = useState(state);
  const [statistics, setStatistics] = useState({});
  let stateUpdateEdit = {};

  /* get data */
  useEffect(() => {
    if (Object.keys(state).length === 0) {
      navigate("/");
    }

    fetch(`http://localhost:3001/statistics/${state[0].city}`)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        setStatistics(data);
      });
  }, []);

  function deleteProp(stateId) {
    for (let i = 0; i < stateUpdated.length; i++) {
      if (stateUpdated[i].id === stateId) {
        stateUpdated.splice(i, 1);
      }
    }

    setUpdatedState(state);
    fetch(`http://localhost:3001/${stateId}`, {
      method: `DELETE`,
    }).then((data) => {
      navigate("/properties", { state: stateUpdated });
    });
  }

  function editProp(stateId) {
    for (let i = 0; i < stateUpdated.length; i++) {
      if (stateUpdated[i].id === stateId) {
        stateUpdateEdit = stateUpdated[i];
      }
    }

    return navigate("/propUpdate", { state: [stateUpdateEdit] });
  }

  function Doc() {
    return (
      <div className="aboutus box">
        <p>This is the properties page. </p>
        <p>
          The top of the page shows statistics information of the city being
          quried, with its mean, median and standard deviation of rent and
          deposit. This is made with an <span className="text-white">GET</span>{" "}
          api call to the <strong>/statistics/[city]</strong> inside the{" "}
          <code>properties.js</code> file. Directly calling the api and
          displaying elements at the same time causes bugs, so we made it
          possible for this section to display "Loading..." while the api call
          was being made, then display something if there's an object retreived
          by the api call. The statistics will update and change whenever a
          proprty is being deleted or edited. (Maybe it requires you to refresh
          sometimes)
        </p>{" "}
        <p>
          Below the statistics section is a list of properties according to the
          queries being made in boxes, with the boxes showing some informations
          of the properties. Each box will have two buttons, the delete and edit
          button. The delete button will make the{" "}
          <span className="text-white">DELETE</span> call to the{" "}
          <strong>properties/ [id]</strong> endpoint and delete one property
          according to its id in that box.
        </p>
        <p>
          The edit button will take user to the{" "}
          <strong>http://localhost:3000propUpdate</strong> directory (
          <code>propUpdate.js</code>). This will turn the data of that one
          property into textboxes and change the 2 buttons into one "Update"
          button. The user can edit a certain values of a property. We didn't
          make all values editable for simplicity, unless requirements
          specifically specified it. This is only to show that the{" "}
          <span className="text-white">UPDATE</span> method works. After making
          changes, the button will call the update method to{" "}
          <strong>/[id]</strong> endpoint. Then users will be taken back here,
          and you can see that statistics will also change depending on changes
          to rent or deposit.
        </p>
      </div>
    );
  }
  return (
    <div className="propertyPage">
      <Doc />
      {Object.keys(statistics).length === 0 ? (
        <section id="statistics">
          <h1>Loading...</h1>
        </section>
      ) : (
        <section id="statistics">
          <h1>City : {statistics.city} </h1>
          <p> Mean of rent: {Math.round(statistics.meanRent)} </p>
          <p> Median of rent: {Math.round(statistics.medianDeposit)}</p>
          <p> Standard Deviation of rent: {Math.round(statistics.sdRent)} </p>
          <p> Mean of deposit: {Math.round(statistics.meanDeposit)} </p>
          <p> Median of deposit: {Math.round(statistics.medianDeposit)} </p>
          <p>
            {" "}
            Standard Deviation of deposit: {Math.round(
              statistics.sdDeposit
            )}{" "}
          </p>
        </section>
      )}

      <section>
        <div className="propertyList">
          {stateUpdated.map(
            ({
              id,
              city,
              rent,
              deposit,
              longitude,
              latitude,
              postalCode,
              costSqm,
              title,
              is_active,
            }) => {
              if (deposit === null) {
                deposit = 0;
              }

              return (
                <div>
                  <p>Id : {id}</p>
                  <p>City : {city}</p>
                  <p>Rent : {rent}</p>
                  <p>deposit : {deposit}</p>
                  <p>longitude : {longitude}</p>
                  <p>latitude : {latitude}</p>
                  <p>Postalcode : {postalCode}</p>
                  <p>costSqm : {costSqm}</p>
                  <p>Title : {title}</p>
                  <p>Available : {is_active}</p>
                  <p>
                    <button
                      className="btnProp"
                      id="buttonProp"
                      onClick={() => deleteProp(id)}
                    >
                      delete
                    </button>
                  </p>
                  <p>
                    <button
                      className="btnProp"
                      id="buttonProp"
                      onClick={() => editProp(id)}
                    >
                      edit
                    </button>
                  </p>
                </div>
              );
            }
          )}
        </div>
      </section>
    </div>
  );
}

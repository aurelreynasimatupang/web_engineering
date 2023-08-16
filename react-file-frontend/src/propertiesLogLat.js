import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import GoogleMapReact from "google-map-react";

export default function PropertiesLogLat() {
  const { state } = useLocation();
  const navigate = useNavigate();
  const [stateUpdated] = useState(state);

  if (state.length === 0) {
    return (
      <div className="titl">
        <h1>No properties found</h1>
      </div>
    );
  }

  function deleteProp(longitude, latitude) {
    stateUpdated.length = 0;
    alert("All properties successfully deleted");
    fetch(
      `http://localhost:3001/?longitude=${longitude}&latitude=${latitude}`,
      {
        method: `DELETE`,
      }
    ).then((data) => {
      navigate("/propertiesLogLat", { state: stateUpdated });
    });
  }

  function editProp() {
    return navigate("/propUpdateMulti", { state: [stateUpdated[0]] });
  }

  const center = { lat: state[0].latitude, lng: state[0].longitude };
  const Marker = (props) => {
    return <div className="markerpin"></div>;
  };

  function Doc() {
    return (
      <div className="aboutus box">
        <p>
          <strong>http:loclahost:3000/propertiesLogLat</strong> site is similar
          to <strong>http:localhost:3000/properties</strong>, except that there
          will not be a city statistics. Instead, there will be a google map
          with a landmark showing the point of latitude longitude searched. The
          way this works is that we used the <code>google-map-react</code>{" "}
          package that contains the component <code>GoogleMapReact</code> and
          make an api call to google to create this component. The parameters
          includes the latitude and longitude we just queried, the apiKey
          created in Google Developers (made with Aurel's google email), and how
          much to zoom into this point. This is the 3rd party api we chose to
          use for the project.
        </p>
        <p>
          Below the map looks almost the same, with boxes of properties. The
          only thing different from{" "}
          <strong>http:localhost:3000/properties</strong> directory is that on
          the left down corner there is a delete all and update all button. The
          delete all button calls the{" "}
          <span className="text-white">DELETE </span> method on{" "}
          <strong>/</strong> (not <strong>/</strong> [id]) and take the query
          parameters longitude and latitude to delete all properties with this
          same coordinates. The update all button takes us to the{" "}
          <strong>http:localhost:3000/propUpdateMulti</strong> directory built
          by the <code>propUpdateMulti.js</code> files, in here only one box
          will be available to edit the same parameters in the properties,
          except the changes will be applied to all properties of this
          latititude and longitude. Clicking the button makes an{" "}
          <span className="text-white">PUT</span> api call to <strong>/</strong>{" "}
          and updates corressponding properties to the same value.
        </p>
      </div>
    );
  }

  return (
    <div>
      <Doc />
      <div className="mapPage">
        <div style={{ height: "450px", width: "900px" }}>
          <GoogleMapReact
            bootstrapURLKeys={{
              key: "AIzaSyCYSKHG9B5gM--jR2Csb8tN65-FRY-csbE",
            }}
            defaultCenter={center}
            defaultZoom={10}
          >
            <Marker lat={center.lat} lng={center.lng} />
          </GoogleMapReact>
        </div>
      </div>
      <section>
        <div className="propertyListLog">
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
                </div>
              );
            }
          )}
        </div>

        <div className="logLatBt">
          <div>
            <p>
              <button
                className="btnPropLat"
                id="buttonPropLat"
                onClick={() =>
                  deleteProp(state[0].longitude, state[0].latitude)
                }
              >
                Delete All
              </button>
            </p>
            <p>
              <button
                className="btnPropLat"
                id="buttonPropLat"
                onClick={() => editProp()}
              >
                Update All
              </button>
            </p>
          </div>
        </div>
      </section>
    </div>
  );
}

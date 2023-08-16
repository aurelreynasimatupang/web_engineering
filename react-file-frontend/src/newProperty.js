import React, { useState } from "react";
import { useNavigate } from "react-router";

export default function NewProperty() {

  const navigate = useNavigate();
  const [inputDataPost, setInputDataPost] = useState({
    id: "",
    title: "",
    propertyType: "",
    areaSqm: 1,
    costSqm: 1.5,
    city: "",
    is_active: "true",
    postalCode: "",
    latitude: 1.5,
    longitude: 1.5,
    rent: 100,
    deposit: 0,
    additionalCosts: 0,
    userId: 0,
    user: {
      id: 0,
      displayName: "",
    },

    placeInfo: {
      smoking: "",
      pets: "",
      rentDetails: "",
      roommates: "",
      toilet: "",
      gender: "",
    },

    matchTenant: {
      matchAge: "",
      matchCapacity: "",
      matchGender: "",
      matchLanguages: "",
      matchStatus: "",
    },
  });

  function PostInfo(prop) {
 

    fetch(`http://localhost:3001/${prop.id}`)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        navigate("/")
      });

    fetch(`http://localhost:3001/`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(prop),
    }).then(() => {
      alert("Property Added Successfuly")
      navigate("/")
    });
  }

  return (
    <div className="propertyListNew">
      <div>
        <p>
          Id :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({ ...inputDataPost, id: event.target.value })
            }
          />
        </p>

        <p>
          Title :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({ ...inputDataPost, title: event.target.value })
            }
          />
        </p>

        <p>
          Property type :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                propertyType: event.target.value,
              })
            }
          />
        </p>

        <p>
          AreaSqm :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                areaSqm: event.target.value,
              })
            }
          />
        </p>

        <p>
          costSqm :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                costSqm: event.target.value,
              })
            }
          />
        </p>

        <p>
          City:{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({ ...inputDataPost, city: event.target.value })
            }
          />
        </p>

        <p>
          {" "}
          is_active:{" "}
          <select className="mt-1" name="" id="orderDir"  onChange={ (event) => setInputDataPost({...inputDataPost, is_active: event.target.value})}>
           <option value="true" > true</option>
           <option value="false" > false</option>
                                             
</select>
        </p>

        <p>
          Postalcode :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                postalCode: event.target.value,
              })
            }
          />
        </p>

        <p>
          latitude :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                longitude: event.target.value,
              })
            }
          />
        </p>

        <p>
          longitude :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                latitude: event.target.value,
              })
            }
          />
        </p>

        <p>
          Rent :
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({ ...inputDataPost, rent: event.target.value })
            }
          />
        </p>

        <p>
          deposit :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                deposit: event.target.value,
              })
            }
          />
        </p>

        <p>
          {" "}
          additionalCosts :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                additionalCosts: event.target.value,
              })
            }
          />
        </p>

        <p>
          userId :{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                user: {
                  ...inputDataPost.user,
                  displayName: event.target.value,
                },
              })
            }
          />
        </p>

        <p>
          {" "}
          Id:{" "}
          <input
            type="text"
            id="form1"
            className="form-control"
            aria-label="text"
            onChange={(event) =>
              setInputDataPost({
                ...inputDataPost,
                user: { ...inputDataPost.user, id: event.target.value },
              })
            }
          />
          <p>
            displayName :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  user: {
                    ...inputDataPost.user,
                    displayName: event.target.value,
                  },
                })
              }
            />
          </p>
        </p>

        <p>
          {" "}
          placeInfo
          <p>
            Smoking :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    smoking: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            Pets :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    pets: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            rentalDetails :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    rentDetails: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            roommates :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    roommates: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            toilet :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    toilet: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            gender :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  placeInfo: {
                    ...inputDataPost.placeInfo,
                    gender: event.target.value,
                  },
                })
              }
            />
          </p>
        </p>

        <p>
          {" "}
          matchTenant
          <p>
            matchAge :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  matchTenant: {
                    ...inputDataPost.matchTenant,
                    matchAge: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            matchCapacity :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  matchTenant: {
                    ...inputDataPost.matchTenant,
                    matchCapacity: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            matchGender :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  matchTenant: {
                    ...inputDataPost.matchTenant,
                    matchGender: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            matchLanguages :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  matchTenant: {
                    ...inputDataPost.matchTenant,
                    matchLanguages: event.target.value,
                  },
                })
              }
            />
          </p>
          <p>
            matchStatus :{" "}
            <input
              type="text"
              id="form1"
              className="form-control"
              aria-label="text"
              onChange={(event) =>
                setInputDataPost({
                  ...inputDataPost,
                  matchTenant: {
                    ...inputDataPost.matchTenant,
                    matchStatus: event.target.value,
                  },
                })
              }
            />
          </p>
        </p>

        <p>
          <button
            className="btnPropPost"
            id="buttonProp"
            onClick={() => PostInfo(inputDataPost)}
          >
            Post Data
          </button>
        </p>
      </div>
    </div>
  );
}

import { useLocation } from "react-router-dom";

export default function UsersDisplay() {

  const { state } = useLocation();
  return (

    <div className="userInfo">
      <section>
        <div className="propertyUser">
        {Object.keys(state).length === 0 ? (
          <div>
          <h1>Empty...</h1>
          </div>
      ) : (
       <div>
           <h1 id="titleYellow"> User Information</h1>
          <p> UserId : {state.id} </p>
          <p> UserName : {state.displayName} </p>
          <p> LastLoggedOn : {state.lastLoggedOn} </p>
          <p> MemberSince : {state.memberSince} </p>
       </div>
       
      )}
        </div>
      </section>
    </div>
  );
}

import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import NavBar from "./NavBar.js";
import Home from "./home.js";
import Properties from "./properties.js";
import AboutUs from "./aboutUs.js";
import NotFound from "./notFound.js";
import LogLat from "./logLat";
import { Routes, Route } from "react-router-dom";
import ProperyUpdate from "./propUpdate";
import ProperyUpdateMulti from "./propUpdateMulti";
import PropertiesLogLat from "./propertiesLogLat";
import NewProperty from "./newProperty";
import Users from "./User";
import UsersDisplay from "./usersDisplay";

function App() {
  return (
    <div>
     <NavBar/>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<AboutUs />} />
        <Route path="/properties" element={<Properties />} />
        <Route path="/propUpdate" element={<ProperyUpdate />} />
        <Route path="/propUpdateMulti" element={<ProperyUpdateMulti />} />
        <Route path="/propertiesLogLat" element={<PropertiesLogLat />} />
        <Route path="/logLat" element={<LogLat />} />
        <Route path="/newProperty" element={<NewProperty />} />
        <Route path="/users" element={<Users />} />
        <Route path="/usersDisplay" element={<UsersDisplay />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
   
    </div>
  );
}

export default App;

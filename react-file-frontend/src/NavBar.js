import React from "react";
import {Link} from "react-router-dom";

function Navbar() {
    return (<div className="Home">

<nav className="navbar navbar-expand-lg bg-dark">
        <div className="container">
          
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            </button>
            <div>
            </div>
            <div className="collapse navbar-collapse" id="navbarNav">
                <ul className="ms-auto navbar-nav">
                    <li className="nav-item">
                    <Link to="/" className="nav-link active pr-5">
                    Home
                    </Link>
                    </li>
                    <li className="nav-item"><Link to="/about" className="nav-link active pr-5"> About Us
                    </Link>
                    </li>
                    <li className="nav-item"><Link to="/logLat" className="nav-link active pr-5"> Search By Log/Lat
                    </Link>
                    </li>
                    <li className="nav-item"><Link to="/users" className="nav-link active pr-5"> Search By User
                    </Link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    </div>);
}
export default Navbar;
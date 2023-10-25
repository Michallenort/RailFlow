import { observer } from "mobx-react-lite";
import "bootstrap/dist/css/bootstrap.min.css"
import { Link, NavLink } from "react-router-dom";

export default observer(function NavBar() {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark main-color py-3">
      <div className="container-fluid">
        <span className="navbar-brand">RailFlow</span>
        <div className="collapse navbar-collapse" id="navbarNavDropdown">
          <ul className="navbar-nav">
            <li className="nav-item">
              <NavLink className="nav-link" to="/home">
                Home
              </NavLink>
            </li>
          </ul>
          <ul className="navbar-nav ms-auto">
            <li className="nav-item m-1">
              <Link type="button" className="btn btn-outline-light" to="/signin">
                Sign In
              </Link>
            </li>
            <li className="nav-item m-1">
              <Link type="button" className="btn btn-outline-light" to="/signup">
                Sign Up
              </Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  )
})
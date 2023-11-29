import { observer } from "mobx-react-lite";
import "bootstrap/dist/css/bootstrap.min.css"
import { Link, NavLink } from "react-router-dom";
import { useStore } from "../stores/store";

export default observer(function NavBar() {

  const {userStore : {isAdmin, logout, isLoggedIn}} = useStore();

  return (
    <nav className="navbar navbar-expand-lg navbar-dark main-color py-3">
      <div className="container-fluid">
        <span className="navbar-brand">RailFlow</span>
        <div className="collapse navbar-collapse" id="navbarNavDropdown">
          <ul className="navbar-nav">
            <li className="nav-item">
              <NavLink className="nav-link" to="/">
                Home
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/search">
                Search
              </NavLink>
            </li>
            <li>
              <NavLink className="nav-link" to="/stations">
                Stations
              </NavLink>
            </li>
            {isAdmin &&
            <li className="nav-item">
              <NavLink className="nav-link" to="/supervisor">
                Admin
              </NavLink>
            </li>}
          </ul>
          <ul className="navbar-nav ms-auto">
            {!isLoggedIn ? (
                <>
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
                </>
            ) : (
              <button className='btn btn-outline-light' onClick={logout}>Logout</button>
            )}
          </ul>
        </div>
      </div>
    </nav>
  )
})
import { NavLink } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'; 

const Navbar = () => {
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
      <div className="container">
        <NavLink className="navbar-brand text-primary" to="/">
          Leap Event Technology Manager
        </NavLink>
        <button
          className="navbar-toggler"
          type="button">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto">
            <li className="nav-item">
              <NavLink
                className={({ isActive }) =>
                  `nav-link ${isActive ? 'active text-primary fw-bold' : ''}`}
                to="/events">
                Event Listing
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink
                className={({ isActive }) =>
                  `nav-link ${isActive ? 'active text-primary fw-bold' : ''}`}
                to="/sales-summary">
                Sales Summary
              </NavLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
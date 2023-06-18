import React, { useState } from "react";
import axios from 'axios';
import { useUser } from "../lib/customHooks";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { Button } from "reactstrap";
import { API_ROUTES, APP_ROUTES } from "../utils/constants";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
} from "reactstrap";

const AppNavbar = () => {
  const [isOpen, setIsOpen] = useState(false);
  const { user, authenticated } = useUser();
  console.log("here",useUser(),);
  const toggle = () => setIsOpen(!isOpen);
  let navigate = useNavigate();

  async function handleLogout() {
    try {
        await axios.post(API_ROUTES.LOGOUT);

        // Remove the token from localStorage
        localStorage.removeItem('token');

        // Redirect to login page
        navigate(APP_ROUTES.SIGN_IN);
    } catch (err) {
        console.error('Logout failed:', err);
    }
}

  return (
    <div>
      <Navbar color="dark" dark expand="md">
        <NavbarBrand href="/" aria-label="Home">
          Takvim Uygulaması
        </NavbarBrand>
        <NavbarToggler onClick={toggle} aria-label="Toggle navigation" />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="ml-auto" navbar>
            {authenticated && (
              <NavItem>
                <Link
                  to="/calendar/"
                  className="nav-link"
                  aria-label="Calendar"
                >
                  Takvim
                </Link>
              </NavItem>
            )}

            {!authenticated && (
              <NavItem>
                <Link to="/signin/" className="nav-link" aria-label="Sign In">
                  Oturum Aç
                </Link>
              </NavItem>
            )}

            {authenticated && (
              <NavItem>
                <Button to="/logout/" className="nav-link" aria-label="Log out" onClick={handleLogout}>
                  Çıkış Yap
                </Button>
              </NavItem>
            )}
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
};

export default AppNavbar;

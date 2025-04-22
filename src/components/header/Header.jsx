import React from "react";
import "./header.css";
import { assets } from "../../assets/assets";
import { Link } from "react-router-dom";

const Header = () => {
  return (
    <>
      <header>
        <div className="nav-group left">
          <a href="#hero" className="logo">
            <img src={assets.logo} />
          </a>
          <a href="Login.html" className="nav-link">
            Black Sun Member
          </a>
        </div>

        <div className="nav-group center">
          <a href="#history" className="nav-link">
            History
          </a>
          <a href="#rooms" className="nav-link">
            Rooms
          </a>
          <a href="#services" className="nav-link">
            Services
          </a>
        </div>

        <div className="nav-group right">
          <Link className="nav-link" to={"/login"}>
            Login
          </Link>
          <Link className="nav-link" to={"/signup"}>
            Sign up
          </Link>
        </div>
      </header>
    </>
  );
};

export default Header;

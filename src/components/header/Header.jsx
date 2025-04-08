import React from "react";
import "./header.css";
import { assets } from "../../assets/assets";

const Header = () => {
  return (
    <>
      <header>
        <div class="nav-group left">
          <a href="test2.html" class="logo">
            <img src={assets.logo} />
          </a>
          <a href="Login.html" class="nav-link">
            Black Sun Member
          </a>
        </div>

        <div class="nav-group center">
          <a href="#history" class="nav-link">
            History
          </a>
          <a href="#rooms" class="nav-link">
            Rooms
          </a>
          <a href="#services" class="nav-link">
            Services
          </a>
        </div>

        <div class="nav-group right">
          <a href="Login.html" class="nav-link">
            Login
          </a>
          <a href="SignUp.html" class="nav-link">
            Sign up
          </a>
        </div>
      </header>
    </>
  );
};

export default Header;

import React from "react";
import "./signUp.css";
import { useNavigate } from "react-router-dom";

const SignUp = () => {
  const navigate = useNavigate();
  return (
    <div className="signup-wrapper">
      <div className="signup-container">
        <h1>Sign up</h1>

        <div className="input-box">
          <input type="text" placeholder="Username" required />
          <i className="bx bxs-user"></i>
        </div>

        <div className="input-box">
          <input type="email" placeholder="Email" required />
          <i className="bx bxs-envelope"></i>
        </div>

        <div className="input-box">
          <input type="password" placeholder="Password" required />
          <i className="bx bxs-lock-alt"></i>
        </div>

        <button type="submit" className="btn">
          Sign up
        </button>

        <div className="login-link">
          <p>
            Already have an account?{" "}
            <a onClick={() => navigate("/login")}>Login</a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default SignUp;

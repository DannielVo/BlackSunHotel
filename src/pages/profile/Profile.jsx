import React from "react";
import "./profile.css";
import { useNavigate } from "react-router-dom";

const Profile = () => {
  const navigate = useNavigate();
  return (
    <div class="user-profile-container">
      <div class="user-avatar">
        <i class="bx bx-user"></i>
      </div>
      <div class="user-info">
        <h2>Your Information</h2>
        <form>
          <div class="profile-form-group">
            <label for="fullname">Full Name</label>
            <input
              type="text"
              id="fullname"
              name="fullname"
              placeholder="Enter your full name"
            />
          </div>
          <div class="profile-form-group">
            <label for="email">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              placeholder="Enter your email"
            />
          </div>

          <div class="profile-form-group">
            <label for="phone">Phone Number</label>
            <input
              type="tel"
              id="phone"
              name="phone"
              placeholder="Enter your phone number"
            />
          </div>

          <div class="profile-btn">
            <button
              type="reset"
              class="cancel-btn"
              onClick={() => navigate("/")}
            >
              Cancel
            </button>
            <button type="submit" class="update-btn">
              Update
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Profile;

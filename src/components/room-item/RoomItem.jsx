import React from "react";
import "./roomItem.css";
import { assets } from "../../assets/assets";

const RoomItem = () => {
  return (
    <>
      <div class="room-card">
        <div class="room-image">
          <img src={assets.bed1} alt="Economy Room" />
        </div>
        <div class="room-content">
          <h3 class="card-title">Economy Room</h3>
          <p class="room-type">
            CITY VIEW | DOUBLE BED OR TWO SINGLE BEDS | 41 M²
          </p>
          <p class="room-description">
            Comfortable and spacious room with views over the city featuring a
            double bed or 2 single beds, LCD TV, minibar, safe, and a range of
            quality amenities and excellent services.
          </p>
          <button class="book-now-btn">Book now</button>
        </div>
      </div>
    </>
  );
};

export default RoomItem;

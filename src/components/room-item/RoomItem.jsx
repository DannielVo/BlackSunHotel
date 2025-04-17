import React from "react";
import "./roomItem.css";
import { assets } from "../../assets/assets";

const RoomItem = () => {
  return (
    <>
      <div className="room-card">
        <div className="room-image">
          <img src={assets.bed1} alt="Economy Room" />
        </div>
        <div className="room-content">
          <h3 className="card-title">Economy Room</h3>
          <p className="room-type">
            CITY VIEW | DOUBLE BED OR TWO SINGLE BEDS | 41 M²
          </p>
          <p className="room-description">
            Comfortable and spacious room with views over the city featuring a
            double bed or 2 single beds, LCD TV, minibar, safe, and a range of
            quality amenities and excellent services.
          </p>
          <button className="book-now-btn">Book now</button>
        </div>
      </div>
    </>
  );
};

export default RoomItem;

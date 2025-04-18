import React from "react";
import "./roomItem.css";
import { assets } from "../../assets/assets";

const RoomItem = ({ title, type, description, img }) => {
  return (
    <>
      <div className="room-card">
        <div className="room-image">
          <img src={img} alt={title} />
        </div>
        <div className="room-content">
          <h3 className="card-title">{title}</h3>
          <p className="room-type">{type}</p>
          <p className="room-description">{description}</p>
          <button className="book-now-btn">Book now</button>
        </div>
      </div>
    </>
  );
};

export default RoomItem;

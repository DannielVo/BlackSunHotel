import React from "react";
import "./rooms.css";
import RoomItem from "../../../components/room-item/RoomItem";

const Rooms = () => {
  return (
    <>
      {" "}
      <section id="rooms" className="rooms-section white-bg">
        <div className="container">
          <h2 className="room-title">Accommodation</h2>

          <div className="room-cards">
            <RoomItem></RoomItem>
            <RoomItem></RoomItem>
            <RoomItem></RoomItem>
          </div>
        </div>
      </section>
    </>
  );
};

export default Rooms;

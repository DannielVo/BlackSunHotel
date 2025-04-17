import React from "react";
import "./rooms.css";
import RoomItem from "../../../components/room-item/RoomItem";

const Rooms = () => {
  return (
    <>
      {" "}
      <section id="rooms" class="rooms-section white-bg">
        <div class="container">
          <h2 class="room-title">Accommodation</h2>

          <div class="room-cards">
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

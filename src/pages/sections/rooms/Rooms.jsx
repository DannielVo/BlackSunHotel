import React from "react";
import "./rooms.css";
import RoomItem from "../../../components/room-item/RoomItem";
import { ROOM_LIST } from "../../../assets/assets";

const Rooms = () => {
  return (
    <>
      {" "}
      <section id="rooms" className="rooms-section white-bg">
        <div className="container">
          <h2 className="room-title">Accommodation</h2>

          <div className="room-cards">
            {ROOM_LIST.length === 0
              ? ""
              : ROOM_LIST.map((item, index) => (
                  <RoomItem
                    key={`room ${index}`}
                    title={item.title}
                    type={item.type}
                    description={item.description}
                    img={item.img}
                  ></RoomItem>
                ))}
          </div>
        </div>
      </section>
    </>
  );
};

export default Rooms;

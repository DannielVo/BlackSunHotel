import React, { useRef, useEffect } from "react";
import flatpickr from "flatpickr";
import "flatpickr/dist/flatpickr.min.css";
import "./bookingbar.css";

const Bookingbar = () => {
  const checkinRef = useRef(null);

  useEffect(() => {
    if (checkinRef.current) {
      flatpickr(checkinRef.current, {
        mode: "range",
        dateFormat: "d/m/Y",
        minDate: "today",
      });
    }
  }, []);
  return (
    <>
      <section className="booking-bar" id="booking-bar">
        {/*  Check in / Check out */}
        <div className="booking-item">
          <div className="date-wrapper" id="date-wrapper">
            <label htmlFor="checkin">Check-in / Check-out</label>
            <div className="date-input-wrapper">
              <input
                ref={checkinRef}
                id="checkin"
                className="date-input flatpickr-input"
                placeholder="Select dates"
              />
              <i className="bx bx-chevron-down"></i>
            </div>
          </div>
        </div>

        {/* <!-- Rooms --> */}
        <div className="booking-item vertical-center">
          <label>ROOMS</label>
          <div className="counter">
            <button onClick="changeValue('adults', -1)">-</button>
            <span id="rooms-quantity">1</span>
            <button onClick="changeValue('adults', 1)">+</button>
          </div>
        </div>

        {/* Adults */}
        <div className="booking-item vertical-center">
          <label>ADULTS</label>
          <div className="counter">
            <button onClick="changeValue('adults', -1)">-</button>
            <span id="adults">1</span>
            <button onClick="changeValue('adults', 1)">+</button>
          </div>
        </div>

        {/*  Children  */}
        <div className="booking-item vertical-center">
          <label>CHILDREN</label>
          <div className="counter">
            <button onClick="changeValue('children', -1)">-</button>
            <span id="children">1</span>
            <button onClick="changeValue('children', 1)">+</button>
          </div>
        </div>

        <button className="check-btn">CHECK AVAILABILITY</button>
      </section>
    </>
  );
};

export default Bookingbar;

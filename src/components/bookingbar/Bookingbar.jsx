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
      <section class="booking-bar" id="booking-bar">
        {/*  Check in / Check out */}
        <div class="booking-item">
          <div class="date-wrapper" id="date-wrapper">
            <label for="checkin">Check-in / Check-out</label>
            <div class="date-input-wrapper">
              <input
                ref={checkinRef}
                id="checkin"
                class="date-input flatpickr-input"
                placeholder="Select dates"
              />
              <i class="bx bx-chevron-down"></i>
            </div>
          </div>
        </div>

        {/* <!-- Rooms --> */}
        <div class="booking-item vertical-center">
          <label>ROOMS</label>
          <div class="counter">
            <button onclick="changeValue('adults', -1)">-</button>
            <span id="rooms-quantity">1</span>
            <button onclick="changeValue('adults', 1)">+</button>
          </div>
        </div>

        {/* Adults */}
        <div class="booking-item vertical-center">
          <label>ADULTS</label>
          <div class="counter">
            <button onclick="changeValue('adults', -1)">-</button>
            <span id="adults">1</span>
            <button onclick="changeValue('adults', 1)">+</button>
          </div>
        </div>

        {/*  Children  */}
        <div class="booking-item vertical-center">
          <label>CHILDREN</label>
          <div class="counter">
            <button onclick="changeValue('children', -1)">-</button>
            <span id="children">1</span>
            <button onclick="changeValue('children', 1)">+</button>
          </div>
        </div>

        <button class="check-btn">CHECK AVAILABILITY</button>
      </section>
    </>
  );
};

export default Bookingbar;

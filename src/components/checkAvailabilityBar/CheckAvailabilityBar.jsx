import React, { useEffect, useRef, useState } from "react";
import "./checkAvailabilityBar.css";
import flatpickr from "flatpickr";
import { useHotel } from "../../context/HotelContext";

const CheckAvailabilityBar = () => {
  const checkinRef = useRef(null);
  const guestDropdownRef = useRef(null);
  const guestToggleRef = useRef(null);
  const {
    roomsBarCount,
    setRoomsBarCount,
    adultsCount,
    setAdultsCount,
    childrenCount,
    setChildrenCount,
    setCheckinDate,
    setCheckoutDate,
  } = useHotel();

  const [isGuestDropdownOpen, setIsGuestDropdownOpen] = useState(false);

  const toggleGuestDropdown = (e) => {
    e.stopPropagation();
    setIsGuestDropdownOpen((prev) => !prev);
  };

  const handleClickOutside = (e) => {
    if (
      guestDropdownRef.current &&
      !guestDropdownRef.current.contains(e.target)
    ) {
      setIsGuestDropdownOpen(false);
    }
  };

  const handleAdjust = (type, isPlus) => {
    if (type === "room") {
      setRoomsBarCount((prev) => Math.max(1, prev + (isPlus ? 1 : -1)));
    } else if (type === "adult") {
      setAdultsCount((prev) => Math.max(1, prev + (isPlus ? 1 : -1)));
    } else if (type === "child") {
      setChildrenCount((prev) => Math.max(0, prev + (isPlus ? 1 : -1)));
    }
  };

  useEffect(() => {
    if (checkinRef.current) {
      flatpickr(checkinRef.current, {
        mode: "range",
        dateFormat: "d/m/Y",
        minDate: "today",
        onChange: function (selectedDates) {
          if (selectedDates.length === 2) {
            setCheckinDate(selectedDates[0]);
            setCheckoutDate(selectedDates[1]);
          } else if (selectedDates.length === 1) {
            setCheckinDate(selectedDates[0]);
            setCheckoutDate(null);
          } else {
            setCheckinDate(null);
            setCheckoutDate(null);
          }
        },
      });
    }
  }, []);

  useEffect(() => {
    document.addEventListener("click", handleClickOutside);
    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, []);

  return (
    <>
      {/* <!-- BOOKINGBAR --> */}
      <section className="setting-booking-bar">
        {/* <!-- Check in / Check out --> */}
        <div className="setting-booking-item">
          <div className="setting-date-wrapper">
            <div className="setting-date-input-wrapper">
              <i className="bx bx-calendar"></i>
              <input
                id="checkin"
                ref={checkinRef}
                className="setting-date-input"
                placeholder="Select dates"
              />
              <i className="bx bx-chevron-down"></i>
            </div>
          </div>
        </div>

        {/* <!-- Quantity of customers --> */}
        <div className="setting-booking-item">
          <div
            className="setting-guest-wrapper"
            id="guestToggle"
            ref={guestToggleRef}
            onClick={toggleGuestDropdown}
          >
            <i className="bx bx-user"></i>
            <input
              id="guests"
              className="setting-guests-input"
              placeholder="1 Room, 1 Guest"
              readOnly
            />
            <i className="bx bx-chevron-down"></i>
          </div>
        </div>

        {/* <!-- Btn CHECK AVAILABILITY --> */}
        <button className="setting-check-btn">CHECK AVAILABILITY</button>

        {isGuestDropdownOpen && (
          <div
            className="setting-guest-dropdown"
            id="guestDropdown"
            ref={guestDropdownRef}
          >
            <div className="setting-guest-item">
              <span>Rooms</span>
              <div className="control">
                <button
                  className="minus"
                  data-type="room"
                  onClick={() => handleAdjust("room", false)}
                >
                  -
                </button>
                <span>{roomsBarCount}</span>
                <button
                  className="plus"
                  data-type="room"
                  onClick={() => handleAdjust("room", true)}
                >
                  +
                </button>
              </div>
            </div>

            <div className="setting-guest-item">
              <span>Adults</span>
              <div className="control">
                <button
                  className="minus"
                  data-type="adult"
                  onClick={() => handleAdjust("adult", false)}
                >
                  -
                </button>
                <span id="adult-count">{adultsCount}</span>
                <button
                  className="plus"
                  data-type="adult"
                  onClick={() => handleAdjust("adult", true)}
                >
                  +
                </button>
              </div>
            </div>

            <div className="setting-guest-item">
              <span>
                Children <small>(0-11)</small>
              </span>
              <div className="control">
                <button
                  className="minus"
                  data-type="child"
                  onClick={() => handleAdjust("child", false)}
                >
                  -
                </button>
                <span id="child-count">{childrenCount}</span>
                <button
                  className="plus"
                  data-type="child"
                  onClick={() => handleAdjust("child", true)}
                >
                  +
                </button>
              </div>
            </div>
          </div>
        )}
      </section>
    </>
  );
};

export default CheckAvailabilityBar;

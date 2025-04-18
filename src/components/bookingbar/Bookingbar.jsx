import React, { useRef, useEffect, useState } from "react";
import flatpickr from "flatpickr";
import "flatpickr/dist/flatpickr.min.css";
import "./bookingbar.css";

const Bookingbar = () => {
  const bookingBarRef = useRef(null);
  const [rooms, setRooms] = useState(1);
  const [adults, setAdults] = useState(1);
  const [children, setChildren] = useState(0);
  const [isManualShow, setIsManualShow] = useState(false);
  let lastScrollTop = useRef(window.scrollY);
  let timeout = useRef(null);
  const checkinRef = useRef(null);

  const changeValue = (type, delta) => {
    switch (type) {
      case "rooms":
        setRooms((prev) => Math.max(1, prev + delta));
        break;
      case "adults":
        setAdults((prev) => Math.max(1, prev + delta));
        break;
      case "children":
        setChildren((prev) => Math.max(0, prev + delta));
        break;
      default:
        break;
    }
  };

  useEffect(() => {
    const handleScroll = () => {
      if (isManualShow) return;

      const bookingBar = bookingBarRef.current;
      const currentScroll = window.scrollY;
      const nearBottom =
        window.innerHeight + currentScroll >= document.body.offsetHeight - 100;

      clearTimeout(timeout.current);

      if (currentScroll > lastScrollTop.current || nearBottom) {
        bookingBar.classList.add("hidden");
        bookingBar.classList.remove("show");
      } else {
        timeout.current = setTimeout(() => {
          bookingBar.classList.remove("hidden");
        }, 30);
      }

      lastScrollTop.current = currentScroll;
    };

    const handleClickOutside = (e) => {
      if (bookingBarRef.current && !bookingBarRef.current.contains(e.target)) {
        setIsManualShow(false);
        bookingBarRef.current.classList.remove("show");
      }
    };

    window.addEventListener("scroll", handleScroll);
    document.addEventListener("click", handleClickOutside);
    return () => {
      window.removeEventListener("scroll", handleScroll);
      document.removeEventListener("click", handleClickOutside);
    };
  }, [isManualShow]);

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
      <section className="booking-bar" id="booking-bar" ref={bookingBarRef}>
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

        {/* Rooms */}
        <div className="booking-item vertical-center">
          <label>ROOMS</label>
          <div className="counter">
            <button onClick={() => changeValue("rooms", -1)}>-</button>
            <span>{rooms}</span>
            <button onClick={() => changeValue("rooms", 1)}>+</button>
          </div>
        </div>

        {/* Adults */}
        <div className="booking-item vertical-center">
          <label>ADULTS</label>
          <div className="counter">
            <button onClick={() => changeValue("adults", -1)}>-</button>
            <span>{adults}</span>
            <button onClick={() => changeValue("adults", 1)}>+</button>
          </div>
        </div>

        {/* Children */}
        <div className="booking-item vertical-center">
          <label>CHILDREN</label>
          <div className="counter">
            <button onClick={() => changeValue("children", -1)}>-</button>
            <span>{children}</span>
            <button onClick={() => changeValue("children", 1)}>+</button>
          </div>
        </div>

        <button className="check-btn">CHECK AVAILABILITY</button>
      </section>
    </>
  );
};

export default Bookingbar;

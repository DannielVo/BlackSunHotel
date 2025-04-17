import React from "react";
import "./hotelServices.css";

const HotelServices = () => {
  return (
    <>
      {" "}
      <section id="services" className="services-section white-bg">
        <div className="container">
          <h2>Services and Facilities</h2>
          <p className="services-desc">
            Discover the wide range of services and facilities that ensure a
            most pleasant and comfortable stay at our hotel.
          </p>

          <div className="services-list">
            <div className="service-item">
              <i className="bx bxs-bowl-hot"></i>
              <span>Cuisine</span>
            </div>

            <div className="service-item">
              <i className="bx bx-closet"></i>
              <span>Cleaning services</span>
            </div>

            <div className="service-item">
              <i className="bx bx-swim"></i>
              <span>Outdoor swimming pool</span>
            </div>

            <div className="service-item">
              <i className="bx bx-spa"></i>
              <span>Spa treatment</span>
            </div>

            <div className="service-item">
              <i className="bx bx-child"></i>
              <span>Kids club</span>
            </div>
            <div className="service-item">
              <i className="bx bx-dumbbell"></i>
              <span>Fitness center</span>
            </div>
            <div className="service-item">
              <i className="bx bxs-bus"></i>
              <span>Airport hotel transfer</span>
            </div>

            <div className="service-item">
              <i className="bx bxs-parking"></i>
              <span>Parking</span>
            </div>

            <div className="learn-more">
              <a href="services-detail.html" target="_blank">
                Learn more
              </a>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default HotelServices;

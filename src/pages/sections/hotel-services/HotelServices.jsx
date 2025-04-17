import React from "react";
import "./hotelServices.css";

const HotelServices = () => {
  return (
    <>
      {" "}
      <section id="services" class="services-section white-bg">
        <div class="container">
          <h2>Services and Facilities</h2>
          <p class="services-desc">
            Discover the wide range of services and facilities that ensure a
            most pleasant and comfortable stay at our hotel.
          </p>

          <div class="services-list">
            <div class="service-item">
              <i class="bx bxs-bowl-hot"></i>
              <span>Cuisine</span>
            </div>

            <div class="service-item">
              <i class="bx bx-closet"></i>
              <span>Cleaning services</span>
            </div>

            <div class="service-item">
              <i class="bx bx-swim"></i>
              <span>Outdoor swimming pool</span>
            </div>

            <div class="service-item">
              <i class="bx bx-spa"></i>
              <span>Spa treatment</span>
            </div>

            <div class="service-item">
              <i class="bx bx-child"></i>
              <span>Kids club</span>
            </div>
            <div class="service-item">
              <i class="bx bx-dumbbell"></i>
              <span>Fitness center</span>
            </div>
            <div class="service-item">
              <i class="bx bxs-bus"></i>
              <span>Airport hotel transfer</span>
            </div>

            <div class="service-item">
              <i class="bx bxs-parking"></i>
              <span>Parking</span>
            </div>

            <div class="learn-more">
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

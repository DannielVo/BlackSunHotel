import React from "react";

import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation } from "swiper/modules";

import "swiper/css";
import "swiper/css/navigation";
import { assets } from "../../../assets/assets";
import "./slider.css";

const Slider = () => {
  return (
    <>
      <section className="slider-section">
        <Swiper
          modules={[Navigation]}
          spaceBetween={30}
          loop={true}
          centeredSlides={true}
          navigation={{
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
          }}
          className="slider"
        >
          <div className="slider-container">
            <SwiperSlide className="slide">
              <img src={assets.slide1} alt="Image 1" />
            </SwiperSlide>
            <SwiperSlide className="slide">
              <img src={assets.slide2} alt="Image 2" />
            </SwiperSlide>
            <SwiperSlide className="slide">
              <img src={assets.slide3} alt="Image 3" />
            </SwiperSlide>
            <SwiperSlide className="slide">
              <img src={assets.slide4} alt="Image 4" />
            </SwiperSlide>
          </div>
        </Swiper>

        <div className="swiper-button-prev"></div>
        <div className="swiper-button-next"></div>
      </section>
    </>
  );
};

export default Slider;

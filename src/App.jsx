import React from "react";
import Header from "./components/header/Header";
import Hero from "./components/hero/Hero";
import Bookingbar from "./components/bookingbar/Bookingbar";
import History from "./pages/sections/history/History";
import Rooms from "./pages/sections/rooms/Rooms";
import Slider from "./pages/sections/slider/Slider";
import HotelServices from "./pages/sections/hotel-services/HotelServices";
import HotelLocation from "./pages/sections/hotel-location/HotelLocation";
import Footer from "./components/footer/Footer";

const App = () => {
  return (
    <div>
      <Header></Header>
      <main>
        <Hero></Hero>
        <Bookingbar></Bookingbar>
        <History></History>
        <Rooms></Rooms>
        <Slider></Slider>
        <HotelServices></HotelServices>
        <HotelLocation></HotelLocation>
      </main>
      <Footer></Footer>
    </div>
  );
};

export default App;

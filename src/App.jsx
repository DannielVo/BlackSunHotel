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
import { Route, Router, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import ServicesDetails from "./pages/servicesDetails/ServicesDetails";
import BlankPage from "./pages/BlankPAge";

const App = () => {
  const plainPages = [
    {
      path: "/services-details",
      component: <ServicesDetails></ServicesDetails>,
    },
  ];
  return (
    <Routes>
      <Route
        path="/"
        element={
          <HomePage>
            <Hero></Hero>
            <Bookingbar></Bookingbar>
            <History></History>
            <Rooms></Rooms>
            <Slider></Slider>
            <HotelServices></HotelServices>
            <HotelLocation></HotelLocation>
          </HomePage>
        }
      ></Route>
      {plainPages.map(({ path, component }) => (
        <Route
          key={path}
          path={path}
          element={<BlankPage>{component}</BlankPage>}
        ></Route>
      ))}
    </Routes>
    // <div>
    //   <Header></Header>
    //   <main>
    //     <Hero></Hero>
    //     <Bookingbar></Bookingbar>
    //     <History></History>
    //     <Rooms></Rooms>
    //     <Slider></Slider>
    //     <HotelServices></HotelServices>
    //     <HotelLocation></HotelLocation>
    //   </main>
    //   <Footer></Footer>
    // </div>
  );
};

export default App;

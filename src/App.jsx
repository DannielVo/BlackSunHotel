import React from "react";
import Header from "./components/header/Header";
import Hero from "./components/hero/Hero";
import Bookingbar from "./components/bookingbar/Bookingbar";

const App = () => {
  return (
    <div>
      <Header></Header>
      <main>
        <Hero></Hero>
        <Bookingbar></Bookingbar>
      </main>
    </div>
  );
};

export default App;

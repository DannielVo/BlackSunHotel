import React from "react";
import Header from "./components/header/Header";
import Hero from "./components/hero/Hero";
import Bookingbar from "./components/bookingbar/Bookingbar";
import History from "./pages/sections/history/History";

const App = () => {
  return (
    <div>
      <Header></Header>
      <main>
        <Hero></Hero>
        <Bookingbar></Bookingbar>
        <History></History>
      </main>
    </div>
  );
};

export default App;

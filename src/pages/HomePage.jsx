import React from "react";
import Header from "../components/header/Header";
import Footer from "../components/footer/Footer";

const HomePage = ({ children }) => {
  return (
    <>
      <Header></Header>
      <main>{children}</main>
      <Footer></Footer>
    </>
  );
};

export default HomePage;

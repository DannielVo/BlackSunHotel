import logo from "./Logo_2.png";
import bed1 from "./Bed1.jpg";
import bed2 from "./Bed2.jpg";
import bed3 from "./Bed3.avif";
import slide1 from "./Slide1.jpg";
import slide2 from "./Slide2.jpg";
import slide3 from "./Slide3.jpg";
import slide4 from "./Slide4.avif";

export const assets = {
  logo,
  bed1,
  slide1,
  slide2,
  slide3,
  slide4,
};

export const ROOM_LIST = [
  {
    id: 0,
    title: "Economy Room",
    type: "CITY VIEW | DOUBLE BED OR TWO SINGLE BEDS | 41 M²",
    description:
      "Comfortable and spacious room with views over the city featuring a double bed or 2 single beds, LCD TV, minibar, safe, and a range of quality amenities and excellent services.",
    img: bed1,
  },
  {
    id: 1,
    title: "Deluxe Room",
    type: "RIVER VIEW | DOUBLE BED OR TWO SINGLE BEDS | 41 M²",
    description:
      "Step into luxury and enjoy the views of the river from your room. Perfect for families or business travellers who appreciate space and luxurious design, offering a spacious lounge with a comfortable sofa and a separate bedroom.",
    img: bed2,
  },
  {
    id: 2,
    title: "Premium Room",
    type: "CITY VIEW | KING-SIZE BED | 55 M²",
    description:
      "Our most lavish rooms offer a spacious king-size bed and large windows with city views. The separate lounge has a sofa and a dining table for up to 4 people. Enjoy magnificent city views from all of the spaces in this suite.",
    img: bed3,
  },
];

export const SERVICES_LIST = [
  {
    id: 0,
    icon: "bx bxs-bowl-hot",
    serviceName: "Cuisine",
  },
  {
    id: 1,
    icon: "bx bx-closet",
    serviceName: "Cleaning services",
  },
  {
    id: 2,
    icon: "bx bx-swim",
    serviceName: "Outdoor swimming pool",
  },
  {
    id: 3,
    icon: "bx bx-spa",
    serviceName: "Spa treatment",
  },
  {
    id: 4,
    icon: "bx bx-child",
    serviceName: "Kids club",
  },
  {
    id: 5,
    icon: "bx bx-dumbbell",
    serviceName: "Fitness center",
  },
  {
    id: 6,
    icon: "bx bxs-bus",
    serviceName: "Airport hotel transfer",
  },
  {
    id: 7,
    icon: "bx bxs-parking",
    serviceName: "Parking",
  },
];

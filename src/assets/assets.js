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

export const SLIDER_IMG = [slide1, slide2, slide3, slide4];

export const ROOM_LIST = [
  {
    id: 0,
    title: "Economy Room",
    type: "CITY VIEW | DOUBLE BED | 41 M²",
    description:
      "Comfortable and spacious room with views over the city featuring a double bed or 2 single beds, LCD TV, minibar, safe, and a range of quality amenities and excellent services.",
    img: bed1,
  },
  {
    id: 1,
    title: "Deluxe Room",
    type: "RIVER VIEW | DOUBLE BED | 45 M²",
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

export const SERVICES_DETAILS = [
  {
    icon: "bx bxs-bowl-hot",
    title: "Cuisine",
    items: ["Restaurant", "Pool bar", "Room service"],
  },
  {
    icon: "bx bx-closet",
    title: "Cleaning services",
    items: ["Daily housekeeping"],
  },
  {
    icon: "bx bx-swim",
    title: "Swimming pool",
    items: ["Outdoor swimming pool", "Indoor swimming pool"],
  },
  {
    icon: "bx bx-spa",
    title: "Beauty and wellness",
    items: ["Spa treatment"],
  },
  {
    icon: "bx bx-dumbbell",
    title: "Sport activities",
    items: ["Fitness center", "Gym"],
  },
  {
    icon: "bx bx-child",
    title: "Family and children",
    items: ["Kids club"],
  },
  {
    icon: "bx bxs-bus",
    title: "Transportation",
    items: ["Airport hotel transfer"],
  },
  {
    icon: "bx bx-handicap",
    title: "Accessibility",
    items: ["Ramp at entrance", "Lift"],
  },
  {
    icon: "bx bxs-cat",
    title: "Pets",
    items: ["Pets not allowed"],
  },

  {
    icon: "bx bx-wifi-2",
    title: "Internet",
    items: ["Free wi-fi"],
  },
  {
    icon: "bx bx-help-circle",
    title: "Reception services",
    items: [
      "24 hours reception",
      "Luggage storage service",
      "Early check in available",
      "Late check out available",
    ],
  },
  {
    icon: "bx bx-shield-x",
    title: "Safety security",
    items: [
      "24 hours security",
      "CCTV/Cameras in common areas",
      "Smoke alarms",
      "Fire extinguisher",
    ],
  },
];

export const POLICY = [
  {
    name: "Information We Collect",
    items: [
      `We may collect personal information when you make a reservation,
                check in, or use our services. This may include your name,
                address, email, phone number, payment details, and other
                information you voluntarily provide.`,
      ` When you visit our website, we may collect technical data such
                as your IP address, browser type, operating system, referring
                URLs, and usage patterns. This information helps us analyze
                trends and improve the performance of our services.`,
    ],
  },

  {
    name: "How We Use Your Information",
    items: [
      `Your information is used to process bookings, personalize your
                experience, respond to your requests, and provide updates about
                our hotel and amenities.`,
      ` With your consent, we may send you promotional offers,
                newsletters, or event updates. You can opt out at any time via
                the unsubscribe link or by contacting us.`,
      `We may use or disclose your information to comply with legal
                obligations or protect the rights, property, or safety of Black
                Sun, our guests, or others.`,
    ],
  },

  {
    name: "Data Security",
    items: [
      ` We apply appropriate technical and organizational measures to
                safeguard your personal data. However, please note that no
                internet-based system can be guaranteed 100% secure.`,
    ],
  },

  {
    name: "Your Choices",
    items: [
      `You may request to access, update, or correct your personal
                information by contacting us.`,
      `You can unsubscribe from promotional communications at any time
                via the provided links or by reaching out to us.`,
    ],
  },

  {
    name: "Children’s Privacy",
    items: [
      `Our services are not intended for children under 16. We do not
                knowingly collect data from anyone under this age. If you
                believe a child has provided us with personal information,
                please contact us immediately.`,
    ],
  },
];

export const BOOKING_CANCEL = [
  {
    name: "Check-in & Check-out",
    items: [
      "Check-in time: from 13:30",
      "Check-out time: before 12:00",
      `Early check-in and late check-out are subject to availability.
                Please contact the reception directly to arrange.`,
    ],
  },
  {
    name: "Children & Extra Beds",
    items: [
      `Children aged 5 to 11 using existing bedding: 200,000 VND per
              child per night`,
      {
        name: `Children aged 5 to 11 sharing a room with parents require an extra
              bed, charged at:`,
        items: [
          `710,000 VND per night (Sunday to Friday)`,
          `1,150,000 VND per night (Saturday and Holidays)`,
        ],
      },
      {
        name: `Third person aged 12 and above (includes breakfast & extra bed):`,
        items: [
          `710,000 VND per night (Sunday to Friday)`,
          `1,150,000 VND per night (Saturday and Holidays)`,
        ],
      },
    ],
  },
  {
    name: "Room Rates",
    items: [`All prices include VAT and a 5% service charge.`],
  },
  {
    name: "Cancellation Policy",
    items: [
      `Cancellation and prepayment policies vary depending on the room
                type and rate plan. Please check the specific conditions when
                selecting your accommodation.`,
    ],
  },
];

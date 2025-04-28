import { createContext, useContext, useState } from "react";

const HotelContext = createContext();
export function HotelContextProvider({ children }) {
  const [roomsBarCount, setRoomsBarCount] = useState(0);
  const [adultsCount, setAdultsCount] = useState(0);
  const [childrenCount, setChildrenCount] = useState(0);
  const [checkinDate, setCheckinDate] = useState(null);
  const [checkoutDate, setCheckoutDate] = useState(null);

  const [totalPrice, setTotalPrice] = useState(0);
  const [roomCounts, setRoomCounts] = useState({
    economy: 0,
    deluxe: 0,
    premium: 0,
  });
  const [extraBedCount, setExtraBedCount] = useState(0);

  const roomPrices = {
    economy: 1800000,
    deluxe: 2500000,
    premium: 3000000,
    extraBed: 500000,
  };

  const updateTotalPrice = () => {
    let total = 0;

    Object.keys(roomCounts).forEach((roomType) => {
      const count = roomCounts[roomType];
      if (roomPrices[roomType]) {
        total += count * roomPrices[roomType];
      }
    });

    total += extraBedCount * roomPrices.extraBed;

    setTotalPrice(total);
  };

  const value = {
    roomsBarCount,
    setRoomsBarCount,
    adultsCount,
    setAdultsCount,
    childrenCount,
    setChildrenCount,
    checkinDate,
    setCheckinDate,
    checkoutDate,
    setCheckoutDate,
    totalPrice,
    setTotalPrice,
    roomPrices,
    updateTotalPrice,
    roomCounts,
    setRoomCounts,
    extraBedCount,
    setExtraBedCount,
  };

  return (
    <HotelContext.Provider value={value}>{children}</HotelContext.Provider>
  );
}

export function useHotel() {
  return useContext(HotelContext);
}

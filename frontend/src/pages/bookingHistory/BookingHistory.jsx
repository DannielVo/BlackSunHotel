import React, { useState } from "react";
import "./bookingHistory.css";
import { useHotel } from "../../context/HotelContext";
import { EXTRA_BED, ROOM_LIST } from "../../assets/assets";

const BookingHistory = () => {
  const {
    totalPrice,
    roomCounts,
    extraBedCount,
    checkinDate,
    checkoutDate,
    adultsCount,
    childrenCount,
  } = useHotel();

  const totalRoomsSelected = Object.values(roomCounts).reduce(
    (sum, count) => sum + count,
    0
  );

  const [selectedOption, setSelectedOption] = useState("");

  const handleSelectChange = (event) => {
    setSelectedOption(event.target.value);
  };

  return (
    <>
      {/* <!-- Titlebar --> */}
      <section className="bhistory-title-bar">Bookings History</section>

      {/* <!-- Your Bookings --> */}
      <section className="bhistory-wrapper">
        <div className="bhistory-room-options">
          <div className="bhistory-room-option">
            <div className="bhistory-option-title">
              {/* 03/04/2024 - 07/04/2024 */}

              <select
                id="myDropdown"
                value={selectedOption}
                onChange={handleSelectChange}
              >
                <option value="">
                  -- Please select previous booking date --
                </option>
                <option value="apple">03/04/2024 - 07/04/2024</option>
                <option value="banana">03/05/2024 - 07/05/2024</option>
                <option value="orange">03/06/2024 - 07/06/2024</option>
              </select>
            </div>

            <div className="bhistory-room-grid">
              {ROOM_LIST.length === 0
                ? ""
                : ROOM_LIST.map((roomItem, index) => (
                    <div key={`bhistory-roomItem ${index}`}>
                      <div className="bhistory-room-card">
                        <div className="bhistory-room-img">
                          <img src={roomItem.img} alt={roomItem.title} />
                        </div>
                        <div className="bhistory-room-details">
                          <h4 className="bhistory-room-title">
                            {roomItem.title}
                          </h4>
                          <p className="bhistory-room-type">
                            {roomItem.features[0].detail +
                              ", " +
                              roomItem.features[1].detail +
                              ", " +
                              roomItem.features[2].detail}
                          </p>
                          <p className="bhistory-room-description">
                            {roomItem.shortDescription}
                          </p>

                          {roomItem.amenities.length === 0 ? (
                            ""
                          ) : (
                            <div className="bhistory-room-service">
                              {roomItem.amenities
                                .slice(0, 3) //get only first 3 furniture
                                .map((amenItem, idx) => (
                                  <div
                                    className="bhistory-service-item"
                                    key={`bhistory-amenItem ${idx}`}
                                  >
                                    • {amenItem}
                                  </div>
                                ))}
                            </div>
                          )}
                        </div>

                        <div className="bhistory-room-quantity">
                          <div className="bhistory-quantity-control">
                            <span className="bhistory-card-room-count">
                              {roomCounts[roomItem.typeId] || 0}
                            </span>
                          </div>
                        </div>

                        <div className="bhistory-room-price">
                          <div className="bhistory-price">
                            {roomItem.price.toLocaleString("vi-VN")} VND
                          </div>
                        </div>
                      </div>
                    </div>
                  ))}

              {/* <!-- Extra Bed --> */}
              <div className="bhistory-room-card extra-item">
                <div className="bhistory-room-img">
                  <img src={EXTRA_BED.img} alt={EXTRA_BED.title} />
                </div>
                <div className="bhistory-room-details">
                  <h4 className="bhistory-room-title">{EXTRA_BED.title}</h4>
                  <p className="bhistory-room-type">{EXTRA_BED.type}</p>
                </div>

                <div className="bhistory-room-quantity">
                  <div className="bhistory-quantity-control">
                    <span className="bhistory-extra-bed-count">
                      {extraBedCount}
                    </span>
                  </div>
                </div>

                <div className="bhistory-room-price">
                  <div className="bhistory-price">
                    {EXTRA_BED.price.toLocaleString("vi-VN")} VND
                  </div>
                </div>
              </div>
            </div>

            <div className="bhistory-option-total">
              <div className="bhistory-option-details">
                <p>
                  {checkinDate +
                    " - " +
                    checkoutDate +
                    "; " +
                    adultsCount +
                    " adults; " +
                    childrenCount +
                    " children; " +
                    totalRoomsSelected +
                    " rooms"}
                </p>
              </div>
              <div className="bhistory-option-price">
                <div className="bhistory-total-label">Total cost:</div>
                <div className="bhistory-total-price">
                  {totalPrice.toLocaleString("vi-VN")} VND
                </div>
              </div>
            </div>

            {/* <!-- Review --> */}
            <div className="bhistory-room-review">
              <textarea
                placeholder="Write your review here..."
                className="bhistory-review-input"
              ></textarea>
              <button className="bhistory-send-review">Send</button>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default BookingHistory;

import React, { useState } from "react";
import "./dashboardTable.css";
import RatingStars from "../ratingStars/RatingStars";
import DashboardSettingModal from "../dashboardSettingModal/DashboardSettingModal";

const DashboardTable = ({ dataObj, type, staticFields }) => {
  const data = dataObj.mockData || [];
  const [searchTerm, setSearchTerm] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [recordItem, setRecordItem] = useState(null);
  const [modalTitle, setModalTitle] = useState("");

  const closeModal = () => {
    setIsModalOpen(false);
    setRecordItem(null);
    setModalTitle("");
  };

  const handleDelete = (type, id) => {
    if (window.confirm("Are you sure you want to delete this item?")) {
      if (type === "users") {
        setUserData(userData.mockData.filter((u) => u.id !== id));
      } else if (type === "rooms") {
        setRoomData(roomData.mockData.filter((r) => r.name !== id));
      } else if (type === "bookings") {
        setBookingData(bookingData.mockData.filter((b) => b.id !== id));
      }
    }
  };

  const filteredData = (data) => {
    return data.filter((item) =>
      Object.values(item).some((val) =>
        String(val).toLowerCase().includes(searchTerm.toLowerCase())
      )
    );
  };

  return (
    <div className="dashboard-content-section">
      <div className="dashboard-content-header">
        <div className="dashboard-rooms-left-part">
          <h2>{dataObj.title}</h2>
          {type !== "reviews" && (
            <button
              className="dashboard-add-items-btn add-rooms"
              onClick={() => {
                setRecordItem(null);
                setModalTitle("");
                setIsModalOpen(true);
              }}
            >
              {staticFields.title}
            </button>
          )}
        </div>
        <div className="dashboard-table-controls">
          <div className="dashboard-search-box">
            <span>Search:</span>
            <input
              type="text"
              placeholder="Search..."
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
        </div>
      </div>

      <table
        className={`dashboard-data-table ${
          type === "bookings" ? "dashboard-bookings-table" : ""
        }`}
      >
        <thead>
          <tr>
            {Object.keys(data[0] || {}).map((key) => (
              <th key={key}>{key.charAt(0).toUpperCase() + key.slice(1)}</th>
            ))}

            {type !== "reviews" && <th>Actions</th>}
          </tr>
        </thead>
        <tbody>
          {filteredData(data).map((item, index) => (
            <tr key={index}>
              {Object.entries(item).map(([key, val], i) =>
                key === "status" ? (
                  <td key={i}>
                    <span className={`dashboard-status ${val.toLowerCase()}`}>
                      {val.charAt(0).toUpperCase() + val.slice(1)}
                    </span>
                  </td>
                ) : key === "price" ? (
                  <td key={i}>{"$" + val}</td>
                ) : key === "rating" ? (
                  <td key={i}>
                    <RatingStars rating={val} />
                  </td>
                ) : (
                  <td key={i}>{val}</td>
                )
              )}

              {type !== "reviews" && (
                <td className="dashboard-action-buttons">
                  <button
                    className="dashboard-edit-btn"
                    onClick={() => {
                      setRecordItem(item);
                      setModalTitle(staticFields.settingBtnTitle);
                      setIsModalOpen(true);
                    }}
                  >
                    <i className="bx bx-edit"></i>
                  </button>
                  <button
                    className="dashboard-delete-btn"
                    onClick={() => handleDelete(type, item.id || item.name)}
                  >
                    <i className="bx bx-trash"></i>
                  </button>
                </td>
              )}
            </tr>
          ))}
        </tbody>
      </table>

      <div className="dashboard-table-footer">
        <div className="dashboard-pagination">
          <button disabled>Previous</button>
          <button className="active">1</button>
          <button>Next</button>
        </div>
      </div>

      <DashboardSettingModal
        isOpen={isModalOpen}
        title={modalTitle}
        originItem={recordItem}
        originFields={staticFields}
        onClose={closeModal}
        confirmText="Confirm"
      />
    </div>
  );
};

export default DashboardTable;

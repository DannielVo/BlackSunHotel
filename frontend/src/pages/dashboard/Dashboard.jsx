import React, { useEffect, useState } from "react";
import "./dashboard.css";
import {
  assets,
  BOOKINGS_TAB_KEY,
  DASHBOARD_BOOKING_FIELD_MODAL,
  DASHBOARD_PARKING_SERVICE_FIELD_MODAL,
  DASHBOARD_ROOM_FIELD_MODAL,
  DASHBOARD_ROOM_SERVICE_FIELD_MODAL,
  DASHBOARD_TABS,
  DASHBOARD_USER_FIELD_MODAL,
  MOCK_BOOKINGS,
  MOCK_PARKING_SERVICES,
  MOCK_REVIEWS,
  MOCK_ROOM_SERVICES,
  MOCK_ROOMS,
  MOCK_USERS,
  PARKING_TAB_KEY,
  REVIEWS_TAB_KEY,
  ROOMS_TAB_KEY,
  RSERVICES_TAB_KEY,
  USERS_TAB_KEY,
} from "../../assets/assets";
import { Link } from "react-router-dom";
import DashboardSettingModal from "../../components/dashboardSettingModal/DashboardSettingModal";
import DashboardTable from "../../components/dashboardTable/DashboardTable";

const Dashboard = () => {
  const [activeTab, setActiveTab] = useState("users");

  const [staticFields, setStaticFields] = useState(DASHBOARD_USER_FIELD_MODAL);
  const [dashboardData, setDashboardData] = useState(MOCK_USERS);

  const handleTabChange = (key) => {
    setActiveTab(key);
    switch (key) {
      case USERS_TAB_KEY:
        setStaticFields(DASHBOARD_USER_FIELD_MODAL);
        setDashboardData(MOCK_USERS);
        break;
      case ROOMS_TAB_KEY:
        setStaticFields(DASHBOARD_ROOM_FIELD_MODAL);
        setDashboardData(MOCK_ROOMS);
        break;
      case BOOKINGS_TAB_KEY:
        setStaticFields(DASHBOARD_BOOKING_FIELD_MODAL);
        setDashboardData(MOCK_BOOKINGS);
        break;
      case REVIEWS_TAB_KEY:
        setStaticFields(null);
        setDashboardData(MOCK_REVIEWS);
        break;
      case RSERVICES_TAB_KEY:
        setStaticFields(DASHBOARD_ROOM_SERVICE_FIELD_MODAL);
        setDashboardData(MOCK_ROOM_SERVICES);
        break;
      case PARKING_TAB_KEY:
        setStaticFields(DASHBOARD_PARKING_SERVICE_FIELD_MODAL);
        setDashboardData(MOCK_PARKING_SERVICES);
        break;
    }
  };

  useEffect(() => {
    document.title = "Black Sun Hotel | Dashboard";
  }, []);

  return (
    <div className="dashboard-wrapper">
      {/* <!-- HEADER --> */}
      <header className="dashboard-header">
        <div className="dashboard-nav-group left">
          <Link to={"/"} className="dashboard-logo">
            <img src={assets.logo} />
          </Link>
        </div>

        <div className="dashboard-nav-group right">
          <div className="dashboard-user-profile">
            <i className="bx bx-user-circle dashboard-user-icon"></i>
            <span className="dashboard-username">Administrator</span>
          </div>
        </div>
      </header>

      <div className="dashboard-container">
        {/* <!-- SIDE BAR --> */}
        <aside className="dashboard-sidebar">
          <ul className="dashboard-sidebar-menu">
            {DASHBOARD_TABS.map((item) => (
              <li
                key={item.key}
                className={`dashboard-menu-item ${
                  activeTab === item.key ? "active" : ""
                }`}
                onClick={() => handleTabChange(item.key)}
              >
                <a href="#">
                  <i className={`bx ${item.icon}`}></i> {item.label}
                </a>
              </li>
            ))}
          </ul>
        </aside>

        {/* <!-- MAIN CONTENT --> */}
        <main className="dashboard-main-content">
          <DashboardTable
            dataObj={dashboardData}
            type={activeTab}
            staticFields={staticFields}
          />
        </main>
      </div>
    </div>
  );
};

export default Dashboard;

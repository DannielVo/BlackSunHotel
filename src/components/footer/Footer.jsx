import React, { useRef } from "react";
import "./footer.css";
import { assets } from "../../assets/assets";

const Footer = () => {
  const toastRef = useRef(null);

  const handleCopy = (textToCopy) => {
    if (!textToCopy) return;
    navigator.clipboard.writeText(textToCopy).then(() => {
      showCopyToast();
    });
  };

  const showCopyToast = () => {
    const toast = toastRef.current;
    if (!toast) return;
    toast.classList.add("show");
    setTimeout(() => {
      toast.classList.remove("show");
    }, 1500);
  };

  const copyToast = (e, text) => {
    e.preventDefault();
    handleCopy(text);
  };
  return (
    <>
      <footer class="footer">
        <div class="footer-content">
          {/* <!-- LOGO --> */}
          <div class="footer-logo">
            <img src={assets.logo} alt="Black Sun Hotel Logo" />
          </div>

          {/* <!-- Hotel Information --> */}
          <div class="footer-info">
            <h4>Hotel Information</h4>
            <ul>
              <li>
                <a href="#history">About Us</a>
              </li>
              <li>
                <a href="#location">Location</a>
              </li>
            </ul>
          </div>

          {/* <!-- Terms & Policies --> */}
          <div class="footer-info">
            <h4>Terms & Policies</h4>
            <ul>
              <li>
                <a href="privacy-policy.html" target="_blank">
                  Privacy Policy
                </a>
              </li>
              <li>
                <a href="booking-cancel.html" target="_blank">
                  Booking & Cancellation
                </a>
              </li>
            </ul>
          </div>

          {/* <!-- Contact Us --> */}
          <div class="footer-info">
            <h4>Contact Us</h4>
            <ul>
              <li>+84 28 1234 5678</li>
              <li>info@blacksunhotel.vn</li>
              <li>123 Yersin St., Nha Trang City, Vietnam</li>
            </ul>
          </div>
        </div>

        {/* <!-- Footer-social --> */}
        <div class="footer-bottom">
          <div class="footer-social">
            <a
              href="#"
              class="social-icon"
              data-copy="+84 28 1234 5678"
              title="Copy phone number"
              onClick={(e) => copyToast(e, "+84 28 1234 5678")}
            >
              <i class="bx bxs-phone"></i>
            </a>
            <a
              href="#"
              class="social-icon"
              data-copy="https://www.facebook.com/BlackSunHotel"
              title="Copy Facebook link"
              onClick={(e) =>
                copyToast(e, "https://www.facebook.com/BlackSunHotel")
              }
            >
              <i class="bx bxl-facebook-circle"></i>
            </a>
            <a
              href="#"
              class="social-icon"
              data-copy="https://www.youtube.com/@BlackSunHotel"
              title="Copy YouTube link"
              onClick={(e) =>
                copyToast(e, "https://www.youtube.com/@BlackSunHotel")
              }
            >
              <i class="bx bxl-youtube"></i>
            </a>
            <a
              href="#"
              class="social-icon"
              data-copy="info@blacksunhotel.vn"
              title="Copy email"
              onClick={(e) => copyToast(e, "info@blacksunhotel.vn")}
            >
              <i class="bx bx-envelope"></i>
            </a>
          </div>
          <p>© 2025 Black Sun Hotel. All Rights Reserved</p>
        </div>

        {/* <!-- Notification Toast --> */}
        <div id="copy-toast" class="copy-toast">
          Copy to clipboard
        </div>
      </footer>
    </>
  );
};

export default Footer;

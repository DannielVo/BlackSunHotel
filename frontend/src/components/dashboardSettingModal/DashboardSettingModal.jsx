import React from "react";
import "./dashboardSettingModal.css";

const DashboardSettingModal = ({
  isOpen,
  title,
  originItem,
  originFields,
  onClose,
  confirmText = "Submit",
}) => {
  if (!isOpen) return null;

  const handleOverlayClick = () => {
    onClose();
  };

  const handleModalClick = (e) => {
    e.stopPropagation();
  };

  const onSubmit = (e) => {
    e.preventDefault();
    // Lấy dữ liệu từ form nếu cần
  };

  // Tạo field với giá trị từ `item`
  const fields = originItem
    ? {
        ...originFields,
        fieldItems: originFields.fieldItems.map((fieldItem) => ({
          ...fieldItem,
          value: originItem?.[fieldItem.name] ?? fieldItem.value ?? "",
        })),
      }
    : originFields;

  return (
    <>
      <div class="dashboard-modal" onClick={handleOverlayClick}>
        <div class="modal-content" onClick={handleModalClick}>
          <span class="close-btn" id="closeAddUserPopup" onClick={onClose}>
            &times;
          </span>
          <h2>{title || originFields.title}</h2>
          <form id={fields.id} onSubmit={onSubmit}>
            {fields.fieldItems.map((field, index) => (
              <div class="form-group grid-form" key={`field ${index}`}>
                {!(title === "" && field.name === "id") && (
                  <>
                    <label htmlFor={field.id}>{field.label}</label>
                    {field.type === "select" ? (
                      <select
                        id={field.id}
                        name={field.name}
                        defaultValue={field.value}
                        required={field.required}
                        disabled={field.readOnly}
                      >
                        {field.options.map((option, idx) => (
                          <option value={option.value} key={`option ${idx}`}>
                            {option.label}
                          </option>
                        ))}
                      </select>
                    ) : (
                      <input
                        type={field.type}
                        id={field.id}
                        required={field.required}
                        name={field.name}
                        disabled={field.readOnly}
                        defaultValue={field.value}
                      />
                    )}
                  </>
                )}
              </div>
            ))}
            <button type="submit" class="confirm-btn">
              {confirmText}
            </button>
          </form>
        </div>
      </div>
    </>
  );
};

export default DashboardSettingModal;

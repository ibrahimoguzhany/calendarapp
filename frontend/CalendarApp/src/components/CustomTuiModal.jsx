import React, { useState, useRef, useEffect, useLayoutEffect } from "react";
import { Modal,FormGroup,Label } from "reactstrap";
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import DateRangePicker from "./DateRangePicker";

export default function CustomTuiModal({
  isOpen = false,
  toggle,
  onSubmit,
  submitText = "Save",
  calendars = [],
  schedule,
  startDate,
  endDate
}) {
  const [openSelectCalendars, setOpenSelectCalendars] = useState(false);
  const wrapperSelectCalendarsRef = useRef(null);
  const dateRangePickerRef = useRef(null);
  const subjectRef = useRef(null);

  const [calendarId, setCalendarId] = useState(calendars[0].id);
  const [title, setTitle] = useState("");
  const [start, setStart] = useState(null);
  const [end, setEnd] = useState(null);
  
  const handleClick = (e) => {
    if (wrapperSelectCalendarsRef.current?.contains(e.target)) {
      return;
    }
    setOpenSelectCalendars(false);
  };

  useEffect(() => {
    document.addEventListener("click", handleClick, false);

    return () => {
      document.removeEventListener("click", handleClick, false);
    };
  });

  useLayoutEffect(() => {
    if (schedule) {
      setCalendarId(schedule.calendarId);
      setTitle(schedule.title);
      setStart(schedule.start.toDate());
      setEnd(schedule.end.toDate());
      dateRangePickerRef.current.setStartDate(schedule.start.toDate());
      dateRangePickerRef.current.setEndDate(schedule.end.toDate());
    }
    if (startDate && endDate) {
      dateRangePickerRef.current.setStartDate(startDate.toDate());
      dateRangePickerRef.current.setEndDate(endDate.toDate());
    }
    return () => {};
  }, [schedule, startDate, endDate]);

  function reset() {
    setCalendarId(calendars[0].id);
    setTitle("");
    setStart(new Date());
    setEnd(new Date());
    dateRangePickerRef.current.setStartDate(new Date());
    dateRangePickerRef.current.setEndDate(new Date());
  }

  return (
    <Modal
      isOpen={isOpen}
      toggle={() => {
        toggle();
        reset();
      }}
      centered
    >
      <div className="tui-full-calendar-popup-container">
        <div style={{ display: "flex" }}>
          {/* Department */}
          <div
            ref={wrapperSelectCalendarsRef}
            className={`tui-full-calendar-popup-section tui-full-calendar-dropdown tui-full-calendar-close tui-full-calendar-section-calendar ${
              openSelectCalendars && "tui-full-calendar-open"
            }`}
          >
            <button
              onClick={() => setOpenSelectCalendars(!openSelectCalendars)}
              className="tui-full-calendar-button tui-full-calendar-dropdown-button tui-full-calendar-popup-section-item"
            >
              <span
                className="tui-full-calendar-icon tui-full-calendar-calendar-dot"
                style={{
                  backgroundColor: calendars.find(
                    (element) => element.id === calendarId
                  )?.bgColor ?? "black"
                }}
              />
              <span
                id="tui-full-calendar-schedule-calendar"
                className="tui-full-calendar-content"
                style={{
                  whiteSpace: "nowrap",
                  overflow: "hidden",
                  textOverflow: "ellipsis"
                }}
              >
                {calendars.find((element) => +element.id === +calendarId)?.name}
              </span>
              <span className="tui-full-calendar-icon tui-full-calendar-dropdown-arrow" />
            </button>
            <ul
              className="tui-full-calendar-dropdown-menu"
              style={{ zIndex: 1004 }}
            >
              {calendars.map((element, i) => (
                <li
                  onClick={() => {
                    setCalendarId(element.id);
                    setOpenSelectCalendars(false);
                  }}
                  key={i}
                  className="tui-full-calendar-popup-section-item tui-full-calendar-dropdown-menu-item"
                  data-calendar-id={element.id}
                >
                  <span
                    className="tui-full-calendar-icon tui-full-calendar-calendar-dot"
                    style={{ backgroundColor: element.bgColor }}
                  />
                  <span className="tui-full-calendar-content">
                    {element.name}
                  </span>
                </li>
              ))}
            </ul>
          </div>
          <span className="tui-full-calendar-section-date-dash">-</span>
         
        </div>
        {/* Açıklama */}
        <div className="tui-full-calendar-popup-section">
          <div className="tui-full-calendar-popup-section-item tui-full-calendar-section-location">
            <span className="tui-full-calendar-icon tui-full-calendar-ic-title" />
            <input
              ref={subjectRef}
              id="tui-full-calendar-schedule-title"
              className="tui-full-calendar-content"
              placeholder="Açıklama"
              value={title}
              onChange={(e) => {
                setTitle(e.target.value);
              }}
            />
          </div>
        </div>
        
        <div className="tui-full-calendar-popup-section">
          <DateRangePicker
            ref={dateRangePickerRef}
            date={new Date()}
            start={start}
            end={end}
            format="yyyy/MM/dd HH:mm"
            timePicker={{
              layoutType: "tab",
              inputType: "spinbox"
            }}
            onChange={(e) => {
              setStart(e[0]);
              setEnd(e[1]);
            }}
          />
        </div>
        <button
          onClick={() => {
            toggle();
            // reset()
          }}
          className="tui-full-calendar-button tui-full-calendar-popup-close"
        >
          <span className="tui-full-calendar-icon tui-full-calendar-ic-close" />
        </button>
        <div className="tui-full-calendar-section-button-save">
          <button
            onClick={() => {
              if (!subjectRef.current.value) {
                subjectRef.current.focus();
              } else {
                const event = {
                  calendarId,
                  title,
                  start,
                  end,
                  ...calendars.find((element) => element.id === calendarId)
                };
                onSubmit(event);
              }
            }}
            className="tui-full-calendar-button tui-full-calendar-confirm tui-full-calendar-popup-save"
          >
            <span>{submitText}</span>
          </button>
        </div>
      </div>
    </Modal>
  );
}

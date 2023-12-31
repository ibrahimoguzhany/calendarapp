import React, {
  useRef,
  useLayoutEffect,
  useEffect,
  useState,
  forwardRef,
  useImperativeHandle
} from "react";
import TuiCalendar from "tui-calendar";
import moment from "moment";

import "tui-calendar/dist/tui-calendar.css";

import "./styles.css";

const CustomTuiCalendar = forwardRef(
  (
    {
      height = "800px",
      defaultView = "week",
      calendars = [],
      schedules = [],
      isReadOnly = true,
      showSlidebar = false,
      showMenu = false,
      onCreate,
      createText = "New schedule",
      onBeforeCreateSchedule = () => false,
      onBeforeUpdateSchedule = () => false,
      onBeforeDeleteSchedule = () => false,
      ...rest
    },
    ref
  ) => {
    const calendarInstRef = useRef(null);
    const tuiRef = useRef(null);
    const wrapperRef = useRef(null);
    const [open, setOpen] = useState(false);
    const [renderRange, setRenderRange] = useState("");
    const [workweek, setWorkweek] = useState(true);
    const [narrowWeekend, setNarrowWeekend] = useState(true);
    const [startDayOfWeek, setStartDayOfWeek] = useState(1);
    const [type, setType] = useState("Weekly");
    const [filterSchedules, setFilterSchedules] = useState(schedules);

    useImperativeHandle(ref, () => ({
      getAlert() {
        alert("getAlert from Child");
      },
      createSchedule,
      updateSchedule,
      deleteSchedule
    }));

    useEffect(() => {
      calendarInstRef.current = new TuiCalendar(tuiRef.current, {
        useDetailPopup: true,
        useCreationPopup: true,
        template: {
          milestone: function (schedule) {
            return (
              '<span class="calendar-font-icon ic-milestone-b"></span> <span style="background-color: ' +
              schedule.bgColor +
              '">' +
              schedule.title +
              "</span>"
            );
          },
          milestoneTitle: function () {
            return '<span class="tui-full-calendar-left-content">MILESTONE</span>';
          },
          task: function (schedule) {
            return "#" + schedule.title;
          },
          taskTitle: function () {
            return '<span class="tui-full-calendar-left-content">TASK</span>';
          },
          allday: function (schedule) {
            return _getTimeTemplate(schedule, true);
          },
          alldayTitle: function () {
            return '<span class="tui-full-calendar-left-content">ALL DAY</span>';
          },
          time: function (schedule) {
            return _getTimeTemplate(schedule, false);
          },
          goingDuration: function (schedule) {
            return (
              '<span class="calendar-icon ic-travel-time"></span>' +
              schedule.goingDuration +
              "min."
            );
          },
          comingDuration: function (schedule) {
            return (
              '<span class="calendar-icon ic-travel-time"></span>' +
              schedule.comingDuration +
              "min."
            );
          },
          monthMoreTitleDate: function (date, dayname) {
            var day = date.split(".")[2];

            return (
              '<span class="tui-full-calendar-month-more-title-day">' +
              day +
              '</span> <span class="tui-full-calendar-month-more-title-day-label">' +
              dayname +
              "</span>"
            );
          },
          monthMoreClose: function () {
            return '<span class="tui-full-calendar-icon tui-full-calendar-ic-close"></span>';
          },
          monthGridHeader: function (dayModel) {
            var date = parseInt(dayModel.date.split("-")[2], 10);
            var classNames = ["tui-full-calendar-weekday-grid-date "];

            if (dayModel.isToday) {
              classNames.push("tui-full-calendar-weekday-grid-date-decorator");
            }

            return (
              '<span class="' + classNames.join(" ") + '">' + date + "</span>"
            );
          },
          monthGridHeaderExceed: function (hiddenSchedules) {
            return (
              '<span class="weekday-grid-more-schedules">+' +
              hiddenSchedules +
              "</span>"
            );
          },
          monthGridFooter: function () {
            return "";
          },
          monthGridFooterExceed: function (hiddenSchedules) {
            return "";
          },
          monthDayname: function (model) {
            return model.label.toString().toLocaleUpperCase();
          },
          weekDayname: function (model) {
            return (
              '<span class="tui-full-calendar-dayname-date">' +
              model.date +
              '</span>&nbsp;&nbsp;<span class="tui-full-calendar-dayname-name">' +
              model.dayName +
              "</span>"
            );
          },
          weekGridFooterExceed: function (hiddenSchedules) {
            return "+" + hiddenSchedules;
          },
          dayGridTitle: function (viewName) {
            var title = "";
            switch (viewName) {
              case "milestone":
                title =
                  '<span class="tui-full-calendar-left-content">MILESTONE</span>';
                break;
              case "task":
                title =
                  '<span class="tui-full-calendar-left-content">TASK</span>';
                break;
              case "allday":
                title =
                  '<span class="tui-full-calendar-left-content">ALL DAY</span>';
                break;
              default:
                break;
            }

            return title;
          },
          collapseBtnTitle: function () {
            return '<span class="tui-full-calendar-icon tui-full-calendar-ic-arrow-solid-top"></span>';
          },
          timegridDisplayPrimayTime: function (time) {
            var meridiem = "am";
            var hour = time.hour;

            if (time.hour > 12) {
              meridiem = "pm";
              hour = time.hour - 12;
            }

            return hour + " " + meridiem;
          },
          timegridDisplayPrimaryTime: function (time) {
            var meridiem = "am";
            var hour = time.hour;

            if (time.hour > 12) {
              meridiem = "pm";
              hour = time.hour - 12;
            }
            return hour + " " + meridiem;
          },
          timegridCurrentTime: function (timezone) {
            var templates = [];

            if (timezone.dateDifference) {
              templates.push(
                "[" +
                  timezone.dateDifferenceSign +
                  timezone.dateDifference +
                  "]<br>"
              );
            }

            templates.push(moment(timezone.hourmarker).format("HH:mm a"));

            return templates.join("");
          },
          popupIsAllDay: function () {
            return "All Day";
          },
          popupStateFree: function () {
            return "Free";
          },
          popupStateBusy: function () {
            return "Busy";
          },
          titlePlaceholder: function () {
            return "Konu";
          },
          locationPlaceholder: function () {
            return "Konum";
          },
          startDatePlaceholder: function () {
            return "Başlangıç Tarihi";
          },
          endDatePlaceholder: function () {
            return "Bitiş Tarihi";
          },
          popupSave: function () {
            return "Kaydet";
          },
          popupUpdate: function () {
            return "Güncelle";
          },
          popupDetailDate: function (isAllDay, start, end) {
            var isSameDate = moment(start).isSame(end);
            var endFormat = (isSameDate ? "" : "YYYY/MM/DD ") + "HH:mm";

            if (isAllDay) {
              return (
                moment(start).format("YYYY/MM/DD") +
                (isSameDate ? "" : " - " + moment(end).format("YYYY/MM/DD"))
              );
            }

            return (
              moment(start.toDate()).format("YYYY/MM/DD HH:mm") +
              " - " +
              moment(end.toDate()).format(endFormat)
            );
          },
          popupDetailLocation: function (schedule) {
            return "Location : " + schedule.location;
          },
          popupDetailState: function (schedule) {
            return "State : " + schedule.state || "Busy";
          },
          popupDetailRepeat: function (schedule) {
            return "Repeat : " + schedule.recurrenceRule;
          },
          popupDetailBody: function (schedule) {
            return "Body : " + schedule.body;
          },
          popupEdit: function () {
            return "Edit";
          },
          popupDelete: function () {
            return "Delete";
          }
        },
        calendars,
        ...rest
      });
      setRenderRangeText();
      // render schedules
      calendarInstRef.current.clear();
      calendarInstRef.current.createSchedules(filterSchedules, true);
      calendarInstRef.current.render();

      calendarInstRef.current.on("beforeCreateSchedule", function (event) {
        onBeforeCreateSchedule(event);
      });
      calendarInstRef.current.on("beforeUpdateSchedule", function (event) {
        onBeforeUpdateSchedule(event);
      });
      calendarInstRef.current.on("beforeDeleteSchedule", function (event) {
        onBeforeDeleteSchedule(event);
      });
      calendarInstRef.current.on("clickSchedule", function (event) {
      });
      calendarInstRef.current.on("clickDayname", function (event) {
        if (calendarInstRef.current.getViewName() === "week") {
          calendarInstRef.current.setDate(new Date(event.date));
          calendarInstRef.current.changeView("day", true);
        }
      });

      calendarInstRef.current.on("clickMore", function (event) {
      });

      calendarInstRef.current.on("clickTimezonesCollapseBtn", function (
        timezonesCollapsed
      ) {
      });

      calendarInstRef.current.on("afterRenderSchedule", function (event) {
      });

      return () => {
        calendarInstRef.current.destroy();
      };
    }, [tuiRef, schedules]);

    useLayoutEffect(() => {
    });

    function currentCalendarDate(format) {
      var currentDate = moment([
        calendarInstRef.current.getDate().getFullYear(),
        calendarInstRef.current.getDate().getMonth(),
        calendarInstRef.current.getDate().getDate()
      ]);

      return currentDate.format(format);
    }

    function setRenderRangeText() {
      var options = calendarInstRef.current.getOptions();
      var viewName = calendarInstRef.current.getViewName();

      var html = [];
      if (viewName === "day") {
        html.push(currentCalendarDate("YYYY.MM.DD"));
      } else if (
        viewName === "month" &&
        (!options.month.visibleWeeksCount ||
          options.month.visibleWeeksCount > 4)
      ) {
        html.push(currentCalendarDate("YYYY.MM"));
      } else {
        html.push(
          moment(calendarInstRef.current.getDateRangeStart().getTime()).format(
            "YYYY.MM.DD"
          )
        );
        html.push(" ~ ");
        html.push(
          moment(calendarInstRef.current.getDateRangeEnd().getTime()).format(
            " MM.DD"
          )
        );
      }
      setRenderRange(html.join(""));
    }

    function _getTimeTemplate(schedule, isAllDay) {
      var html = [];

      if (!isAllDay) {
        html.push(
          "<strong>" +
            moment(schedule.start.toDate()).format("HH:mm") +
            "</strong> "
        );
      }
      if (schedule.isPrivate) {
        html.push('<span class="calendar-font-icon ic-lock-b"></span>');
        html.push(" Private");
      } else {
        if (schedule.isReadOnly) {
          html.push('<span class="calendar-font-icon ic-readonly-b"></span>');
        } else if (schedule.recurrenceRule) {
          html.push('<span class="calendar-font-icon ic-repeat-b"></span>');
        } else if (schedule.location) {
          html.push('<span class="calendar-font-icon ic-location-b"></span>');
        }

        html.push(" " + schedule.title);
      }

      return html.join("");
    }

    useEffect(() => {
      document.addEventListener("click", handleClick, false);

      return () => {
        document.removeEventListener("click", handleClick, false);
      };
    });

    const handleClick = (e) => {
      if (wrapperRef.current?.contains(e.target)) {
        return;
      }
      setOpen(false);
    };
    function createSchedule(schedule) {
      console.log("createSchedule");

      calendarInstRef.current.createSchedules([schedule]);
      const cloneFilterSchedules = [...filterSchedules];
      setFilterSchedules((prevState) => [...cloneFilterSchedules, schedule]);
    }

    function updateSchedule(schedule, changes) {
      console.log("updateSchedule");

      calendarInstRef.current.updateSchedule(
        schedule.id,
        schedule.calendarId,
        changes
      );
      const cloneFilterSchedules = [...filterSchedules];
      setFilterSchedules((prevState) =>
        cloneFilterSchedules.map((item) => {
          if (item.id === schedule.id) {
            return { ...item, ...changes };
          }
          return item;
        })
      );
    }

    function deleteSchedule(schedule) {
      console.log("deleteSchedule");

      calendarInstRef.current.deleteSchedule(schedule.id, schedule.calendarId);
      const cloneFilterSchedules = [...filterSchedules];
      setFilterSchedules((prevState) =>
        cloneFilterSchedules.filter((item) => item.id !== schedule.id)
      );
    }

    return (
      <div>
        <div id="mainContainer" style={{ left: !showSlidebar && 0 }}>
          {showMenu && (
            <div id="menu">
              <span
                ref={wrapperRef}
                style={{ marginRight: "4px" }}
                className={`dropdown ${open && "open"}`}
              >
                <button
                  id="dropdownMenu-calendarType"
                  className="btn btn-default btn-sm dropdown-toggle"
                  type="button"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded={open}
                  onClick={() => setOpen(!open)}
                >
                  <i
                    id="calendarTypeIcon"
                    className="calendar-icon ic_view_week"
                    style={{ marginRight: "4px" }}
                  />
                  <span id="calendarTypeName">{type}</span>&nbsp;
                  <i className="calendar-icon tui-full-calendar-dropdown-arrow" />
                </button>
                <ul
                  className="dropdown-menu"
                  role="menu"
                  aria-labelledby="dropdownMenu-calendarType"
                >
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.changeView("day", true);
                        setType("Daily");
                        setOpen(false);
                      }}
                      className="dropdown-menu-title"
                      role="menuitem"
                      data-action="toggle-daily"
                    >
                      <i className="calendar-icon ic_view_day" />
                      Daily
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.changeView("week", true);
                        setType("Weekly");
                        setOpen(false);
                      }}
                      className="dropdown-menu-title"
                      role="menuitem"
                      data-action="toggle-weekly"
                    >
                      <i className="calendar-icon ic_view_week" />
                      Weekly
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { month: { visibleWeeksCount: 6 } },
                          true
                        ); // or null
                        calendarInstRef.current.changeView("month", true);
                        setType("Month");
                        setOpen(false);
                      }}
                      className="dropdown-menu-title"
                      role="menuitem"
                      data-action="toggle-monthly"
                    >
                      <i className="calendar-icon ic_view_month" />
                      Month
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { month: { visibleWeeksCount: 2 } },
                          true
                        ); // or null
                        calendarInstRef.current.changeView("month", true);
                        setType("2 weeks");
                        setOpen(false);
                      }}
                      className="dropdown-menu-title"
                      role="menuitem"
                      data-action="toggle-weeks2"
                    >
                      <i className="calendar-icon ic_view_week" />2 weeks
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { month: { visibleWeeksCount: 3 } },
                          true
                        ); // or null
                        calendarInstRef.current.changeView("month", true);
                        setType("3 weeks");
                        setOpen(false);
                      }}
                      className="dropdown-menu-title"
                      role="menuitem"
                      data-action="toggle-weeks3"
                    >
                      <i className="calendar-icon ic_view_week" />3 weeks
                    </a>
                  </li>
                  <li role="presentation" className="dropdown-divider" />
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { month: { workweek } },
                          true
                        );
                        calendarInstRef.current.setOptions(
                          { week: { workweek } },
                          true
                        );
                        calendarInstRef.current.changeView(
                          calendarInstRef.current.getViewName(),
                          true
                        );
                        setWorkweek(!workweek);
                        setOpen(false);
                      }}
                      role="menuitem"
                      data-action="toggle-workweek"
                    >
                      <input
                        type="checkbox"
                        className="tui-full-calendar-checkbox-square"
                        checked={workweek}
                        onChange={() => {}}
                      />
                      <span className="checkbox-title" />
                      Show weekends
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { week: { startDayOfWeek } },
                          true
                        );
                        calendarInstRef.current.setOptions(
                          { month: { startDayOfWeek } },
                          true
                        );
                        calendarInstRef.current.changeView(
                          calendarInstRef.current.getViewName(),
                          true
                        );
                        setStartDayOfWeek(startDayOfWeek === 1 ? 0 : 1);
                        setOpen(false);
                      }}
                      role="menuitem"
                      data-action="toggle-start-day-1"
                    >
                      <input
                        type="checkbox"
                        className="tui-full-calendar-checkbox-square"
                        checked={startDayOfWeek !== 1 ? true : false}
                        onChange={() => {}}
                      />
                      <span className="checkbox-title" />
                      Start Week on Monday
                    </a>
                  </li>
                  <li role="presentation">
                    <a
                      href="/"
                      onClick={(e) => {
                        e.preventDefault();
                        calendarInstRef.current.setOptions(
                          { month: { narrowWeekend } },
                          true
                        );
                        calendarInstRef.current.setOptions(
                          { week: { narrowWeekend } },
                          true
                        );
                        calendarInstRef.current.changeView(
                          calendarInstRef.current.getViewName(),
                          true
                        );
                        setNarrowWeekend(!narrowWeekend);
                        setOpen(false);
                      }}
                      role="menuitem"
                      data-action="toggle-narrow-weekend"
                    >
                      <input
                        type="checkbox"
                        className="tui-full-calendar-checkbox-square"
                        checked={narrowWeekend}
                        onChange={() => {}}
                      />
                      <span className="checkbox-title" />
                      Narrower than weekdays
                    </a>
                  </li>
                </ul>
              </span>

              <span id="menu-navi">
                <button
                  type="button"
                  className="btn btn-default btn-sm move-today"
                  style={{ marginRight: "4px" }}
                  data-action="move-today"
                  onClick={() => {
                    // console.log("today");
                    calendarInstRef.current.today();
                    setRenderRangeText();
                  }}
                >
                  Today
                </button>
                <button
                  type="button"
                  className="btn btn-default btn-sm move-day"
                  style={{ marginRight: "4px" }}
                  data-action="move-prev"
                  onClick={() => {
                    // console.log("pre");
                    calendarInstRef.current.prev();
                    setRenderRangeText();
                  }}
                >
                  <i
                    className="calendar-icon ic-arrow-line-left"
                    data-action="move-prev"
                  />
                </button>
                <button
                  type="button"
                  className="btn btn-default btn-sm move-day"
                  style={{ marginRight: "4px" }}
                  data-action="move-next"
                  onClick={() => {
                    // console.log("next");
                    calendarInstRef.current.next();
                    setRenderRangeText();
                  }}
                >
                  <i
                    className="calendar-icon ic-arrow-line-right"
                    data-action="move-next"
                  />
                </button>
              </span>
              <span id="renderRange" className="render-range">
                {renderRange}
              </span>
            </div>
          )}
          <div ref={tuiRef} style={{ height }} />
        </div>
      </div>
    );
  }
);

export default CustomTuiCalendar;

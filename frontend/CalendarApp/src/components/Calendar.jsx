import React, { useState, useRef, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.css";
import "./styles.css";
import CustomTuiCalendar from "./CustomTuiCalendar";
import CustomTuiModal from "./CustomTuiModal";
import { useUser } from "../lib/customHooks";
import { API_ROUTES } from "../utils/constants";
import { getTokenFromLocalStorage } from "../lib/common";

import NotificationComponent from "./Notification";


const start = new Date();
const end = new Date(new Date().setMinutes(start.getMinutes() + 60));

// const schedules = [
//   {
//     id: "1",
//     title: "İbrahimle Görüşme",
//     calendarId: "1",
//     category: "time",
//     isVisible: true,
//     start,
//     end,
//   },
//   {
//     id: "2",
//     title: "Oğuzhanla toplantı",
//     calendarId: "2",
//     category: "time",
//     isVisible: true,
//     start: new Date(new Date().setHours(start.getHours() + 1)),
//     end: new Date(new Date().setHours(start.getHours() + 2)),
//   },
//   {
//     id: "3",
//     title: "Cerenle gezi",
//     calendarId: "3",
//     category: "time",
//     isVisible: true,
//     start: new Date(new Date().setHours(start.getHours() + 2)),
//     end: new Date(new Date().setHours(start.getHours() + 4)),
//   },
//   {
//     id: "4",
//     title: "Barışla yemek",
//     calendarId: "4",
//     category: "time",
//     isVisible: true,
//     start: new Date(new Date().setHours(start.getHours() + 2)),
//     end: new Date(new Date().setHours(start.getHours() + 6)),
//   },
// ];

const colors = [
  {
    id: "1",
    color: "#ffffff",
    bgColor: "#34C38F",
    dragBgColor: "#34C38F",
    borderColor: "#34C38F",
  },
  {
    id: "2",
    color: "#ffffff",
    bgColor: "#F4696A",
    dragBgColor: "#F4696A",
    borderColor: "#F4696A",
  },
  {
    id: "3",
    color: "#ffffff",
    bgColor: "#00a9ff",
    dragBgColor: "#00a9ff",
    borderColor: "#00a9ff",
  },
  {
    id: "4",
    color: "#ffffff",
    bgColor: "#F2B34C",
    dragBgColor: "#F2B34C",
    borderColor: "#F2B34C",
  },
  {
    id: "5",
    color: "#ffffff",
    bgColor: "#74788D",
    dragBgColor: "#74788D",
    borderColor: "#74788D",
  },
  {
    id: "6",
    color: "#ffffff",
    bgColor: "#343A40",
    dragBgColor: "#343A40",
    borderColor: "#343A40",
  },
  {
    id: "7",
    color: "#000000",
    bgColor: "#FFFFFF",
    dragBgColor: "#FFFFFF",
    borderColor: "#FFFFFF",
  },
];

const calendars = [
  {
    id: "1",
    name: "Teknik",
  },
  {
    id: "2",
    name: "Temizlik",
  },
  {
    id: "3",
    name: "Aquapark",
  },
  {
    id: "4",
    name: "İş Toplantısı",
  },
  {
    id: "5",
    name: "Yemek",
  },
  {
    id: "6",
    name: "Gezi",
  },
  {
    id: "7",
    name: "Tatil",
  },
];

export default function Calendar() {
  const { user, authenticated } = useUser();
  const [modal, setModal] = useState(false);
  const [event, setEvent] = useState(null);

  const [schedules, setSchedules] = useState([]);
  const childRef = useRef();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const token = getTokenFromLocalStorage();

        const response = await axios.get(API_ROUTES.GET_LIST_EVENT, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setSchedules(
          response.data.data.map((item) => {
            return {
              id: item.id,
              title: item.title,
              calendarId: item.calendarId,
              category: "time",
              isVisible: true,
              start: new Date(item.startDate),
              end: new Date(item.endDate),
            };
          })
        );
      } catch (error) {
        console.error("Error fetching data: ", error);
        // handle error
      }
    };

    fetchData();
  }, []);
  if (!user || !authenticated) {
    return (
      <div className="p-16 bg-gray-800 h-screen">
        <div className="text-2xl mb-4 font-bold text-white">Dashboard</div>
        <div className="ml-2 w-8 h-8 border-l-2 rounded-full animate-spin border-white" />
      </div>
    );
  }

  const toggle = () => {
    setModal(!modal);
    setEvent(null);
  };

  function onBeforeCreateSchedule(event) {
    event.guide.clearGuideElement();
    setModal(true);
    setEvent(event);
  }

  async function handleCreateSchedule(newEvent) {
    const newSchedule = {
      ...event,
      id: schedules.length,
      title: newEvent.title,
      calendarId: newEvent.calendarId,
      category: event.isAllDay ? "allday" : "time",
      isVisible: true,
      start: newEvent.start,
      end: newEvent.end,
      isAllDay: event.isAllDay,
      dueDateClass: "",
      location: event.location,
      state: event.state,
      body: event.body,
    };
    const data = {
      title: newEvent.title,
      calendarId: newEvent.calendarId,
      startDate: newEvent.start,
      endDate: newEvent.end,
    };

    try {
      const token = getTokenFromLocalStorage();

      const response = await axios.post(API_ROUTES.ADD_EVENT, data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (response.data.isSuccess) {
        childRef.current.createSchedule(newSchedule);
        newSchedule.id = response.data.data.id;
        window.location.reload();
        setModal(false);
      }
    } catch (error) {
      console.error("Error creating new schedule: ", error);
    }
  }

  function onBeforeUpdateSchedule(event) {
    setModal(true);
    setEvent(event);
  }

  async function handleUpdateSchedule(updateEvent) {
    // call api
    try {
      const token = getTokenFromLocalStorage();
      const { schedule } = event;
      const data = {
        id: schedule.id,
        title: updateEvent.title,
        calendarId: updateEvent.calendarId,
        startDate: updateEvent.start,
        endDate: updateEvent.end,
      };
      const response = await axios.post(API_ROUTES.UPDATE_EVENT, data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.data.isSuccess) {
        await childRef.current.deleteSchedule(schedule);
        const newSchedule = {
          ...event,
          id: schedule.id,
          title: updateEvent.title,
          calendarId: updateEvent.calendarId,
          category: "time",
          isVisible: true,
          start: updateEvent.start,
          end: updateEvent.end,
          isAllDay: false,
          dueDateClass: "",
          location: event.location,
          state: event.state,
          body: event.body,
        };
        await childRef.current.createSchedule(newSchedule);
        window.location.reload();

        setModal(false);
      }
    } catch (error) {
      console.error("Error creating new schedule: ", error);
    }
  }

  async function onBeforeDeleteSchedule(event) {
    const token = getTokenFromLocalStorage();
    const { schedule } = event;
    const response = await axios.post(
      API_ROUTES.DELETE_EVENT,
      { id: schedule.id },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    if (response.data.isSuccess) {
      const { schedule } = event;
      childRef.current.deleteSchedule(schedule);
    }
    return true;
  }
  const formatCalendars = calendars.map((element) => ({
    ...colors.find((element2) => element2.id === element.id),
    ...element,
  }));

  return (
    <div>
      <NotificationComponent schedules={schedules} />
      <CustomTuiCalendar
        ref={childRef}
        {...{
          isReadOnly: false,
          showSlidebar: false,
          showMenu: true,
          useCreationPopup: false,
          calendars: formatCalendars,
          schedules,
          onBeforeCreateSchedule,
          onBeforeUpdateSchedule,
          onBeforeDeleteSchedule,
        }}
      />
      <CustomTuiModal
        {...{
          isOpen: modal,
          toggle,
          onSubmit:
            event?.triggerEventName === "mouseup"
              ? handleCreateSchedule
              : handleUpdateSchedule,
          submitText:
            event?.triggerEventName === "mouseup" ? "Kaydet" : "Güncelle",
          calendars: formatCalendars,
          schedule: event?.schedule,
          startDate: event?.start,
          endDate: event?.end,
        }}
      />
    </div>
  );
}

import React, { useEffect, useState } from "react";
import notificationSound from "../assets/notification.mp3";
import { ToastContainer, toast } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

const NotificationComponent = ({ schedules }) => {
  const [notificationState, setNotificationState] = useState(
    schedules.map((schedule) => ({
      ...schedule,
      hasNotified: false,
    }))
  );

  useEffect(() => {
    const audio = new Audio(notificationSound);

    const checkEvents = () => {
      const now = new Date();
      const newNotificationState = notificationState.map((schedule) => {
        if (schedule.start === null) return schedule;
        const startDate = new Date(schedule.start);
        if (
          startDate.getFullYear() === now.getFullYear() &&
          startDate.getMonth() === now.getMonth() &&
          startDate.getDate() === now.getDate() &&
          startDate.getHours() === now.getHours() &&
          startDate.getMinutes() === now.getMinutes() 
        ) {
          audio.play();
          setTimeout(() => {
            audio.pause();
            audio.currentTime = 0;
          }, 6000);
          toast(`${schedule.title} etkinliğinizin başlama tarihi ${startDate}'dır.`);
          return { ...schedule, hasNotified: true };
        }
        return schedule;
      });
      setNotificationState(newNotificationState);
    };

    const timerID = setInterval(() => {
      checkEvents();
    }, 30000);

    return () => {
      clearInterval(timerID);
    };
  }, [schedules, notificationState]);

  return (
    <div>
      <ToastContainer />  {/* Toast mesajlarını göstermek için eklenmiş bir ToastContainer */}
    </div>
  );
};

export default NotificationComponent;

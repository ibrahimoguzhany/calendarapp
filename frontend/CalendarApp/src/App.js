import React from "react";
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import Calendar from './components/Calendar';
import SignIn from './components/SignIn';
import SignUp from './components/SignUp';
import { APP_ROUTES } from './utils/constants';
import Dashboard from './components/Dashboard';
import AppNavbar from "./components/AppNavbar";

function App() {
  return (
    <BrowserRouter>
    <AppNavbar/>
      <Routes>
        <Route exact path="/" element={<Navigate to={APP_ROUTES.CALENDAR} />} />
        <Route path={APP_ROUTES.SIGN_UP} exact element={<SignUp />} />
        <Route path={APP_ROUTES.SIGN_IN} element={<SignIn />} />
        <Route path={APP_ROUTES.CALENDAR} element={<Calendar />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

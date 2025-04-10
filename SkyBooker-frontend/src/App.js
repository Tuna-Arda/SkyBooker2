import React from "react";
import { Routes, Route, Link } from "react-router-dom";

import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import FlightList from "./components/Flight/FlightList";
import FlightDetail from "./components/Flight/FlightDetail";
import FlightForm from "./components/Flight/FlightForm";
import BookingList from "./components/Booking/BookingList";
import BookingDetail from "./components/Booking/BookingDetail";
import BookingForm from "./components/Booking/BookingForm";

function App() {
  return (
    <div>
      <nav>
        <ul>
          <li><Link to="/flights">Flights</Link></li>
          <li><Link to="/flight/new">Add Flight</Link></li>
          <li><Link to="/bookings">Bookings</Link></li>
          <li><Link to="/booking/new">New Booking</Link></li>
          <li><Link to="/login">Login</Link></li>
          <li><Link to="/register">Register</Link></li>
        </ul>
      </nav>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/flights" element={<FlightList />} />
        <Route path="/flight/new" element={<FlightForm />} />
        <Route path="/flight/:id" element={<FlightDetail />} />
        <Route path="/bookings" element={<BookingList />} />
        <Route path="/booking/new" element={<BookingForm />} />
        <Route path="/booking/:id" element={<BookingDetail />} />
        <Route path="*" element={<FlightList />} />
      </Routes>
    </div>
  );
}

export default App;

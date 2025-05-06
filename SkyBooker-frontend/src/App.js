import React, { useContext } from "react";
import { Routes, Route, Link } from "react-router-dom";
import { AuthContext } from "./context/AuthContext";

import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import FlightList from "./components/Flight/FlightList";
import FlightDetail from "./components/Flight/FlightDetail";
import FlightForm from "./components/Flight/FlightForm";
import BookingList from "./components/Booking/BookingList";
import BookingDetail from "./components/Booking/BookingDetail";
import BookingForm from "./components/Booking/BookingForm";

function App() {
  const { token, user, logout } = useContext(AuthContext);

  return (
    <div>
      <nav>
        <ul>
          <li><Link to="/flights">Flights</Link></li>
          {token && <li><Link to="/flight/new">Add Flight</Link></li>}
          <li><Link to="/bookings">Bookings</Link></li>
          {token && <li><Link to="/booking/new">New Booking</Link></li>}
          {!token && <>
            <li><Link to="/login">Login</Link></li>
            <li><Link to="/register">Register</Link></li>
          </>}
          {token && <li><button onClick={logout} style={{ background: 'none', border: 'none', color: 'white', cursor: 'pointer', fontWeight: 'bold' }}>Logout</button></li>}
          {user && <li style={{ marginLeft: 'auto' }}>Welcome, {user.username}</li>}
        </ul>
      </nav>
      <div className="container">
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
    </div>
  );
}

export default App;

import React, { useState, useContext } from "react";
import { createBooking } from "../../services/bookingService";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";

const BookingForm = () => {
  const { token } = useContext(AuthContext);
  const navigate = useNavigate();
  const [form, setForm] = useState({
    FlightId: "",
    PassengerId: "",
    PassengerFirstname: "",
    PassengerLastname: "",
    TicketCount: 1
  });
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createBooking(form, token);
      navigate("/bookings");
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div>
      <h2>New Booking</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Flight ID:</label>
          <input name="FlightId" value={form.FlightId} onChange={handleChange} required />
        </div>
        <div>
          <label>Passenger ID:</label>
          <input name="PassengerId" value={form.PassengerId} onChange={handleChange} required />
        </div>
        <div>
          <label>First Name:</label>
          <input name="PassengerFirstname" value={form.PassengerFirstname} onChange={handleChange} required />
        </div>
        <div>
          <label>Last Name:</label>
          <input name="PassengerLastname" value={form.PassengerLastname} onChange={handleChange} required />
        </div>
        <div>
          <label>Ticket Count:</label>
          <input name="TicketCount" type="number" value={form.TicketCount} onChange={handleChange} required />
        </div>
        <button type="submit">Book Flight</button>
      </form>
    </div>
  );
};

export default BookingForm;

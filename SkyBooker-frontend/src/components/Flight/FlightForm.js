import React, { useState, useContext } from "react";
import { createFlight } from "../../services/flightService";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";

const FlightForm = () => {
  const { token } = useContext(AuthContext);
  const navigate = useNavigate();
  const [form, setForm] = useState({
    FlightId: "",
    AirlineName: "",
    Source: "",
    Destination: "",
    DepartureTime: "",
    ArrivalTime: "",
    AvailableSeats: 0
  });
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createFlight(form, token);
      navigate("/flights");
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h2>Add Flight</h2>
      {error && <p className="error">{error}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Flight ID:</label>
          <input name="FlightId" value={form.FlightId} onChange={handleChange} required />
        </div>
        <div>
          <label>Airline Name:</label>
          <input name="AirlineName" value={form.AirlineName} onChange={handleChange} required />
        </div>
        <div>
          <label>Source:</label>
          <input name="Source" value={form.Source} onChange={handleChange} required />
        </div>
        <div>
          <label>Destination:</label>
          <input name="Destination" value={form.Destination} onChange={handleChange} required />
        </div>
        <div>
          <label>Departure Time:</label>
          <input name="DepartureTime" type="datetime-local" value={form.DepartureTime} onChange={handleChange} required />
        </div>
        <div>
          <label>Arrival Time:</label>
          <input name="ArrivalTime" type="datetime-local" value={form.ArrivalTime} onChange={handleChange} required />
        </div>
        <div>
          <label>Available Seats:</label>
          <input name="AvailableSeats" type="number" value={form.AvailableSeats} onChange={handleChange} required />
        </div>
        <button type="submit">Add Flight</button>
      </form>
    </div>
  );
};

export default FlightForm;

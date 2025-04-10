import React, { useState, useContext } from "react";
import { createFlight } from "../../services/flightService";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";

const FlightForm = () => {
  const { token } = useContext(AuthContext);
  const navigate = useNavigate();
  const [form, setForm] = useState({
    flightId: "",
    airlineName: "",
    source: "",
    destination: "",
    departure_time: "",
    arrival_time: "",
    available_seats: 0
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
    <div>
      <h2>Add Flight</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Flight ID:</label>
          <input name="flightId" value={form.flightId} onChange={handleChange} required />
        </div>
        <div>
          <label>Airline Name:</label>
          <input name="airlineName" value={form.airlineName} onChange={handleChange} required />
        </div>
        <div>
          <label>Source:</label>
          <input name="source" value={form.source} onChange={handleChange} required />
        </div>
        <div>
          <label>Destination:</label>
          <input name="destination" value={form.destination} onChange={handleChange} required />
        </div>
        <div>
          <label>Departure Time:</label>
          <input name="departure_time" type="datetime-local" value={form.departure_time} onChange={handleChange} required />
        </div>
        <div>
          <label>Arrival Time:</label>
          <input name="arrival_time" type="datetime-local" value={form.arrival_time} onChange={handleChange} required />
        </div>
        <div>
          <label>Available Seats:</label>
          <input name="available_seats" type="number" value={form.available_seats} onChange={handleChange} required />
        </div>
        <button type="submit">Add Flight</button>
      </form>
    </div>
  );
};

export default FlightForm;

import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getFlightById } from "../../services/flightService";

const FlightDetail = () => {
  const { id } = useParams();
  const [flight, setFlight] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchFlight = async () => {
      try {
        const data = await getFlightById(id);
        setFlight(data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchFlight();
  }, [id]);

  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (!flight) return <p>Loading...</p>;

  return (
    <div>
      <h2>Flight Detail</h2>
      <p><strong>Flight ID:</strong> {flight.flightId}</p>
      <p><strong>Airline:</strong> {flight.airlineName}</p>
      <p><strong>Route:</strong> {flight.source} → {flight.destination}</p>
      <p><strong>Departure:</strong> {flight.departure_time}</p>
      <p><strong>Arrival:</strong> {flight.arrival_time}</p>
      <p><strong>Available Seats:</strong> {flight.available_seats}</p>
    </div>
  );
};

export default FlightDetail;

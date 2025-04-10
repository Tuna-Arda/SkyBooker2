import React, { useState, useEffect, useContext } from "react";
import { getFlights } from "../../services/flightService";
import { Link } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";

const FlightList = () => {
  const [flights, setFlights] = useState([]);
  const [error, setError] = useState(null);
  const { token } = useContext(AuthContext);

  useEffect(() => {
    const fetchFlights = async () => {
      try {
        const data = await getFlights();
        setFlights(data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchFlights();
  }, [token]);

  return (
    <div>
      <h2>Flights</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <ul>
        {flights.map(flight => (
          <li key={flight.id}>
            <Link to={`/flight/${flight.id}`}>
              {flight.flightId} - {flight.airlineName} : {flight.source} → {flight.destination}
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FlightList;

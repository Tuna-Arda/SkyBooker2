import React, { useState, useEffect } from "react";
import { getBookings } from "../../services/bookingService";
import { Link } from "react-router-dom";

const BookingList = () => {
  const [bookings, setBookings] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBookings = async () => {
      try {
        const data = await getBookings();
        setBookings(data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchBookings();
  }, []);

  return (
    <div>
      <h2>Bookings</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <ul>
        {bookings.map(booking => (
          <li key={booking.Id}>
            <Link to={`/booking/${booking.Id}`}>
              {booking.PassengerFirstname} {booking.PassengerLastname} - {booking.TicketCount} Ticket(s)
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BookingList;

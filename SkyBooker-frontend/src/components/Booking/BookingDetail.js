import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getBookingById } from "../../services/bookingService";

const BookingDetail = () => {
  const { id } = useParams();
  const [booking, setBooking] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBooking = async () => {
      try {
        const data = await getBookingById(id);
        setBooking(data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchBooking();
  }, [id]);

  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (!booking) return <p>Loading...</p>;

  return (
    <div>
      <h2>Booking Detail</h2>
      <p><strong>Flight ID:</strong> {booking.FlightId}</p>
      <p><strong>Passenger:</strong> {booking.PassengerFirstname} {booking.PassengerLastname}</p>
      <p><strong>Ticket Count:</strong> {booking.TicketCount}</p>
    </div>
  );
};

export default BookingDetail;

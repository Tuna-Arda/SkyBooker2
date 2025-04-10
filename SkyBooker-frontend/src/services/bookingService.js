const API_BASE = "http://localhost:5002/api";

export const getBookings = async () => {
  const response = await fetch(`${API_BASE}/booking`, {
    headers: {
      "Content-Type": "application/json"
    }
  });
  if (!response.ok) throw new Error("Error fetching bookings");
  return response.json();
};

export const getBookingById = async (id) => {
  const response = await fetch(`${API_BASE}/booking/${id}`, {
    headers: {
      "Content-Type": "application/json"
    }
  });
  if (!response.ok) throw new Error("Error fetching booking");
  return response.json();
};

export const createBooking = async (bookingData, token) => {
  const response = await fetch(`${API_BASE}/booking`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(bookingData)
  });
  if (!response.ok) throw new Error("Error creating booking");
  return response.json();
};

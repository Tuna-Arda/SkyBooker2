const API_BASE = "http://localhost:5001/api";

export const getFlights = async () => {
  const response = await fetch(`${API_BASE}/flight`, {
    headers: {
      "Content-Type": "application/json"
    }
  });
  if (!response.ok) throw new Error("Error fetching flights");
  return response.json();
};

export const getFlightById = async (id) => {
  const response = await fetch(`${API_BASE}/flight/${id}`, {
    headers: {
      "Content-Type": "application/json"
    }
  });
  if (!response.ok) throw new Error("Error fetching flight");
  return response.json();
};

export const createFlight = async (flightData, token) => {
  const response = await fetch(`${API_BASE}/flight`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(flightData)
  });
  if (!response.ok) throw new Error("Error creating flight");
  return response.json();
};

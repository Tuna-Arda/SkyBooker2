// When running in Docker, we need to use the gateway container name instead of localhost
const API_BASE = process.env.REACT_APP_API_URL || "http://localhost:8080/api";

export const getFlights = async () => {
  // Get token from localStorage if available
  const token = localStorage.getItem('token');
  
  const headers = {
    "Content-Type": "application/json"
  };
  
  // Add authorization header if token exists
  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }
  
  const response = await fetch(`${API_BASE}/flight`, {
    headers
  });
  
  if (!response.ok) throw new Error("Error fetching flights");
  return response.json();
};

export const getFlightById = async (id) => {
  // Get token from localStorage if available
  const token = localStorage.getItem('token');
  
  const headers = {
    "Content-Type": "application/json"
  };
  
  // Add authorization header if token exists
  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }
  
  const response = await fetch(`${API_BASE}/flight/${id}`, {
    headers
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

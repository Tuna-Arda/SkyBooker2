const API_BASE = "http://localhost:5000/api";

export const loginUser = async (username, password) => {
  const response = await fetch(`${API_BASE}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ username, password })
  });
  if (!response.ok) throw new Error("Login failed");
  return response.json();
};

export const registerUser = async (username, email, password) => {
  const response = await fetch(`${API_BASE}/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ username, email, password })
  });
  if (!response.ok) throw new Error("Registration failed");
  return response.json();
};

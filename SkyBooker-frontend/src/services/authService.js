const API_BASE = process.env.REACT_APP_API_URL;

export const loginUser = async (username, password) => {
  const response = await fetch(`${API_BASE}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Accept": "application/json"
    },
    body: JSON.stringify({ username, password })
  });
  
  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || "Login failed");
  }
  
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

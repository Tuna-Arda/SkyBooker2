# SkyBooker

SkyBooker is a flight booking application with a microservices architecture.

## Architecture

The application consists of the following components:

### Backend Services (C#)
- **AuthService** - User authentication and registration
- **FlightService** - Flight management
- **BookingService** - Booking management
- **Gateway** - API Gateway that routes requests to appropriate services

### Frontend (React)
- React application for user interface

## Running with Docker

The easiest way to run the entire application is using Docker Compose:

```bash
# Navigate to the root directory
cd SkyBooker

# Build and start all services
docker-compose up -d --build

# To view logs
docker-compose logs -f

# To stop all services
docker-compose down
```

After starting the containers:
- Frontend will be available at: http://localhost:3000
- Gateway API will be available at: http://localhost:8080

## API Endpoints

### Auth Service
- POST /api/register - Register a new user
- POST /api/login - Login and get JWT token

### Flight Service
- GET /api/flight - Get all flights
- GET /api/flight/{id} - Get flight by ID
- POST /api/flight - Create a new flight (requires authentication)
- PUT /api/flight/{id} - Update a flight (requires authentication)
- DELETE /api/flight/{id} - Delete a flight (requires authentication)

### Booking Service
- GET /api/booking - Get all bookings
- GET /api/booking/{id} - Get booking by ID
- POST /api/booking - Create a new booking (requires authentication)
- PUT /api/booking/{id} - Update a booking (requires authentication)
- DELETE /api/booking/{id} - Delete a booking (requires authentication)

## Development

For local development without Docker:

### Backend

1. Open the solution in Visual Studio
2. Set multiple startup projects:
   - AuthService
   - FlightService
   - BookingService
   - Gateway
3. Run the solution

### Frontend

1. Navigate to the frontend directory:
   ```
   cd SkyBooker-frontend
   ```
2. Install dependencies:
   ```
   npm install
   ```
3. Start the development server:
   ```
   npm start
   ```
4. Access the application at http://localhost:3001
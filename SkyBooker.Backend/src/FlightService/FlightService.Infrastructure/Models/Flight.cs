using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightService.Infrastructure.Models
{
    public class Flight
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FlightId { get; set; }
        public string AirlineName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

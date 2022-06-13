
namespace HWWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int TicketID { get; set; }
        public int TripID { get; set; }
        public int PassengerID { get; set; }
        public int Seat { get; set; }
        public string AvailabilityBenefits { get; set; }
        public int Price { get; set; }
    
        public virtual Passenger Passenger { get; set; }
        public virtual Trip Trip { get; set; }
    }
}

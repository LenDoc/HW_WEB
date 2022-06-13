
namespace HWWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trip()
        {
            this.Ticket = new HashSet<Ticket>();
        }
    
        public int TripID { get; set; }
        public int StationID { get; set; }
        public int TrainID { get; set; }
        public string Name { get; set; }
        public int Distance { get; set; }
        public System.DateTime DatetimeDeparture { get; set; }
        public Nullable<System.DateTime> DatetimeArrival { get; set; }
    
        public virtual Station Station { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual Train Train { get; set; }
    }
}

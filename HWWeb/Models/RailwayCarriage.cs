
namespace HWWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RailwayCarriage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RailwayCarriage()
        {
            this.CompositionWagons = new HashSet<CompositionWagons>();
        }
    
        public int RailwayCarriageID { get; set; }
        public string Type { get; set; }
        public int NumberSeats { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompositionWagons> CompositionWagons { get; set; }
    }
}

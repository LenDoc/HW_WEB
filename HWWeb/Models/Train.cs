

namespace HWWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Train
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Train()
        {
            this.Trip = new HashSet<Trip>();
        }
    
        public int TrainID { get; set; }
        public int CompositionWagonsID { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }
    
        public virtual CompositionWagons CompositionWagons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trip> Trip { get; set; }
    }
}


namespace HWWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompositionWagons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompositionWagons()
        {
            this.Train = new HashSet<Train>();
        }
    
        public int CompositionWagonsID { get; set; }
        public int RailwayCarriageID { get; set; }
        public int Number { get; set; }
    
        public virtual RailwayCarriage RailwayCarriage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Train> Train { get; set; }
    }
}

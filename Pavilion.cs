//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pavilion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pavilion()
        {
            this.Rents = new HashSet<Rents>();
        }
    
        public string Pavilion_Number { get; set; }
        public Nullable<int> Floor { get; set; }
        public string Status_pavilion { get; set; }
        public Nullable<double> Square { get; set; }
        public Nullable<double> Cost_per_square_meter { get; set; }
        public Nullable<double> Added_value_factor { get; set; }
        public int Shopping_center_ID { get; set; }
    
        public virtual Shopping_center Shopping_center { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MengajiOneToOneSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClassRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassRecord()
        {
            this.Class_Package = new HashSet<Class_Package>();
        }
    
        public string class_id { get; set; }
        public string u_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class_Package> Class_Package { get; set; }
        public virtual User User { get; set; }
    }
}

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
    
    public partial class PerformanceReport
    {
        public Nullable<int> p_month { get; set; }
        public string u_id { get; set; }
        public string student_performance { get; set; }
        public Nullable<System.DateTime> class_date { get; set; }
        public string class_ref { get; set; }
        public string p_status { get; set; }
        public int p_id { get; set; }
    
        public virtual User User { get; set; }
    }
}

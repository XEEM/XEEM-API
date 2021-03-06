//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XeemAPI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerTransportation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerTransportation()
        {
            this.MaintainanceHistories = new HashSet<MaintainanceHistory>();
            this.Requests = new HashSet<Request>();
        }
    
        public int userId { get; set; }
        public int transId { get; set; }
        public System.DateTime createdDate { get; set; }
        public int id { get; set; }
    
        public virtual Transportation Transportation { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintainanceHistory> MaintainanceHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
    }
}

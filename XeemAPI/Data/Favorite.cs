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
    
    public partial class Favorite
    {
        public int userId { get; set; }
        public int shopId { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
    
        public virtual Shop Shop { get; set; }
        public virtual User User { get; set; }
    }
}
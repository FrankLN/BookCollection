//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookCollection.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class linkAspNetUsersBooklists
    {
        public int linkAspNetUsersBooklistsId { get; set; }
        public string aspNetUserId { get; set; }
        public Nullable<int> booklistId { get; set; }
    
        public virtual Booklists Booklists { get; set; }
    }
}

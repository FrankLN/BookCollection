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
    
    public partial class BooklistDetailView
    {
        public string Id { get; set; }
        public int booklistId { get; set; }
        public string booklistName { get; set; }
        public Nullable<int> bookId { get; set; }
        public string bookName { get; set; }
        public string bookPublishYear { get; set; }
        public string bookEANNumber { get; set; }
        public Nullable<bool> bookOwned { get; set; }
    }
}

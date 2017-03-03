using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookCollection.Models
{
    public class BookCollectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBooks { get; set; }
        public List<Book> Books {get;set;}

        public void calculateNumberOfBooks()
        {
            NumberOfBooks = Books.Count();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishYear { get; set; }
    }
}
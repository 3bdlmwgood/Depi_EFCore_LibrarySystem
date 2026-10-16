using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AuthorId { get; set; }

        public Author Author { get; set; } = null;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}

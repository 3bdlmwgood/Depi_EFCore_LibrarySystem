
using LibrarySystem.Data;
using LibrarySystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new AppDbContext();

            #region Create

            var author = new Author
            {
                Name = "Naguib Mahfouz",
                BirthDate = new DateTime(1911, 12, 11),

                Books = new List<Book>
                {
                    new Book
                    {
                        Title = "Palace Walk",
                        ISBN = "9789774160010"
                    },

                    new Book
                    {
                        Title = "Miramar",
                        ISBN = "9789774160027"
                    }
                }
            };

            var borrower = new Borrower
            {
                Name = "Abdallah",
                MembershipDate = DateTime.Today
            };

            dbContext.Authors.Add(author);
            dbContext.Borrowers.Add(borrower);

            dbContext.SaveChanges();

            #endregion

            #region Create Loan

            var book = dbContext.Books.FirstOrDefault(b => b.Title == "Palace Walk");

            if (book != null)
            {
                var loan = new Loan
                {
                    BookId = book.Id,
                    BorrowerId = borrower.Id,
                    LoanDate = DateTime.Today,
                    ReturnDate = null
                };

                dbContext.Loans.Add(loan);
                dbContext.SaveChanges();

                Console.WriteLine("Loan created successfully.");
            }

            #endregion

            #region Read

            var books = dbContext.Books
                .Include(b => b.Author)
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"Book: {b.Title}");
                Console.WriteLine($"ISBN: {b.ISBN}");
                Console.WriteLine($"Author: {b.Author.Name}");
                Console.WriteLine("----------------------");
            }

            #endregion

            #region Read Loans

            var loans = dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .ToList();

            foreach (var loan in loans)
            {
                Console.WriteLine($"Book: {loan.Book.Title}");
                Console.WriteLine($"Borrower: {loan.Borrower.Name}");
                Console.WriteLine($"Loan Date: {loan.LoanDate:d}");

                Console.WriteLine($"Return Date: {loan.ReturnDate?.ToString("d") ?? "Not returned"}");

                Console.WriteLine("----------------------");
            }

            #endregion
        }
    }
}

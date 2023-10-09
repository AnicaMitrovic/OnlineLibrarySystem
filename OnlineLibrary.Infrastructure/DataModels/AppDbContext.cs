using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Infrastructure.DataModels
{
    public class AppDbContext
    {
        public List<Book> BookList
        {
            get
            {
                return _bookList;
            }
            set
            {
                _bookList = value;
            }
        }

        public List<User> Users
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList = value;
            }
        }

        private static List<Book> _bookList = new List<Book>
        {
            new Book {Id = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", Publisher = "J.B. Lippincott"},
            new Book {Id = 2, Title = "1984", Author = "George Orwell", Publisher = "Secker & Warburg"},
            new Book {Id = 3, Title = "Moby Dick", Author = "Herman Melville", Publisher = "Harper & Brothers"},
            new Book {Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Publisher = "T. Egerton"},
            new Book {Id = 5, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Publisher = "Charles Scribner's Sons"},
            new Book {Id = 6, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Publisher = "Little, Brown and Company"},
            new Book {Id = 7, Title = "War and Peace", Author = "Leo Tolstoy", Publisher = "The Russian Messenger"},
            new Book {Id = 8, Title = "The Hobbit", Author = "J.R.R. Tolkien", Publisher = "George Allen & Unwin"},
            new Book {Id = 9, Title = "Brave New World", Author = "Aldous Huxley", Publisher = "Chatto & Windus"},
            new Book {Id = 10, Title = "The Chronicles of Narnia", Author = "C.S. Lewis", Publisher = "Geoffrey Bles"}
        };

        private static List<User> _userList = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin" }
        };
    }
}

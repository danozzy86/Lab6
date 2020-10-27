using System;
using System.Collections.Generic;

namespace Lab6
{
    class Program
    {
        public static bool Exit = false;//Exit parameter for the while loop in main.

        static void Main(string[] args)
        {
            while (!Exit)
            {
                MainMenu();

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //View all books.
                        ViewAllBooks();
                        break;
                    case 2:
                        //Withdraw a book.

                        break;
                    case 3:
                        //Reserves a book.
                        break;
                    case 4:
                        //Returns a book.
                        break;
                    case 5:
                        //Adds a book to the catalog.
                        break;
                    case 6:
                        //Removes book from the catalog.
                        break;
                    case 7:
                        //Exits the program.
                        Exit = true;
                        break;
                }
            }

        }

        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("             Library Catalog               ");
            Console.WriteLine("===========================================");
            Console.WriteLine("Welcome to the Library Catalog! What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) View All Books");
            Console.WriteLine("2) Withdraw a Book");
            Console.WriteLine("3) Reserve a Book");
            Console.WriteLine("4) Return a Book");
            Console.WriteLine("5) Add a Book to the Library Catalog");
            Console.WriteLine("6) Remove a Book from the Library Catalog");
            Console.WriteLine("7) Exit");
        }

        public static void ViewAllBooks()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("               View All Books              ");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("--------------Books in Library-------------");

            foreach (Book b in Book.BooksInLibrary)
            {
                Console.WriteLine("{0} {1} {2} {3}", Book.BooksInLibrary.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
            }

            Console.WriteLine();
            Console.WriteLine("--------------Books Withdrawn--------------");

            foreach (Book b in Book.BooksWithdrawn)
            {
                Console.WriteLine("{0} {1} {2} {3}", Book.BooksWithdrawn.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
            }

            Console.WriteLine();
            Console.WriteLine("--------------Books on Reserve-------------");

            foreach (Book b in Book.BooksOnReserve)
            {
                Console.WriteLine("{0} {1} {2} {3}", Book.BooksOnReserve.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    class Book
    {
        //Setting accessors and default values for the constructor
        public string Name
        {
            get
            {
                return Name;
            }
            private set
            {
                Name = "null";
            }
        }
        public string ISBN13
        {
            get
            {
                return ISBN13;
            }
            private set
            {
                ISBN13 = "0000000000000";
            }
        }
        public int PageCount
        {
            get
            {
                return PageCount;
            }
            private set
            {
                PageCount = -1;
            }
        }
        //Lists of Books
        public static List<Book> BooksInLibrary = new List<Book>();
        public static List<Book> BooksWithdrawn = new List<Book>();
        public static List<Book> BooksOnReserve = new List<Book>();

        public Book(string name, string isbn13, int pageCount)
        {
            Name = name;
            ISBN13 = isbn13;
            PageCount = pageCount;

            BooksInLibrary.Add(this);
        }

        public static void AddBook()
        {
            //The user is prompted to input a book title, the isbn-13 numbers, and the page numbers of the book, input is then checked.
            //If the input is valid, the book is added to the Library list.
        }

        public static void RemoveBook()
        {
            //You can only remove books in the library!!!
            //Print out all available books from the library list with the index numbers.
            //Book selected by the user is removed from the library catalog.
        }

        public static void WithdrawBook()
        {
            //Is the book in the library or on reserve?
            //Print out list of books from specified list with the index numbers.
            //The specified book is then moved from the library list to the withdrawn list.
        }

        public static void ReserveBook()
        {
            //Print out the list of available books from the library list with index numbers.
            //User selects the index and that book is moved from the library to the reserve list.
        }

        public static void ReturnBook()
        {
            //Is the book withdrawn or on reserve?
            //Print out list of books with the list index value, user selects the index value to choose which book to return.
            //Remove book from withdrawn/reserve list and add back to library list.
        }
    }



}

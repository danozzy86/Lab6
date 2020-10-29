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
                string tempName;
                string tempISBN13;
                int tempPageCount;

                Menu();
                int choice = Sanitize(Convert.ToString(Console.ReadLine()), 7, 1);

                switch (choice)
                {
                    case 1:
                        //View all books.
                        Menu(1);
                        ViewBooks();

                        break;
                    case 2:
                        //Withdraw a book.
                        Menu(2);
                        Console.WriteLine("- Is the book being withdrawn in the library or on reserve?");
                        Console.WriteLine("1) In the Library");
                        Console.WriteLine("2) On Reserve");  
                        choice  = Sanitize(Convert.ToString(Console.ReadLine()), 2, 1);

                        switch(choice){
                            case 1:
                                //Withdraw a book from the library.
                                Menu(2);
                                ViewBooks(1);

                                Console.WriteLine("Which book would you like to withdraw (use the index #)");
                                choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksInLibrary));

                                Book.WithdrawBook(choice, true);

                                break;
                            case 2:
                                //Withdraw a book that is on reserve.
                                Menu(2);
                                ViewBooks(3);

                                Console.WriteLine("Which book would you like to withdraw (use the index #)");
                                choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksOnReserve));

                                Book.WithdrawBook(choice, false);

                                break;
                        }
                        break;
                    case 3:
                        //Reserves a book.
                        Menu(3);
                        ViewBooks(1);

                        Console.WriteLine("Which book would you like to put on reserve?");
                        choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksInLibrary));

                        Book.ReserveBook(choice);

                        break;
                    case 4:
                        //Returns a book.
                        Menu(4);
                        Console.WriteLine("Is the book being returned withdrawn or on reserve?");
                        Console.WriteLine("1) Withdrawn");
                        Console.WriteLine("2) On Reserve");
                        choice = Sanitize(Convert.ToString(Console.ReadLine()), 2, 1);

                        switch(choice){
                            case 1:
                                //Book being returned is withdrawn.
                                Menu(4);
                                ViewBooks(2);

                                Console.WriteLine("Which book would you like to return?");
                                choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksWithdrawn));

                                Book.ReturnBook(choice, false);

                                break;
                            case 2:
                                //Book being returned is on the reserve list.
                                Menu(4);
                                ViewBooks(3);

                                Console.WriteLine("Which book would you like to return?");
                                choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksOnReserve));

                                Book.ReturnBook(choice, true);

                                break;
                        }             
                        break;
                    case 5:
                        //Adds a book to the catalog.
                        Menu(5);
                        
                        Console.WriteLine("- What is the name of the book?");
                        tempName = Sanitize(Convert.ToString(Console.ReadLine()), false);
                        
                        Console.WriteLine("- How many pages does the book have?");
                        tempPageCount = Sanitize(Convert.ToString(Console.ReadLine()), 15000, 1);

                        Console.WriteLine("- What is the ISBN13 number of the book?");
                        tempISBN13 = Sanitize(Convert.ToString(Console.ReadLine()), true);

                        Book.AddBook(tempName, tempISBN13, tempPageCount);

                        break;
                    case 6:
                        //Removes book from the catalog.
                        Menu(6);
                        ViewBooks(1);

                        Console.WriteLine("What book would you like to remove from the catalog?");
                        choice = Sanitize(Convert.ToString(Console.ReadLine()), ListLength(Book.BooksInLibrary));

                        Book.RemoveBook(choice);

                        break;
                    case 7:
                        //Exits the program.
                        Exit = true;
                        break;
                }
            }
        }
        private static int Sanitize(string usrInput, int max = 100, int min = 0){
            //Sanitizes numeral input including user choice and page count.
            bool valid = false;
            int intInput = -1;
            
            while(!valid){
                if(!int.TryParse(usrInput, out int temp) || temp < min || temp > max){
                    Console.WriteLine("Enter a valid input");
                    usrInput = Console.ReadLine();
                    valid = false;
                }else{
                    valid = true;
                    intInput = temp;
                }
            }
            return intInput;
        }

        private static string Sanitize(string usrInput, bool isbn13 = false){
            //Sanitizes certain string input, mainly making sure that the ISBN13 number is valid since having it as an int would use more RAM.
            if(isbn13){
                while(int.TryParse(usrInput, out int temp) || usrInput.Length != 13){
                    Console.WriteLine("The ISBN13 number must be 13 numbers long.");
                    usrInput = Convert.ToString(Console.ReadLine());
                }
            }else{
                //The longest book title is 26000 characters but that is silly.
                while(usrInput.Length > 1000){
                    Console.WriteLine("Invalid title, too long.");
                    usrInput = Convert.ToString(Console.ReadLine());
                }
            }
            return usrInput;
        }

        private static int ListLength(List<Book> List){
            int listLength = -1;

            foreach(Book b in List){
                listLength++;
            }
            return listLength;
        }

        private static void ViewBooks(int choice = 0){
            switch(choice){
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("--------------Books in Library-------------");

                    foreach (Book b in Book.BooksInLibrary)
                    {
                        Console.WriteLine("{0} (Title){1} (PageCount){2} (ISBN13){3}", Book.BooksInLibrary.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
                    }
                    Console.WriteLine();

                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("--------------Books Withdrawn--------------");

                    foreach (Book b in Book.BooksWithdrawn)
                    {
                        Console.WriteLine("{0} (Title){1} (PageCount){2} (ISBN13){3}", Book.BooksWithdrawn.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
                    }
                    Console.WriteLine();

                    break;
                case 3:
                    Console.WriteLine();
                    Console.WriteLine("--------------Books on Reserve-------------");

                    foreach (Book b in Book.BooksOnReserve)
                    {
                        Console.WriteLine("{0} (Title){1} (PageCount){2} (ISBN13){3}", Book.BooksOnReserve.IndexOf(b), b.Name, b.PageCount, b.ISBN13);
                    }
                    Console.WriteLine();

                    break;
                default:
                    ViewBooks(1);
                    ViewBooks(2);
                    ViewBooks(3);   
                    Console.WriteLine(); 
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    break;
            }
        }
        private static void Menu(int choice = 0){
            switch(choice){
                case 1:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("               View All Books              ");
                    Console.WriteLine("===========================================");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("             Withdraw a Book               ");
                    Console.WriteLine("===========================================");
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("             Reserve a Book                ");
                    Console.WriteLine("===========================================");
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("              Return a Book                ");
                    Console.WriteLine("===========================================");
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("     Add a Book to the Library Catalog     ");
                    Console.WriteLine("===========================================");
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("===========================================");
                    Console.WriteLine("   Remove a book from the Library Catalog  ");
                    Console.WriteLine("===========================================");
                    break;
                default:
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
                    break;
            }
        }
    }

    class Book
    {
        //Setting accessors and default values for the constructor
        public string Name = "null";
        public string ISBN13 = "0000000000000";
        public int PageCount = -1;
        
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

        public static void AddBook(string name, string isbn13, int pageCount)
        {
            //The user is prompted to input a book title, the isbn-13 numbers, and the page numbers of the book, input is then checked.
            //If the input is valid, the book is added to the Library list.
            Book b = new Book(name, isbn13, pageCount);
        }

        public static void RemoveBook(int bookIndex)
        {
            //You can only remove books in the library!!!
            //Print out all available books from the library list with the index numbers.
            //Book selected by the user is removed from the library catalog.
            BooksInLibrary.RemoveAt(bookIndex);
        }

        public static void WithdrawBook(int bookIndex, bool inLibrary)
        {
            //Is the book in the library or on reserve?
            //Print out list of books from specified list with the index numbers.
            //The specified book is then moved from the library list to the withdrawn list.
            
            if(inLibrary){
                BooksWithdrawn.Add(BooksInLibrary[bookIndex]);
                BooksInLibrary.RemoveAt(bookIndex);
            } else {
                BooksWithdrawn.Add(BooksOnReserve[bookIndex]);
                BooksOnReserve.RemoveAt(bookIndex);
            }   
        }

        public static void ReserveBook(int bookIndex)
        {
            //Print out the list of available books from the library list with index numbers.
            //User selects the index and that book is moved from the library to the reserve list.
            
            BooksOnReserve.Add(BooksInLibrary[bookIndex]);
            BooksInLibrary.RemoveAt(bookIndex);
        }

        public static void ReturnBook(int bookIndex, bool onReserve)
        {
            //Is the book withdrawn or on reserve?
            //Print out list of books with the list index value, user selects the index value to choose which book to return.
            //Remove book from withdrawn/reserve list and add back to library list.

            if(onReserve) {
                BooksInLibrary.Add(BooksOnReserve[bookIndex]);
                BooksOnReserve.RemoveAt(bookIndex);
            } else {
                BooksInLibrary.Add(BooksWithdrawn[bookIndex]);
                BooksWithdrawn.RemoveAt(bookIndex);
            }
        }
    }



}

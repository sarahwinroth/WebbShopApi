using System;
using System.Collections.Generic;
using WebbShopApi.Controllers;
using WebbShopApi.Models;

namespace WebbShopApi
{
    public class UserAPI
    {
        private static readonly ApiController api = new ApiController();
        /// <summary>
        /// Själva fronten för när användaren ska köpa en bok. 
        /// Användaren anger titeln på den bok som skall inköpas som sedan skickas in i en metod som först hämtar information om boken för att visa för användaren. 
        /// Vid köp så anropas en metod som kopierar boken till SoldBooks.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserBuyBook(int userId)
        {
            Console.Clear();
            bool keepBuying = true;
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[VILKEN BOK VILL DU KÖPA?]\n");
                Console.WriteLine("Ange titeln på boken du vill köpa:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                while (keepBuying)
                {
                    Console.Clear();
                    Console.WriteLine("[BOKEN SOM MATCHAR DIN SÖKNING]\n");
                    int bookId = api.GetBookId(userInput);
                    if (bookId != 0)
                    {
                        Book book = api.GetBook(bookId);
                        Console.WriteLine(book.ToString());
                        Console.WriteLine("\nTryck [ENTER] för att slutföra köpet eller [Q] för att gå tillbaka.");
                        userInput = Console.ReadLine();

                        if (userInput.Trim().ToLower() == "q")
                        {
                            keepBuying = false;
                        }
                        else
                        {
                            bool purchased = api.BuyBook(userId, bookId);
                            if (purchased)
                            {
                                Console.WriteLine("Köpet genomfört!");
                            }
                            else
                            {
                                Console.WriteLine("Köpet INTE genomfört!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingen bok matchade din sökning..");
                    }
                    Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                    Console.ReadLine();
                    keepBuying = false;
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska se alla tillgängliga böcker utifrån en kategori. 
        /// Användaren anger namnet på den kategori som användaren vill se böcker ifrån.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserGetAllAvailebleBooks(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[HÄMTA TILLGÄNGLIGA BÖCKER EFTER KATEGORI]\n");
                Console.WriteLine("Sök på kategori nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[ALLA TILLGÄNGLIGA BÖCKER I KATEGORI]\n");
                int categoryId = api.GetCategoryId(userInput);
                if (categoryId != 0)
                {
                    List<Book> books = api.GetAvailableBooks(categoryId);
                    API.PrintList(books);
                }
                else
                {
                    Console.WriteLine("Ingen bok matchade din sökning..");
                }
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska se alla kategorier som finns i databsen. 
        /// Metoden visar information och feedback, samt listar kategorierna.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserGetCategories(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[ALLA KATEGORIER]\n");
                List<Category> categories = api.GetCategories();
                API.PrintList(categories);
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska se information om en bok. 
        /// Användaren matar in titel på den bok hen vill se info om.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserGetInfoAboutBook(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK EFTER EN BOK]\n");
                Console.WriteLine("Ange titeln på boken du söker:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[BOKEN SOM MATCHAR DIN SÖKNING]\n");
                int bookId = api.GetBookId(userInput);
                if (bookId != 0)
                {
                    Book book = api.GetBook(bookId);
                    Console.WriteLine(book.ToString());
                }
                else
                {
                    Console.WriteLine("Ingen bok matchade din sökning..");
                }
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska söka efter böcker. 
        /// Användaren matar in en sträng och metoden anropar en annan metod som söker i databasen efter böcker vars titel innehåller den sträng som användaren matat in.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserSearchBooks(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK EFTER BÖCKER]\n");
                Console.WriteLine("Gör din sökning nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[ALLA BÖCKER MATCHANDE DIN SÖKNING]\n");
                List<Book> books = api.GetBooks(userInput);
                API.PrintList(books);
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska söka efter böcker utifrån en författare. 
        /// Användaren matar in en sträng och metoden anropar en annan metod som söker i databasen efter böcker vars författare innehåller den sträng som användaren matat in.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserSearchBooksFromAuthor(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK BÖCKER EFTER FÖRFATTARE]\n");
                Console.WriteLine("Gör din sökning nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[ALLA BÖCKER MATCHANDE DIN SÖKNING]\n");
                List<Book> books = api.GetAuthors(userInput);
                API.PrintList(books);
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska söka efter böcker utifrån kategori. 
        /// Användaren matar in en sträng och metoden anropar en annan metod som söker i databasen efter kategorier som heter liknande det användaren matat in. 
        /// Id från kategorin returneras som i sin tur skickas vidare i en annan metod som hämtar alla böcker med det Kategori-Id:t.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserSearchBooksFromCategory(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK BÖCKER EFTER KATEGORI]\n");
                Console.WriteLine("Gör din sökning nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[ALLA BÖCKER MATCHANDE DIN SÖKNING]\n");
                int categoryId = api.GetCategoryId(userInput);
                if (categoryId != 0)
                {
                    List<Book> books = api.GetCategory(categoryId);
                    API.PrintList(books);
                }
                else
                {
                    Console.WriteLine("Ingen bok matchade din sökning..");
                }
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren ska söka efter kategorier. 
        /// Användaren matar in en sträng och metoden anropar en annan metod som söker i databasen efter kategorier vars namn innehåller den sträng som användaren matat in.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserSearchCategory(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK KATEGORI]\n");
                Console.WriteLine("Gör din sökning nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[ALLA KATEGORIER MATCHANDE DIN SÖKNING]\n");
                List<Category> categories = api.GetCategories(userInput);
                API.PrintList(categories);
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren uppdaterar sidan. 
        /// Metoden ger information och feedback till användaren.  
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void UserUpdateSite(int userId)
        {
            string message = api.Ping(userId);
            if (message != string.Empty)
            {
                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine("Du är fortfarande ansluten och sidan har uppdaterats.");
                Console.WriteLine("\nTryck [ENTER] för att gå tillbaka till menyn");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
    }
}
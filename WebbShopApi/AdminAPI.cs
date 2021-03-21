using System;
using System.Collections.Generic;
using WebbShopApi.Controllers;
using WebbShopApi.Models;

namespace WebbShopApi
{
    public class AdminAPI
    {
        private static readonly ApiController api = new ApiController();
        /// <summary>
        /// Själva fronten för när användaren/administratören lägger till en bok och lägger värdena in i variablar som skickas med i metoden som lägger in boken i databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminAddBook(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[LÄGG TILL EN BOK]\n");
                Console.Write("Titel: ");
                string title = Console.ReadLine();
                Console.Write("Författare: ");
                string author = Console.ReadLine();
                Console.Write("Pris: ");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Antalet böcker som läggs till: ");
                int amount = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nTryck [ENTER] för att lägga till boken.");
                Console.ReadLine();
                bool bookAdded = api.AddBook(userId, title, author, price, amount);
                if (bookAdded)
                {
                    Console.WriteLine("Boken är tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och boken är INTE tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören lägger till en kategori till en bok. Värdena läggs in i variablar som skickas med i metoden som ger boken en kategori i databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminAddBookToCategory(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[GE EN BOK EN KATEGORI]\n");
                Console.Write("Titel på boken: ");
                string titel = Console.ReadLine();
                Console.Write("Kateorin som ska läggas till: ");
                string category = Console.ReadLine();
                int bookId = api.GetBookId(titel);
                int categoryId = api.GetCategoryId(category);
                Console.WriteLine("\nTryck [ENTER] för att lägga till kategorin.");
                Console.ReadLine();
                if (bookId != 0 && categoryId != 0)
                {
                    bool categoryAdded = api.AddBookToCategory(userId, bookId, categoryId);
                    if (categoryAdded)
                    {
                        Console.WriteLine("Kategorin är tillagd!");
                        Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel och kategorin är INTE tillagd!");
                        Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Kategorin eller boken finns inte.");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören lägger till en kategori i databasen. 
        /// Värden läggs in i variabler som skickas in i metoden som lägger till kategorin i databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminAddCategory(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[LÄGG TILL EN KATEGORI]\n");
                Console.Write("Namn: ");
                string name = Console.ReadLine();

                Console.WriteLine("\nTryck [ENTER] för att lägga till kategorin.");
                Console.ReadLine();
                bool categoryAdded = api.AddCategory(userId, name);
                if (categoryAdded)
                {
                    Console.WriteLine("Kategorin är tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och kategorin är INTE tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören uppgraderar en användare till administratör i databasen. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter användaren i databasen och ändrar IsAdmin till true.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void PromoteUser(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[UPPGRADERA ANVÄNDARE]\n");
                Console.WriteLine("Vem vill du uppgradera till administratör?");
                Console.Write("Användarnamn: ");
                string name = Console.ReadLine();

                int promoteUserId = api.GetUserId(name);

                Console.WriteLine("\nTryck [ENTER] för att uppgradera användaren.");
                Console.ReadLine();
                bool userPromoted = api.Promote(userId, promoteUserId);
                if (userPromoted)
                {
                    Console.WriteLine("Användaren är uppgraderad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och användaren är INTE uppgraderad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören nedgraderar en användare från administratör till vanlig användare i databasen. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter användaren i databasen och ändrar IsAdmin till false.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void DemoteUser(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[NEDGRADERA ANVÄNDARE]\n");
                Console.WriteLine("Vilken användare vill du ta bort administratörsåtkomst?");
                Console.Write("Användarnamn: ");
                string name = Console.ReadLine();

                int demoteUserId = api.GetUserId(name);

                Console.WriteLine("\nTryck [ENTER] för att nedgradera användaren.");
                Console.ReadLine();
                bool userDemoted = api.Demote(userId, demoteUserId);
                if (userDemoted)
                {
                    Console.WriteLine("Användaren är nedgraderad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och användaren är INTE nedgraderad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska aktivera en användare. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter användaren i databasen och ändrar IsActive till true.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminActivateUser(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[AKTIVERA ANVÄNDARE]\n");
                Console.WriteLine("Vilken användare vill du aktivera?");
                Console.Write("Användarnamn: ");
                string name = Console.ReadLine();

                int activateUserId = api.GetUserId(name);

                Console.WriteLine("\nTryck [ENTER] för att aktivera användaren.");
                Console.ReadLine();
                bool userActivated = api.Activate(userId, activateUserId);
                if (userActivated)
                {
                    Console.WriteLine("Användaren är aktiverad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och användaren är INTE aktiverad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska inaktivera en användare. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter användaren i databasen och ändrar IsActive till false.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminInactivateUser(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[INAKTIVERA ANVÄNDARE ]\n");
                Console.WriteLine("Vilken användare vill du inaktivera?");
                Console.Write("Användarnamn: ");
                string name = Console.ReadLine();

                int inactivateUserId = api.GetUserId(name);

                Console.WriteLine("\nTryck [ENTER] för att inaktivera användaren.");
                Console.ReadLine();
                bool userInactivated = api.Inactivate(userId, inactivateUserId);
                if (userInactivated)
                {
                    Console.WriteLine("Användaren är inaktiverad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och användaren är INTE inaktiverad!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska ta fram användaren som köpt mest böcker. 
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void GetBestCustomer(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.Clear();
                Console.WriteLine("[ANVÄNDARE SOM KÖPT FLEST BÖCKER]\n");
                string user = api.BestCustomer(userId);
                if (user != string.Empty)
                {
                    Console.WriteLine($"{user} har köpt flest böcker");
                }
                else
                {
                    Console.WriteLine("Det finns ingen som köpt böcker eller så har något gått fel.. ");
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
        /// Själva fronten för när användaren/administratören ska lägga till en användare. 
        /// Värden läggs in i variabler som skickas in i metoden som lägger till en användare i databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminAddUser(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[LÄGG TILL EN ANVÄNDARE]\n");
                Console.Write("Användarnamn: ");
                string name = Console.ReadLine();
                Console.Write("Lösenord: ");
                string password = Console.ReadLine();

                Console.WriteLine("\nTryck [ENTER] för att lägga till användaren.");
                Console.ReadLine();
                bool userAdded = api.AddUser(userId, name, password);
                if (userAdded)
                {
                    Console.WriteLine("Användaren är tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Något gick fel och användaren är INTE tillagd!");
                    Console.WriteLine("Tryck [ENTER] för att gå tillbaka.");
                    Console.ReadLine();
                }
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska få ut summan för alla sålda böcker. 
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void GetMoneyEarned(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.Clear();
                Console.WriteLine("[VINST EFTER FÖRSÄLJNING]");
                int money = api.MoneyEarned(userId);
                if (money != 0)
                {
                    Console.WriteLine($"Intjänade pengar: {money}kr");
                }
                else
                {
                    Console.WriteLine("Något gick fel, alternativt det finns inga sålda böcker.");
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
        /// Själva fronten för när användaren/administratören ska ta bort en bok. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter boken i databasen som minskar antalet med 1, om därefter antalet blivit 0 så tas boken bort helt från databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminDeleteBook(int userId)
        {
            Console.Clear();
            bool keepBuying = true;
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[RADERA EN BOK]\n");
                Console.WriteLine("Ange titeln på boken du vill radera:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                while (keepBuying)
                {
                    Console.Clear();
                    Console.WriteLine("[BOKEN SOM MATCHAR DIN SÖKNING]\n");
                    int bookId = api.GetBookId(userInput);
                    if (bookId != 0)
                    {
                        var book = api.GetBook(bookId);
                        Console.WriteLine(book.ToString());
                        Console.WriteLine("\nTryck [ENTER] för att RADERA boken eller [Q] för att gå tillbaka.");
                        userInput = Console.ReadLine();

                        if (userInput.Trim().ToLower() == "q")
                        {
                            keepBuying = false;
                        }
                        else
                        {
                            bool bookDeleted = api.DeleteBook(userId, bookId);
                            if (bookDeleted)
                            {
                                Console.WriteLine("Boken är borttagen!");
                            }
                            else
                            {
                                Console.WriteLine("Något gick fel och boken är INTE borttagen!");
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
        /// Själva fronten för när användaren/administratören ska ta bort en kategori. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter kategorin i databasen och kollar så ingen bok är kopplad till kategorin för att sedan tas bort från databasen.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminDeleteCategory(int userId)
        {
            Console.Clear();
            bool keepBuying = true;
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[RADERA EN KATEGORI]\n");
                Console.WriteLine("Ange namnet på kategorin du vill radera:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                while (keepBuying)
                {
                    Console.Clear();
                    Console.WriteLine("[KATEGORIN SOM MATCHAR DIN SÖKNING]\n");
                    int categoryId = api.GetCategoryId(userInput);
                    if (categoryId != 0)
                    {
                        Category category = api.GetACategory(categoryId);
                        Console.WriteLine(category.ToString());
                        Console.WriteLine("\nTryck [ENTER] för att RADERA kategorin eller [Q] för att gå tillbaka.");
                        userInput = Console.ReadLine();

                        if (userInput.Trim().ToLower() == "q")
                        {
                            keepBuying = false;
                        }
                        else
                        {
                            bool categoryDeleted = api.DeleteCategory(userId, categoryId);
                            if (categoryDeleted)
                            {
                                Console.WriteLine("Kategorin är borttagen!");
                            }
                            else
                            {
                                Console.WriteLine("Något gick fel och kategorin är INTE borttagen!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingen kategori matchade din sökning..");
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
        /// Själva fronten för när användaren/administratören ska uppdatera en bok. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter boken i databasen och ger boken de värden som matats in av användaren.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminUpdateBook(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[REDIGERA EN BOK]\n");
                Console.Write("Skriv in titeln på boken du vill redigera: ");
                string oldTitle = Console.ReadLine();
                int bookId = api.GetBookId(oldTitle);
                Console.WriteLine("\nFyll i nedan de uppgifter som nu stämmer med boken:\n");
                Console.Write("Titel: ");
                string title = Console.ReadLine();
                Console.Write("Författare: ");
                string author = Console.ReadLine();
                Console.Write("Pris: ");
                int price = Convert.ToInt32(Console.ReadLine());

                if (bookId != 0)
                {
                    bool bookUpdated = api.UpdateBook(userId, bookId, title, author, price);
                    if (bookUpdated)
                    {
                        Console.WriteLine("Boken är redigerad och sparad!");
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel och boken kunde INTE redigeras.");
                    }
                }
                else
                {
                    Console.WriteLine("Ingen bok matchade din sökning..");
                }
                Console.WriteLine("\nTryck [ENTER] för att lägga till boken.");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska uppdatera en kategori. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter kategorin i databasen och ger kategorin de nya värdet som matats in av användaren.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void AdminUpdateCategory(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[REDIGERA EN KATEGORI]\n");
                Console.Write("Namn på kategorin: ");
                string name = Console.ReadLine();
                int categoryId = api.GetCategoryId(name);

                Console.Write("\nNya namnet på kategorin: ");
                string newName = Console.ReadLine();

                if (categoryId != 0)
                {
                    bool categoryUpdated = api.UpdateCategory(userId, categoryId, newName);
                    if (categoryUpdated)
                    {
                        Console.WriteLine("Kategorin är redigerad och sparad!");
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel och kategorin kunde INTE redigeras.");
                    }
                }
                else
                {
                    Console.WriteLine("Ingen kategori matchade din sökning..");
                }
                Console.WriteLine("\nTryck [ENTER] för att lägga till boken.");
                Console.ReadLine();
            }
            else
            {
                api.Logout(userId);
                API.UserHasTimedOut();
            }
        }
        /// <summary>
        /// Själva fronten för när användaren/administratören ska se alla användare. 
        /// Metoden ger information och feedback till användaren och anropar metoden som hämtar alla användare från databasen för att visa för användaren.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void ListAllUsers(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[LISTA ALLA ANVÄNDARE]\n");
                List<User> users = api.ListUsers(userId);
                API.PrintList(users);
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
        /// Själva fronten för när användaren/administratören ska se alla sålda böcker. 
        /// Metoden ger information och feedback till användaren och anropar metoden som hämtar alla sålda böcker från databasen och visar för användaren.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void ListAllSoldBooks(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[LISTA ALLA SÅLDA BÖCKER]\n");
                List<SoldBook> soldBooks = api.SoldItems(userId);
                API.PrintList(soldBooks);
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
        /// Själva fronten för när användaren/administratören vill se information om en användare. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter en användare i databasen utefter den inmatade strängen och visar för användaren.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void SearchUsers(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[SÖK EFTER ANVÄNDARE]\n");
                Console.WriteLine("Sök på användare nedan:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                List<User> users = api.FindUser(userId, userInput);
                API.PrintList(users);
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
        /// Själva fronten för när användaren/administratören ska tilldela en bok hur många böcker som finns av den. 
        /// Värden läggs in i variabler som skickas in i metoden som söker efter en boken i databasen och ger det nya inmatade värdet till bokens Amount.
        /// </summary>
        /// <param name="userId">Användarens Id</param>
        public void SetAmountOfBook(int userId)
        {
            Console.Clear();
            bool loggedIn = api.CheckSessionTimer(userId);
            if (loggedIn)
            {
                Console.WriteLine("[ANGE VILKET ANTAL SOM FINNS AV EN BOK]\n");
                Console.WriteLine("Ange titeln på boken du vill ange antalet för:");
                Console.Write("> ");
                string userInput = Console.ReadLine();
                int bookId = api.GetBookId(userInput);
                if (bookId != 0)
                {
                    Console.WriteLine("\nAnge antalet böcker som finns tillgängliga av denna bok:");
                    Console.Write("> ");
                    int newAmount = Convert.ToInt32(Console.ReadLine());
                    api.SetAmount(userId, bookId, newAmount);
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
    }
}
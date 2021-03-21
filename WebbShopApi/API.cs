using System;
using System.Collections.Generic;
using WebbShopApi.Controllers;
using WebbShopApi.Models;

namespace WebbShopApi
{
    public static class API
    {
        public static AdminAPI adminApi = new AdminAPI();
        public static UserAPI userApi = new UserAPI();
        private static readonly ApiController api = new ApiController();

        /// <summary>
        /// Själva fronten för när användaren registerar sig som användare med variabler som tar emot namn, lösenord och verifieringslösenord
        /// samt ger feedback om registrering lyckad eller inte.
        /// </summary>
        public static void GetDataToRegister()
        {
            Console.Clear();
            Console.WriteLine("[REGISTRERA KONTO]");
            Console.WriteLine("Vänligen fyll i följande information:");
            Console.WriteLine("Ditt namn: ");
            string name = Console.ReadLine();
            Console.Write("Lösenord: ");
            string password = Console.ReadLine();
            Console.Write("Verifiera lösenordet: ");
            string passwordVerify = Console.ReadLine();
            bool costumerCreated = api.Register(name, password, passwordVerify);
            if (costumerCreated == true)
            {
                Console.WriteLine("\nHurra! Du har nu skapat ett konto hos oss.");
                Console.WriteLine("Tryck ENTER för att logga in");
                Console.ReadLine();
                VerifyUser();
            }
            else
            {
                Console.WriteLine("\nDet gick inte att skapa ett konto hos oss. Du kanske redan har ett konto hos oss eller så stämde inte lösenordet.");
                Console.WriteLine("Tryck ENTER för att gå tillbaka.");
                Console.ReadLine();
                Start();
            }
        }

        /// <summary>
        /// Själva fronten för användarens alla valmöjligheter (menyn). Om användare är Administratör så läggs ytterligare valmöjligheter till.
        /// </summary>
        public static void Menu(int userId)
        {
            try
            {
                bool run = true;
                bool userIsAdmin;

                while (run)
                {
                    Console.Clear();
                    Console.WriteLine("[MENY]");
                    Console.WriteLine("1. Lista alla kategorier");
                    Console.WriteLine("2. Sök efter kategori");
                    Console.WriteLine("3. Lista böcker från en kategori");
                    Console.WriteLine("4. Lista alla tillgängliga böcker");
                    Console.WriteLine("5. Hämta information om en bok");
                    Console.WriteLine("6. Sök efter böcker");
                    Console.WriteLine("7. Sök böcker efter författare");
                    Console.WriteLine("8. Köp bok");
                    Console.WriteLine("9. Ping - Uppdatera sidan");
                    userIsAdmin = api.CheckIfUserIsAdmin(userId);
                    if (userIsAdmin)
                    {
                        Console.WriteLine("\n10. Lägg till en bok");
                        Console.WriteLine("11. Lägg till antalet av en bok");
                        Console.WriteLine("12. Lista alla användare");
                        Console.WriteLine("13. Sök efter användare");
                        Console.WriteLine("14. Updatera en bok");
                        Console.WriteLine("15. Radera en bok");
                        Console.WriteLine("16. Lägg till en kategori");
                        Console.WriteLine("17. Ge en bok en kategori");
                        Console.WriteLine("18. Updatera kategori");
                        Console.WriteLine("19. Radera kategori");
                        Console.WriteLine("20. Lägg till en användare");

                        Console.WriteLine("\n21. Sålda böcker");
                        Console.WriteLine("22. Vinst av försäljning");
                        Console.WriteLine("23. Kunden som köpt flest böcker");
                        Console.WriteLine("24. Uppgradera användare");
                        Console.WriteLine("25. Nedgradera användare");
                        Console.WriteLine("26. Aktivera användare");
                        Console.WriteLine("27. Avaktivera användare");
                    }
                    Console.WriteLine("0. Logga ut");
                    Console.Write("> ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            userApi.UserGetCategories(userId);
                            break;

                        case 2:
                            userApi.UserSearchCategory(userId);
                            break;

                        case 3:
                            userApi.UserSearchBooksFromCategory(userId);
                            break;

                        case 4:
                            userApi.UserGetAllAvailebleBooks(userId);
                            break;

                        case 5:
                            userApi.UserGetInfoAboutBook(userId);
                            break;

                        case 6:
                            userApi.UserSearchBooks(userId);
                            break;

                        case 7:
                            userApi.UserSearchBooksFromAuthor(userId);
                            break;

                        case 8:
                            userApi.UserBuyBook(userId);
                            break;

                        case 9:
                            userApi.UserUpdateSite(userId);
                            break;

                        case 10:
                            adminApi.AdminAddBook(userId);
                            break;

                        case 11:
                            adminApi.SetAmountOfBook(userId);
                            break;

                        case 12:
                            adminApi.ListAllUsers(userId);
                            break;

                        case 13:
                            adminApi.SearchUsers(userId);
                            break;

                        case 14:
                            adminApi.AdminUpdateBook(userId);
                            break;

                        case 15:
                            adminApi.AdminDeleteBook(userId);
                            break;

                        case 16:
                            adminApi.AdminAddCategory(userId);
                            break;

                        case 17:
                            adminApi.AdminAddBookToCategory(userId);
                            break;

                        case 18:
                            adminApi.AdminUpdateCategory(userId);
                            break;

                        case 19:
                            adminApi.AdminDeleteCategory(userId);
                            break;

                        case 20:
                            adminApi.AdminAddUser(userId);
                            break;

                        case 21:
                            adminApi.ListAllSoldBooks(userId);
                            break;

                        case 22:
                            adminApi.GetMoneyEarned(userId);
                            break;

                        case 23:
                            adminApi.GetBestCustomer(userId);
                            break;

                        case 24:
                            adminApi.PromoteUser(userId);
                            break;

                        case 25:
                            adminApi.DemoteUser(userId);
                            break;

                        case 26:
                            adminApi.AdminActivateUser(userId);
                            break;

                        case 27:
                            adminApi.AdminInactivateUser(userId);
                            break;

                        case 0:
                            api.Logout(userId);
                            Console.WriteLine("\nDu är nu utloggad!");
                            Console.WriteLine("Tack för besöket och välkommen åter.");
                            Console.ReadLine();
                            run = false;
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fel inmatning, vänligen försök igen!");
                Console.WriteLine("Tryck [ENTER]");
                Console.ReadLine();
                Menu(userId);
            }
        }
        /// <summary>
        /// Loopar och skriver ut kategori-objekt från en lista.  
        /// </summary>
        /// <param name="list">Lista med kategorier</param>
        public static void PrintList(List<Category> list)
        {
            if (list == null)
            { Console.WriteLine("Sökningen misslyckades, inga kriterier matchade din sökning.."); }
            else
            {
                foreach (var i in list)
                {
                    if (i != null)
                    {
                        Console.WriteLine("- " + i.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Loopar och skriver ut bok-objekt från en lista.  
        /// </summary>
        /// <param name="list">Lista med böcker</param>
        public static void PrintList(List<Book> list)
        {
            if (list == null)
            { Console.WriteLine("Sökningen misslyckades, inga kriterier matchade din sökning.."); }
            else
            {
                foreach (var i in list)
                {
                    if (i != null)
                    {
                        Console.WriteLine(i.ToString());
                        Console.WriteLine("***************************");
                    }
                }
            }
        }
        /// <summary>
        /// Loopar och skriver ut objekt av sålda böcker från en lista.  
        /// </summary>
        /// <param name="list">Lista med sålda böcker</param>
        public static void PrintList(List<SoldBook> list)
        {
            if (list == null)
            { Console.WriteLine("Sökningen misslyckades, inga kriterier matchade din sökning.."); }
            else
            {
                foreach (var i in list)
                {
                    if (i != null)
                    {
                        Console.WriteLine(i.ToString());
                        Console.WriteLine("***************************");
                    }
                }
            }
        }
        /// <summary>
        /// Loopar och skriver ut användar-objekt från en lista.  
        /// </summary>
        /// <param name="list">Lista med användare</param>
        public static void PrintList(List<User> list)
        {
            if (list == null)
            { Console.WriteLine("Sökningen misslyckades, inga kriterier matchade din sökning.."); }
            else
            {
                foreach (var i in list)
                {
                    if (i != null)
                    {
                        Console.WriteLine(i.ToString());
                        Console.WriteLine("***************************");
                    }
                }
            }
        }
        /// <summary>
        /// Första metoden som anropas när programmet startar. Användaren får alternativen att Registrera sig som användare eller Logga in.  
        /// </summary>
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Sarah's e-handel!");
            Console.WriteLine("Registrera dig för att bli kund hos oss eller logga in om du redan har ett konto.");
            Console.WriteLine("1. Registrera");
            Console.WriteLine("2. Logga in");
            Console.Write("> ");
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        GetDataToRegister();
                        break;

                    case 2:
                        VerifyUser();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Fel inmatning, vänligen försök igen.");
            }
        }
        /// <summary>
        /// Informerar användaren att hen varit inaktiv i mer än 15 min och därmed utloggad.  
        /// </summary>
        public static void UserHasTimedOut()
        {
            Console.WriteLine("Du har varit inaktiv i mer än 15 min och har blivit utloggad.");
            Console.WriteLine("\nTryck [ENTER] för att logga in igen.");
            Console.ReadLine();
            VerifyUser();
        }
        /// <summary>
        /// Själva fronten för när användaren skall logga in och metoden lägger användarens namn och lösenord i variabler som skickas in i Login-metoden 
        /// samt ger feedback om inloggning lyckad eller inte för att sedan gå vidare till menyn.
        /// </summary>
        public static void VerifyUser()
        {
            Console.Clear();
            Console.WriteLine("[LOGGA IN]");
            Console.WriteLine("Vänligen fyll i följande information:");
            Console.Write("Användarnamn: ");
            string name = Console.ReadLine();
            Console.Write("Lösenord: ");
            string password = Console.ReadLine();
            int userId = api.Login(name, password);
            if (userId == 0)
            {
                Console.WriteLine("\nInloggning misslyckad!");
                Console.WriteLine("För att Registerar konto hos oss vänligen tryck [R].");
                Console.WriteLine("Tryck [ENTER] för att gå tillbaka och testa logga in igen.");
                string input = Console.ReadLine();
                if (input.Trim().ToLower() == "r")
                {
                    GetDataToRegister();
                }
                else
                {
                    VerifyUser();
                }
            }
            Menu(userId);
        }
    }
}
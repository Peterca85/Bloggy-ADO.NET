using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Bloggy;
class Program
{


    static void Main(string[] args)
    {

        // Skapar en connection string
        String connectionString = "Data Source=localhost, 1433;Initial Catalog=Bloggy;Integrated Security=true;Trusted_Connection=false;User id=sa;Password=bubba123;";
        //länkar ihop connectionstringen med databasen
        SqlConnection connection = new SqlConnection(connectionString);

        {
            //öppnar en connection till databasen
            connection.Open();

            // Skapar en while loop som loopar igenom menyn
            while (true)
            {
                //skriver ut menyn

                Console.WriteLine("Welcome to Bloggy! Choose a option:");
                Console.WriteLine("------------------------------------\r\n");
                Console.WriteLine("1. Display all blogposts");
                Console.WriteLine("2. Display the names of all categories");
                Console.WriteLine("3. Add a new category");
                Console.WriteLine("4. Add a new blog post");
                Console.WriteLine("5. Display all the blog titles from a category");
                Console.WriteLine("6. Add an existing blog post to an existing category:");
                Console.WriteLine("q. For Exit");

                Console.WriteLine("------------------------------------\r\n");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().ToLower();


                //skapar en switch sats med metoder för menyn
                switch (choice)
                {
                    case "1":
                        BlogPost.AllBlogPosts(connection);
                        break;

                    case "2":
                        Category.AllCategories(connection);
                        break;

                    case "3":
                        Category.AddCategory(connection);
                        break;

                    case "4":

                        BlogPost.AddBlogPost(connection);
                        break;

                    case "5":
                        BlogPost.BlogPostFromCategory(connection);
                        break;

                    case "6":

                        BlogPost.AddExistingBlogPost(connection);
                        break;

                    //avslutar programmet
                    case "q":
                        //skriver ut alla blog posts i databasen
                        Console.WriteLine("Here are all your blogpost: ");
                        Console.WriteLine("-------------------------------------");
                        BlogPost.AllBlogPosts(connection);

                        //stänger en connection till databasen
                        connection.Close();
                        Console.WriteLine(" Goodbye !");
                        return;

                    default:
                        Console.WriteLine("Incorrect choice please choose again");
                        break;
                }

            }
        }

    }

}
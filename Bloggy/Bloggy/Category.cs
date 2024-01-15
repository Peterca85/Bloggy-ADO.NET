using System;
using System.Data.SqlClient;

namespace Bloggy
{
    public class Category
    {

        public static void AddCategory(SqlConnection connection)
        {

            // input från användaren
            Console.WriteLine("Add a new category:");
            string Category = Console.ReadLine();

            //Skapar variabeln för sql kommandot
            string sqlCategory = "INSERT INTO Category VALUES('" + Category + "')";

            //skapar ett sql kommando
            SqlCommand commandoCat = new SqlCommand(sqlCategory, connection);


            // kör sql kommandot
            commandoCat.ExecuteNonQuery();


            Console.WriteLine("The Category is added to the database");
        }

        public static List<CategoryDAO> AllCategories(SqlConnection connection)
        {
            //skapar ett sql kommando
            SqlCommand selectAllCategories = new SqlCommand("SELECT Id, Category FROM Category", connection);

            //skapar en lista av kategorier
            List<CategoryDAO> CategoryList = new List<CategoryDAO>();

            //läser ut sql datan för kategorierna
            SqlDataReader categoryReader = selectAllCategories.ExecuteReader();

            //skapar en while loop som loopar igenom kategorierna i databasen
            while (categoryReader.Read())
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                categoryDAO.Category = (string)categoryReader["category"];

                categoryDAO.ID = (int)categoryReader["Id"];

                CategoryList.Add(categoryDAO);



                Console.WriteLine($"Id: {categoryReader["Id"]},\r\n Category: {categoryReader["category"]}\r\n");
            }
            categoryReader.Close();
            return CategoryList;
        }

        public Category()
        {
        }
    }
}


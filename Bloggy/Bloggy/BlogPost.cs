using System;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Bloggy
{
    public class BlogPost
    {

        public static void AddBlogPost(SqlConnection connection)
        {


            Console.WriteLine("Insert a blogpost:");


            //Användarens input
            Console.Write("Enter blog title:");
            string Title = Console.ReadLine();

            Console.Write("Enter blog text:");
            string Content = Console.ReadLine();


            //skapar ett sql object
            string sqlBlog = "INSERT INTO Blog_Posts OUTPUT INSERTED.id VALUES('" + Title + "','" + Content + "')";
            SqlCommand commandBlog = new SqlCommand(sqlBlog, connection);

            Console.WriteLine("Choose a category by Id to add to the Blogpost. Press ENTER to continue ");

            //skapar en lista för alla kategorier
            List<CategoryDAO> categoryDAOs = Category.AllCategories(connection);

            Console.WriteLine("Enter ID:");


            string category = Console.ReadLine();

            // skapar en if -sats som körs om användaren matar in ett värde 
            if (category.Length > 0)
            {

                // gör om en string till en int
                int categoryId = int.Parse(category);


                // skapar en lista för kategorierna efter användarens input
                List<int> categoryIdList = new List<int>();
                categoryIdList.Add(categoryId);


                // skapar en while loop
                while (true)
                {

                    Console.WriteLine("If you want to add the post to another category enter id or press ENTER to continue ");
                    Console.WriteLine("Enter ID: ");


                    string categoryString = Console.ReadLine();

                    //skapar en if-sats som kontrollerar användarens input
                    if (categoryString.Length < 1)
                    {

                        break;
                    }

                    int categoryInt = int.Parse(categoryString);

                    categoryIdList.Add(categoryInt);
                }


                //får ut Id från blogg posten som vi lägger in databasen
                int blogId = (int)commandBlog.ExecuteScalar();

                //skapar en foreach loop som går igenom listan efter användarens input
                foreach (int id in categoryIdList)
                {

                    string sqlCategory = "INSERT INTO Blog_Category VALUES('" + id + "','" + blogId + "')";
                    //skapar ett sql object
                    SqlCommand commandCategory = new SqlCommand(sqlCategory, connection);

                    // kör sql kommandot
                    commandCategory.ExecuteNonQuery();


                }

            }

            //skapar en else-sats som körs om inget kategori val görs av användaren 
            else
            {
                // kör sql kommandot
                commandBlog.ExecuteNonQuery();
            }

            Console.WriteLine("The BlogPost is added to the database");
        }

        public static void AllBlogPosts(SqlConnection connection)
        {
            //skapar ett sql object
            SqlCommand selectAllPosts = new SqlCommand("SELECT Id, Title, Content FROM Blog_Posts", connection);

            //läser ut sql datan för kategorierna
            SqlDataReader postReader = selectAllPosts.ExecuteReader();

            //skapar en while loop som skriver ut alla Blog posts i databasen
            while (postReader.Read())
            {
                Console.WriteLine($"Id: {postReader["Id"]},\r\n Title: {postReader["Title"]},\r\n Content:  {postReader["content"]}\r\n");
            }
            postReader.Close();
        }



        public static void BlogPostFromCategory(SqlConnection connection)
        {

            Category.AllCategories(connection);


            Console.WriteLine("Enter category id:");
            int categoryId = int.Parse(Console.ReadLine());

            //skapar ett sql object
            SqlCommand PostFromCategory = new SqlCommand("SELECT Blog_Posts.title FROM Blog_Posts INNER JOIN Blog_Category ON Blog_Posts.id = Blog_Category.blog_post_id WHERE category_id = " + categoryId, connection);
            // kör sql objektet
            SqlDataReader postReader = PostFromCategory.ExecuteReader();
            while (postReader.Read())
            {
                Console.WriteLine($"Title: {postReader["Title"]}");
            }
            postReader.Close();
        }


        public static void AddExistingBlogPost(SqlConnection connection)

        {

            BlogPost.AllBlogPosts(connection);

            Console.WriteLine("Choose a Blogpost ID: ");
            Console.WriteLine("-----------------------\r\n");
            int blogId = int.Parse(Console.ReadLine());


            Category.AllCategories(connection);

            Console.WriteLine("Choose a category ID: ");
            Console.WriteLine("-----------------------\r\n");
            int catId = int.Parse(Console.ReadLine());


            //skapar ett sql object
            SqlCommand AddBlogPostToCategory = new SqlCommand("INSERT INTO Blog_Category VALUES ('" + catId + "','" + blogId + "')", connection);

            //läser ut sql datan in sql databasen
            SqlDataReader postReader = AddBlogPostToCategory.ExecuteReader();


            postReader.Close();

            Console.WriteLine("The blogpost is added to a category");
        }


        public BlogPost()
        {


        }
    }
}


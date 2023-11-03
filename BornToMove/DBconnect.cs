using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BornToMove
{
    public class DBConnect
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "born2move";
            uid = "WebShopUser";
            password = "1234";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can use your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool nameExists(string name)
        {
            if (this.OpenConnection())
            {
                string query = "SELECT name FROM move WHERE name = @name";

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    
                    using (MySqlDataReader dataReader = cmd.ExecuteReader()) // apply using to other queries too!!!
                    {
                        return dataReader.Read();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return false;
        }

        public List<int> getAllIds()
        {
            List<int> idList = new List<int>();

            //Open connection
            if (this.OpenConnection())
            {
                string query = "SELECT id FROM move";

                //maybe use using to close?

                try
                {
                    //Create command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    
                    // Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        int id = Convert.ToInt32(dataReader["id"]);
                        idList.Add(id);
                    }

                    //close Data Reader
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return idList;
        }

        public Dictionary<int, Move> getExercise(int exerciseId)
        { 
            Dictionary<int, Move> exerciseArray = new Dictionary<int, Move>();
            
            if (this.OpenConnection())
            {
                string query = "SELECT * FROM move WHERE id = @exerciseId";

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@exerciseId", exerciseId);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int id = dataReader.GetInt32("id");
                        string name = dataReader.GetString("name");
                        string description = dataReader.GetString("description");
                        int sweatrate = dataReader.GetInt32("sweatrate");
                        exerciseArray.Add(id, new Move(id, name, description, sweatrate));
                    }

                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return exerciseArray;
        }

        public int saveExercise(string name, string description, int sweatrate)
        {
            int rowsAffected = 0;

            if (this.OpenConnection())
            {
                string query = "INSERT INTO move (name, description, sweatrate) VALUES (@name, @description, @sweatrate)";

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))

                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@sweatrate", sweatrate);

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return rowsAffected;
        }


        public Dictionary<int, Tuple<string, int>> getExercices()
        {
            Dictionary<int, Tuple<string, int>> exerciseList = new Dictionary<int, Tuple<string,int>>();

            //Open connection
            if (this.OpenConnection())
            {
                string query = "SELECT id, name, sweatrate FROM move";

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    // Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        int id = dataReader.GetInt32("id");
                        string name = dataReader.GetString("name");
                        int sweatrate = dataReader.GetInt32("sweatrate");
                        exerciseList.Add(id, new Tuple<string, int>(name, sweatrate));
                    }

                    //close Data Reader
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            return exerciseList;
        }











    }
    
}

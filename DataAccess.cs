using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void AddData(String inputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db")) 
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = " INSERT INTO MyTable VALUES (NULL,@Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
        public static List<String> GetData()
        {

            List<string>entries = new List<string>();
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from Mytable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while ( query.Read())
                {
                    entries.Add(query.GetString(0));

                }
                db.Close();
                {
                    return entries;
                }
            }
        }
    }
}

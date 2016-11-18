using System;
using System.Data;
using System.IO;
using SQLite;

namespace dictionary.ORM
{
    public class DBRepository
    {
        //code to create the database
        public string CreateDB()
        {
            var output = "";
            output += "Creating Database if it doesn`t exist.";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            output += "\nDatabase Created...";
            return output;
        }

        //Code to create the table
        public string CreateTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<ToDoTasks>();
                string result = "Table Created successfully..";
                return result;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Code to insert a record
        public string InsertRecord(string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
                var db = new SQLiteConnection(dbPath);

                ToDoTasks item = new ToDoTasks();
                item.Task = task;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }

        }

        //Code to insert a record from archive
        public string InsertRecordFromArchive(int id, string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
                var db = new SQLiteConnection(dbPath);

                ToDoTasks item = new ToDoTasks();
                item.Id = id;
                item.Task = task;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }

        }

        //Ñode to retrieve all the records
        public string GetAllRecords()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ToDoTasks>();

            foreach (var item in table)
            {
                output += "\n" + item.Id + " --- " + item.Task;
            }
            return output;
        }

        //code to retrieve specific record using ORM
        public string GetTaskById(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<ToDoTasks>(id);
            return item.Task;
        }

        //code to update the record using ORM
        public string updateRecord(int id, string task)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<ToDoTasks>(id);
            item.Task = task;
            db.Update(item);
            return "Record Updated...";
        }

        //code to remove the record
        public string RemoveTask(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<ToDoTasks>(id);
            db.Delete(item);
            return "record Deleted..";
        }



        //my trying to find by name
        public string find_name(string task)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo15.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<ToDoTasks>();

            foreach (var item in table)
            {
                if (item.Task == task)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Task;
                }
            }
            return output;
        }
    }
}
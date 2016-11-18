using System;
using System.Data;
using System.IO;
using SQLite;
using System.Collections;
using Android.Widget;

namespace dictionary.ORM
{
    public class DBCards
    {
        //ArrayList for the english cards
        public static ArrayList EngArrList = new ArrayList();



        ////////////////////////////////////////////
        //For Table of cards1
        //Code to create the table
        public string CreateTableCategory1Cards()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Category1Cards>();
                string ret1 = "Category1Cards TABLE CREATED";
                return ret1;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Code to insert a record
        public string InsertRecordCategory1Cards(string EngC1, string RusC1, int CategoryId, string categoryName)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
                var db = new SQLiteConnection(dbPath);

                Category1Cards item = new Category1Cards();
                item.Eng1c = EngC1;
                item.Rus1c = RusC1;
                item.CategoryId = CategoryId;
                item.CategoryName = categoryName;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Finding id methods
        public string FindENG_Id1(string engC1)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<Category1Cards>();

            foreach (var item in table)
            {
                if (item.Eng1c == engC1)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Id;
                }
            }
            return output;
        }

        public string FindRUS_Id1(string rusC1)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<Category1Cards>();

            foreach (var item in table)
            {
                if (item.Rus1c == rusC1)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Id;
                }
            }
            return output;
        }

        //deleting cards methods
        public string RemoveCard1(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Category1Cards>(id);
            db.Delete(item);
            return "Card Deleted..";
        }
        //method for deleting cards when you delete their category
        public void RemoveCardsFromTheCategory()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Метод удаления всех карт удаленной категории");
            var table = db.Table<Category1Cards>();
            foreach (var item in table)
            {
                if (item.CategoryId == dicListActivity.ID_of_catGlob)
                {
                    RemoveCard1(item.Id);
                }
            }
        }

        //Сode to retrieve all the records
        public string GetAllRecordsCategory1Cards()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<Category1Cards>();

            foreach (var item in table)
            {
                output += "\n" + item.Id + " --- Eng: " + item.Eng1c + ",   Rus: " + item.Rus1c + " catID: " + item.CategoryId + " catNAME: " + item.CategoryName;
                if (item.Eng1c != null)
                {
                    EngArrList.Add(item.Eng1c);
                }
            }
            foreach (string i in EngArrList)
            {
                Console.WriteLine(i);
            }
            return output;
        }

        //For Table of cards1 ENDED


        ///////////////////////////////
        //THIS CODE IS FOR ARCHIVE CATEGORIES TABLE

        //Code to create the table
        public string CreateTablecardsInArchive()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<cardsInArchive>();
                string ret1 = "ArchiveCat TABLE CREATED";
                return ret1;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Code to insert a record
        public string InsertRecordcardsInArchive(string categoryForArchive, int CatId, string engToArchive, string rusToArchive)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
                var db = new SQLiteConnection(dbPath);

                cardsInArchive item = new cardsInArchive();
                item.CategoryName = categoryForArchive;
                item.CategoryId = CatId;
                item.EngCardArchive = engToArchive;
                item.RusCardArchive = rusToArchive;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //UpdateMethod
        public string updateArchiveCard(int idCard, int catId, string catName)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<cardsInArchive>(idCard);
            item.CategoryId = catId;
            item.CategoryName = catName;
            db.Update(item);
            return "Record Updated...";
        }

        //Finding id method
        public string cardsInArchive_Id1(string categoryForArchive)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<cardsInArchive>();

            foreach (var item in table)
            {
                if (item.EngCardArchive == categoryForArchive)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Id;
                }
            }
            return output;
        }

        //deleting cards methods
        public string RemoveArchiveCategory(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<cardsInArchive>(id);
            db.Delete(item);
            return "Category Deleted..";
        }

        //deleting cards methods
        public string cardsInArchiveRemoveCard(int idCard)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<Category1Cards>(idCard);
            db.Delete(item);
            return "Card Deleted..";
        }

        //Сode to retrieve all the records
        public string GetAllRecordsCategoriesFromArchive()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardsInArchiv1e1.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<cardsInArchive>();

            foreach (var item in table)
            {
                output += "\n" + item.Id + " --- Category in archive: " + item.CategoryName + ", Cat ID: " + item.CategoryId + ", EngCard: " + item.EngCardArchive + ", RusCard: " + item.RusCardArchive;
                if (item.CategoryName != null)
                {
                    //output += "\n" + item.Id + " --- Category in archive: " + item.CategoryName + ", Cat ID: " + item.CategoryId + ", EngCard: " + item.EngCardArchive + ", RusCard: " + item.RusCardArchive;
                }
            }
            return output;
        }
        //find rus name by id of the card
        public string FindRUS_name1(int rusId1)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<Category1Cards>();

            foreach (var item in table)
            {
                if (item.Id == rusId1)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Rus1c;
                }
            }
            return output;
        }

        //find eng name by id of the card
        public string FindENG_name1(int engId1)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Card1111sDB.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<Category1Cards>();

            foreach (var item in table)
            {
                if (item.Id == engId1)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Eng1c;
                }
            }
            return output;
        }
        //THIS CODE IS FOR ARCHIVE CATEGORIES TABLE ENDED





        ///////////////////////////////
        //THIS CODE IS FOR USER`S IRREGULAR VERBS


        //Code to create the table
        public string CreateTableIrrVerbsUSERS()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<IrrVerbsUsers>();
                string ret1 = "IrrVerbsUser TABLE CREATED";
                return ret1;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Code to insert a record
        public string InsertIrregVerb(string form1, string form2, string form3, string transl)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
                var db = new SQLiteConnection(dbPath);

                IrrVerbsUsers item = new IrrVerbsUsers();
                item.form1 = form1;
                item.form2 = form2;
                item.form3 = form3;
                item.translation = transl;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Сode to retrieve all the records
        public string GetAllRecordsIrregularVerbs()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            //output += "Retrieving the data using ORM...";
            var table = db.Table<IrrVerbsUsers>();

            foreach (var item in table)
            {
                output += "\n" + item.Id + " --- F1: " + item.form1 + ", F2: " + item.form2 + ", F3: " + item.form3 + ", T: " + item.translation;
                /*if (item.CategoryName != null)
                {
                    //output += "\n" + item.Id + " --- Category in archive: " + item.CategoryName + ", Cat ID: " + item.CategoryId + ", EngCard: " + item.EngCardArchive + ", RusCard: " + item.RusCardArchive;
                }*/
            }
            return output;
        }

        //deleting IrrVerb method
        public string IrrVerbRemoveCard(int idCard)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<IrrVerbsUsers>(idCard);
            db.Delete(item);
            return "Card Deleted..";
        }

        //THIS CODE IS FOR USER`S IRREGULAR VERBS.ENDED


        //////////////

        //THIS CODE IS FOR USER`S IRREGULAR VERBS ____ARCHIVE____
        public string CreateTableIrrVerbsARCHIVE()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<IrrVerbsArchive>();
                string ret1 = "IrrVerbsUser TABLE CREATED";
                return ret1;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Code to insert a record
        public string InsertIrregVerbArchive(string form1, string form2, string form3, string transl)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
                var db = new SQLiteConnection(dbPath);

                IrrVerbsArchive item = new IrrVerbsArchive();
                item.form1 = form1;
                item.form2 = form2;
                item.form3 = form3;
                item.translation = transl;
                db.Insert(item);
                return "Record Added...";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        //Сode to retrieve all the records
        public string GetAllRecordsIrregularVerbsArchive()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            //output += "Retrieving the data using ORM...";
            var table = db.Table<IrrVerbsArchive>();

            foreach (var item in table)
            {
                output += "\n" + item.Id + " --- F1: " + item.form1 + ", F2: " + item.form2 + ", F3: " + item.form3 + ", T: " + item.translation;
            }
            return output;
        }

        //Finding id method
        public string FindIdOfIrrVerbInArchive(string form1)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            var table = db.Table<IrrVerbsArchive>();

            foreach (var item in table)
            {
                if (item.form1 == form1)
                {
                    //if you want to type "item.Id" instead of "item.Task", you need concatenation:
                    output = "" + item.Id;
                }
            }
            return output;
        }

        //deleting IrrVerb method
        public string IrrVerbRemoveCardArchive(int idCard)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IrrVerbsArchive.db3");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<IrrVerbsArchive>(idCard);
            db.Delete(item);
            return "Card Deleted..";
        }

        //THIS CODE IS FOR USER`S IRREGULAR VERBS ____ARCHIVE____ENDED
    }
}
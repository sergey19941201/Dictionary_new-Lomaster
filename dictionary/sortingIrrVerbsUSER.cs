using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections;
using System.IO;
using SQLite;

namespace dictionary
{
    public class FillingList
    {
        public int ID { get; set; }
        public string FORM1 { get; set; }
        public string FORM2 { get; set; }
        public string FORM3 { get; set; }
        public string TRANSL { get; set; }
    }

    public static class sortingIrrVerbsUSER
    {
        public static List<FillingList> AllDataList = new List<FillingList>();

        //Sorted ArrayList ENG
        public static ArrayList form1IsSortedRandom = new ArrayList();
        public static ArrayList form2IsSortedRandom = new ArrayList();
        public static ArrayList form3IsSortedRandom = new ArrayList();
        //Sorted ArrayList RUS
        public static ArrayList translationIsSortedRandom = new ArrayList();
        //Sorted ArrayList ID
        public static ArrayList idIsSortedRandom = new ArrayList();

        public static void FILL_Big_list()
        {
            string dbPath1 = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IrrVerbsUSERS.db3");
            var db1 = new SQLiteConnection(dbPath1);
            var table1 = db1.Table<ORM.IrrVerbsUsers>();

            AllDataList.Clear();

            foreach (var item1 in table1)
            {
                AllDataList.Add(new FillingList { ID = item1.Id, FORM1 = item1.form1, FORM2 = item1.form2, FORM3 = item1.form3, TRANSL = item1.translation });
            }

            //clearing sorted random array lists
            form1IsSortedRandom.Clear();
            form2IsSortedRandom.Clear();
            form3IsSortedRandom.Clear();
            translationIsSortedRandom.Clear();
            idIsSortedRandom.Clear();

            ///////////////////////////////////////
            //RANDOMIZING
            var rnd = new Random();
            var randomlyOrdered = AllDataList.OrderBy(i => rnd.Next());
            foreach (var i in randomlyOrdered)
            {
                Console.WriteLine("_____ID: " + i.ID + ". form1: " + i.FORM1 + ". form2: " + i.FORM2 + ". form3: " + i.FORM3 + ". transl: " + i.TRANSL);
                form1IsSortedRandom.Add(i.FORM1);
                form2IsSortedRandom.Add(i.FORM2);
                form3IsSortedRandom.Add(i.FORM3);
                translationIsSortedRandom.Add(i.TRANSL);
                idIsSortedRandom.Add(i.ID);
            }
            //RANDOMIZING.ENDED
            ///////////////////////////////////////
        }
    }
}

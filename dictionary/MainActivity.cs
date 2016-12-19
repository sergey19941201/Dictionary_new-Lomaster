using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Timers;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Android.Content.PM;
using dictionary.mCode;
using System.Collections;

namespace dictionary
{
    public class FillingListIrrVerbsSystem
    {
        public string sysFORM1 { get; set; }
        public string sysFORM2 { get; set; }
        public string sysFORM3 { get; set; }
        public string sysTRANSL { get; set; }
    }

    [Activity(MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Label = "Словарь", Theme = "@android:style/Theme.Black.NoTitleBar")]

    public class MainActivity : Activity
    {
        //ArrayList for the english IrrVerbsSystem verbs. WE NEED ARRAYLIST TO COLLECT ELEMENTS, THAT ARE NOT NULL
        public static ArrayList form1AL = new ArrayList();
        public static ArrayList form2AL = new ArrayList();
        public static ArrayList form3AL = new ArrayList();
        //ArrayList for the russian translation of IrrVerbsSystem.WE NEED ARRAYLIST TO COLLECT ELEMENTS, THAT ARE NOT NULL
        public static ArrayList translationAL = new ArrayList();

        public static List<FillingListIrrVerbsSystem> AllDataListIrrVerbsSystem = new List<FillingListIrrVerbsSystem>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            
            AllDataListIrrVerbsSystem.Clear();

            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "be", sysFORM2 = "was / were", sysFORM3 = "been", sysTRANSL = "быть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "become", sysFORM2 = "became", sysFORM3 = "become", sysTRANSL = "становиться" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "begin", sysFORM2 = "began", sysFORM3 = "begun", sysTRANSL = "начинать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "blow", sysFORM2 = "blew", sysFORM3 = "blown", sysTRANSL = "дуть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "break", sysFORM2 = "broke", sysFORM3 = "broken", sysTRANSL = "ломать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "bring", sysFORM2 = "brought", sysFORM3 = "brought", sysTRANSL = "приносить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "build", sysFORM2 = "built", sysFORM3 = "built", sysTRANSL = "строить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "buy", sysFORM2 = "bought", sysFORM3 = "bought", sysTRANSL = "покупать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "catch", sysFORM2 = "caught", sysFORM3 = "caught", sysTRANSL = "ловить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "choose", sysFORM2 = "chose", sysFORM3 = "chosen", sysTRANSL = "выбирать" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "come", sysFORM2 = "came", sysFORM3 = "come", sysTRANSL = "приходить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "cost", sysFORM2 = "cost", sysFORM3 = "cost", sysTRANSL = "стоить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "cut", sysFORM2 = "cut", sysFORM3 = "cut", sysTRANSL = "резать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "do", sysFORM2 = "did", sysFORM3 = "done", sysTRANSL = "делать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "draw", sysFORM2 = "drew", sysFORM3 = "drawn", sysTRANSL = "чертить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "dream", sysFORM2 = "dreamt", sysFORM3 = "dreamt", sysTRANSL = "мечтать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "drink", sysFORM2 = "drank", sysFORM3 = "drunk", sysTRANSL = "пить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "drive", sysFORM2 = "drove", sysFORM3 = "driven", sysTRANSL = "водить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "eat", sysFORM2 = "ate", sysFORM3 = "eaten", sysTRANSL = "есть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "fall", sysFORM2 = "fell", sysFORM3 = "fallen", sysTRANSL = "падать" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "feel", sysFORM2 = "felt", sysFORM3 = "felt", sysTRANSL = "чувствовать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "find", sysFORM2 = "found", sysFORM3 = "found", sysTRANSL = "находить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "fly", sysFORM2 = "flew", sysFORM3 = "flown", sysTRANSL = "летать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "forget", sysFORM2 = "forgot", sysFORM3 = "forgotten", sysTRANSL = "забывать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "get", sysFORM2 = "got", sysFORM3 = "got", sysTRANSL = "получать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "give", sysFORM2 = "gave", sysFORM3 = "given", sysTRANSL = "давать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "go", sysFORM2 = "went", sysFORM3 = "gone", sysTRANSL = "идти" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "grow", sysFORM2 = "grew", sysFORM3 = "grown", sysTRANSL = "расти" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "have", sysFORM2 = "had", sysFORM3 = "had", sysTRANSL = "иметь" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "hear", sysFORM2 = "heard", sysFORM3 = "heard", sysTRANSL = "слышать" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "hit", sysFORM2 = "hit", sysFORM3 = "hit", sysTRANSL = "ударить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "hold", sysFORM2 = "held", sysFORM3 = "held", sysTRANSL = "держать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "hurt", sysFORM2 = "hurt", sysFORM3 = "hurt", sysTRANSL = "ранить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "keep", sysFORM2 = "kept", sysFORM3 = "kept", sysTRANSL = "удерживать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "know", sysFORM2 = "knew", sysFORM3 = "known", sysTRANSL = "знать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "learn", sysFORM2 = "learnt", sysFORM3 = "learnt", sysTRANSL = "учить что-нибудь" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "leave", sysFORM2 = "left", sysFORM3 = "left", sysTRANSL = "покидать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "let", sysFORM2 = "let", sysFORM3 = "let", sysTRANSL = "позволять" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "light", sysFORM2 = "lit", sysFORM3 = "lit", sysTRANSL = "освещать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "lose", sysFORM2 = "lost", sysFORM3 = "lost", sysTRANSL = "терять" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "make", sysFORM2 = "made", sysFORM3 = "made", sysTRANSL = "изготавливать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "mean", sysFORM2 = "meant", sysFORM3 = "meant", sysTRANSL = "значить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "meet", sysFORM2 = "met", sysFORM3 = "met", sysTRANSL = "встречать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "pay", sysFORM2 = "paid", sysFORM3 = "paid", sysTRANSL = "платить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "put", sysFORM2 = "put", sysFORM3 = "put", sysTRANSL = "класть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "read", sysFORM2 = "read", sysFORM3 = "read", sysTRANSL = "читать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "ride", sysFORM2 = "rode", sysFORM3 = "ridden", sysTRANSL = "ехать верхом" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "ring", sysFORM2 = "rang", sysFORM3 = "rung", sysTRANSL = "звонить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "rise", sysFORM2 = "rose", sysFORM3 = "risen", sysTRANSL = "подниматься" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "run", sysFORM2 = "ran", sysFORM3 = "run", sysTRANSL = "бежать" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "say", sysFORM2 = "said", sysFORM3 = "said", sysTRANSL = "сказать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "see", sysFORM2 = "saw", sysFORM3 = "seen", sysTRANSL = "видеть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "sell", sysFORM2 = "sold", sysFORM3 = "sold", sysTRANSL = "продавать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "send", sysFORM2 = "sent", sysFORM3 = "sent", sysTRANSL = "посылать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "shake", sysFORM2 = "shook", sysFORM3 = "shaken", sysTRANSL = "трясти" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "shut", sysFORM2 = "shut", sysFORM3 = "shut", sysTRANSL = "закрывать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "sing", sysFORM2 = "sang", sysFORM3 = "sung", sysTRANSL = "петь" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "sit", sysFORM2 = "sat", sysFORM3 = "sat", sysTRANSL = "сидеть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "sleep", sysFORM2 = "slept", sysFORM3 = "slept", sysTRANSL = "спать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "speak", sysFORM2 = "spoke", sysFORM3 = "spoken", sysTRANSL = "говорить" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "spend", sysFORM2 = "spent", sysFORM3 = "spent", sysTRANSL = "тратить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "stand", sysFORM2 = "stood", sysFORM3 = "stood", sysTRANSL = "стоять" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "steal", sysFORM2 = "stole", sysFORM3 = "stolen", sysTRANSL = "украсть" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "swim", sysFORM2 = "swam", sysFORM3 = "swum", sysTRANSL = "плавать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "take", sysFORM2 = "took", sysFORM3 = "taken", sysTRANSL = "брать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "teach", sysFORM2 = "taught", sysFORM3 = "taught", sysTRANSL = "учить кого-нибудь" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "tell", sysFORM2 = "told", sysFORM3 = "told", sysTRANSL = "рассказать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "think", sysFORM2 = "thought", sysFORM3 = "thought", sysTRANSL = "думать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "throw", sysFORM2 = "threw", sysFORM3 = "thrown", sysTRANSL = "бросать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "understand", sysFORM2 = "understood", sysFORM3 = "understood", sysTRANSL = "понимать" });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "wake", sysFORM2 = "woke", sysFORM3 = "woken", sysTRANSL = "просыпаться" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "wear", sysFORM2 = "wore", sysFORM3 = "worn", sysTRANSL = "носить" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "win", sysFORM2 = "won", sysFORM3 = "won", sysTRANSL = "побеждать" });
            AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "write", sysFORM2 = "wrote", sysFORM3 = "written", sysTRANSL = "писать" });
            //AllDataListIrrVerbsSystem.Add(new FillingListIrrVerbsSystem { sysFORM1 = "", sysFORM2 = "", sysFORM3 = "", sysTRANSL = "" });
            
            //clearing
            form1AL.Clear();
            form2AL.Clear();
            form3AL.Clear();
            translationAL.Clear();
            
            foreach (var i in AllDataListIrrVerbsSystem)
            {
                form1AL.Add(i.sysFORM1);
                form2AL.Add(i.sysFORM2);
                form3AL.Add(i.sysFORM3);
                translationAL.Add(i.sysTRANSL);
            }

            //Making SearchIndicator false:
            SearchFragmentShow.textToSearch = null;

            //Create the database..
            ORM.DBRepository dbr = new ORM.DBRepository();
            var result = dbr.CreateDB();

            //To Create the Table
            ORM.DBRepository dbr1 = new ORM.DBRepository();
            var result1 = dbr.CreateTable();

            //Creating irrVerbsArchive table:
            new ORM.DBCards().CreateTableIrrVerbsARCHIVE();

            //Creating irrVerbsUSER table:
            new ORM.DBCards().CreateTableIrrVerbsUSERS();

            ImageButton slovari = FindViewById<ImageButton>(Resource.Id.slovari);
            slovari.Click += delegate 
            {
                slovari.SetBackgroundResource(Resource.Drawable.slovariPODSV);
                StartActivity(new Intent(this, typeof(dicListActivity)));
            };
            
            ImageButton nepravelnieglagoli = FindViewById<ImageButton>(Resource.Id.nepravelnieglagoli);
            nepravelnieglagoli.Click += delegate
            {
                nepravelnieglagoli.SetBackgroundResource(Resource.Drawable.nepravelnieGlagoliPODSV);
                StartActivity(new Intent(this, typeof(NGActivity)));
            };

            ImageButton arhiv = FindViewById<ImageButton>(Resource.Id.arhiv);
            arhiv.Click += delegate
            {
                arhiv.SetBackgroundResource(Resource.Drawable.arhivPODSV);
                StartActivity(new Intent(this, typeof(ARCHIVEActivity)));
            };
        }
        //This code is to exit from the app:
        public override void OnBackPressed()
        {
            this.FinishAffinity();
        }
    }
}


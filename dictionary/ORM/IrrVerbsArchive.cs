using System;
using System.Data;
using System.IO;
using SQLite;


namespace dictionary.ORM
{
    [Table("IrrVerbsArchive")]
    class IrrVerbsArchive
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        public string form1 { get; set; }

        public string form2 { get; set; }

        public string form3 { get; set; }

        public string translation { get; set; }
    }
}
using System;
using System.Data;
using System.IO;
using SQLite;

namespace dictionary.ORM
{
    [Table("cardsInArchive")]
    class cardsInArchive
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [MaxLength(105)]

        public string CategoryName { get; set; }

        [MaxLength(105)]

        public string EngCardArchive { get; set; }

        [MaxLength(105)]

        public string RusCardArchive { get; set; }
    }
}

﻿using LinqToDB.Mapping;
using System.Linq;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "Work")]
    public class WorkModel
    {
        [Column(IsPrimaryKey = true, Name ="WorkId", CanBeNull = false)]
        public int WorkId { get; set; }

        [Column(IsPrimaryKey = true, Name = "UserId", CanBeNull = false)]
        public int UserId { get; set; }

        [Column(Name = "Title", CanBeNull = false)]
        public string Title { get; set; } = null!;

        [Column(Name = "Description", CanBeNull = true)]
        public string Description { get; set; } = null!;

        [Column(Name = "TextBlockOne", CanBeNull = true)]
        public string TextBlockOne { get; set; } = null!;

        [Column(Name = "TextBlockTwo", CanBeNull = true)]
        public string TextBlockTwo { get; set; } = null!;

        [Column(Name = "TextBlockThree", CanBeNull = true)]
        public string TextBlockThree { get; set; } = null!;

        [Column(Name = "Status", CanBeNull = false)]
        public int Status { get; set; } = 0;

    }
}
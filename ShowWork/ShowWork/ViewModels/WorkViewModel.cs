﻿namespace ShowWork.ViewModels
{
    public class WorkViewModel
    {
        public int? WorkId { get; set; }

        public int UserId { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string TextBlockOne { get; set; } = null!;

        public string TextBlockTwo { get; set; } = null!;

        public string TextBlockThree { get; set; } = null!;

        public int Status { get; set; }
        public int LikesCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int CategoryOfWork { get; set; }
        public int PatternOfWork { get; set; }
        public string? Category { get; set; }

        public string Login { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserImagePath { get; set; }
        public double MiddleGrade { get; set; }
        public DateTime Published { get; set; }

        public string? ImagePath { get; set; }

    }
}

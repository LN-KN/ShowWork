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

        public int Status { get; set; } = 0;
        public int LikesCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int? TypeOfWork { get; set; } = 0;
    }
}
﻿using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewModels;

namespace ShowWork.ViewMapper
{
    public class WorkMapper
    {
        public static WorkModel MapWorkViewModelToWorkModel(WorkViewModel m)
        {
            return new WorkModel()
            {
                Title = m.Title,
                CommentsCount = m.CommentsCount,
                LikesCount = m.LikesCount,
                Description = m.Description,
                TextBlockOne = m.TextBlockOne,
                TextBlockThree = m.TextBlockThree,
                TextBlockTwo = m.TextBlockTwo,
                TypeOfWork = m.TypeOfWork,
                UserId = m.UserId,
                WorkId = m.WorkId
            };
        }

        public static WorkViewModel MapWorkModelToWorkViewModel(WorkModel m)
        {
            return new WorkViewModel()
            {
                Title = m.Title,
                CommentsCount = m.CommentsCount,
                LikesCount = m.LikesCount,
                Description = m.Description,
                TextBlockOne = m.TextBlockOne,
                TextBlockThree = m.TextBlockThree,
                TextBlockTwo = m.TextBlockTwo,
                TypeOfWork = m.TypeOfWork,
                UserId = m.UserId,
                WorkId = m.WorkId
            };
        }
    }
}

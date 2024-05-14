﻿using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewModels;
using ShowWorkUI.Pages;
using System;

namespace ShowWork.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;


        public ResumeController(ICurrentUser currentUser, IProfile profile, IResume resume)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
        }

        [Route("/resume/{UserId}")]
        public async Task<IActionResult> Index(int UserId)
        {
            var currentUserId = await currentUser.GetCurrentUserId();
            if(currentUserId != null) 
            {
                if (currentUserId == UserId) return Redirect("/profile");
                var userProfile = await profile.GetAllProfiles();
                var p = userProfile.Where(x => x.UserId == UserId).FirstOrDefault();
                var f = await resume.GetUserFollows((int)currentUserId);
                var w = await profile.GetProfileWorks(UserId);
                Tuple<UserModel?, IEnumerable<UserModel>, IEnumerable<WorkModel?>> tuple = new Tuple<UserModel?, IEnumerable<UserModel>, IEnumerable<WorkModel?>>(p,f,w);
                return View(tuple);
            }
            
            return View("Index");
            
        }
    }
}

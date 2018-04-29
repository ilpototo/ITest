﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITest.Infrastructure.Providers;
using ITest.Models.ResultsViewModels;
using ITest.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IMappingProvider mapper;
        private readonly IUserTestsService userTestsService;

        public ResultsController(IMappingProvider mapper,
                                 IUserTestsService userTestsService)
        {
            this.mapper = mapper;
            this.userTestsService = userTestsService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowResults()
        {
            var model = new ResultBagViewModel();

            var userTests = this.userTestsService.GetAllUserTests();
            model.ResultBag = mapper.ProjectTo<ResultsViewModel>(userTests.AsQueryable());


            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RecalculateTests()
        {
            this.userTestsService.RecalculateAllTestsScore();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
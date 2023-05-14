using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUsetId);
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUsetId));
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInputModel courseCreateInputModel)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            courseCreateInputModel.UserId = _sharedIdentityService.GetUsetId;
            var newcourse = await _catalogService.AddCourseAsync(courseCreateInputModel);
            return RedirectToAction(nameof(Index), "Courses");
        }
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseIdAsync(id);
            var categories = await _catalogService.GetAllCategoryAsync();

            if (course == null)
                return RedirectToAction(nameof(Index));

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.CategoryId);

            CourseUpdateInputModel umodel = new()
            {
                CategoryId = course.CategoryId,
                Description = course.Description,
                Feature = course.Feature,
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                UserId = course.UserId,
                Picture = course.Picture,
                CreateDate = course.CreateDate


            };
            return View(umodel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInputModel courseUpdateInputModel)
        {
            var course = await _catalogService.GetByCourseIdAsync(courseUpdateInputModel.Id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInputModel.CategoryId);
            if (!ModelState.IsValid)
                return View();

            await _catalogService.UpdateCourseAsync(courseUpdateInputModel);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

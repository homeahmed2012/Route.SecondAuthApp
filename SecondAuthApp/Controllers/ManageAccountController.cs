using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SecondAuthApp.Data;
using SecondAuthApp.Models;
using SecondAuthApp.ViewModels;

namespace SecondAuthApp.Controllers
{
    public class ManageAccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageAccountController(ApplicationDbContext dbContext,
                                        UserManager<IdentityUser> userManager,
                                        SignInManager<IdentityUser> signInManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult RegisterNewAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterNewStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewStudent(RegisterStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser() { Email = viewModel.Email, UserName = viewModel.Email };
                var result = await _userManager.CreateAsync(newUser, viewModel.Password);

                if (result.Succeeded)
                {
                    
                    Student newStudent = new Student() { Year = viewModel.Year, AppUserId = newUser.Id };
                    _dbContext.Students.Add(newStudent);
                    _dbContext.SaveChanges();
                    await _userManager.AddToRoleAsync(newUser, "StudentRole");
                    await _signInManager.SignInAsync(newUser, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegisterNewTeacher() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewTeacher(RegisterTeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser() { Email = viewModel.Email, UserName = viewModel.Email };
                var result = await _userManager.CreateAsync(newUser, viewModel.Password);
                if (result.Succeeded)
                {
                    Teacher newTeacher = new Teacher() { Salary = viewModel.Salary, HireDate = viewModel.HireDate, AppUserId = newUser.Id };
                    _dbContext.Add(newTeacher);
                    _dbContext.SaveChanges();

                    await _userManager.AddToRoleAsync(newUser, "TeacherRole");
                    await _signInManager.SignInAsync(newUser, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "This Email already exists");
                }

            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddAllRoles()
        {
            IdentityRole StudentRole = new IdentityRole() { Name = "StudentRole" };
            IdentityRole TeacherRole = new IdentityRole() { Name = "TeacherRole" };

            var resutl = await _roleManager.CreateAsync(StudentRole);
            var result2 = await _roleManager.CreateAsync(TeacherRole);

            if(resutl.Succeeded && result2.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return Content("an error happend ");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

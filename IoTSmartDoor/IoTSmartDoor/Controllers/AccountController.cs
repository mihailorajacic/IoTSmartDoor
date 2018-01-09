using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IoTSmartDoor.Models;
using IoTSmartDoor.Services;
using IoTSmartDoor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace IoTSmartDoor.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private IHostingEnvironment _appEnviroment;
        private IFaceAPIService _faceAPIService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IHostingEnvironment appEnviroment, IFaceAPIService faceAPIService)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appEnviroment = appEnviroment;
            _faceAPIService = faceAPIService;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(user != null)
            {
                var result = await _singInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if(result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("result", "Username/password not found");
            return View(loginViewModel);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { filePath });
        }

        private string GetImage(IFormFile file, out string imageName)
        {
            if (file.Length > 0)
            {
                var uploads = Path.Combine(_appEnviroment.WebRootPath, "images");
                imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, imageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return filePath;
            }
            imageName = null;
            return null;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser() { Name = registerViewModel.Name, UserName = registerViewModel.Email, Email = registerViewModel.Email, Allowed = false };
                var imageUrl = GetImage(registerViewModel.Image, out string imageName);
                user.ImageName = imageName;
                if (imageUrl != null)
                {
                    try
                    {
                        var facesTask = _faceAPIService.DetectAsync(imageUrl);
                        facesTask.Wait();

                        var faces = facesTask.Result;
                        if (faces.Length != 1)
                        {
                            ModelState.AddModelError("photo", "There must be exactly one face in the image. Image has to be JPEG, PNG or BMP, size from 1KB to 4MB. Face size must be from 36x36 to 4096x4096 pixels.");
                            System.IO.File.Delete(imageUrl);
                            return View(registerViewModel);
                        }

                        user.ImageUrl = imageUrl;

                        var listTask = _faceAPIService.AddFaceToFaceListAsync(imageUrl, "no");
                        listTask.Wait();

                        user.PersistedFaceId = listTask.Result.PersistedFaceId.ToString();
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("photo", "There must be exactly one face in the image. Image has to be JPEG, PNG or BMP, size from 1KB to 4MB. Face size must be from 36x36 to 4096x4096 pixels.");
                        System.IO.File.Delete(imageUrl);
                        return View(registerViewModel);
                    }

                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }  
            }

            ModelState.AddModelError("result", "User wtih email " + registerViewModel.Email + " already exists!");
            return View(registerViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
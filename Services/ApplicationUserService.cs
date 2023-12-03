using AutoMapper;
using Core.Domain;
using Core.DTO;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;

namespace Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IBookingsServices _bookingsServices;

        public ApplicationUserService(IUnitOfWork UnitOfWork, IMapper mapper, IBookingsServices bookingsServices)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
            _bookingsServices = bookingsServices;
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            return await _unitOfWork.ApplicationUser.GetUsersCountInRole(roleName);
        }

        #region base methods

        public IActionResult GetAll(int Page, int PageSize, string search)
        {
            return _unitOfWork.ApplicationUser.GetAll(Page, PageSize, search);
        }
        #endregion

        public async Task<IActionResult> AddUser(UserDTO userDTO, UserRole userRole)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);
            string? Role = Enum.GetName(userRole);

            if (Role == null)
            {
                return new NotFoundObjectResult($"Role {userRole} is not found");
            }

            try
            {
                var result = await _unitOfWork.ApplicationUser.Add(user);
                if (result is OkResult)
                {
                    await _unitOfWork.ApplicationUser.AssignRoleToUser(user, Role);
                    try
                    {
                        await _unitOfWork.ApplicationUser.AddSignInCookie(user, userDTO.RememberMe);
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.ApplicationUser.DeleteUser(user);
                        return new ObjectResult($"An error occurred while Creating cookie \n: {ex.Message}")
                        {
                            StatusCode = 500
                        };
                    }
                    return new OkObjectResult(user);
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.ApplicationUser.DeleteUser(user);
                return new BadRequestObjectResult($"{ex.Message}\n {ex.InnerException?.Message}");
            }
        }

        public IActionResult GetImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return new NotFoundResult();
            }

            //return PhysicalFile(imagePath, "image/jpeg");
            byte[] fileBytes = System.IO.File.ReadAllBytes(imagePath);
            var fileStream = new MemoryStream(fileBytes);
            string fileName = Path.GetFileName(imagePath);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, null, fileName);

            return new OkObjectResult(formFile);
        }
        public async Task<ActionResult> ValidateUser(string Email, String Password, bool RememberMe)
        {
            ApplicationUser user = await _unitOfWork.ApplicationUser.GetUserByEmail(Email);

            if (user == null)
            {
                return new UnauthorizedObjectResult($"No User With Email {Email}");
            }

            bool valid = await _unitOfWork.ApplicationUser.CheckUserPassword(user, Password);
            if (!valid)
            {
                return new UnauthorizedObjectResult(new { message = "Invalid email or password" });
            }
            return new OkObjectResult(user);
        }
        public async Task<IActionResult> SignIn(string Email, string Password, bool RememberMe)
        {
            // Handle failed login scenarios
            var ValidationResult =  await ValidateUser(Email, Password, RememberMe);

            if (ValidationResult is not OkObjectResult OkReult)
            {
                return ValidationResult;
            }

            if(OkReult == null)
            {
                return new ObjectResult("User") { StatusCode = 500 };
            }
            
            ApplicationUser User = OkReult.Value as ApplicationUser;
            string UserId = User.Id;

            // Store user and doctor information in Cookie
            List<Claim> userClaims = new List<Claim>();

            bool IsDoctor = await _unitOfWork.ApplicationUser.IsInRole(User, "Doctor");
            if (IsDoctor)
            {
                int doctorId =  _unitOfWork.Doctors.GetDoctorIdByUserId(UserId);
                userClaims.Add(new Claim("DoctorId", doctorId.ToString()));
            }


             await _unitOfWork.ApplicationUser.SignInUser(User, RememberMe, userClaims); ;

             return new OkObjectResult(User);
        }

        public async Task SignOut()
        {
            await _unitOfWork.ApplicationUser.SignOut();
        }
    }
}




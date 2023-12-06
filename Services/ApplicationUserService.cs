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
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Drawing;

namespace Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ApplicationUserService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetUsersCountInRole(string roleName)
        {
            return await _unitOfWork.ApplicationUser.GetUsersCountInRole(roleName);
        }
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

                    //Don't log in if added user is doctor
                    if (userRole == UserRole.Patient)
                    {
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
                return new BadRequestObjectResult($"There is a problem during adding user \n" +
                    $"{ex.Message}\n {ex.InnerException?.Message}");
            }
        }

        protected Image GetImage(string imagePath)
        {
            
                if (string.IsNullOrEmpty(imagePath))
                {
                    return null;
                }

                //return PhysicalFile(imagePath, "image/jpeg");

                //byte[] fileBytes = System.IO.File.ReadAllBytes(imagePath);
                //var fileStream = new MemoryStream(fileBytes);
                //string fileName = Path.GetFileName(imagePath);
                //var formFile = new FormFile(fileStream, 0, fileStream.Length, null, fileName);
                // return new OkObjectResult(formFile);

                //Read the image file into a byte array
                //byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                //// Determine the content type based on the file extension
                //string contentType = GetContentType(Path.GetExtension(imagePath));

                //// Return the image as a file result
                //return new OkObjectResult(File(imageBytes, contentType));

                return Image.FromFile(imagePath);
        }
        //private string GetContentType(string fileExtension)
        //{
        //    // Add more content types as needed
        //    switch (fileExtension.ToLower())
        //    {
        //        case ".jpg":
        //        case ".jpeg":
        //            return "image/jpeg";
        //        case ".png":
        //            return "image/png";
        //        case ".gif":
        //            return "image/gif";
        //        default:
        //            return "application/octet-stream"; // Default content type
        //    }
        //}
        private async Task<ActionResult> ValidateUser(string Email, String Password, bool RememberMe)
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

        public IActionResult ChangeBookingState(int BookingId, BookingState bookingState)
        {
            Booking booking = _unitOfWork.Bookings.GetById(BookingId);

            if (booking == null)
            {
                return new NotFoundObjectResult($"Booking Id {BookingId} is not exist");
            }

            if (booking.BookingState != BookingState.Pending)
            {
                return new BadRequestObjectResult($"Tis booking is {booking.BookingState} " +
                    $"Can't change this booking state now.");
            }
            
            booking.BookingState = bookingState;
            try
            {
                _unitOfWork.Bookings.Update(booking);
                _unitOfWork.Complete();
                return new OkObjectResult("Successful operation");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"There is problem during booking confirmation")
                {
                    StatusCode = 500
                };
            }
        }
        public async Task<IActionResult> UpdateUser(ApplicationUser CurrentUser, UserDTO userDTO)
        {
            try
            {
                ApplicationUser NewUserData = _mapper.Map<ApplicationUser>(userDTO);
                CurrentUser.PhoneNumber = NewUserData.PhoneNumber;
                CurrentUser.FullName = NewUserData.FullName;
                CurrentUser.DateOfBirth = NewUserData.DateOfBirth;
                CurrentUser.Email = NewUserData.Email;
                CurrentUser.Gender = NewUserData.Gender;
                CurrentUser.Image = NewUserData.Image;
                CurrentUser.PasswordHash = NewUserData.PasswordHash;

                var result = await _unitOfWork.ApplicationUser.Update(CurrentUser);
                return result;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"There is a problem during updating user\n" +
                    $"{ex.Message}\n {ex.InnerException?.Message}");
            }
        }

    }
}




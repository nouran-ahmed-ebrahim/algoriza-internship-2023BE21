﻿using Core.Domain;
using Core.Repository;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Migrations;
using Services;

namespace Vezeeta.Controllers
{
    [Route("api/admin/statistics")]
    [ApiController]
    public class AdminStatisticsController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBookingsServices _bookingsServices;

        public AdminStatisticsController(IApplicationUserService ApplicationUserService, IBookingsServices bookingsServices)
        {
            _applicationUserService = ApplicationUserService;
            _bookingsServices = bookingsServices;
        }
        
        [HttpGet("Doctors")]
        public IActionResult GetNumberOfDoctors()
        {
            string? Role = Enum.GetName(UserRole.Doctor);
            return _applicationUserService.GetUsersCountInRole(Role).Result;
        }

        [HttpGet("Patients")]
        public IActionResult GetNumberOfPatients()
        {
            string? Role = Enum.GetName(UserRole.Patient);

            return _applicationUserService.GetUsersCountInRole(Role).Result;
        }

        [HttpGet("Bookings")]
        public IActionResult GetNumberOfBookings()
        {
            return  _bookingsServices.NumOfBookings();
        }
    }
}

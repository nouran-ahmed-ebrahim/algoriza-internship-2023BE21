using Core.DTO;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDoctorServices : IApplicationUserService
    {
        IActionResult ConfirmCheckUp(int BookingId);
        Task<IActionResult> AddDoctor(UserDTO userDTO, UserRole patient, string specialize);
        Task<IActionResult> Delete(int id);
        IActionResult AddAppointments(int DoctorId,AppointmentsDTO appointments);
        IActionResult SetPrice(int doctorId, decimal price);
    }
}

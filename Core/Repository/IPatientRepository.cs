using Core.Domain;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IPatientRepository:IApplicationUserRepository
    {
        Task<IActionResult> GetAllPatients(int Page, int PageSize, 
                                Func<PatientDTO, bool> criteria = null);

        bool IsExist(string id);
    }
}

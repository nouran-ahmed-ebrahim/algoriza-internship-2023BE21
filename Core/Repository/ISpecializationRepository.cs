using Core.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Core.Repository
{
    public interface ISpecializationRepository: IBaseRepository<Specialization>
    {
        public Specialization GetByName(string Specialization);
        IActionResult GetTop5();
    }
}

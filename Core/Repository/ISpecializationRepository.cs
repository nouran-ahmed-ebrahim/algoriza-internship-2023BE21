using Core.Domain;


namespace Core.Repository
{
    public interface ISpecializationRepository: IBaseRepository<Specialization>
    {
        public Specialization GetByName(string Specialization);
    }
}

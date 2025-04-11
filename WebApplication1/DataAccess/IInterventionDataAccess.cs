namespace WebApplication1
{
    public interface IInterventionDataAccess
    {
        Task<IEnumerable<Intervention>> GetAllForTechnicianAsync(string userId);
        Task<Intervention> Create(Intervention intervention);
    }
}

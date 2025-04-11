namespace WebApplication1
{
    public interface IInterventionService
    {
        Task<IEnumerable<Intervention>> GetAllForTechnicianAsync(string userId);
        Task<Intervention> CreateAsync(Intervention intervention, List<string> technicianIds, int typeId, string clientId);
    }
}

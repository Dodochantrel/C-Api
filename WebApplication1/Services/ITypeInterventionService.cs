namespace WebApplication1
{
    public interface ITypeInterventionService
    {
        Task<IEnumerable<TypeIntervention>> GetAllAsync();
        Task<TypeIntervention> GetByIdAsync(int id);
        Task<TypeIntervention> CreateAsync(TypeIntervention typeIntervention);
        Task<TypeIntervention> UpdateAsync(TypeIntervention typeIntervention);
        Task<bool> DeleteAsync(int id);
    }
}

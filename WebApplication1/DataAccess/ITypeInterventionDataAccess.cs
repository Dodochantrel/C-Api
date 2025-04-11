namespace WebApplication1
{
    public interface ITypeInterventionDataAccess
    {
        Task<IEnumerable<TypeIntervention>> GetAllAsync();
        Task<TypeIntervention?> GetByIdAsync(int id);
        Task<TypeIntervention> CreateAsync(TypeIntervention typeIntervention);
        Task<bool> DeleteAsync(int id);
        Task<TypeIntervention> UpdateAsync(TypeIntervention typeIntervention);
    }
}

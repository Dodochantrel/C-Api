namespace WebApplication1
{
    public class TypeInterventionService : ITypeInterventionService
    {
        private readonly ITypeInterventionDataAccess _dataAccess;

        public TypeInterventionService(ITypeInterventionDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TypeIntervention>> GetAllAsync()
        {
            return await _dataAccess.GetAllAsync();
        }

        public async Task<TypeIntervention> GetByIdAsync(int id)
        {
            return await _dataAccess.GetByIdAsync(id);
        }

        public async Task<TypeIntervention> CreateAsync(TypeIntervention typeIntervention)
        {
            return await _dataAccess.CreateAsync(typeIntervention);
        }

        public async Task<TypeIntervention> UpdateAsync(TypeIntervention typeIntervention)
        {
            return await _dataAccess.UpdateAsync(typeIntervention);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dataAccess.DeleteAsync(id);
        }
    }
}

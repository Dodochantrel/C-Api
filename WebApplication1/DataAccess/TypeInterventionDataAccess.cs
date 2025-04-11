using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class TypeInterventionDataAccess : ITypeInterventionDataAccess
    {
        private readonly AppDbContext _context;

        public TypeInterventionDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeIntervention>> GetAllAsync()
        {
            return await _context.TypeInterventions.ToListAsync();
        }

        public async Task<TypeIntervention?> GetByIdAsync(int id)
        {
            return await _context.TypeInterventions.FindAsync(id);
        }

        public async Task<TypeIntervention> CreateAsync(TypeIntervention typeIntervention)
        {
            _context.TypeInterventions.Add(typeIntervention);
            await _context.SaveChangesAsync();
            return typeIntervention;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var typeIntervention = await GetByIdAsync(id);
            if (typeIntervention == null) return false;

            _context.TypeInterventions.Remove(typeIntervention);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TypeIntervention> UpdateAsync(TypeIntervention typeIntervention)
        {
            var existingTypeIntervention = await GetByIdAsync(typeIntervention.Id);
            if (existingTypeIntervention == null) return null;

            existingTypeIntervention.Label = typeIntervention.Label;

            _context.TypeInterventions.Update(existingTypeIntervention);
            await _context.SaveChangesAsync();
            return existingTypeIntervention;
        }
    }
}

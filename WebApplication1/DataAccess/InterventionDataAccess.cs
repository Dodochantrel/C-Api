using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class InterventionDataAccess : IInterventionDataAccess
    {
        private readonly AppDbContext _context;

        public InterventionDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intervention>> GetAllForTechnicianAsync(string userId)
        {
            return await _context.Interventions
                .Include(i => i.Client)
                .Include(i => i.Technicians)
                .Include(i => i.TypeIntervention)
                .Where(i => i.Technicians.Any(t => t.Id == userId))
                .ToListAsync();
        }

        public async Task<Intervention> Create(Intervention intervention)
        {
            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();
            return intervention;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using WebApplication1.Identity;
using WebApplication1.Resources;

namespace WebApplication1
{
    public class InterventionService : IInterventionService
    {
        private readonly IInterventionDataAccess _dataAccess;
        private readonly ITypeInterventionDataAccess _typeInterventionDataAccess;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<Trad> _localizer;

        public InterventionService(IInterventionDataAccess dataAccess, ITypeInterventionDataAccess typeInterventionDataAccess, UserManager<ApplicationUser> userManager, IStringLocalizer<Trad> localizer)
        {
            _dataAccess = dataAccess;
            _typeInterventionDataAccess = typeInterventionDataAccess;
            _userManager = userManager;
            _localizer = localizer;
        }

        public async Task<IEnumerable<Intervention>> GetAllForTechnicianAsync(string userId)
        {
            return await _dataAccess.GetAllForTechnicianAsync(userId);
        }

        public async Task<Intervention> CreateAsync(Intervention intervention, List<string> technicianIds, int typeId, string clientId)
        {
            // Controller si la date est dépassé
            if (intervention.DateTime < DateTime.Now)
            {
                throw new Exception(_localizer["DatePassed"].Value);
            }
            // Controller si il n'y a pas plus de 2 techniciens
            if (technicianIds.Count > 2)
            {
                throw new Exception(_localizer["MaxTwoTechnicians"].Value);
            }
            var client = await _userManager.FindByIdAsync(clientId);
            if (client == null)
            {
                throw new Exception(_localizer["ClientNotFound"].Value);
            }
            intervention.Client = client;
            var type = await _typeInterventionDataAccess.GetByIdAsync(typeId);
            if (type == null)
            {
                throw new Exception(_localizer["TypeNotFound"].Value);
            }
            intervention.TypeIntervention = type;
            technicianIds.ForEach(async id =>
            {
                var technician = await _userManager.FindByIdAsync(id);
                if (technician != null)
                {
                    intervention.Technicians.Add(technician);
                }
            });
            return await _dataAccess.Create(intervention);
        }
    }
}

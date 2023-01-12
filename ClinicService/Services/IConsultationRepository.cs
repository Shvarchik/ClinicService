using ClinicService.Models;

namespace ClinicService.Services
{
    public interface IConsultationRepository : IRepository<Consultation>
    {
        IList<Consultation> GetAllByPetIdForPeriod(int petId, DateTime dateFrom, DateTime dateTo);
    }
}

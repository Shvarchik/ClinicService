namespace ClinicService.Models.Request
{
    public class CreateConsultationRequest
    {
        public int ClientId { get; set; }

        public int PetId { get; set; }

        public DateTime ConsultationDate { get; set; }

        public string Description { get; set; }
    }
}

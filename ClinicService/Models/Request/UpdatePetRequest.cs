﻿namespace ClinicService.Models.Request
{
    public class UpdatePetRequest
    {
        public int PetId { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }
}

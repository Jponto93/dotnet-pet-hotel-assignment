using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PetBreedType
    {
        Shepherd,
        Poodle,
        Beagle,
        Bulldog,
        Terrier,
        Boxer,
        Labrador,
        Retriever,

    }
    public enum PetColorType
    {
        White,
        Black,
        Golden,
        Tricolor,
        Spotted,

    }
    public class Pet
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public PetBreedType BreedType { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetColorType ColorType { get; set; }

        public DateTime? checkedInAt { get; set; }


        [ForeignKey("PetOwner")]
        public int petOwnerId { get; set; }
    }
}

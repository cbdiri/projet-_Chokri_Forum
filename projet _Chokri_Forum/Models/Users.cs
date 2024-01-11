using System.ComponentModel.DataAnnotations;

namespace projet__Chokri_Forum.Models
{
    public class Users
    {
        public int id { get; set; }

        [Required]
        public string pseudonyme { get; set; }

        [Required]
        public string motdepasse { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        public bool inscrit { get; set; }

        public bool valide { get; set; }

        public string cheminavatar { get; set; }

        public string signature { get; set; }

        public bool actif { get; set; } = true;

        public bool admin { get; set; } = false;


    }
}

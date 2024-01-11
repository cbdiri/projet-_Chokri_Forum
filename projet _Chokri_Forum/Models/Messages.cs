namespace projet__Chokri_Forum.Models
{
    public class Messages
    {
        public int id { get; set; }
        public string contenu { get; set; }

        public DateTime dateMsg { get; set; }

        public int PostID { get; set; }
        public int UserID { get; set; }
        public Users User { get; set; }
        public Posts Post { get; set; }


    }
}

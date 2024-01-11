namespace projet__Chokri_Forum.Models
{
    public class Posts
    {
        public int id { get; set; } 
        public string message { get; set; } 
        public DateTime datecreationmessage { get; set; } 
        public int ThemeID { get; set; } 
        public int UserID { get; set; } 
        public Users User { get; set; }
    }
}
    
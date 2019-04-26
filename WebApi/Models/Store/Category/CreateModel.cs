namespace WebApi.Models.Store
{
    public class CreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ImageID { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }
}

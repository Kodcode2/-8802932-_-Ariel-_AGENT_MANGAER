namespace BE_AgentGuard.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string name {  get; set; }
        public string position {  get; set; }
        public string photo_url {  get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool is_alive { get; set; }       
    }
}

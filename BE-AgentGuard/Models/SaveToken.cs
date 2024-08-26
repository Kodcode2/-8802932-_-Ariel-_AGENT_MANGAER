namespace BE_AgentGuard.Models
{
    public class SaveToken
    {
        public SaveToken(string id, string token)
        {
            this.id = id;
            this.token = token;
        }

        public string id { get; set; }
        public string token { get; set; }
    }
}

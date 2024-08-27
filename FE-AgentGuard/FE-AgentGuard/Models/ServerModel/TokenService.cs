namespace FE_AgentGuard.Models.ServerModel
{
    public class TokenService
    {
        public Tokens Token { get; private set; }

        public void SetToken(Tokens token)
        {
            Token = token;
        }
    }

}

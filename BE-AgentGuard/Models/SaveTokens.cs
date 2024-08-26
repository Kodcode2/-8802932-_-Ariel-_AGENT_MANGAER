using NuGet.Common;

namespace BE_AgentGuard.Models
{
    public static class SaveTokens
    {
        public static List<SaveToken> tokenAndID = new List<SaveToken> { new("SimulationServer",null),new("MVCServer",null) };
        

        //static Dictionary<string, Token> tokens = new Dictionary<string, Token>();
    }
}

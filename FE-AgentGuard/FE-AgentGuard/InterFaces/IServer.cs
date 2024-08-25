using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FE_AgentGuard.Interfaces
{
    public interface IServer<t>
    {
        Task<List<t>> GetObjectsAsync();
        Task<t> GetObjectAsync(int id);
        Task<t> CreateObjectAsync(t person);
        Task UpdateObjectAsync(t person,int id);
        Task DeleteObjectAsync(int id);
    }
}

using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface ISessionRepository
    {
        Task<TokenModel> CreateAsync(LoginModel model);

        void Remove(string token);
    }
}

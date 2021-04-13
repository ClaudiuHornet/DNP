using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment_1.Models;

namespace Assignment_1.Data
{
    public interface IAdultData
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
    }
}
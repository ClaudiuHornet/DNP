using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public interface IAdultData
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> GetFilteredAdultsAsync(int id);
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
    }
}
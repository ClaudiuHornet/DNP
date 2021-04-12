using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Data.Impl
{
    public class AdultData : IAdultData
    {
        [Inject] private FileContext _fileContext { get; set; }

        public AdultData()
        {
            _fileContext = new FileContext();
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            return _fileContext.Adults;
        }

        public async Task AddAdultAsync(Adult adult)
        {
            _fileContext.Adults.Add(adult);
            _fileContext.SaveChanges();
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult adultToRemove = _fileContext.Adults.First(a => a.Id == adultId);
            _fileContext.Adults.Remove(adultToRemove);
            _fileContext.SaveChanges();

        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FileData;
using Microsoft.AspNetCore.Components;
using Models;

namespace Assignment_1.Data
{
    public class AdultData:IAdultData
    {
        [Inject] private FileContext _fileContext { get; set; }

        public AdultData()
        {
            _fileContext = new FileContext();
        }

        public IList<Adult> GetAdults()
        {
            return _fileContext.Adults;
        }

        public void AddAdult(Adult adult)
        {
            _fileContext.Adults.Add(adult);
            _fileContext.SaveChanges();
        }

        public void RemoveAdult(int adultId)
        {
            Adult adultToRemove = _fileContext.Adults.First(a => a.Id == adultId);
            _fileContext.Adults.Remove(adultToRemove);
            _fileContext.SaveChanges();

        }
    }
}
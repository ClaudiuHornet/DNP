using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Repository.Impl
{
    public class AdultRepository : IRepositoryAdults
    {
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            return await dbContext.Adults.ToListAsync();
        }

        public async Task<Adult> GetFilteredAdultsAsync(int id)
        {
            IList<Adult> adults = await GetAdultsAsync();
            Adult adultToSend = null;
            foreach (Adult adult in adults)
            {
                if (id == adult.Id)
                {
                    adultToSend = adult;
                }
            }

            if (adultToSend != null)
            {
                return adultToSend;
            }
            else
            {
                throw new Exception("adult not found on API");
            }
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            EntityEntry<Adult> newlyAdded = await dbContext.Adults.AddAsync(adult);
            await dbContext.SaveChangesAsync();
            return newlyAdded.Entity;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            Adult adultToDelete = await dbContext.Adults.FirstOrDefaultAsync(adult => adult.Id == adultId);
            if (adultToDelete != null)
            {
                dbContext.Adults.Remove(adultToDelete);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
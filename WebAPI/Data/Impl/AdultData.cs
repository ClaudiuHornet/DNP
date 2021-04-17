﻿using System;
using System.Collections;
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

        public async Task<Adult> GetFilteredAdultsAsync(int id)
        {
            IList<Adult> adults = _fileContext.Adults;
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
            _fileContext.Adults.Add(adult);
            _fileContext.SaveChanges();
            return adult;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Console.WriteLine("Hello from AdultData removeAsync");
            Adult adultToRemove = _fileContext.Adults.First(a => a.Id == adultId);
            _fileContext.Adults.Remove(adultToRemove);
            _fileContext.SaveChanges();
        }
    }
}
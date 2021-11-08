using LPX2YCDProject2020.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class AddressRepository : IAddressRepository
    {
        private ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public List<Province> GetProvinceListAsync()
        {
            List<Province> provinces = _context.Provinces.ToList();
            return provinces;
        }

        public List<Suburb> GetSuburbListAsync()
        {
            List<Suburb> suburbs = _context.Suburbs.ToList();
            return suburbs;
        }

        public List<SubjectDetails> GetSubjectListAsync()
        {
            List<SubjectDetails> subject = _context.Subject.ToList();
            return subject;
        }

        public List<City> GetCityListAsync()
        {
            List<City> cities = _context.Cities.ToList();
            return cities;
        }

       
    }
}

using LPX2YCDProject2020.Models.Account;
using System.Collections.Generic;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public interface IAddressRepository
    {
        List<City> GetCityListAsync();
        List<Suburb> GetSuburbListAsync();
        List<Province> GetProvinceListAsync();
        List<SubjectDetails> GetSubjectListAsync();
    }
}
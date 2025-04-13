// Mapper/ProvinceMapper.cs
using ModelTest.Controllers;

public static class ProvinceMapper
{
    public static ThaiProvinceDto ToDto(ThaiProvince model)
    {
        return new ThaiProvinceDto
        {
            ThaiProvinceId = model.ThaiProvinceId,
            ThaiProvinceName = model.ThaiProvinceName,
            CountryName = model.Country?.Name,
            GeographyName = model.Geography?.GeographyName
        };
    }

    public static ProvinceDto ToDto(ProvinceModel model)
    {
        return new ProvinceDto
        {
            ProvinceId = model.ProvinceId,
            ProvinceName = model.ProvinceName,
            CountryName = model.Country?.Name,
            GeographyName = model.Geography?.GeographyName
        };
    }

    public static CountryDto ToDto(CountryModel model)
    {
        return new CountryDto
        {
            CountryId = model.CountryId,
            Name = model.Name
        };
    }
}

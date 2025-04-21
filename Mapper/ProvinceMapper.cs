using ModelTest.Controllers;

public static class ProvinceMapper
{
    public static ProvinceDto ToDto(ProvinceModel model)
    {
        return new ProvinceDto
        {
            ProvinceId = model.ProvinceId,
            ProvinceName = model.ProvinceName,
            CountryName = model.Country?.CountryName
        };
    }
}

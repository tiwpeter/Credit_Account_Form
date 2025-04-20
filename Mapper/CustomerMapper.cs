using ModelTest.Controllers;
public static class CustomerMapper
{
    public static AddressDto ToDto(AddressModel model)
    {
        return new AddressDto
        {
            Street = model.Street,
            ZipCode = model.ZipCode,
            ProvinceName = model.Province?.ProvinceName,
            CountryName = model.Province?.Country?.CountryName
        };
    }
}

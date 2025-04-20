using ModelTest.Controllers;
public static class CustomerMapper
{
    public static CustomerDto ToDto(CustomerModel model)
    {
        return new CustomerDto
        {
            CustomerId = model.CustomerId,
            CustomerName = model.CustomerName,
            General = model.General != null ? new GeneralDto
            {
                GeneralId = model.General.general_id,
                FullName = model.General.generalName,
                Address = model.General.Address != null ? new AddressDto
                {
                    Street = model.General.Address.Street,
                    ZipCode = model.General.Address.ZipCode,
                    Country = model.General.Address.Country != null ? new CountryDto
                    {
                        CountryId = model.General.Address.Country.CountryId,
                        CountryName = model.General.Address.Country.CountryName
                    } : null
                } : null
            } : null
        };
    }
}

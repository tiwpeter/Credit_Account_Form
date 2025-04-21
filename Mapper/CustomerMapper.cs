using ModelTest.Controllers;

public static class CustomerMapper
{
    public static CustomerDto ToDto(CustomerModel customer)
    {

        return new CustomerDto
        {
            CustomerId = customer.CustomerId,
            CustomerName = customer.CustomerName,
            General = new GeneralDto
            {
                GeneralId = customer.General.general_id,
                FullName = customer.General.generalName,
                Address = new AddressDto
                {
                    Street = customer.General.Address.Street,
                    ZipCode = customer.General.Address.ZipCode,
                    ProvinceName = customer.General.Address.Province?.ProvinceName,
                    CountryName = customer.General.Address.Country?.CountryName,
                    GeographyName = customer.General.Address.Province?.Geography?.GeographyName,
                    AmphureName = customer.General.Address.Province?.Amphures?
                        .FirstOrDefault(a => a.Tambons.Any(t => t.TambonName == customer.General.Address.Street))?.AmphureName,
                    TambonName = null // ใส่ใน AddressModel แล้วจะ map ได้ตรง
                }
            }
        };
    }
}

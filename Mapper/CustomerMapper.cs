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
                    CountryId = customer.General.Address.Country?.CountryId,

                    GeographyName = customer.General.Address.Province?.Geography?.GeographyName,
                    AmphureName = customer.General.Address.Province?.Amphures?
                        .FirstOrDefault(a => a.Tambons.Any(t => t.TambonName == customer.General.Address.Street))?.AmphureName,
                    TambonName = null // ใส่ใน AddressModel แล้วจะ map ได้ตรง
                }
            }
        };
    }
    //ตอนนี้คุณมีแค่ ToDto ต้องเพิ่ม ToModel เพื่อแปลงกลับแบบนี้:

    public static CustomerModel ToModel(CustomerDto dto)
    {
        return new CustomerModel
        {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            General = new GeneralModel
            {
                general_id = dto.General.GeneralId,
                generalName = dto.General.FullName,
                Address = new AddressModel
                {
                    Street = dto.General.Address.Street,
                    ZipCode = dto.General.Address.ZipCode,
                    Country = new CountryModel { CountryId = dto.General.Address.CountryId.GetValueOrDefault() },

                    Province = new ProvinceModel
                    {
                        ProvinceName = dto.General.Address.ProvinceName,
                        Geography = new GeographyModel
                        {
                            GeographyName = dto.General.Address.GeographyName
                        },
                        Amphures = new List<AmphureModel>
                    {
                        new AmphureModel
                        {
                            AmphureName = dto.General.Address.AmphureName,
                            Tambons = new List<TambonModel>
                            {
                                new TambonModel
                                {
                                    TambonName = dto.General.Address.TambonName
                                }
                            }
                        }
                    }
                    }
                }
            }
        };
    }

}

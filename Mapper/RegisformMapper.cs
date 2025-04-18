using ModelTest.Controllers;

public static class RegisformMapper
{
    public static RegisformDto ToDto(RegisformModel model)
    {
        var countryName = model.Address.Country?.CountryName;
        bool isThailand = countryName == "ประเทศไทย";

        return new RegisformDto
        {
            Id = model.Id,
            AddressId = model.Address.AddressId,
            Street = model.Address.Street,
            CountryId = model.Address.CountryId,
            CountryName = countryName,
            ProvinceId = model.Address.ProvinceId,
            ProvinceName = isThailand ? null : model.Address.Province?.ProvinceName,
            ThaiProvinceId = model.Address.ThaiProvinceId,
            ThaiProvinceName = isThailand ? model.Address.ThaiProvince?.ThaiProvinceName : null,

            // 👉 Shipping fields
            ShippingId = model.shipping_id,
            ShippingSubDistrict = model.Shipping?.subDistrict,
            ShippingProvinceId = model.Shipping?.ProvinceId ?? 0,
        };
    }

    public static RegisformModel ToModel(RegisformDto dto)
    {
        return new RegisformModel
        {
            Id = dto.Id,
            shipping_id = dto.ShippingId,
            Address = new AddressModel
            {
                AddressId = dto.AddressId,
                Street = dto.Street,
                CountryId = dto.CountryId,
                ProvinceId = dto.ProvinceId,
                ThaiProvinceId = dto.ThaiProvinceId
            },
            Shipping = new ShippingModel
            {
                shipping_id = dto.ShippingId,
                subDistrict = dto.ShippingSubDistrict,
                ProvinceId = dto.ShippingProvinceId
            }
        };
    }
}

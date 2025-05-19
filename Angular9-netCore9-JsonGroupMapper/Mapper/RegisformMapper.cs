using ModelTest.Controllers;

public static class RegisformMapper
{
    public static RegisformDto ToDto(RegisformModel Regis)
    {

        return new RegisformDto
        {
            Id = Regis.Id,

            Shipping = new ShippingDto
            {
                ShippingId = Regis.ShippingId,
                ShippingSubDistrict = Regis.Shipping?.subDistrict,
                ShippingProvinceId = Regis.Shipping?.ProvinceId ?? 0,
                ShippingProvinceName = Regis.Shipping?.Province?.ProvinceName
            },
            General = new GeneralModel
            {
                general_id = Regis.General?.general_id ?? 0,
                generalName = Regis.General?.generalName,
                Address = new AddressModel
                {
                    AddressId = Regis.General?.Address?.AddressId ?? 0,
                    Street = Regis.General?.Address?.Street,
                    CountryId = Regis.General?.Address?.CountryId ?? 0,
                    Country = Regis.General?.Address?.Country,

                    Province = Regis.General?.Address?.Province == null ? null : new ProvinceModel
                    {
                        ProvinceId = Regis.General.Address.Province.ProvinceId,
                        ProvinceName = Regis.General.Address.Province.ProvinceName,
                        // ไม่ใส่ Country หรืออื่นๆ ที่จะวนลูป
                    }
                    // ใส่ ProvinceId, ThaiProvinceId ตามต้องการ
                }
            }

        };
    }
    public static RegisformModel ToModel(RegisformDto dto)
    {
        return new RegisformModel
        {
            Id = dto.Id,
            General = new GeneralModel
            {
                general_id = dto.General?.general_id ?? 0,
                generalName = dto.General?.generalName,
                Address = new AddressModel
                {
                    AddressId = dto.General?.Address?.AddressId ?? 0,
                    Street = dto.General?.Address?.Street,
                    CountryId = dto.General?.Address?.CountryId ?? 0,
                    ProvinceId = dto.General?.Address?.ProvinceId,
                    ThaiProvinceId = dto.General?.Address?.ThaiProvinceId
                }
            },
            ShippingId = dto.Shipping?.ShippingId ?? 0,
            Shipping = new ShippingModel
            {
                shipping_id = dto.Shipping?.ShippingId ?? 0,
                subDistrict = dto.Shipping?.ShippingSubDistrict,
                ProvinceId = dto.Shipping?.ShippingProvinceId ?? 0
            }
        };
    }


}
//ToModel
//ToDto
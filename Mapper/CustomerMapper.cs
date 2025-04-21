using ModelTest.Controllers;

public static class ManualMapper
{
    // General
    public static GeneralDto ToDto(this GeneralModel model)
    {
        return new GeneralDto
        {
            GeneralId = model.general_id,
            GeneralName = model.generalName,
            AddressId = model.AddressId,
            Address = model.Address?.ToDto()
        };
    }

    public static GeneralModel ToEntity(this GeneralDto dto)
    {
        return new GeneralModel
        {
            general_id = dto.GeneralId,
            generalName = dto.GeneralName,
            AddressId = dto.AddressId,
            Address = dto.Address?.ToEntity()
        };
    }

    // Customer
    public static CustomerDto ToDto(this CustomerModel model)
    {
        return new CustomerDto
        {
            CustomerId = model.CustomerId,
            CustomerName = model.CustomerName,
            GeneralId = model.GeneralId,
            General = model.General?.ToDto()
        };
    }

    public static CustomerModel ToEntity(this CustomerDto dto)
    {
        return new CustomerModel
        {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            GeneralId = dto.GeneralId,
            General = dto.General?.ToEntity()
        };
    }

    // Address
    public static AddressDto ToDto(this AddressModel model)
    {
        return new AddressDto
        {
            AddressId = model.AddressId,
            CustomerName = model.CustomerName,
            CountryId = model.CountryId,
            Country = model.Country?.ToDto()
        };
    }

    public static AddressModel ToEntity(this AddressDto dto)
    {
        return new AddressModel
        {
            AddressId = dto.AddressId,
            CustomerName = dto.CustomerName,
            CountryId = dto.CountryId,
            Country = dto.Country?.ToEntity()
        };
    }

    // Country
    public static CountryDto ToDto(this CountryModel model)
    {
        return new CountryDto
        {
            CountryId = model.CountryId,
            CountryName = model.CountryName
        };
    }

    public static CountryModel ToEntity(this CountryDto dto)
    {
        return new CountryModel
        {
            CountryId = dto.CountryId,
            CountryName = dto.CountryName
        };
    }
}

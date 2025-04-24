using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModelTest.Controllers
{


    public class CreateCustomerRequest
    {
        // Customer
        public string CustomerName { get; set; }

        // General
        public string GeneralName { get; set; }

        // Address
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }

        // Shipping
        public string SubDistrict { get; set; }
        public int ShippingProvinceId { get; set; }

        // BusinessType
        public int BusinessTypeId { get; set; }

        // CreditInfo
        public decimal EstimatedPurchase { get; set; }
        public int TimeRequired { get; set; }
        public decimal CreditLimit { get; set; }

        // CustomerSign
        public string CustSignFirstName { get; set; }
    }


    public class GetCustomersDTO
    {
        public ShopTypeModel ShopType { get; set; }  // แบบ owned entity

        public string CustomerId { get; set; }
        public GeneralDto General { get; set; }
        public ShippingDto Shipping { get; set; }
        public BusinessTypeDTO BusinessType { get; set; }

        public CreditInfoDto CreditInfo { get; set; }  // ชี้ไปที่ CreditInfoDto

        public CustomerSignDto CustomerSign { get; set; }
    }

}
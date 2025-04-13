Customer
  ├── FK → General
  │       └── FK → AddressModel
  ├── FK → Shipping
  │       ├── FK → ProvinceModel
  │       │       └── FK → CountryModel


=> country/1 => thaiProvinceName

Shipping => find CountryId => Province

ค้น หาจังหวัดใน ไทยด้วย id CountryId
ตาราง thai_provinces สำหรับจังหวัดในประเทศไทยโดยเฉพาะ

if country/1 => find thaiProvinceName
esle if country/2 => Province


General => have AddressModel => CountryId Province


General => have AddressModel =>  CountryId Province
 Shipping => CountryId Province
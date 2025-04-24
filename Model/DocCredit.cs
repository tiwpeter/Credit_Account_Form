using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//สินเชื่อหรือการตรวจสอบเอกสารที่เกี่ยวข้องกับการกู้ยืมเงินในทางการเงิน
//ใช้เก็บข้อมูลว่าเอกสารเครดิตนั้นมีใบรับรองบริษัทหรือไม่
public class DocCreditModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocCreditId { get; set; }  // รหัสเอกสารเครดิต
    public bool CompanyCertificate { get; set; }  // True = มี, False = ไม่มี

    public bool CopyOfPP_20 { get; set; }  // True = มี, False = ไม่มี
                                           // การเก็บข้อมูลสำเนาของทะเบียนการค้า
    public bool CopyOfCoRegis { get; set; }  // True = มี, False = ไม่มี

    // การเก็บข้อมูลสำเนาของบัตรประชาชน
    public bool CopyOfIDCard { get; set; }  // True = มี, False = ไม่มี

    // การเก็บข้อมูลแผนที่ที่ตั้งของบริษัท
    public bool CompanyLocationMap { get; set; }  // True = มี, False = ไม่มี
    public string OtherSpecify { get; set; }  // ข้อความเพิ่มเติม

}

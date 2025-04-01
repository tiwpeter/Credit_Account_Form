//using System.Data.SQLite;

//ตัวแปร connectionString ใช้ในการเชื่อมต่อกับฐานข้อมูล SQLite
// ถ้าไฟล์นี้ยังไม่มีอยู่, SQLite จะสร้างไฟล์ฐานข้อมูลใหม่ขึ้นมาโดยอัตโนมัติ
//string connectionString = "Data Source=mydatabase.db;Version=3;";
//using (var connection = new SQLiteConnection(connectionString))
/*{
    //เปิดการเชื่อมต่อ
    connection.Open();

    //// การทำงานกับฐานข้อมูล (เช่น query หรือ insert ข้อมูล)
    //คำสั่ง SQL CREATE TABLE IF NOT EXISTS จะสร้างตารางใหม่ชื่อ users ในฐานข้อมูล ถ้าตารางนี้ยังไม่มีอยู่
    string createTableQuery = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT, age INTEGER)";

    //ตัวแปร command เป็นอ็อบเจ็กต์ของ SQLiteCommand ซึ่งใช้สำหรับการรันคำสั่ง SQL บนฐานข้อมูล
    // ใข้ตัว แปร command สำหรับการรันคำสั่ง SQL บนฐานข้อมูล
    using (var command = new SQLiteCommand(createTableQuery, connection))
    {
        //ในกรณีนี้, เExecuteNonQuery() เป็น เมธอด จะรันคำสั่ง createTableQuery ซึ่งจะทำให้ตารางใหม่ถูกสร้างขึ้นในฐานข้อมูล
        command.ExecuteNonQuery();
    }

    connection.Close();
}*/

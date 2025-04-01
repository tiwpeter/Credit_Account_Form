import xml.etree.ElementTree as ET

# ฟังก์ชันในการแปลงไฟล์ XML เป็น SQL
def xml_to_sql(xml_file, table_name):
    tree = ET.parse(xml_file)
    root = tree.getroot()

    sql_statements = []

    # สร้างคำสั่ง SQL สำหรับการสร้างตาราง
    columns = []

    for cell in root.findall(".//mxCell[@value]"):
        value = cell.get('value')
        if value:
            column_name = value.strip()
            if column_name:
                columns.append(f"`{column_name}` VARCHAR(255)")  # กำหนดให้ทุกคอลัมน์เป็น VARCHAR

    # สร้างคำสั่ง SQL สำหรับการสร้างตาราง
    create_table_sql = f"CREATE TABLE `{table_name}` (\n"
    create_table_sql += ",\n".join(columns)  # รวมคอลัมน์ทั้งหมด
    create_table_sql += "\n);"
    sql_statements.append(create_table_sql)

    # สร้างคำสั่ง SQL สำหรับการแทรกข้อมูล
    insert_sql = f"INSERT INTO `{table_name}` ({', '.join(columns)}) VALUES\n"
    insert_values = []
    
    for cell in root.findall(".//mxCell[@value]"):
        value = cell.get('value')
        if value:
            insert_values.append(f"('{value.strip()}')")  # แทรกค่าแต่ละรายการ
    insert_sql += ",\n".join(insert_values) + ";"
    sql_statements.append(insert_sql)

    return "\n\n".join(sql_statements)


# อ่านและแปลงไฟล์ XML 2 ไฟล์
def convert_multiple_xml_to_sql(files):
    all_sql = []
    for i, xml_file in enumerate(files):
        table_name = f"table_{i+1}"  # กำหนดชื่อของตาราง
        sql_output = xml_to_sql(xml_file, table_name)
        all_sql.append(sql_output)

    # เขียนผลลัพธ์ไปยังไฟล์ SQL
    with open("output.sql", "w") as sql_file:
        sql_file.write("\n\n".join(all_sql))

    print("SQL file generated successfully.")

# รายชื่อไฟล์ XML ที่ต้องการแปลง
xml_files = ["file1.xml"]  # แทนที่ด้วยชื่อไฟล์ XML ของคุณ

# เรียกใช้ฟังก์ชัน
convert_multiple_xml_to_sql(xml_files)

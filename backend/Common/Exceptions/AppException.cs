namespace CreditAccountApi.Common.Exceptions;

// ============================================================
// Exception ทั่วไปของระบบ
// ============================================================
public class AppException : Exception
{
    public int StatusCode { get; }
    public AppException(string message, int statusCode = 400)
        : base(message)
    {
        StatusCode = statusCode;
    }
}

// ============================================================
// ไม่พบข้อมูล
// ============================================================
public class NotFoundException : AppException
{
    public NotFoundException(string name, object key)
        : base($"ไม่พบข้อมูล '{name}' รหัส '{key}'", 404)
    {
    }
}

// ============================================================
// ข้อมูลไม่ถูกต้อง
// ============================================================
public class ValidationException : AppException
{
    public IEnumerable<string> Errors { get; }
    public ValidationException(IEnumerable<string> errors)
        : base("ข้อมูลไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง", 422)
    {
        Errors = errors;
    }
}
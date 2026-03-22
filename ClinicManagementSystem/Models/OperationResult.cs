namespace ClinicManagementSystem.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public OperationResult() { }

        public OperationResult(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static OperationResult SuccessResult(string message = "Operation completed successfully.", object data = null)
        {
            return new OperationResult(true, message, data);
        }

        public static OperationResult ErrorResult(string message = "An error occurred.", object data = null)
        {
            return new OperationResult(false, message, data);
        }
    }
}
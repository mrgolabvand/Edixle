namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
        }

        public OperationResult Succeeded(string message = "عملیات با موفقیت انجام شد.")
        {
            Message = message;
            IsSucceeded = true;
            return this;
        }

        public OperationResult Failed (string message)
        {
            Message = message;
            IsSucceeded = false;
            return this;
        }

    }
}

namespace Porter.Dto
{
    public class ErrorResponseDto
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }



        public ErrorResponseDto(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

    }
}

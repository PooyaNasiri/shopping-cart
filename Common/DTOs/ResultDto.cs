namespace Common.DTOs
{
    public class ResultDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class ResultDto<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}

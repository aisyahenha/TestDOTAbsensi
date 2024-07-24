namespace TestAbsensi.Exeptions
{
    public class BadRequest: Exception
    {
        public BadRequest()
        {
        }

        public BadRequest(string? message) : base(message)
        {
        }
    }
}

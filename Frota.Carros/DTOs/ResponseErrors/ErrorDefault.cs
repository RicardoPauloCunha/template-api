namespace Frota.Carros.DTOs.ResponseErrors
{
    public class ErrorDefault
    {
        public string Message { get; private set; }

        public ErrorDefault(string message)
        {
            Message = message;
        }
    }
}

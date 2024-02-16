namespace DomainServiceLayer.CustomExceptionService
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }

}

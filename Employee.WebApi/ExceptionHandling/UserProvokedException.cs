namespace Employee.WebApi.ExceptionHandling
{
    public class UserProvokedException : CustomException
    {
        public UserProvokedException(string message) 
            : base(message)
        {
            
        }
    }
}
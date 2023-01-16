namespace Testing.JobApplication.Services
{
    public interface IIdentityValidatior
    {
        bool IsValid(string identityNumber);
    }
}
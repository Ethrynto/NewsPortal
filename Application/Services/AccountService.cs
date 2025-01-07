using Application.Abstractions.Repositories;

namespace Application.Services;

public class AccountService(IAccountRepository accountRepository)
{
    public void Register(string username, string email, string password)
    {
        //accountRepository.Add(username, password);
    }
    
    public void Login(string username, string email, string password)
    {
        //accountRepository.Add(username, password);
    }
}
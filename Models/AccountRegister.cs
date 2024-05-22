using System;

namespace FadeFactory_Accounts.Models;
public class AccountRegister
{
    public string FirstName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}

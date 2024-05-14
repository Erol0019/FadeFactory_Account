using Microsoft.EntityFrameworkCore;
using FadeFactory_Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Cosmos;
using FadeFactory_Account.Data;
using DotEnv;

using (var fadeFactoryContext = new FadeFactoryContext())
{
    #region Inserting Accounts

    var account1 = new Account()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "Martin",
        FirstName = "Kenneth",
        birthDate = new DateTime(1998, 12, 8),
        Email = "hygge@hotmail.com",
        Address = "Kea 1.tv",
        Phone = "20202020"

    };

    var account2 = new Account()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "John",
        FirstName = "Wick",
        birthDate = new DateTime(1992, 10, 10),
        Email = "Emanuel@hotmail.com",
        Address = "Kea 3.tv",
        Phone = "21212121",
    };
    fadeFactoryContext.Accounts?.Add(account1);
    fadeFactoryContext.Accounts?.Add(account2);

    await fadeFactoryContext.SaveChangesAsync();

    Console.WriteLine("accounts inserted");

    #endregion

    // #region Inserting Customers


    // Customer customer1 = new Customer()
    // {
    //     Id = Guid.NewGuid().ToString(),
    //     LastName = "Denzel",
    //     FirstName = "Washington",
    //     birthDate = new DateTime(1993, 11, 8),
    //     Email = "Okay@hotmail.com",
    //     Address = "Kea 2.tv",
    //     Phone = "22222222",
    //     Orders = new List<Order>()
    //     {
    //         new Order()
    //         {
    //             Id = Guid.NewGuid().ToString(),
    //             Fade = 200.00
    //         },
    //     }
    // };
    // fadeFactoryContext.Customers?.Add(customer1);
    // await fadeFactoryContext.SaveChangesAsync();

    // Console.WriteLine("customers inserted");
    // #endregion

    // #region GET Accounts

    // if (fadeFactoryContext.Accounts != null)
    // {
    //     var accounts = await fadeFactoryContext.Accounts.ToListAsync();
    //     Console.WriteLine("");

    //     foreach (var account in accounts)
    //     {
    //         Console.WriteLine($"Account Id: {account.Id}");
    //         Console.WriteLine($"Account FirstName: {account.FirstName}");
    //         Console.WriteLine($"Account LastName: {account.LastName}");
    //         Console.WriteLine($"Account birthDate: {account.birthDate}");
    //         Console.WriteLine($"Account Email: {account.Email}");
    //         Console.WriteLine($"Account Address: {account.Address}");
    //         Console.WriteLine($"Account Phone: {account.Phone}");
    //         Console.WriteLine("--------------------------------\n");
    //     }
    // }
    // #endregion

    // #region Get an Account by FirstName

    // if (fadeFactoryContext.Accounts != null)
    // {
    //     var account = await fadeFactoryContext.Accounts
    //         .Where(a => a.FirstName == "Wick")
    //         .FirstOrDefaultAsync();

    //     Console.WriteLine("");

    //     Console.WriteLine($"Account Id: {account?.Id}");
    //     Console.WriteLine($"Account FirstName: {account?.FirstName}");
    //     Console.WriteLine($"Account LastName: {account?.LastName}");
    //     Console.WriteLine($"Account birthDate: {account?.birthDate}");
    //     Console.WriteLine($"Account Email: {account?.Email}");
    //     Console.WriteLine($"Account Address: {account?.Address}");
    //     Console.WriteLine($"Account Phone: {account?.Phone}");
    //     Console.WriteLine("--------------------------------\n");
    // }

    // #endregion

    // #region Update an Account

    // if (fadeFactoryContext.Accounts != null)
    // {
    //     var account = await fadeFactoryContext.Accounts
    //         .Where(e => e.FirstName == "Wick")
    //         .FirstOrDefaultAsync();

    //     if (account != null)
    //     {
    //         account.LastName = "Dog";
    //         account.birthDate = new DateTime(2002, 12, 01);

    //         await fadeFactoryContext.SaveChangesAsync();

    //         Console.WriteLine("\nIt has been updated.\n");
    //     }
    // }

    // #endregion
    // #region Delete an Account

    // if (fadeFactoryContext.Accounts != null)
    // {
    //     var account = await fadeFactoryContext.Accounts
    //         .Where(e => e.FirstName == "Kenneth")
    //         .FirstOrDefaultAsync();

    //     if (account != null)
    //     {
    //         fadeFactoryContext.Accounts.Remove(account);
    //         await fadeFactoryContext.SaveChangesAsync();

    //         Console.WriteLine("\nThe account has been deleted.\n");
    //     }
    // }

    // #endregion
}
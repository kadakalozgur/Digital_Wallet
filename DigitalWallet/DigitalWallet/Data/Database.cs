using DigitalWallet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;

namespace DigitalWallet.Data
{
    public class Database : DbContext
    {

        public Database(DbContextOptions<Database> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Process> Transactions { get; set; }
    }
}

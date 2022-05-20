using JWTAKAR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAKAR.Data
{
    public class JwtDataContext:IdentityDbContext<User>
    {

        // bildiğğimiz ef context aslında da ıdentity server içerisinde ef yi kullanıyoruz başka bir esprisi yok 
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Server = .;Database=jwtloginapp; Integrated Security = true");

        }


        public DbSet<User> User { get; set; }
    }
}

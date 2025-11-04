using EduQuiz.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Infrastructure;

public class EduQuizDbContext(DbContextOptions<EduQuizDbContext> options) : IdentityDbContext<UserDbTable>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
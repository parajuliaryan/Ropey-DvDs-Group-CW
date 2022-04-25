using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ropey_DvDs_Group_CW.Models;
using Ropey_DvDs_Group_CW.Models.ViewModels;

namespace Ropey_DvDs_Group_CW.DBContext
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        /*
        public DbSet<UserRegisterModel> UserRegisterModel { get; set; }

        public DbSet<ActorModel> ActorModel { get; set; }

        public DbSet<StudioModel> StudioModel { get; set; }

        public DbSet<ProducerModel> ProducerModel { get; set; }

        public DbSet<LoanTypeModel> LoanTypeModel { get; set; }

        public DbSet<DVDCategoryModel> DVDCategoryModel { get; set; }

        public DbSet<MembershipCategoryModel> MembershipCategoryModel { get; set; }

        public DbSet<DVDTitleModel> DVDTitleModel { get; set; }

        public DbSet<DVDCopyModel> DVDCopyModel { get; set; }

        public DbSet<CastMemberModel> CastMemberModel { get; set; }

        public DbSet<MemberModel> MemberModel { get; set; }

        public DbSet<LoanModel> LoanModel { get; set; }
        */

    }
}

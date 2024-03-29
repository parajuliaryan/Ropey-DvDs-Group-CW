﻿using Microsoft.AspNetCore.Identity;
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
        public DbSet<Ropey_DvDs_Group_CW.Models.DVDTitleModel> DVDTitleModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.ActorModel> ActorModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.CastMemberModel> CastMemberModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.LoanTypeModel> LoanTypeModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.LoanModel> LoanModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.DVDCategoryModel> DVDCategoryModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.DVDCopyModel> DVDCopyModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.MemberModel> MemberModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.ProducerModel> ProducerModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.MembershipCategoryModel> MembershipCategoryModel { get; set; }
        public DbSet<Ropey_DvDs_Group_CW.Models.StudioModel> StudioModel { get; set; }

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

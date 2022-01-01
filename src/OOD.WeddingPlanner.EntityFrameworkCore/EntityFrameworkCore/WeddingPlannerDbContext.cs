using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using OOD.WeddingPlanner.Locations;
using Volo.Abp.EntityFrameworkCore.Modeling;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitees;
using OOD.WeddingPlanner.Invitations;
using OOD.WeddingPlanner.Weddings;
using OOD.WeddingPlanner.InvitationDesigns;
using OOD.WeddingPlanner.Tables;

namespace OOD.WeddingPlanner.EntityFrameworkCore
{
  [ReplaceDbContext(typeof(IIdentityDbContext))]
  [ReplaceDbContext(typeof(ITenantManagementDbContext))]
  [ConnectionStringName("Default")]
  public class WeddingPlannerDbContext :
      AbpDbContext<WeddingPlannerDbContext>,
      IIdentityDbContext,
      ITenantManagementDbContext
  {
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Location> Locations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Invitee> Invitees { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Wedding> Weddings { get; set; }
    public DbSet<InvitationDesign> InvitationDesigns { get; set; }
    public DbSet<Table> Tables { get; set; }

    public WeddingPlannerDbContext(DbContextOptions<WeddingPlannerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      /* Include modules to your migration db context */

      builder.ConfigurePermissionManagement();
      builder.ConfigureSettingManagement();
      builder.ConfigureBackgroundJobs();
      builder.ConfigureAuditLogging();
      builder.ConfigureIdentity();
      builder.ConfigureIdentityServer();
      builder.ConfigureFeatureManagement();
      builder.ConfigureTenantManagement();

      builder.Entity<Location>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Locations", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasMany(p => p.Events).WithOne(p => p.Location);
      });


      builder.Entity<Event>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Events", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasOne(p => p.Location).WithMany(p => p.Events);
        b.HasOne(p => p.Wedding).WithMany(p => p.Events);
        b.HasMany(p => p.Tables).WithOne(p => p.Event);
      });


      builder.Entity<Invitee>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Invitees", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasOne(p => p.Invitation).WithMany(p => p.Invitees);
        b.HasOne(p => p.Table).WithMany(p => p.Invitees);
      });


      builder.Entity<Invitation>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Invitations", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasMany(p => p.Invitees).WithOne(p => p.Invitation);
        b.HasOne(p => p.Wedding).WithMany(p => p.Invitations);
        b.HasOne(p => p.Design).WithMany(p => p.Invitations);
      });


      builder.Entity<Wedding>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Weddings", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasMany(p => p.Invitations).WithOne(p => p.Wedding);
        b.HasMany(p => p.Events).WithOne(p => p.Wedding);
      });


      builder.Entity<InvitationDesign>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "InvitationDesigns", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasMany(p => p.Invitations).WithOne(p => p.Design);
      });


      builder.Entity<Table>(b =>
      {
        b.ToTable(WeddingPlannerConsts.DbTablePrefix + "Tables", WeddingPlannerConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasOne(p => p.Event).WithMany(p => p.Tables);
        b.HasMany(p => p.Invitees).WithOne(p => p.Table);
      });
    }
  }
}

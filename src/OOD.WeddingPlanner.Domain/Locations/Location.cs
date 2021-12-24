using System;
using System.Collections.Generic;
using OOD.WeddingPlanner.Events;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Locations
{
  public class Location : FullAuditedEntity<Guid>, IMultiTenant
  {
    public virtual Guid? TenantId { get; set; }

    public virtual string Name { get; set; }

    public virtual string Description { get; set; }

    public virtual double Longitude { get; set; }

    public virtual double Latitude { get; set; }

    public virtual List<Event> Events { get; set; } = new List<Event>();

    protected Location()
    {
    }

    public Location(
        Guid id,
        Guid? tenantId,
        string name,
        string description,
        double longitude,
        double latitude
    ) : base(id)
    {
      TenantId = tenantId;
      Name = name;
      Description = description;
      Longitude = longitude;
      Latitude = latitude;
    }
  }
}

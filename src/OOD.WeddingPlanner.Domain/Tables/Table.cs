using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Invitees;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace OOD.WeddingPlanner.Tables
{
  public class Table : FullAuditedEntity<Guid>, IMultiTenant
  {
    public virtual Guid? TenantId { get; set; }

    public virtual Guid? EventId { get; set; }

    public virtual string Name { get; set; }

    public virtual string Description { get; set; }

    public virtual int Size { get; set; }

    public virtual int Row { get; set; }

    public virtual int Column { get; set; }

    public virtual Event Event { get; set; }

    public virtual List<Invitee> Invitees { get; set; } = new List<Invitee>();

    protected Table()
    {
    }

    public Table(
        Guid id,
        Guid? tenantId,
        Guid? eventId,
        string name,
        int size,
        int x,
        int y
    ) : base(id)
    {
      TenantId = tenantId;
      EventId = eventId;
      Name = name;
      Size = size;
      Row = x;
      Column = y;
    }
  }
}

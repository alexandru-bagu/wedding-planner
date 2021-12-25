using System;
using System.ComponentModel;
namespace OOD.WeddingPlanner.Invitations.Dtos
{
  [Serializable]
  public class CreateUpdateInvitationDto
  {
    public Guid? WeddingId { get; set; }
    
    public string Destination { get; set; }
  }
}
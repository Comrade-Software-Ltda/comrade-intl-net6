using Comrade.Domain.Bases;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comrade.Domain.Models;

[Table("syus_system_user_syro_system_role")]
public class SystemUserSystemRole : Entity
{
    public SystemUserSystemRole()
    {
        SystemUserId = new Guid();
        SystemRoleId = new Guid();
    }

    public SystemUserSystemRole(Guid systemUserId, Guid systemRoleId)
    {
        SystemUserId = systemUserId;
        SystemRoleId = systemRoleId;
    }

    [Column("syus_uuid_system_user")]
    //[BsonId]
    //[BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "System user Id is required")]
    public Guid SystemUserId { get; set; }

    [Column("syro_uuid_system_role")]
    //[BsonId]
    //[BsonRepresentation(BsonType.String)]
    [Required(ErrorMessage = "System role Id is required")]
    public Guid SystemRoleId { get; set; }
}
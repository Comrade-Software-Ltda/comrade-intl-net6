﻿using Comrade.Domain.Models;

namespace Comrade.Persistence.Mappings;

public class SystemPermissionConfiguration : IEntityTypeConfiguration<SystemPermission>
{
    public void Configure(EntityTypeBuilder<SystemPermission> builder)
    {
        builder.Property(b => b.Id).HasColumnName("sype_uuid_system_permission").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_sype_system_permission");

        builder.HasMany(x => x.SystemRoles)
            .WithMany(x => x.SystemPermissions)
            .UsingEntity(j => j.ToTable("syro_system_role_sype_system_permission"));
    }
}
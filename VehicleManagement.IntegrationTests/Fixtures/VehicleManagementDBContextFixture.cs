using Microsoft.EntityFrameworkCore;
using VehicleManagement.Infrastructure.Data.DBContexts;

namespace VehicleManagement.IntegrationTests.Fixtures;

public class VehicleManagementDBContextFixture : BaseDBContextFixture<VehicleManagementDBContext>
{
    protected override VehicleManagementDBContext Build(DbContextOptions<VehicleManagementDBContext> options)
    {
        return new VehicleManagementDBContext(options);
    }
}

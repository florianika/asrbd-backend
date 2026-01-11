using Application.Queries.GetBuildingWithQuePendingLogs;
using Application.Queries.GetBuildingWithQuePendingLogs.Request;
using Application.Queries.GetBuildingWithQuePendingLogs.Response;
using Application.Queries.GetStatisticsFromRules.Response;
using Application.Queries.GetStatisticsFromRules;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Queries.GetBuildingWithQuePendingLogs
{
    public class GetBuildingWithQuePendingLogsQuery : IGetBuildingWithQuePendingLogs
    {
        private readonly DataContext _context;
        public GetBuildingWithQuePendingLogsQuery(DataContext context)
        {
            _context = context;
        }
        public async Task<GetBuildingWithQuePendingLogsResponse> Execute(GetBuildingWithQuePendingLogsRequest request)
        {
            var rows = await _context.Set<BuildingIdDTO>()
                .FromSqlRaw(
                    "EXEC [asrbd-qms].[dbo].[GetBuildingWithQuePendingLogs] @MunicipalityCode",
                    new SqlParameter("@MunicipalityCode", request.MunicipalityCode)
                )
                .ToListAsync();

            var list = rows.Select(x => x.BldId).ToList();

            return new GetBuildingWithQuePendingLogsResponse
            {
                BuildingIds = list
            };            
        }       
    }
}

using System;
using Domain.Enum;

namespace Application.Dtos.Quality
{
    public class EntranceDto : BaseDto
    {
        public int EntId { get; set; }
        public int EntBuildingId { get; set; }
        public string? EntAddressId { get; set; }
        public QualityCheck EntQuality { get; set; } = QualityCheck.UNTESTED_DATA;
        public double EntLatitude { get; set; }
        public double EntLongitude { get; set; }
        public string? EntBuildingNumber { get; set; }
        public string? EntEntranceNumber { get; set; }
        public GeoPointStatus EntPointStatus { get; set; } = GeoPointStatus.UNKNOWN;
        public int EntStreetCode { get; set; }
        public int EntTownCode { get; set; }
        public int EntZipCode { get; set; }
        public int EntDwellingRecs { get; set; } = 999;
        public int EntDwellingExpec { get; set; } = 999;
        public Guid Fk_building { get; set; }


    }
}

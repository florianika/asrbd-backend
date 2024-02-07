using System;
using Domain.Enum;

namespace Application.Dtos.Quality
{
    public class BuildingDto : BaseDto
    {  
        public string? BldCensus2023 { get; set; }
        public string? BldAddressId { get; set; }
        public QualityCheck BldQuality { get; set; } = QualityCheck.UNTESTED_DATA;
        public string BldMunicipality { get; set; } = "99";
        public string BldEnumerationArea { get; set; } = "999999";
        public double BldLatitude { get; set; }
        public double BldLongitude { get; set; }
        public int BldCadastralZone { get; set; }
        public string? BldProperty { get; set; }
        public string? BldPermitNumber { get; set; }
        public DateOnly BldPermitDate { get; set; }
        public BuildingStatus BldStatus { get; set; } = BuildingStatus.UNKNOWN;
        public int BldYearOfConstruction { get; set; } = 9999;
        public int BldYearOfDemolition { get; set; } = 9999;
        public BuildingType BldType { get; set; } = BuildingType.UNKNOWN;
        public BuildingClass BldClass { get; set; } = BuildingClass.UNKNOWN_BUILDING_CLASS;
        public int BldArea { get; set; } = 9999;
        public int BldFloorsAbove { get; set; } = 99;
        public Presence BldFloors { get; set; } = Presence.UNKNOWN;
        public int BldHeight { get; set; } = 999;
        public double BldVolume { get; set; } = 9999.99;
        public Presence BldPipedWater { get; set; } = Presence.UNKNOWN;
        public Presence BldRainWater { get; set; } = Presence.UNKNOWN;
        public Presence BldWasteWater { get; set; } = Presence.UNKNOWN;
        public Presence BldElectricity { get; set; } = Presence.UNKNOWN;
        public Presence BldPipedGas { get; set; } = Presence.UNKNOWN;
        public Presence BldElevator { get; set; } = Presence.UNKNOWN;
        public GeoPointStatus BldCentroidStatus { get; set; } = GeoPointStatus.UNKNOWN;
        public int BldEntranceRecs { get; set; } = 99;
        public int BldDwellingRecs { get; set; } = 999;

    }
}

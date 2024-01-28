using System;
using Domain.Enum;

namespace Application.Quality
{
    public class DwellingDto : BaseDto
    {
        public double DwlId { get; set; }
        public int DwlEntranceId { get; set; }
        public string? DwlCensus2023 { get; set; }
        public string? DwlAddressId { get; set; }
        public QualityCheck DwlQuality { get; set; } = QualityCheck.UNTESTED_DATA;
        public int DwlFloor { get; set; }
        public string? DwlApartNumber { get; set; }
        public DwellingStatus DwlStatus { get; set; } = DwellingStatus.UNKNOWN;
        public int DwlYearOfConstruction { get; set; } = 9999;
        public int DwlYearOfElimination { get; set; } = 9999;
        public DwellingType DwlType { get; set; } = DwellingType.UNKNOWN;
        public Ownership DwlOwnership { get; set; } = Ownership.UNKNOWN;
        public Occupancy DwlOccupancy { get; set; } = Occupancy.UNKNOWN;
        public int DwlSurface { get; set; } = 999;
        public WaterSupplyType DwlWaterSupply { get; set; } = WaterSupplyType.UNKNOWN;
        public ToiletType DwlToilet { get; set; } = ToiletType.UNKNOWN;
        public Presence DwlBath { get; set; } = Presence.UNKNOWN;
        public HeatingFacilityType DwlHeatingFacility { get; set; } = HeatingFacilityType.UNKNOWN;
        public HeatingEnergyType DwlHeatingEnergy { get; set; } = HeatingEnergyType.UNKNOWN;
        public Presence DwlAirConditioner { get; set; }  = Presence.UNKNOWN;
        public Presence DwlSolarPanel { get; set;} = Presence.UNKNOWN;
        public Guid Fk_entrance { get; set; }

    }
}

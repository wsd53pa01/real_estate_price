using System;
using FileHelpers;

namespace RealEstatePrice.Service.FileHelpers
{
    [DelimitedRecord(",")]
    public class LandCsv
    {
        public string TheVillagesAndTownsUrbanDistrict;
        public string TransactionSign;
        public string HouseNumberPlate;
        public string AreaSquareMeter;
        public string UseZoning;
        public string NonMetropolisLandUseDistrict;
        public string NonMetropolisLandUse;
        public string TransactionDate;
        public string TransactionNumber;
        public string ShiftingLevel;
        public string TotalFlorNumber;
        public string BuildingState;
        public string MainUse;
        public string MainBuildingMaterials;
        public string CompleteDate;
        public string BuildingShiftingTotalArea;
        public string Room;
        public string Hall;
        public string Health;
        public string Compartmented;
        public string ManageOrganization;
        public string TotalPrices;
        public string UnitPrices;
        public string BerthCategory;
        public string BerthAreaSquareMeter;
        public string BerthTotalPrices;
        public string Note;
        public string SerialNumber;
        public string MainBuildingArea;
        public string OutbuildingArea;
        public string BalconyArea;
        public string Elevator;
        public string ShiftingSerialNumber;
    }
}
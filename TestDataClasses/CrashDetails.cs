using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class CrashDetails
    {
        public List<object> responseMessages { get; set; }
        public List<Datums> data { get; set; }
        public int status { get; set; }
    }
    public class Crashes
    {
        public string ReferenceNumber { get; set; }
        public object CrashSeverityId { get; set; }
        public int NumberOfVehicles { get; set; }
        public int NumberOfCasualties { get; set; }
        public int CrashDate { get; set; }
        public object CrashTime { get; set; }
        public object DayOfWeekId { get; set; }
        public object LocationEasting { get; set; }
        public object LocationNorthing { get; set; }
        public object CrashDescription { get; set; }
        public object LocationDescription { get; set; }
        public object CollisionTypeId { get; set; }
        public object CollisionTypeAngle { get; set; }
        public int Id { get; set; }
        public int ValidationStatusId { get; set; }
        public object UserDepartment { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int CreatedOn { get; set; }
        public int LastModifiedOn { get; set; }
        public object MobileCrashId { get; set; }
        public int RowVersion { get; set; }
        public object AccidentLocation { get; set; }
        public object PoliceForceId { get; set; }
        public object DriverRankTitleId { get; set; }
        public object DriverSurName { get; set; }
        public object DriverServiceNumber { get; set; }
        public object Age { get; set; }
        public object DriverExperienceId { get; set; }
        public object ReportingUIN { get; set; }
        public object ModOrganisation { get; set; }
        public object TLB { get; set; }
        public object WorkAddress { get; set; }
        public object PostCode { get; set; }
        public object BLB { get; set; }
        public object CountryId { get; set; }
        public object UINAddressStatusId { get; set; }
        public object ContactNumber { get; set; }
        public object DrivingTime { get; set; }
        public object DrivingMiles { get; set; }
        public object RestDrivingTime { get; set; }
        public object RestDrivingMile { get; set; }
        public object TotalTimeOnDuty { get; set; }
        public object TotalMileOnDuty { get; set; }
        public object CrashOnModPropertyId { get; set; }
        public object CrashArea { get; set; }
        public object CountryOfCrashId { get; set; }
        public object RoadNumber { get; set; }
        public object AuthorizedJourneyId { get; set; }
        public object AuthorizedRouteId { get; set; }
        public object FamilirisationTrainingId { get; set; }
        public object UnderInstructionAtTimeOfCrashId { get; set; }
        public object NameOfExcercise { get; set; }
        public object ImpactOnModVehicleId { get; set; }
        public object ModVehicleDrivableId { get; set; }
        public object VehicleCarryingDangerousGoodsId { get; set; }
        public object RoadConfigurationId { get; set; }
        public object TrafficConditionId { get; set; }
        public object WeatherConditionId { get; set; }
        public object CarriageWayTypeId { get; set; }
        public object GradientId { get; set; }
        public object StreetLightingId { get; set; }
        public object PeriodOfDayId { get; set; }
        public object ModVehicleStatusId { get; set; }
        public object RoadSpeedLimit { get; set; }
        public object EstimatedSpeedOfVehicle { get; set; }
        public object SeatBeltStatusId { get; set; }
        public object ThirdPartyInvolvedStatusId { get; set; }
        public object EmergencyServiceAttendedStatusId { get; set; }
        public object CrashDescriptionByDriver { get; set; }
        public object PrecipitatingFactorId { get; set; }
        public object ContributingFactorId { get; set; }
        public object NumberOfWitness { get; set; }
        public object NameOfFirstWitness { get; set; }
        public object ServiceNumberOfFirstWitness { get; set; }
        public object ContactNumberOfFirstWitness { get; set; }
        public object AddressOfFirstWitness { get; set; }
        public object ReportedToRelevantClaimHandlerId { get; set; }
        public object ClaimHandlerReferenceNo { get; set; }
        public object NumberOfThirdParties { get; set; }
        public bool Declaration { get; set; }
        public object SubmittedById { get; set; }
        public object RankTitle { get; set; }
        public object SurName { get; set; }
        public object ServiceNumber { get; set; }
        public int DateSubmitted { get; set; }
        public object UTMEmail { get; set; }
        public object NameOfSecondWitness { get; set; }
        public object ServiceNumberOfSecondWitness { get; set; }
        public object ContactNumberOfSecondWitness { get; set; }
        public object AddressOfSecondWitness { get; set; }
        public object RoadSurfaceConditionId { get; set; }
        public object RoadSurfaceTypeId { get; set; }
        public object DriverLicenseTypeId { get; set; }
        public object Regiment { get; set; }
        public object SkiddingAndOverturingId { get; set; }
        public int Source { get; set; }
        public int CompletedOn { get; set; }
        public object Vehicles { get; set; }
        public object Casualties { get; set; }
        public List<object> CrashValues { get; set; }
    }

    public class Vehicle
    {
        public string VehicleReferenceNumber { get; set; }
        public object VehicleTypeId { get; set; }
        public int Id { get; set; }
        public int CrashId { get; set; }
        public object MobileVehicleId { get; set; }
        public object DrivingPositionId { get; set; }
        public object VehicleUINStatusId { get; set; }
        public object VehicleOwnershipId { get; set; }
        public object VehicleMannedId { get; set; }
        public object ThirdPartyPropertyId { get; set; }
        public object ThirdPartyVehicleStatusId { get; set; }
        public object ThirdPartyVehicleInitialImpactStatusId { get; set; }
        public object ThirdPartyBumpCardStatusId { get; set; }
        public object ResponsibilityStatusId { get; set; }
        public object ThirdPartyResponsibilityStatusId { get; set; }
        public object VehicleRegistrationNumber { get; set; }
        public object AssetDescription { get; set; }
        public object AssetCode { get; set; }
        public object VehicleUIN { get; set; }
        public object ThirdPartyName { get; set; }
        public object ThirdPartyContactNumber { get; set; }
        public object ThirdPartyAddress { get; set; }
        public object DriverPostcode { get; set; }
        public object ThirdPartyVehicleMake { get; set; }
        public object ThirdPartyVehicleModel { get; set; }
        public object ThirdPartyInsuranceCompanyName { get; set; }
        public object ThirdPartyInsurancePolicyNumber { get; set; }
        public object NumberOfPeopleInVehicle { get; set; }
        public object PlateNumber { get; set; }
        public object VehicleHireNumber { get; set; }
        public object Crash { get; set; }
        public List<object> VehicleValues { get; set; }
    }

    public class Datums
    {
        public Crashes Crash { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<object> Casualties { get; set; }
    }


}

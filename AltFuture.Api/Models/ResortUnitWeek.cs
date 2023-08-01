
namespace AltFuture.DataAccessLayer.Models;

public class ResortUnitWeek
{
    public string RegionCode { get; set; }
    public int ResortId { get; set; }   
    public int UnitId { get; set; }
    public int WeekId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

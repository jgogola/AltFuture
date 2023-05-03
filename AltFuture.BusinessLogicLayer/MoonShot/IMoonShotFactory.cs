
namespace AltFuture.BusinessLogicLayer.MoonShot;

public interface IMoonShotFactory
{
    MoonShot Create(MoonShotTypeEnum moonShotType, int appUserId);
}

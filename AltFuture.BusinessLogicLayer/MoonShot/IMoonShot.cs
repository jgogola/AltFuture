namespace AltFuture.BusinessLogicLayer.MoonShot
{
    public interface IMoonShot
    {
        bool MoonShotExists { get; }
        int MoonShotTransactionReferenceNum { get; }
        MoonShotTypeEnum MoonShotType { get; }

        bool Add();
        bool Remove();
    }
}
using System.ComponentModel.DataAnnotations;

namespace AltFuture.BusinessLogicLayer.MoonShot;

public enum MoonShotTypeEnum
{
    None = 0,
    Bitcoin = 1,
    [Display(Name = "Shiba Inu")]
    ShibaInu = 6
}

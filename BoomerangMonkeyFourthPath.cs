using MelonLoader;
using BTD_Mod_Helper;
using BoomerangMonkeyFourthPath;

[assembly: MelonInfo(typeof(BoomerangMonkeyFourthPath.BoomerangMonkeyFourthPath), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BoomerangMonkeyFourthPath;

public class BoomerangMonkeyFourthPath : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<BoomerangMonkeyFourthPath>("BoomerangMonkeyFourthPath loaded!");
    }
}
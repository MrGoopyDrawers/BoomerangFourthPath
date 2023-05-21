using MelonLoader;
using BTD_Mod_Helper;
using BoomerangMonkeyFourthPath;
using PathsPlusPlus;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using JetBrains.Annotations;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppSystem.IO;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using UnityEngine;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using System.Runtime.CompilerServices;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace BoomerangMain;

public class BoomerangMainPath : BloonsTD6Mod
{
    public class FourthPath2 : PathPlusPlus
    {
        public override string Tower => TowerType.BoomerangMonkey;
        public override int UpgradeCount => 5;

    }
    public class SonicBoom : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 150;
        public override int Tier => 1;
        public override string Icon => "tier1Icon";

        public override string? Portrait => "tier1";
        public override string Description => "Smashes right through frozen bloons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.ApplyDisplay<tier1Display>();
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
        }
    }
    public class tier1Display : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey);
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "boomerangMonkeyT1");
        }
    }
    public class MOABSplitter : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 550;
        public override int Tier => 2;
        public override string Icon => VanillaSprites.MOABSHREDRUpgradeIcon;

        public override string? Portrait => "tier2";
        public override string Description => "Gains more damage to MOAB class bloons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.ApplyDisplay<tier2Display>();
            attackModel.weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("Moabs", "Moabs", 1f, 4f, false, false));
        }
    }
    public class tier2Display: ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey);
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "boomerangMonkeyT2");
        }
    }
    public class Bomberman : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 1750;
        public override int Tier => 3;
        public override string Icon => VanillaSprites.BiggerBombsUpgradeIcon;

        public override string? Portrait => "tier3";
        public override string Description => "Throws exploding boomerangs that stun bloons on impact.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.ApplyDisplay<tier3Display>();
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
            attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetWeapon().projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
            attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-400").GetWeapon().projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
        }
    }
    public class tier3Display : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey);
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "boomerangMonkeyT3");
            node.RemoveBone("SuperMonkeyRig:Dart");
        }
    }
    public class ExplodingPineapples : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 7500;
        public override int Tier => 4;
        public override string Icon => VanillaSprites.ExplodingPineappleUpgradeIcon;

        public override string? Portrait => "tier4";
        public override string Description => "Can now throws pineapples really fast, doing devastating damage to MOAB class. Attack range increased.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.ApplyDisplay<tier4Display>();
            towerModel.range *= 1.4f;
            attackModel.range *= 1.4f;
            var newProj = attackModel.weapons[0].Duplicate();
            newProj.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            newProj.rate *= 0.4f;
            newProj.projectile.ApplyDisplay<pineapple>();
            newProj.projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
            newProj.projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
            newProj.projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
            attackModel.AddWeapon(newProj);
        }
    }

    public class tier4Display : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey, 4, 0 ,0);
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "boomerangMonkeyT4");
            node.RemoveBone("SuperMonkeyRig:Dart");
        }
    }
    public class pineapple : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "pineapple");
        }
    }
    public class GrenadeLauncher : UpgradePlusPlus<FourthPath2>
    {
        public override int Cost => 100000;
        public override int Tier => 5;
        public override string Icon => VanillaSprites.TsarBombaUpgradeIcon;

        public override string? Portrait => "tier5";
        public override string Description => "Throws grenades that split into MOAB missles.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.ApplyDisplay<tier5Display>();
            var newProj = Game.instance.model.GetTowerFromId("DartlingGunner-050").GetWeapon().Duplicate();
            newProj.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            newProj.rate *= 0.4f;
            newProj.projectile.ApplyDisplay<grenade>();
            var proj = Game.instance.model.GetTowerFromId("BombShooter-050").GetAttackModel().weapons[0].Duplicate();
            newProj.projectile.AddBehavior(new CreateProjectileOnContactModel("CreateProjectileOnContactModel_", proj.projectile, new ArcEmissionModel("ArcEmissionModel_", 16, 0.0f, 360.0f, null, false), true, false, false));
            attackModel.AddWeapon(newProj);
        }
    }
    public class tier5Display : ModDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey, 0, 5, 0);
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "boomerangMonkeyT5");
        }
    }
    public class grenade : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "grenade");
        }
    }
}

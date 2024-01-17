using BepInEx;
using BepInEx.Configuration;
using R2API;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSpawnChanceForCleansingPoolsChestsAndShrines_v2
{
    // This is an example plugin that can be put in
    // BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    // It's a small plugin that adds a relatively simple item to the game,
    // and gives you that item whenever you press F2.

    // This attribute specifies that we have a dependency on a given BepInEx Plugin,
    // We need the R2API ItemAPI dependency because we are using for adding our item to the game.
    // You don't need this if you're not using R2API in your plugin,
    // it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency(ItemAPI.PluginGUID)]

    // This one is because we use a .language file for language tokens
    // More info in https://risk-of-thunder.github.io/R2Wiki/Mod-Creation/Assets/Localization/
    [BepInDependency(LanguageAPI.PluginGUID)]

    // This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    // This is the main declaration of our plugin class.
    // BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    // BaseUnityPlugin itself inherits from MonoBehaviour,
    // so you can use this as a reference for what you can declare and use in your plugin class
    // More information in the Unity Docs: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	public class CustomSpawnChanceForCleansingPoolsChestsAndShrines_v2 : BaseUnityPlugin
	{
		//define mod ID: uses the format of "com.USERNAME.MODNAME"
		public const string PluginGUID = "com.pseudo.CustomInteractiblesSpawnChance";

		//define the mod name inside quotes. Can be anything.
		public const string PluginName = "Custom spawn shrine";

		//define mod version inside quotes. Follows format of "MAJORVERSION.MINORPATCH.BUGFIX". Ex: 1.2.3 is Major Release 1, Patch 2, Bug Fix 3.
		public const string PluginVersion = "1.0.3";


		public static ConfigEntry<float> InteractibleCountMultiplier { get; set; }

		public static ConfigEntry<float> IscChest1 { get; set; }
		public static ConfigEntry<float> IscChest2 { get; set; }
		public static ConfigEntry<float> IscChest1Stealthed { get; set; }

		public static ConfigEntry<float> IscCategoryChestDamage { get; set; }
		public static ConfigEntry<float> IscCategoryChestHealing { get; set; }
		public static ConfigEntry<float> IscCategoryChestUtility { get; set; }
		public static ConfigEntry<float> IscCategoryChest2Damage { get; set; }
		public static ConfigEntry<float> IscCategoryChest2Healing { get; set; }
		public static ConfigEntry<float> IscCategoryChest2Utility { get; set; }
		public static ConfigEntry<float> IscGoldChest { get; set; }

		public static ConfigEntry<float> IscTripleShop { get; set; }
		public static ConfigEntry<float> IscTripleShopLarge { get; set; }
		public static ConfigEntry<float> IscTripleShopEquipment { get; set; }
		public static ConfigEntry<float> IscCasinoChest { get; set; }

		public static ConfigEntry<float> IscBarrel1 { get; set; }
		public static ConfigEntry<float> IscLunarChest { get; set; }
		public static ConfigEntry<float> IscEquipmentBarrel { get; set; }
		public static ConfigEntry<float> IscScrapper { get; set; }
		public static ConfigEntry<float> IscRadarTower { get; set; }

		public static ConfigEntry<float> IscDuplicator { get; set; }
		public static ConfigEntry<float> IscDuplicatorLarge { get; set; }
		public static ConfigEntry<float> IscDuplicatorWild { get; set; }
		public static ConfigEntry<float> IscDuplicatorMilitary { get; set; }

		public static ConfigEntry<float> IscBrokenTurret1 { get; set; }
		public static ConfigEntry<float> IscBrokenDrone1 { get; set; }
		public static ConfigEntry<float> IscBrokenDrone2 { get; set; }
		public static ConfigEntry<float> IscBrokenEmergencyDrone { get; set; }
		public static ConfigEntry<float> IscBrokenMissileDrone { get; set; }
		public static ConfigEntry<float> IscBrokenEquipmentDrone { get; set; }
		public static ConfigEntry<float> IscBrokenFlameDrone { get; set; }
		public static ConfigEntry<float> IscBrokenMegaDrone { get; set; }

		public static ConfigEntry<float> IscShrineChance { get; set; }
		public static ConfigEntry<float> IscShrineCombat { get; set; }
		public static ConfigEntry<float> IscShrineBlood { get; set; }
		public static ConfigEntry<float> IscShrineBoss { get; set; }
		public static ConfigEntry<float> IscShrineHealing { get; set; }
		public static ConfigEntry<float> IscShrineRestack { get; set; }
		public static ConfigEntry<float> IscShrineGoldshoresAccess { get; set; }
		public static ConfigEntry<float> IscShrineCleanse { get; set; }

		public static ConfigEntry<float> IscVoidCamp { get; set; }
		public static ConfigEntry<float> IscVoidChest { get; set; }
		public static ConfigEntry<float> IscVoidTriple { get; set; }
		public static ConfigEntry<float> IscVoidCoinBarrel { get; set; }

		private static readonly List<string> Interactibles = new()
		{
			"iscChest1",
			"iscChest2",
			"iscChest1Stealthed",

			"iscCategoryChestDamage",
			"iscCategoryChestHealing",
			"iscCategoryChestUtility",
			"iscCategoryChest2Damage",
			"iscCategoryChest2Healing",
			"iscCategoryChest2Utility",
			"iscGoldChest",

			"iscTripleShop",
			"iscTripleShopLarge",
			"iscTripleShopEquipment",
			"iscCasinoChest",

			"iscBarrel1",
			"iscLunarChest",
			"iscEquipmentBarrel",
			"iscScrapper",
			"iscRadarTower",

			"iscDuplicator",
			"iscDuplicatorLarge",
			"iscDuplicatorWild",
			"iscDuplicatorMilitary",

			"iscBrokenTurret1",
			"iscBrokenDrone1",
			"iscBrokenDrone2",
			"iscBrokenEmergencyDrone",
			"iscBrokenMissileDrone",
			"iscBrokenEquipmentDrone",
			"iscBrokenFlameDrone",
			"iscBrokenMegaDrone",

			"iscShrineChance",
			"iscShrineCombat",
			"iscShrineBlood",
			"iscShrineBoss",
			"iscShrineHealing",
			"iscShrineRestack",
			"iscShrineGoldshoresAccess",
			"iscShrineCleanse",

			"iscVoidCamp",
			"iscVoidChest",
            //"iscVoidTriple",
            "iscVoidCoinBarrel"
		};

		private static readonly Dictionary<string, string> InteractibleToLocalized = new Dictionary<string, string>
		{
			{"iscChest1", "Small Chest"},
			{"iscChest2", "Large Chest"},
			{"iscChest1Stealthed", "Invisible Chest"},

			{"iscCategoryChestDamage", "Damage Chest"},
			{"iscCategoryChestHealing", "Healing Chest"},
			{"iscCategoryChestUtility", "Utility Chest"},
			{"iscCategoryChest2Damage", "Large Damage Chest"},
			{"iscCategoryChest2Healing", "Large Healing Chest"},
			{"iscCategoryChest2Utility", "Large Utility Chest"},
			{"iscGoldChest", "Legendary Chest"},

			{"iscTripleShop", "Common Triple Shop"},
			{"iscTripleShopLarge", "Uncommon Triple Shop"},
			{"iscTripleShopEquipment", "Triple Equipment Shop"},
			{"iscCasinoChest", "Adaptive Chest"},

			{"iscBarrel1", "Barrel"},
			{"iscLunarChest", "Lunar Pod"},
			{"iscEquipmentBarrel", "Equipment Barrel"},
			{"iscScrapper", "Scrapper"},
			{"iscRadarTower", "Radio Scanner"},

			{"iscDuplicator", "Common 3D Printer"},
			{"iscDuplicatorLarge", "Uncommon 3D Printer"},
			{"iscDuplicatorWild", "Overgrown 3D Printer"},
			{"iscDuplicatorMilitary", "Mili-Tech Printer"},

			{"iscBrokenTurret1", "Gunner Turret"},
			{"iscBrokenDrone1", "Gunner Drone"},
			{"iscBrokenDrone2", "Healing Drone"},
			{"iscBrokenEmergencyDrone", "Emergency Drone"},
			{"iscBrokenMissileDrone", "Missile Drone"},
			{"iscBrokenEquipmentDrone", "Equipment Drone"},
			{"iscBrokenFlameDrone", "Incinerator Drone"},
			{"iscBrokenMegaDrone", "TC-280 Prototype"},

			{"iscShrineChance", "Shrine of Chance"},
			{"iscShrineCombat", "Shrine of Combat"},
			{"iscShrineBlood", "Shrine of Blood"},
			{"iscShrineBoss", "Shrine of the Mountain"},
			{"iscShrineHealing", "Shrine of the Woods"},
			{"iscShrineRestack", "Shrine of Order"},
			{"iscShrineGoldshoresAccess", "Altar of Gold"},
			{"iscShrineCleanse", "Cleansing Pool"},

			{"iscVoidCamp", "Void Seed"},
			{"iscVoidChest", "Void Cradle"},
			{"iscVoidTriple", "Void Potential"},
			{"iscVoidCoinBarrel", "Void Stalk"},
		};

		public static Dictionary<string, ConfigEntry<float>> InteractibleToBind = new Dictionary<string, ConfigEntry<float>>
		{
			["iscChest1"] = IscChest1,
			["iscChest2"] = IscChest2,
			["iscChest1Stealthed"] = IscChest1Stealthed,

			["iscCategoryChestDamage"] = IscCategoryChestDamage,
			["iscCategoryChestHealing"] = IscCategoryChestHealing,
			["iscCategoryChestUtility"] = IscCategoryChestUtility,
			["iscCategoryChest2Damage"] = IscCategoryChest2Damage,
			["iscCategoryChest2Healing"] = IscCategoryChest2Healing,
			["iscCategoryChest2Utility"] = IscCategoryChest2Utility,
			["iscGoldChest"] = IscGoldChest,

			["iscTripleShop"] = IscTripleShop,
			["iscTripleShopLarge"] = IscTripleShopLarge,
			["iscTripleShopEquipment"] = IscTripleShopEquipment,
			["iscCasinoChest"] = IscCasinoChest,

			["iscBarrel1"] = IscBarrel1,
			["iscLunarChest"] = IscLunarChest,
			["iscEquipmentBarrel"] = IscEquipmentBarrel,
			["iscScrapper"] = IscScrapper,
			["iscRadarTower"] = IscRadarTower,

			["iscDuplicator"] = IscDuplicator,
			["iscDuplicatorLarge"] = IscDuplicatorLarge,
			["iscDuplicatorWild"] = IscDuplicatorWild,
			["iscDuplicatorMilitary"] = IscDuplicatorMilitary,

			["iscBrokenTurret1"] = IscBrokenTurret1,
			["iscBrokenDrone1"] = IscBrokenDrone1,
			["iscBrokenDrone2"] = IscBrokenDrone2,
			["iscBrokenEmergencyDrone"] = IscBrokenEmergencyDrone,
			["iscBrokenMissileDrone"] = IscBrokenMissileDrone,
			["iscBrokenEquipmentDrone"] = IscBrokenEquipmentDrone,
			["iscBrokenFlameDrone"] = IscBrokenFlameDrone,
			["iscBrokenMegaDrone"] = IscBrokenMegaDrone,

			["iscShrineChance"] = IscShrineChance,
			["iscShrineCombat"] = IscShrineCombat,
			["iscShrineBlood"] = IscShrineBlood,
			["iscShrineBoss"] = IscShrineBoss,
			["iscShrineHealing"] = IscShrineHealing,
			["iscShrineRestack"] = IscShrineRestack,
			["iscShrineGoldshoresAccess"] = IscShrineGoldshoresAccess,
			["iscShrineCleanse"] = IscShrineCleanse,

			["iscVoidCamp"] = IscVoidCamp,
			["iscVoidChest"] = IscVoidChest,
			["iscVoidTriple"] = IscVoidTriple,
			["iscVoidCoinBarrel"] = IscVoidCoinBarrel
		};

		public static Dictionary<string, string> InteractibleToGroup = new Dictionary<string, string>
		{
			{"iscChest1", "Chests"},
			{"iscChest2", "Chests"},
			{"iscChest1Stealthed", "Chests"},

			{"iscCategoryChestDamage", "Chests"},
			{"iscCategoryChestHealing", "Chests"},
			{"iscCategoryChestUtility", "Chests"},
			{"iscCategoryChest2Damage", "Chests"},
			{"iscCategoryChest2Healing", "Chests"},
			{"iscCategoryChest2Utility", "Chests"},
			{"iscGoldChest", "Chests"},

			{"iscTripleShop", "Multishop"},
			{"iscTripleShopLarge", "Multishop"},
			{"iscTripleShopEquipment", "Multishop"},
			{"iscCasinoChest", "Multishop"},

			{"iscBarrel1", "Misc"},
			{"iscLunarChest", "Misc"},
			{"iscEquipmentBarrel", "Misc"},
			{"iscScrapper", "Misc"},
			{"iscRadarTower", "Misc"},

			{"iscDuplicator", "Printers"},
			{"iscDuplicatorLarge", "Printers"},
			{"iscDuplicatorWild", "Printers"},
			{"iscDuplicatorMilitary", "Printers"},

			{"iscBrokenTurret1", "Drones"},
			{"iscBrokenDrone1", "Drones"},
			{"iscBrokenDrone2", "Drones"},
			{"iscBrokenEmergencyDrone", "Drones"},
			{"iscBrokenMissileDrone", "Drones"},
			{"iscBrokenEquipmentDrone", "Drones"},
			{"iscBrokenFlameDrone", "Drones"},
			{"iscBrokenMegaDrone", "Drones"},

			{"iscShrineChance", "Shrines"},
			{"iscShrineCombat", "Shrines"},
			{"iscShrineBlood", "Shrines"},
			{"iscShrineBoss", "Shrines"},
			{"iscShrineHealing", "Shrines"},
			{"iscShrineRestack", "Shrines"},
			{"iscShrineGoldshoresAccess", "Shrines"},
			{"iscShrineCleanse", "Shrines"},

			{"iscVoidCamp", "Void"},
			{"iscVoidChest", "Void"},
			{"iscVoidTriple", "Void"},
			{"iscVoidCoinBarrel", "Void"}
		};

		public void Awake()
		{
			Log.Init(Logger);
			ConfigSetup();
			Hooks();
		}

		private void ConfigSetup()
		{
			InteractibleCountMultiplier = Config.Bind(section: "!General", key: "Count multiplier", defaultValue: 1.0f, configDescription: new ConfigDescription("Multiply the TOTAL number of spawnable interactibles. (Capped at 100)."));

			foreach (string interactible in Interactibles)
				InteractibleToBind[interactible] = Config.Bind(section: InteractibleToGroup[interactible], key: InteractibleToLocalized[interactible], defaultValue: 1.0f, configDescription: new ConfigDescription($"Multiply the weighted chance to spawn a/an {InteractibleToLocalized[interactible]}."));
		}

		private void Hooks()
		{
			On.RoR2.SceneDirector.GenerateInteractableCardSelection += SceneDirector_GenerateInteractableCardSelection; //interactibles change
																														// On.RoR2.SceneDirector.PopulateScene += SceneDirector_PopulateScene;
		}

		private WeightedSelection<RoR2.DirectorCard> SceneDirector_GenerateInteractableCardSelection(
			On.RoR2.SceneDirector.orig_GenerateInteractableCardSelection orig,
			RoR2.SceneDirector self)
		{
			self.interactableCredit = (int)(self.interactableCredit * Mathf.Clamp(InteractibleCountMultiplier.Value, 0, 100));

			var list = orig(self);
			for (int i = 0; i < list.Count; i++)
			{
				if (list?.choices[i] != null)
				{
					var cardName = list.choices[i].value.spawnCard.name;

					if (InteractibleToBind.TryGetValue(cardName.Replace("Sandy", "").Replace("Snowy", ""), out var config))
					{
						if (config.Value < 0)
							config.Value = 0;
						list.choices[i].weight *= config.Value;
					}
				}
			}
			return list;
		}
	}
}

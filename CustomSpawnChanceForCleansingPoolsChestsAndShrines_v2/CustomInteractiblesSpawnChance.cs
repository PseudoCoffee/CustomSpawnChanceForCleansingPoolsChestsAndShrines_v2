using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using System.Collections.Generic;
using UnityEngine;

// how to fix other mods
// https://risk-of-thunder.github.io/R2Wiki/Mod-Creation/C%23-Programming/Assembly-References/

namespace CustomSpawnChanceForCleansingPoolsChestsAndShrines_v2
{

	[BepInDependency("com.rune580.riskofoptions")]

	// This attribute is required, and lists metadata for your plugin.
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	public class CustomSpawnChanceForCleansingPoolsChestsAndShrines_v2 : BaseUnityPlugin
	{
		//define mod ID: uses the format of "com.USERNAME.MODNAME"
		public const string PluginGUID = "com.pseudo.CustomInteractiblesSpawnChance";

		//define the mod name inside quotes. Can be anything.
		public const string PluginName = "Custom spawn shrine";

		//define mod version inside quotes. Follows format of "MAJORVERSION.MINORPATCH.BUGFIX". Ex: 1.2.3 is Major Release 1, Patch 2, Bug Fix 3.
		public const string PluginVersion = "1.1.1";


		public static ConfigEntry<float> InteractibleCountMultiplier { get; set; }
		public static ConfigEntry<bool> ResetConfig { get; set; }

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
			SliderConfig globalSliderConfig = new() { min = 10, max = 10000 };
			SliderConfig sliderConfig = new() { min = 0, max = 10000 };

			InteractibleCountMultiplier = Config.Bind(section: "!General", key: "Count multiplier", defaultValue: 100.0f, configDescription: new ConfigDescription("Multiply the TOTAL number of spawnable interactibles. (Capped at 10000% or 100x)."));
			ModSettingsManager.AddOption(new SliderOption(InteractibleCountMultiplier, globalSliderConfig));

			ResetConfig = Config.Bind(section: "!General", key: "Reset config on next launch.", defaultValue: true, configDescription: new ConfigDescription("Config will change from using values from 0-100 to 0%-10000%. This will reset all values to 100% on next launch."));

			if (ResetConfig.Value)
				InteractibleCountMultiplier.Value = 100;

			foreach (string interactible in Interactibles)
			{
				ConfigEntry<float> config = Config.Bind(section: InteractibleToGroup[interactible], key: InteractibleToLocalized[interactible], defaultValue: 100.0f, configDescription: new ConfigDescription($"Multiply the weighted chance to spawn a/an {InteractibleToLocalized[interactible]}. Where 100% is unchanged, 200% is 2x(twice) and 50% is 0.5x(half)."));

				if (ResetConfig.Value)
					config.Value = 100;

				InteractibleToBind[interactible] = config;
				ModSettingsManager.AddOption(new SliderOption(config, sliderConfig));
			}

			ResetConfig.Value = false;
		}

		private void Hooks()
		{
			On.RoR2.SceneDirector.GenerateInteractableCardSelection += SceneDirector_GenerateInteractableCardSelection; //interactibles change
																														// On.RoR2.SceneDirector.PopulateScene += SceneDirector_PopulateScene;
		}

		private WeightedSelection<RoR2.DirectorCard> SceneDirector_GenerateInteractableCardSelection(
			On.RoR2.SceneDirector.orig_GenerateInteractableCardSelection methodReference,
			RoR2.SceneDirector thisReference)
		{
			thisReference.interactableCredit = (int)(thisReference.interactableCredit * Mathf.Clamp(InteractibleCountMultiplier.Value / 100, 0.1f, 100));

			var list = methodReference(thisReference);
			for (int i = 0; i < list.Count; i++)
			{
				if (list?.choices[i] != null)
				{
					var cardName = list.choices[i].value.spawnCard.name;

					if (InteractibleToBind.TryGetValue(cardName.Replace("Sandy", "").Replace("Snowy", ""), out var config))
					{
						if (config.Value < 0)
							config.Value = 0;

						list.choices[i].weight *= config.Value / 100;
					}
				}
			}
			return list;
		}
	}
}

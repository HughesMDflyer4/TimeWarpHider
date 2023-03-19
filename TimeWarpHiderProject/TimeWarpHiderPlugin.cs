using BepInEx;
using HarmonyLib;
using KSP.Game;
using KSP.UI.Flight;
using SpaceWarp;
using SpaceWarp.API.Mods;
using UnityEngine;

namespace TimeWarpHider;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class TimeWarpHiderPlugin : BaseSpaceWarpPlugin
{
    public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    public const string ModName = MyPluginInfo.PLUGIN_NAME;
    public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    public static TimeWarpHiderPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;
        Harmony.CreateAndPatchAll(typeof(TimeWarpHiderPlugin));
    }

    [HarmonyPatch(typeof(UIFlightHud), nameof(UIFlightHud.OnFlightHudCanvasActiveChanged))]
    [HarmonyPostfix]
    public static void OnFlightHudCanvasActiveChanged(bool isVisible)
    {
        GameManager.Instance.Game.UI.FlightHud.gameObject.SetActive(isVisible);
    }
}
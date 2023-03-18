using BepInEx;
using HarmonyLib;
using KSP.Game;
using KSP.Messages;
using RTG;
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

    private GameObject timeWarpGameObject;
    private GameObject gameViewGameObject;

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;
        Harmony.CreateAndPatchAll(typeof(TimeWarpHiderPlugin).Assembly);

        GameManager.Instance.Game.Messages.Subscribe<VesselCreatedMessage>(OnLoaded);
        GameManager.Instance.Game.Messages.Subscribe<VesselChangedMessage>(OnLoaded);
    }

    private void OnLoaded(MessageCenterMessage message)
    {
        GameManager.Instance.Game.UI.FlightHud.onFlightHudCanvasActiveChanged += new Action<bool>(OnFlightHudToggled);
        timeWarpGameObject = GameManager.Instance.Game.UI.GetRootCanvas().gameObject.GetAllChildren().Where(g => g.name == "group_instruments(Clone)").FirstOrDefault();
        gameViewGameObject = GameManager.Instance.Game.UI.GetRootCanvas().gameObject.GetAllChildren().Where(g => g.name == "group_gameview(Clone)").FirstOrDefault();
    }

    private void OnFlightHudToggled(bool isEnabled)
    {
        timeWarpGameObject.SetActive(isEnabled);
        gameViewGameObject.SetActive(isEnabled);
    }
}
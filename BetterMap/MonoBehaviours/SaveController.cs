#if BELOWZERO_MULTI
using System.Collections;
using System.IO;
using BetterMap.Components;
using BetterSubnautica.MonoBehaviours;
using Subnautica.API.Features;
using SubnauticaMap;

namespace BetterMap.MonoBehaviours;

public class SaveController : AbstractAwakeSingleton<SaveController>
{
    private object ProcessLock { get; } = new();

    public StopwatchItem Timing { get; } = new(10000f, autoStart: false);

    public Controller Controller { get; private set; }

    public string ServerId { get; private set; }

    public string SavePath { get; private set; }
    public string MapPath { get; private set; }

    public UserStorage RealUserStorage { get; private set; }
    public UserStorage FakeUserStorage { get; private set; }

    public bool IsStarted { get; private set; }
    public bool IsLoaded { get; private set; }

    protected override void Awake()
    {
        IsStarted = false;
        IsLoaded = false;

        Controller = gameObject.GetComponent<Controller>();
        ServerId = Network.Session?.Current?.ServerId;

        if (Controller == null || string.IsNullOrWhiteSpace(ServerId))
        {
            Destroy(this);
            return;
        }

        SavePath = Paths.GetMultiplayerClientSavePath();
        MapPath = Path.Combine(ServerId, "SubnauticaMap");

        RealUserStorage = new UserStoragePC(SavePath);
        RealUserStorage.CreateContainerAsync(MapPath);
        FakeUserStorage = new FakeStorage();

        base.Awake();
    }

    protected void Start()
    {
        StartCoroutine(StartAsync());
    }

    private IEnumerator StartAsync()
    {
        while (!Controller.isStarted)
        {
            yield return null;
        }

        PerformLoad(true);
    }

    protected void Update()
    {
        if (!IsLoaded) return;

        if (Timing.IsFinished())
        {
            PerformSave();
        }
    }

    private void PerformLoad(bool force)
    {
        if (!force && !IsStarted) return;

        lock (ProcessLock)
        {
            IsStarted = true;

            Map.All().Clear();
            Fogmap.Release();
            Fogmap.All().Clear();
            
            Controller.LoadMapIcons();
            Controller.LoadNotes();
            Controller.ReloadMaps();

            IsLoaded = true;
        }
    }

    public void PerformLoad()
    {
        PerformLoad(false);
    }

    public void PerformSave()
    {
        if (!IsLoaded) return;

        lock (ProcessLock)
        {
            Fogmap.SaveAll();
            Controller.SaveMapIcons();
            Controller.SaveNotes();

            Timing.Restart();
        }
    }
}
#endif

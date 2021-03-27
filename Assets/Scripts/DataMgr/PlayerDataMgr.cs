using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EPlayerPropType
{
    Connection,
    Asset,
    Strength,
}

[System.Serializable]
public class PlayerBasicInfo
{
    public int Connection { get; private set; }
    public int Asset { get; private set; }
    public int Strength { get; private set; }

    public event Action<EPlayerPropType, int> OnPropertyChg;

    public int this[EPlayerPropType type]
    {
        get
        {
            switch (type)
            {
                case EPlayerPropType.Connection:
                    return Connection;
                case EPlayerPropType.Asset:
                    return Asset;
                case EPlayerPropType.Strength:
                    return Strength;
                default:
                    return 0;
            }
        }
        set
        {
            switch (type)
            {
                case EPlayerPropType.Connection:
                    Connection = Mathf.CeilToInt(value);
                    break;
                case EPlayerPropType.Asset:
                    Asset = Mathf.CeilToInt(value);
                    break;
                case EPlayerPropType.Strength:
                    Strength = Mathf.CeilToInt(value);
                    break;
                default:
                    break;
            }
        }
    }

    public void DoFeedBack(TableItemFeedback data)
    {
        if (data.Asset != 0)
        {
            this[EPlayerPropType.Asset] += data.Asset;
            OnPropertyChg?.Invoke(EPlayerPropType.Asset, this.Asset);
        }
        if (data.Connection != 0)
        {
            this[EPlayerPropType.Connection] += data.Connection;
            OnPropertyChg?.Invoke(EPlayerPropType.Connection, this.Connection);
        }
        if (data.Strength != 0)
        {
            this[EPlayerPropType.Strength] += data.Strength;
            OnPropertyChg?.Invoke(EPlayerPropType.Strength, this.Strength);
        }
    }

    public static string GetInfoStr(TableItemFeedback data)
    {
        List<string> infoList = new List<string>();
        if (data.Asset != 0)
            infoList.Add(TableMgr.GetUIString("Asset") + " " + (data.Asset > 0 ? "+" + data.Asset.ToString() : data.Asset.ToString()));
        if (data.Connection != 0)
            infoList.Add(TableMgr.GetUIString("Connection") + " " + (data.Connection > 0 ? "+" + data.Connection.ToString() : data.Connection.ToString()));
        if (data.Strength != 0)
            infoList.Add(TableMgr.GetUIString("Strength") + " " + (data.Strength > 0 ? "+" + data.Strength.ToString() : data.Strength.ToString()));

        return string.Join("\n", infoList.ToArray());
    }
}

[System.Serializable]
public class ArchiveData
{
    public PlayerBasicInfo BasicInfo { get; private set; } = new PlayerBasicInfo();
    public EProgressStage CurProStage { get; set; } = EProgressStage.Preliminary;
}

[System.Serializable]
public class PlayerDataMgr : SingleTon<PlayerDataMgr>
{
    public event Action OnGameSaved;
    public event Action OnGameLoaded;
    public event Action OnGameCreated;

    public override void Init()
    {

    }

    public override void Dispose()
    {

    }

    public ArchiveData CurPlayerData { get; private set; }

    public void SaveGame()
    {
        ArchiveUtil.SaveArchive(CurPlayerData as object, ()=> { OnGameSaved?.Invoke(); });
    }

    public void LoadGame()
    {
        ArchiveUtil.LoadArchive<ArchiveData>((data) =>
        {
            CurPlayerData = data;
            OnGameLoaded?.Invoke();
        });
    }

    public void CreateNewArchive()
    {
        CurPlayerData = new ArchiveData();
        SaveGame();
        OnGameCreated?.Invoke();
    }
}

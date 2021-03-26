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
            this[EPlayerPropType.Asset] += data.Asset;
        if (data.Connection != 0)
            this[EPlayerPropType.Connection] += data.Connection;
        if (data.Strength != 0)
            this[EPlayerPropType.Strength] += data.Strength;
    }
}

[System.Serializable]
public class ArchiveData
{
    public PlayerBasicInfo BasicInfo { get; private set; } = new PlayerBasicInfo();
    public EProgressStage CurProStage { get; private set; } = EProgressStage.Preliminary;
    public int RestDayInCurStage { get; private set; } = 0;
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
        ArchiveUtil.LoadArchive((data) =>
        {
            CurPlayerData = data as ArchiveData;
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

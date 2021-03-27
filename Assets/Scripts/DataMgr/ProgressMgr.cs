using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProgressMgr : SingleTon<ProgressMgr>
{
    public TableItemPeriod CurPeriod { get; private set; }

    public event Action OnStageBegin;
    public event Action OnStageEnd;

    public override void Init()
    {
        CurPeriod = TableMgr.Instance.TablePeriod.GetFirstItem(item => item.Stage == PlayerDataMgr.Instance.CurPlayerData.CurProStage);
        OnStageBegin?.Invoke();
    }

    public override void Dispose()
    {

    }

    public void Go2NextStage()
    {
        OnStageEnd?.Invoke();

        int nextId = CurPeriod.ID + 1;
        CurPeriod = TableMgr.Instance.TablePeriod.GetItem(nextId);

        if (CurPeriod == null)
        {
            GoEnding();
            return;
        }

        EventMgr.Instance.AddEvents(CurPeriod.Events);

        PlayerDataMgr.Instance.CurPlayerData.CurProStage = CurPeriod.Stage;

        OnStageBegin?.Invoke();        
    }

    private void GoEnding()
    {
        int rank = 0;
        int asset = PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Asset;
        int connection = PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Connection;
        int strength = PlayerDataMgr.Instance.CurPlayerData.BasicInfo.Strength;

        if (asset < 15 || connection < 15 || strength < 15)
            rank = 0;
        else if (asset >= connection && asset >= strength)
            rank = 1;
        else if (connection >= asset && connection >= strength)
            rank = 2;
        else
            rank = 3;

        switch (rank)
        {
            case 0:
                DialogWnd.Instance.ShowDialog(TableMgr.Instance.TableEnding.GetItem(1).Dialog, () => ShowEndingInfo());
                break;
            case 1:
                DialogWnd.Instance.ShowDialog(TableMgr.Instance.TableEnding.GetItem(2).Dialog, () => ShowEndingInfo());
                break;
            case 2:
                DialogWnd.Instance.ShowDialog(TableMgr.Instance.TableEnding.GetItem(3).Dialog, () => ShowEndingInfo());
                break;
            case 3:
                DialogWnd.Instance.ShowDialog(TableMgr.Instance.TableEnding.GetItem(4).Dialog, () => ShowEndingInfo());
                break;
        }

    }

    private void ShowEndingInfo()
    {
        ConfirmWnd.Instance.ShowWnd("游戏结束，感谢试玩！", "退出游戏", () =>
        {
            GameRoot.Instance.Back2Menu();
        });
    }
}

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

        OnStageBegin?.Invoke();
    }

    private void GoEnding()
    {
        int rank = 0;
        switch (rank)
        {
            case 0:
                DialogWnd.Instance.ShowDialog(1007);
                break;
            case 1:
                DialogWnd.Instance.ShowDialog(1015);
                break;
            case 2:
                DialogWnd.Instance.ShowDialog(1020);
                break;
            case 3:
                DialogWnd.Instance.ShowDialog(1024);
                break;
        }

        ConfirmWnd.Instance.ShowWnd("游戏结束，感谢试玩！", "退出游戏", () =>
        {
            Application.Quit();
        });
    }
}

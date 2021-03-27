using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventMgr : SingleTon<EventMgr>
{
    public event Action OnEventRefreshed;

    public Queue<int> events = new Queue<int>();

    public override void Init()
    {
        AddEvents(ProgressMgr.Instance.CurPeriod.Events);
    }

    public override void Dispose()
    {

    }

    public void AddEvents(List<int> eventID)
    {
        events.Clear();
        foreach(var id in eventID)
        {
            events.Enqueue(id);
        }
    }

    public bool HasEvent()
    {
        return events.Count != 0;
    }

    public void TriggerEvent()
    {
        int eventId = events.Dequeue();
        var data = TableMgr.Instance.TableEvent.GetItem(eventId);
        switch (data.Type)
        {
            case EEventType.Painting:
                PaintingMatchWnd.Instance.ShowMatch(data.Param);
                break;
            case EEventType.Dialog:
                DialogWnd.Instance.ShowDialog(data.Param);
                break;
            case EEventType.Quiz:
                OptionWnd.Instance.ShowWnd(data.Param);
                break;
        }

        OnEventRefreshed?.Invoke();
    }
}

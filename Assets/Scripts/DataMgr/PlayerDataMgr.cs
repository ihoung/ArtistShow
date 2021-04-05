using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicInfo
{
    public int Connection { get; private set; }
    public int Asset { get; private set; }
    public int Strength { get; private set; }
}

public class PlayerDataMgr : SingleTon<PlayerDataMgr>
{
    public override void Init()
    {

    }

    public override void Dispose()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingMatchWndMono : BasicWndMono
{
    public RectTransform paitingArea;
    public RectTransform comparingArea;

    public RectTransform originalPainting;

    public CommonContainerMono colorOptions;

    public CommonContainer<PaintingColorUnit, PaintingColorUnitMono> colorOptionsContainer;

    public GameObject goFinalScore;
    public Text txtScore;

    public Button btnCommit;
    public Button btnClose;

    protected override void OnInit()
    {
        colorOptionsContainer = new CommonContainer<PaintingColorUnit, PaintingColorUnitMono>(colorOptions);
    }
}

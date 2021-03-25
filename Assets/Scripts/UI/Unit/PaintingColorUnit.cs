using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PaintingColorUnit : BasicUnit<PaintingColorUnitMono>
{
    protected override string ResName => "PaintingColorUnit";

    public Color Color { get; private set; }
    private Action<PaintingColorUnit> m_onClicked;

    protected override void OnInit()
    {
        Mono.btnClicked.onClick.AddListener(OnClicked);
    }

    public void SetData(Color color, Action<PaintingColorUnit> onSelected = null)
    {
        Color = color;
        m_onClicked = onSelected;

        Mono.imgColor.color = color;
        Mono.selectedLine.enabled = false;
    }

    private void OnClicked()
    {
        m_onClicked?.Invoke(this);
    }

    public void SelectUnit(bool isSelected)
    {
        Mono.selectedLine.enabled = isSelected;
    }
}

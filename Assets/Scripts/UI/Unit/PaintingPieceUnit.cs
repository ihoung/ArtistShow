using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PaintingPieceUnit : BasicUnit<PaintingPieceUnitMono>
{
    protected override string ResName => "PaintingPieceUnit";

    public int PieceIndex { get; private set; }

    public Action<PaintingPieceUnit> OnPieceClicked;

    protected override void OnBind()
    {
        Mono.btnPiece.onClick.AddListener(OnClicked);
    }

    public void SetData(int index)
    {
        PieceIndex = index;
    }

    public void SetColor(Color color)
    {
        Mono.imgPiece.color = color;
    }

    public void ClearColor(Color color)
    {
        Mono.imgPiece.color = new Color(1f, 1f, 1f);
    }

    private void OnClicked()
    {
        Debug.LogError(PieceIndex);
        OnPieceClicked?.Invoke(this);
    }
}

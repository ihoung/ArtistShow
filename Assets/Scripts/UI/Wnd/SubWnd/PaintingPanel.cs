using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPanel : SubWnd<PaintingPanelMono>
{
    public PaintingPanel(string resName, Transform parent) : base(resName, parent) { }

    public Color SelectedColor { get; set; }

    private List<PaintingPieceUnit> pieceUnits = new List<PaintingPieceUnit>();

    protected override void OnLoad()
    {
        for(int i = 0; i < Mono.paintingPieces.Count; ++i)
        {
            PaintingPieceUnit piece = new PaintingPieceUnit();
            piece.Bind(Mono.paintingPieces[i]);
            piece.OnPieceClicked += PaintingMatchWnd.Instance.OnPieceClicked;
            piece.SetData(i);
            pieceUnits.Add(piece);
        }
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnDestroy()
    {
        foreach(var piece in pieceUnits)
        {
            piece.OnPieceClicked -= PaintingMatchWnd.Instance.OnPieceClicked;
        }
    }

    protected override void OnHide()
    {

    }

    public void SetStandardActive(bool isActive)
    {
        Mono.goOriginal.SetActive(isActive);
        Mono.goPainting.SetActive(!isActive);
    }

    public List<Color> GetStandardColorList()
    {
        return Mono.standardColors;
    }

    public void SetAllPieceBtnEnable(bool enable)
    {
        foreach(var piece in pieceUnits)
        {
            piece.SetBtnEnable(enable);
        }
    }
}

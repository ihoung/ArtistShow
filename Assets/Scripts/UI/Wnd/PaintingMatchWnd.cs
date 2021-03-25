using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class PaintingMatchWnd : BaseSingleTonWnd<PaintingMatchWnd, PaintingMatchWndMono>
{
    protected override string ResName => "PaintingMatchWnd";

    protected override EUILayer Layer => EUILayer.Top;

    private readonly string paintingNamePrefix = "PaintingPanel_";

    private int m_curMatchID;

    private PaintingPanel m_curPainting;
    private PaintingPanel m_standardPainting;

    public List<Color> m_standardColors = new List<Color>();
    public List<Color> m_curPaintColors = new List<Color>();

    protected override void OnInit()
    {
        m_curPainting = new PaintingPanel(paintingNamePrefix + m_curMatchID.ToString(), Mono.paitingArea.transform);
        m_curPainting.SetStandardActive(false);
        m_standardColors = m_curPainting.GetStandardColorList();

        for (int i = 0; i < m_standardColors.Count; ++i)
        {
            m_curPaintColors.Add(new Color(1f, 1f, 1f, 1f));
        }

        Mono.btnCommit.onClick.AddListener(CommitPainting);
        Mono.btnClose.onClick.AddListener(Hide);
    }

    protected override void OnShow()
    {
        Mono.btnClose.gameObject.SetActive(false);

        for (int i = 0; i < m_standardColors.Count; ++i)
        {
            var colorUnit = Mono.colorOptionsContainer.ShowUnit();
            colorUnit.SetData(m_standardColors[i], OnColorSelected);

            if (i == 0)
            {
                colorUnit.SelectUnit(true);
                m_curSelectedColorUnit = colorUnit;
            }
        }

        Mono.goFinalScore.SetActive(false);
    }

    protected override void OnHide()
    {
        m_curPainting.Destroy();
        m_standardPainting.Destroy();
    }

    protected override void OnDestroy()
    {
        m_curPainting.Destroy();
        m_standardPainting.Destroy();
    }

    public void ShowMatch(int paintingID)
    {
        m_curMatchID = paintingID;
        Show();
    }

    private PaintingColorUnit m_curSelectedColorUnit;

    private void OnColorSelected(PaintingColorUnit colorUnit)
    {
        if (m_curSelectedColorUnit == colorUnit)
            return;

        m_curSelectedColorUnit?.SelectUnit(false);
        m_curSelectedColorUnit = colorUnit;
        colorUnit?.SelectUnit(true);
        m_curPainting.SelectedColor = colorUnit.Color;
    }

    public void OnPieceClicked(PaintingPieceUnit pieceUnit)
    {
        pieceUnit.SetColor(m_curPainting.SelectedColor);
        m_curPaintColors[pieceUnit.PieceIndex] = m_curPainting.SelectedColor;
    }

    private void CommitPainting()
    {
        m_curPainting.SetAllPieceBtnEnable(false);
        Mono.paitingArea.DOAnchorPos(Mono.comparingArea.anchoredPosition, 1f);

        m_standardPainting = new PaintingPanel(paintingNamePrefix + m_curMatchID.ToString(), Mono.originalPainting.transform);
        m_standardPainting.SetStandardActive(true);

        float score = CaculateScore();
        Mono.goFinalScore.SetActive(true);
        Mono.txtScore.SetText(score.ToString("f2") + "/10.0");

        Mono.btnCommit.gameObject.SetActive(false);
        Mono.btnClose.gameObject.SetActive(true);
    }

    private float CaculateScore()
    {
        float sum = 0f;
        int count = 0;
        for (int i = 0; i < m_standardColors.Count; ++i)
        {
            Color standardColor = m_standardColors[i];
            Color selectedColor = m_curPaintColors[i];
            float res = (Mathf.Abs(standardColor.r - selectedColor.r) + Mathf.Abs(standardColor.g - selectedColor.g) + Mathf.Abs(standardColor.b - selectedColor.b)) / 3f;
            sum += res;
            count++;
        }

        return (1 - sum / count) * 10f;
    }
}

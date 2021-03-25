using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingPanelMono : SubWndMono
{
    public GameObject goOriginal;
    public GameObject goPainting;

    public List<Color> standardColors;
    public List<PaintingPieceUnitMono> paintingPieces;
}

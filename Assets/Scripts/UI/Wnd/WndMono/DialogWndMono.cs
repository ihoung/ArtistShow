﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWndMono : BasicWndMono
{
    public Image imgBg;

    public Text txtName;
    public Text txtDialog;
    public Image imgRoleLeft;
    public Image imgRoleMiddle;
    public Image imgRoleRight;

    public Button btnNext;

    public CommonContainerMono optionContainerMono;
    public CommonContainer<OptionUnit, OptionUnitMono> optionContainer;

    protected override void OnInit()
    {
        optionContainer = new CommonContainer<OptionUnit, OptionUnitMono>(optionContainerMono);
    }
}

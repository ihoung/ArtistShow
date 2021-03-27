using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWndMono : BasicWndMono
{
    public Text txtQuestion;
    public CommonContainerMono optionsContainerMono;

    public CommonContainer<OptionUnit, OptionUnitMono> optionContainer;

    protected override void OnInit()
    {
        optionContainer = new CommonContainer<OptionUnit, OptionUnitMono>(optionsContainerMono);
    }
}

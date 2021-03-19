using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIUtil
{
    public static void SetText(this Text text, string str)
    {
        if (text == null)
        {
            Debug.LogError($"Text component {text.name} is null!");
            return;
        }

        text.text = str;
    }

    public static void SetSprite(this Image img, string spriteName)
    {
        if (img == null)
        {
            Debug.LogError($"Image component {img.name} is null!");
            return;
        }

        Sprite sp = ResUtil.LoadSprite(spriteName);
        if (sp != null)
            img.sprite = sp;
    }
}

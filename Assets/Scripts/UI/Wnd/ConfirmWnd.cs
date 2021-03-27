using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ConfirmWnd : BaseSingleTonWnd<ConfirmWnd, ConfirmWndMono>
{
    protected override string ResName => "ConfirmWnd";

    protected override EUILayer Layer => EUILayer.OverAll;

    private string m_content;
    private string m_txtConfirm;
    private string m_txtCancel;

    private Action m_onConfirm;
    private Action m_onCancel;

    protected override void OnInit()
    {
        Mono.btnConfirm.onClick.AddListener(OnConfirm);
        Mono.btnCancel.onClick.AddListener(OnCancel);
    }

    protected override void OnShow()
    {
        Mono.txtContent.SetText(m_content);
        Mono.txtConfirm.SetText(m_txtConfirm);
        Mono.txtCancel.SetText(m_txtCancel);

        if (m_onCancel == null || string.IsNullOrEmpty(m_txtCancel))
            Mono.btnCancel.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {

    }

    public void ShowWnd(string content, string txtConfirm, Action onConfirm = null, string txtCancel = "", Action onCancel = null)
    {
        m_onConfirm = onConfirm;
        m_onCancel = onCancel;

        m_content = content;
        m_txtConfirm = txtConfirm;
        m_txtCancel = txtCancel;

        Show();
    }

    private void OnConfirm()
    {
        m_onConfirm?.Invoke();
        Hide();
    }

    private void OnCancel()
    {
        m_onCancel?.Invoke();
        Hide();
    }
}

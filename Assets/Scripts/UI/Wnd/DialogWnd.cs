using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogWnd : BaseSingleTonWnd<DialogWnd, DialogWndMono>
{
    protected override string ResName => "DialogWnd";

    protected override EUILayer Layer => EUILayer.Top;

    protected override void OnInit()
    {
        Mono.btnNext.onClick.AddListener(OnClickNext);

    }

    private TableItemDialog m_curDialog;
    private Action m_onCompleted;

    protected override void OnShow()
    {
        Mono.optionContainerMono.gameObject.SetActive(false);

        RefreshDialog();
    }

    protected override void OnHide()
    {
        m_curDialog = default;
    }

    protected override void OnDestroy()
    {
    }

    public void ShowDialog(int dialogID, Action OnCompleted = null)
    {
        m_onCompleted = OnCompleted;

        m_curDialog = TableMgr.Instance.TableDialog.GetItem(dialogID);

        if (m_curDialog == null)
            Debug.LogError($"Cannot find the target dialog! ID: {dialogID}");

        Show();
    }

    private void RefreshDialog()
    {
        Mono.btnNext.enabled = true;

        if (m_curDialog == null)
            return;

        if (string.IsNullOrEmpty(m_curDialog.BgImg))
        {
            Mono.imgBg.gameObject.SetActive(false);
        }
        else
        {
            Mono.imgBg.gameObject.SetActive(true);
            Mono.imgBg.sprite = ResUtil.LoadSprite(ESpriteType.Bg, m_curDialog.BgImg);
        }

        Mono.txtName.SetText(m_curDialog.Name);
        Mono.txtDialog.SetText(m_curDialog.DialogContent);

        Mono.imgRoleLeft.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Left || m_curDialog.RoleImgPos == ERoleImgPos.Both);
        Mono.imgRoleMiddle.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Middle);
        Mono.imgRoleRight.gameObject.SetActive(m_curDialog.RoleImgPos == ERoleImgPos.Right || m_curDialog.RoleImgPos == ERoleImgPos.Both);
        switch (m_curDialog.RoleImgPos)
        {
            case ERoleImgPos.Left:
                Mono.imgRoleLeft.SetSprite(ESpriteType.Character, m_curDialog.RoleImg[0]);
                break;
            case ERoleImgPos.Middle:
                Mono.imgRoleMiddle.SetSprite(ESpriteType.Character, m_curDialog.RoleImg[0]);
                break;
            case ERoleImgPos.Right:
                Mono.imgRoleRight.SetSprite(ESpriteType.Character, m_curDialog.RoleImg[0]);
                break;
            case ERoleImgPos.Both:
                Mono.imgRoleLeft.SetSprite(ESpriteType.Character, m_curDialog.RoleImg[0]);
                Mono.imgRoleRight.SetSprite(ESpriteType.Character, m_curDialog.RoleImg[1]);
                break;
            default:
                break;
        }

        if (m_curDialog.Options.Count == 0 || m_curDialog.Options == null)
        {
            Mono.optionContainerMono.gameObject.SetActive(false);
        }
        else
        {
            Mono.btnNext.enabled = false;
            Mono.optionContainerMono.gameObject.SetActive(true);

            Mono.optionContainer.ClearAllUnits();
            foreach(var optionID in m_curDialog.Options)
            {
                TableItemAnswer data = TableMgr.Instance.TableAnswer.GetItem(optionID);
                var unit = Mono.optionContainer.ShowUnit();
                unit.SetData(data, OnOptionSelected);
            }
        }
    }

    private void OnClickNext()
    {
        if (m_curDialog.NextID == 0)
        {
            Hide();
            m_onCompleted?.Invoke();
            m_onCompleted = null;
        }
        else
        {
            m_curDialog = TableMgr.Instance.TableDialog.GetItem(m_curDialog.NextID);
            RefreshDialog();
        }
    }

    private void OnOptionSelected(TableItemAnswer data)
    {
        if(data.Dialog != 0)
        {
            m_curDialog = TableMgr.Instance.TableDialog.GetItem(data.Dialog);
            RefreshDialog();
        }
        else
        {
            OnClickNext();
        }
    }
}

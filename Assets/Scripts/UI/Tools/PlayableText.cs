using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;

public enum ETextPlayMode
{
    Normal,
    ShowAll,
    Pass,
    SpeedUp,
    Pausable,
}

[RequireComponent(typeof(Text))]
public class PlayableText : MonoBehaviour
{
    private enum EStatus
    {
        None,
        Normal,
        Speeding,
        Pause,
        Over,
    }

    [Header("响应按键")]
    public List<Button> ButtonList = new List<Button>();
    [Header("播放模式")]
    public ETextPlayMode PlayMode = ETextPlayMode.ShowAll;
    [Header("默认播放时间")]
    public float Duration = 1f;
    [Header("加速播放倍速（1~10）")]
    public float TimeScale = 5f;

    private Text txtContent;
    private string m_content;
    private EStatus m_status;
    private Tweener m_curTweener;
    private Action m_onCompleted;

    private void Awake()
    {
        txtContent = gameObject.GetComponent<Text>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        Clear();
    }

    /// <summary>
    /// 清空并初始化Text的所有状态
    /// </summary>
    public void Clear()
    {
        txtContent.text = default;
        m_content = default;
        m_status = EStatus.None;
        m_curTweener = default;
        m_onCompleted = default;
    }

    /// <summary>
    /// 设置文本
    /// </summary>
    /// <param name="content">完整文字内容</param>
    /// <param name="autoPlay">是否设置完自动开始播放</param>
    /// <param name="onCompleted">播放完成回调</param>
    /// <returns></returns>
    public void SetTxt(string content, bool autoPlay = true, Action onCompleted = null)
    {
        m_content = content;
        if (m_curTweener != null && m_status != EStatus.None)
        {
            m_curTweener.Kill(true);
        }

        if (autoPlay && PlayMode != ETextPlayMode.Normal)
        {
            PlayTxt(onCompleted);
        }
        else
        {
            txtContent.text = content;
        }
    }

    /// <summary>
    /// 播放文字
    /// </summary>
    /// <param name="onCompleted">播放完成回调</param>
    /// <returns></returns>
    public void PlayTxt(Action onCompleted = null)
    {
        if (PlayMode == ETextPlayMode.Pausable && m_status == EStatus.Pause)
        {
            m_status = EStatus.Normal;
            m_curTweener.Play();
            return;
        }
        else if (m_status != EStatus.None)
            return;

        if (string.IsNullOrEmpty(m_content))
        {
            txtContent.text = "";
        }

        m_onCompleted = onCompleted;
        m_status = EStatus.Normal;
        txtContent.text = "";
        RegisterBtn();
        m_curTweener = txtContent.DOText(m_content, Duration).OnComplete(() =>
        {
            OnCompleted();
        });
    }

    private void OnCompleted()
    {
        DeregisterBtn();
        m_status = EStatus.None;
        m_onCompleted?.Invoke();
    }

    /// <summary>
    /// 暂停播放
    /// </summary>
    public void PauseTxt()
    {
        if (PlayMode != ETextPlayMode.Pausable)
            return;

        if (m_curTweener != null)
        {
            m_status = EStatus.Pause;
            m_curTweener.Pause();
        }
    }

    private void RegisterBtn()
    {
        for (int i = 0; i < ButtonList.Count; ++i)
        {
            ButtonList[i].onClick.AddListener(DoTextSpeedUp);
        }
    }

    private void DeregisterBtn()
    {
        for (int i = 0; i < ButtonList.Count; ++i)
        {
            ButtonList[i].onClick.RemoveListener(DoTextSpeedUp);
        }
    }

    /// <summary>
    /// 控件没有加Button时可以代码调用该方法加速
    /// </summary>
    public void SpeedUpText()
    {
        DoTextSpeedUp();
    }

    private void DoTextSpeedUp()
    {
        if (m_curTweener == null)
            return;

        switch (PlayMode)
        {
            default:
            case ETextPlayMode.Normal:
                break;
            case ETextPlayMode.ShowAll:
                if(m_status == EStatus.Normal)
                {
                    m_status = EStatus.Over;
                    m_curTweener.Kill();
                    txtContent.text = m_content;
                }
                else if (m_status == EStatus.Over)
                {
                    m_curTweener.Complete();
                    OnCompleted();
                }
                break;
            case ETextPlayMode.Pass:
                m_curTweener.Kill(true);
                break;
            case ETextPlayMode.SpeedUp:
                if (m_status == EStatus.Normal)
                {
                    m_status = EStatus.Speeding;
                    m_curTweener.timeScale = TimeScale;
                }
                break;
            case ETextPlayMode.Pausable:
                if (m_status == EStatus.Normal)
                    PauseTxt();
                else if (m_status == EStatus.Pause)
                    PlayTxt();
                break;
        }
    }
}
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EndLevelPopup : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Confetti;

    [SerializeField] private Image m_Background;
    [SerializeField] private float m_BackgroundAnimTime;

    [SerializeField] private GameObject m_Mascot;
    [SerializeField] private GameObject m_Buttons;

    [SerializeField] private GameObject m_SceneMascots;

    private void OnEnable()
    {
        EnablePopup();
    }

    private void EnablePopup()
    {
        UIDisabled();

        if(m_SceneMascots != null)
        {
            m_SceneMascots.SetActive(false);
        }

        Sequence endLevelPopupSequence = DOTween.Sequence();
        endLevelPopupSequence.Append(m_Background.DOFade(0.6f, m_BackgroundAnimTime))
            .AppendCallback(() =>
            {
                m_Confetti.Play();

                UIEnabled();
            });
    }

    private void UIDisabled()
    {
        m_Background.DOFade(0, 0);

        var mascotImages = m_Mascot.GetComponentsInChildren<SpriteRenderer>();

        foreach (var img in mascotImages)
        {
            var tmpColor = img.color;
            tmpColor.a = 0;
            img.color = tmpColor;
        }

        var uiImages = m_Buttons.GetComponentsInChildren<Image>();

        foreach (var img in uiImages)
        {
            var tmpColor = img.color;
            tmpColor.a = 0;
            img.color = tmpColor;
        }
    }

    private void UIEnabled ()
    {
        var mascotImages = m_Mascot.GetComponentsInChildren<SpriteRenderer>();

        foreach (var img in mascotImages)
        {
            img.DOFade(1, m_BackgroundAnimTime)
                    .OnComplete(() => m_Mascot.GetComponent<Animator>().SetTrigger("win"));
        }

        var uiImages = m_Buttons.GetComponentsInChildren<Image>();

        foreach (var img in uiImages)
        {
            img.DOFade(1, m_BackgroundAnimTime);
        }
    }
}
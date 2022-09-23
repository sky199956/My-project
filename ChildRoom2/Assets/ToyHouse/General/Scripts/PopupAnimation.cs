using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupAnimation : MonoBehaviour
{
    [SerializeField] private GameObject m_Popup;
    [SerializeField] private Image m_Background;
    [SerializeField] private float m_Duration = 0.2f;
    [SerializeField] private Ease m_Ease;
    private Vector3 m_DefaultScale = Vector3.one;
    private Color m_DefaultColor = Color.white;

    public void ClosePopup()
    {
        m_Background?.DOColor(Vector4.zero, m_Duration);

        if (m_Popup != null)
        {
            m_Popup.transform.DOScale(Vector3.zero, m_Duration).SetEase(m_Ease).OnComplete(() => gameObject.SetActive(false));            
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        if (m_Popup != null)
        {
            m_DefaultScale = m_Popup.transform.localScale;
        }
        if (m_Background != null)
        {
            m_DefaultColor = m_Background.color;
        }
        m_Popup.transform.localScale = Vector3.zero;
        m_Background.color *= new Vector4(1f, 1f, 1f, 0f); 
    }
    private void OnEnable()
    {
        m_Popup?.transform.DOScale(m_DefaultScale, m_Duration).SetEase(m_Ease);
        m_Background?.DOColor(m_DefaultColor, m_Duration);
    }
    private void OnDisable()
    {
        m_Popup.transform.localScale = Vector3.zero;
        m_Background.color *= new Vector4(1f, 1f, 1f, 0f);
    }
}

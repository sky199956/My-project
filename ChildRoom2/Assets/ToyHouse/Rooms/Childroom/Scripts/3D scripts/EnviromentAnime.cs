using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnviromentAnime : MonoBehaviour, IPointerClickHandler

{
    public UnityEvent AnimeEvent;

    [Header("****Common****")]
    [SerializeField] private float m_Duration = 1.0f;
    [SerializeField] private float m_ReturnDuration = 0.3f;
    [SerializeField] private int m_Vibrato = 10;


    [Header("****Punch****")]
    [Space(20)]
    [SerializeField] private Vector3 m_Punch = Vector3.one;
    [SerializeField] private float m_Elasity = 1;

    private Vector3 m_DefaultPosition;
    private Vector3 m_DefaultRotation;
    private Vector3 m_DefaultScale;



    public void PunchRotation()
    {
        Sequence punchRotation = DOTween.Sequence();

        
        punchRotation.AppendCallback(() => gameObject.GetComponent<BoxCollider2D>().enabled = false);
        //HapticEffect.MediumFeedback();
        punchRotation.Append(transform.DOPunchRotation(m_Punch, m_Duration, m_Vibrato, m_Elasity));
        punchRotation.OnComplete(() => ReturnToDefault()); 


    }


    private void Start()
    {
        m_DefaultPosition = transform.position;
        m_DefaultRotation = transform.rotation.eulerAngles;
        m_DefaultScale = transform.localScale;

    }

    

    private void ReturnToDefault()
    {
        Sequence returnToDefault = DOTween.Sequence();

        returnToDefault.Join(transform.DORotate(m_DefaultRotation, m_ReturnDuration));
        returnToDefault.AppendCallback(() => gameObject.GetComponent<BoxCollider2D>().enabled = true);

    }

    private void OnMouseDown()
    {
        transform.DOKill();

        gameObject.GetComponent<AudioSource>().Play();

        AnimeEvent?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}

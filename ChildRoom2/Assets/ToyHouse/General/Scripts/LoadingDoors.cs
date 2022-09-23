using UnityEngine;
using DG.Tweening;

public class LoadingDoors : MonoBehaviour
{
    public float DoorSpeed => m_DoorsSpeed;

    [SerializeField] private RectTransform m_LeftDoor;
    [SerializeField] private RectTransform m_RightDoor;
    [SerializeField] private float m_DoorsSpeed;

    public void OpenDoors()
    {
        var screenBorder = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta.x / 2;

        m_LeftDoor.transform.position = Vector3.zero;
        m_RightDoor.transform.position = Vector3.zero;

        Sequence openDoorsSequence = DOTween.Sequence();
        openDoorsSequence.Append(m_LeftDoor.DOAnchorPosX(-screenBorder, m_DoorsSpeed).SetEase(Ease.InQuad))
            .Join(m_RightDoor.DOAnchorPosX(screenBorder, m_DoorsSpeed).SetEase(Ease.InQuad));
    }

    public void CloseDoors()
    {
        Sequence closeDoorsSequence = DOTween.Sequence();
        closeDoorsSequence.Append(m_LeftDoor.DOAnchorPosX(0, m_DoorsSpeed).SetEase(Ease.OutQuad))
            .Join(m_RightDoor.DOAnchorPosX(0, m_DoorsSpeed).SetEase(Ease.OutQuad));
    }
}

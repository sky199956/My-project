using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChildRoom
{
    public class PostersMovement : MonoBehaviour
    {

        private float newSize;
        private Transform oldSize;
        private Sequence posterMove;

        private void Start()
        {


            oldSize = transform;
            newSize = 0.9f;
            PosterAnimation();
        }

        private void PosterAnimation()
        {
            posterMove = DOTween.Sequence();

            posterMove.AppendInterval(4f);
            posterMove.Append(transform.DOScale(transform.localScale * newSize, 0.5f).SetEase(Ease.OutQuad));
            posterMove.Append(transform.DOScale(oldSize.localScale, 0.5f).SetEase(Ease.OutQuad));
            posterMove.AppendInterval(4f);
            posterMove.Append(transform.DOJump(transform.position, 6f, 1, 0.5f));
            posterMove.AppendInterval(4f);
            posterMove.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 15f), 0.2f));
            posterMove.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 15f), 0.2f));
            posterMove.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), 0.2f));
            posterMove.SetLoops(-1, LoopType.Restart);
        }

        public void OffSequince()
        {
            posterMove.Kill();
        }

    }
}


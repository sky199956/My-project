using System;
using UnityEngine;
using DG.Tweening;

namespace ChildRoom
{
    public class InterctionObjects3D : MonoBehaviour
    {
        public event Action CubeOnPlace;
        public delegate void DespawnBox(object sender);
        public event DespawnBox DespawnNACubeEvent;

        public delegate void DespawnCube(object sender);
        public event DespawnCube DespawnCubeEvent;

        public event Action MouseDownAction; 
        public event Action MouseUpAction;

        private Vector3 oldPosition;
        private Quaternion oldRotation;
        private Transform hitCube;
        private Vector3 zeroRotation;

        private bool m_anchored;
        private bool m_wrongTarget;
        private bool m_isDrag;

        private Animator m_Mascot;
                                         

        private void Start()
        {
            Input.multiTouchEnabled = false;
            oldPosition = gameObject.transform.position;
            oldRotation = gameObject.transform.rotation;
            zeroRotation = Vector3.zero;
            
            m_Mascot = GameObject.Find("Bear").GetComponent<Animator>();

        }


        private void OnMouseDown()
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            MouseDownAction?.Invoke();

            if (Physics.Raycast(ray, out hit))
            {
                hitCube = hit.transform;

                hitCube.transform.position = GetFrontPosition(Camera.main.transform.position, hitCube.position, 0.2f);
                

                transform.DORotate(zeroRotation, 0.2f);
                transform.GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.05f);

                //HapticEffect.LightFeedback();
                SFXManager.PlaySound("ButtonClick");

            }

            if (m_anchored == false)
            {
                m_isDrag = true;
                
            }

        }


        private void OnMouseUp()
        {


            float duration = 0.5f;

            if (m_anchored == false)
            {
                MouseUpAction?.Invoke();

                if (m_wrongTarget == true)
                {
                    //AmplitudeEvents.FailTry();
                    m_anchored = true;

                    Sequence returnCube = DOTween.Sequence();

                    returnCube.AppendCallback(() => PlayFailAnimation());

                    returnCube.AppendCallback(() => gameObject.GetComponent<BoxCollider>().enabled = false);
                    returnCube.AppendCallback(() => SFXManager.PlaySound("negative"));
                    //returnCube.AppendCallback(() => HapticEffect.MediumMultiFeedback(2, 0.1f));

                    returnCube.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 30f), 0.2f));
                    returnCube.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 30f), 0.2f));
                    returnCube.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), 0.2f));

                    returnCube.Append(transform.DOLocalMove(oldPosition, duration)).SetEase(Ease.OutQuad);
                    returnCube.Join(transform.DORotateQuaternion(oldRotation, duration));
                    returnCube.Join(transform.GetComponentInChildren<SpriteRenderer>().DOFade(1, duration));

                    returnCube.AppendCallback(() => gameObject.GetComponent<BoxCollider>().enabled = true);
                    
                    returnCube.OnComplete(() => m_anchored = false);

                    //AmplitudeEvents.FailTry();
                }
                else
                {
                    Sequence returnOnPosition = DOTween.Sequence();

                    returnOnPosition.AppendCallback(() => gameObject.GetComponent<BoxCollider>().enabled = false);
                    returnOnPosition.Append(transform.DOLocalMove(oldPosition, 0.3f)).SetEase(Ease.OutQuad);
                    returnOnPosition.Join(transform.DORotateQuaternion(oldRotation, 0.3f));
                    returnOnPosition.Join(transform.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0.3f));

                    returnOnPosition.AppendCallback(() => gameObject.GetComponent<BoxCollider>().enabled = true);

                }
                m_isDrag = false;
            }

            m_wrongTarget = false;

        }
         

        private void OnTriggerEnter(Collider collider)
        {

            var takenCube = gameObject.GetComponent<CubeInitScript>();
            var collisionCube = collider.GetComponent<NACubeInitScript>();

            if (takenCube.ID_Cube == collisionCube.id_NA_Cube && m_isDrag == true) 
            {
                m_isDrag = false;
                m_anchored = true;

                MouseUpAction?.Invoke();
                ParticleSystem explosion = collisionCube.GetComponent<ParticleSystem>();
                
                Quaternion coliderRotaion = collider.transform.rotation;

                Sequence cubeOnPosition = DOTween.Sequence();

                cubeOnPosition.AppendCallback(() => PlayHappyAnimation());
                cubeOnPosition.AppendCallback(() => gameObject.GetComponent<BoxCollider>().enabled = false);
                //cubeOnPosition.AppendCallback(() => HapticEffect.MediumFeedback());
                cubeOnPosition.AppendCallback(() => Destroy(gameObject.GetComponent<MoveObjects3D>()));

                cubeOnPosition.Append(transform.DOJump(collider.transform.position, 24f, 1, 0.4f));
                cubeOnPosition.Join(transform.DOScale(new Vector3(transform.localScale.x - 100f, transform.localScale.y - 100f, transform.localScale.z - 100f), 0.4f)); ;
                cubeOnPosition.Join(transform.DORotateQuaternion(coliderRotaion, 0.4f));

                cubeOnPosition.AppendCallback(() => SFXManager.PlaySound("sfx-magic"));
                cubeOnPosition.AppendCallback(() => explosion.Emit(60));
                

                
                cubeOnPosition.AppendCallback(() => collisionCube.GetComponent<SpriteRenderer>().enabled = false);
                cubeOnPosition.AppendCallback(() => DespawnNACubeEvent?.Invoke(collider.gameObject));
                cubeOnPosition.AppendCallback(() => DespawnCubeEvent?.Invoke(gameObject));

                cubeOnPosition.AppendCallback(() => Destroy(collider));
                cubeOnPosition.AppendInterval(0.2f);
                cubeOnPosition.AppendCallback(() => CubeOnPlace?.Invoke());

            }
            else
            {
                m_wrongTarget = true;

            }
        }

        private void OnTriggerStay(Collider collider)
        {
            var takenCube = gameObject.GetComponent<CubeInitScript>();
            var collisionCube = collider.GetComponent<NACubeInitScript>();

            if (!(takenCube.ID_Cube == collisionCube.id_NA_Cube))
            {
                m_wrongTarget = true;
            }

        }

        private void OnTriggerExit(Collider collider)
        {
            m_wrongTarget = false;
            
        }

        private Vector3 GetFrontPosition(Vector3 cameraPosition, Vector3 objectPosition, float offsetPercent)
        {
            var position = cameraPosition * offsetPercent + objectPosition * (1 - offsetPercent);
            return position;
        }

        private void PlayFailAnimation()
        {
            var animatorStateInfo = m_Mascot.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName("Fail"))
            {
                m_Mascot.SetTrigger("fail");
            }
        }

        private void PlayHappyAnimation()
        {
            var animatorStateInfo = m_Mascot.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName("Happy"))
            {
                m_Mascot.SetTrigger("happy");
            }
        }


    }

}


using System.Collections;
using UnityEngine;
using DG.Tweening;
//using ToyHouse.Storage;

namespace ChildRoom
{
    public class HelperChildRoom : MonoBehaviour
    {
        public void HelperSetOn()
        {
            m_Sparks.SetActive(true);

            m_HelperCoroutine = StartCoroutine(HelperCoroutine());

            
        }

        public void HelperSetOff()
        {

            m_Sparks.SetActive(false);

            m_HelperSprite.DOFade(0, 0);

            m_HelperSequence.Kill();

            if (m_HelperCoroutine != null)
            {
                StopCoroutine(m_HelperCoroutine);
            }

            

            transform.localScale = m_StartHelperSize;
        }

        public void InitCubesArray(GameObject[] cubes)
        {
            if (cubes != null)
            {
                foreach (var cube in cubes)
                {
                    cube.GetComponent<InterctionObjects3D>().MouseDownAction -= Helper_MouseDownAction;
                    cube.GetComponent<InterctionObjects3D>().MouseUpAction -= Helper_MouseUpAction;
                }
            }

            m_CubesInGame = new GameObject[cubes.Length];

            m_CubesInGame = cubes;

            foreach (var cube in cubes)
            {
                if (!cube.TryGetComponent<InterctionObjects3D>(out var interctionObjects3D))
                {
                    throw new MissingComponentException();
                }
                cube.GetComponent<InterctionObjects3D>().MouseDownAction += Helper_MouseDownAction;
                cube.GetComponent<InterctionObjects3D>().MouseUpAction += Helper_MouseUpAction;
            }
        }

        public void InitNACubesArray(GameObject[] naCubes)
        {
            m_naCubeInGame = new GameObject[naCubes.Length];

            m_naCubeInGame = naCubes;

            
        }

        public void ChangeHelperTarget(int increment = 0)
        {
            m_CurrentTarget = m_CurrentTarget < m_naCubeInGame.Length - 1 ? increment + m_CurrentTarget : 0;
        }

/*        public void TutorialActivation(int levelInfo)
        {

            var isComplete = ProgressStorage.IsRoomComplete;
            if (levelInfo == 0 && !isComplete[ProgressStorage.Room.Bathroom])

            {
                m_Delay = 1f;
            }
            else
            {
                m_Delay = 6f;
            }

        }*/

        [SerializeField] private GameObject m_Sparks;

        [Header("Helper settings")]
        private float m_Delay=5f;
        [SerializeField] private float m_CursorDelay;
        [SerializeField] private float m_EnableDuration;
        [SerializeField] private float m_ClickSize;
        [SerializeField] private float m_ClickDuration;
        [SerializeField] private float m_MoveDuration;

        private GameObject[] m_CubesInGame;
        private GameObject[] m_naCubeInGame;

        private GameObject m_HelperStartCube;

        private SpriteRenderer m_HelperSprite;

        private Sequence m_HelperSequence;

        private Coroutine m_HelperCoroutine;

        private Vector3 m_StartHelperSize;

        private int m_CurrentTarget;

        private ParticleSystem m_SparksParticleSystem;

        

        private void Start()
        {
            

            m_StartHelperSize = transform.localScale;

            m_HelperSprite = GetComponent<SpriteRenderer>();

            if (!m_Sparks.TryGetComponent<ParticleSystem>(out var particleSystem))
            {
                throw new MissingComponentException();
            }
            m_SparksParticleSystem = particleSystem;
        }

        private void Helper_MouseDownAction()
        {
            HelperSetOff();
        }

        private void Helper_MouseUpAction()
        {
            HelperSetOn();
        }

        private IEnumerator HelperCoroutine()
        {
            yield return new WaitForSeconds(m_Delay);

            SetHelperStartCube();

            m_Sparks.transform.position = GetFrontPosition(Camera.main.transform.position, m_HelperStartCube.transform.position, 0.2f);
            m_SparksParticleSystem.Emit(2);

            yield return new WaitForSeconds(m_CursorDelay);

            transform.position = GetFrontPosition(Camera.main.transform.position, m_HelperStartCube.transform.position, 0.2f);

            m_HelperSequence = DOTween.Sequence();

            m_HelperSequence.Append(m_HelperSprite.DOFade(1, m_EnableDuration));
            m_HelperSequence.Append(transform.DOScale(transform.localScale * m_ClickSize, m_ClickDuration))
                .Append(transform.DOMove(GetFrontPosition(Camera.main.transform.position, m_naCubeInGame[m_CurrentTarget].transform.position, 0.2f), m_MoveDuration).SetEase(Ease.InOutQuint))
                .Append(transform.DOScale(m_StartHelperSize, m_ClickDuration))
                .Join(m_HelperSprite.DOFade(0,m_EnableDuration))
                .SetLoops(-1, LoopType.Restart);
        }

        private void SetHelperStartCube()
        {
            for (int i = 0; i < m_CubesInGame.Length; i++)
            {
                
                var cube = m_CubesInGame[i].GetComponent<CubeInitScript>();
                var naCube = m_naCubeInGame[m_CurrentTarget].GetComponent<NACubeInitScript>();

                if (cube.ID_Cube == naCube.id_NA_Cube) 
                {
                    m_HelperStartCube = m_CubesInGame[i];
                    break;
                }
            }
        }


        private Vector3 GetFrontPosition(Vector3 cameraPosition, Vector3 objectPosition, float offsetPercent)
        {
            var position = cameraPosition * offsetPercent + objectPosition * (1 - offsetPercent);
            return position;
        }
    }
}



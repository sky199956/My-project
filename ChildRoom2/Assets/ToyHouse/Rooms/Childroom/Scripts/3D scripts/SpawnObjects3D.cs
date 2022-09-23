using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChildRoom
{
    public class SpawnObjects3D : MonoBehaviour
    {

        [SerializeField] private List<CubeInfo> cubesInforamtion;
        [SerializeField] private List<NACubeInfo> NACubesInformation;

        [SerializeField] int cubesCount;
        [SerializeField] int cubesSize;
        [SerializeField] int NAСubesSize;
        [SerializeField] private GameObject mainCube;
        [SerializeField] private GameObject mainNASprite;

        [SerializeField] private float intervalCube;
        [SerializeField] private float cubeHeight;
        [SerializeField] private float depth;

        [SerializeField] private float intervalNACube;
        [SerializeField] private float cubeNAHeight;
        [SerializeField] private float depthNA;

        private List<GameObject> m_CubesInGame = new List<GameObject>();
        private List<GameObject> m_NACubes = new List<GameObject>();

        public static Dictionary<GameObject, NACubeInitScript> cubesNA;
        public static Dictionary<GameObject, CubeInitScript> cubes;

        private Queue<GameObject> currentNACubes;
        private Queue<GameObject> currentCubes;

        private GameObject currentNAcube;
        private GameObject currentCube;


        public HelperChildRoom m_helperChildRoom;
        //public HappyWindow windowManager;
        public event Action levelCleaner;

        private Transform parentTransform;

        private int counter;

        private void Awake()
        {
            parentTransform = gameObject.transform;
            counter = 0;

            currentCubes = new Queue<GameObject>();
            currentNACubes = new Queue<GameObject>();

            cubes = new Dictionary<GameObject, CubeInitScript>();
            cubesNA = new Dictionary<GameObject, NACubeInitScript>();

        }

        private void Start()
        {
            CreatingCubes();
            CreatingNACubes();

        }

        private void CreatingCubes()
        {

            float cubePositionX;
            float step = intervalCube + 100;
            cubePositionX = -((cubesCount - 1) * step / 2);

            for (int i = 0; i < cubesCount; i++)
            {

                GameObject newCube = Instantiate(mainCube); 
                float randomOffsetX = UnityEngine.Random.Range(-4, 4);
                float randomRotation = UnityEngine.Random.Range(-40, 40);

                newCube.transform.SetParent(parentTransform);
                newCube.transform.position = new Vector3((cubePositionX / 100) + randomOffsetX, cubeHeight, depth);
                cubePositionX = cubePositionX + step;
                newCube.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + randomRotation, transform.rotation.z);


                var script = newCube.AddComponent<CubeInitScript>();
                cubes.Add(newCube, script);
                currentCubes.Enqueue(newCube);

                

            }

            for (int i = 0; i < cubesCount; i++)
            {
                currentCube = currentCubes.Dequeue();
                var script = cubes[currentCube];

                script.InitCube(cubesInforamtion[i]);

                currentCube.transform.localScale = new Vector3(transform.localScale.x * cubesSize, transform.localScale.y * cubesSize, transform.localScale.z * cubesSize);

                currentCube.name = "Cube" + i;

                currentCube.AddComponent<BoxCollider>();
                currentCube.AddComponent<InterctionObjects3D>();
                currentCube.AddComponent<MoveObjects3D>();

                if (!currentCube.TryGetComponent<InterctionObjects3D>(out var interctionObjects3D))
                {
                    Debug.Log("Missing \"InterctionObjects3D\" component");
                }
                interctionObjects3D.CubeOnPlace += CubeOnPlaceCounter;
                interctionObjects3D.DespawnNACubeEvent += MatchController_DespawnNACubeEvent;
                interctionObjects3D.DespawnCubeEvent += DespawnCube;

                m_CubesInGame.Add(currentCube);
            }

            m_helperChildRoom.InitCubesArray(m_CubesInGame.ToArray());

        }

        private void CreatingNACubes()
        {
            float cubeNAPositionX;
            float step = intervalNACube + 100;
            cubeNAPositionX = -((cubesCount - 1) * step / 2);


            for (int i = 0; i < cubesCount; i++)
            {
                GameObject newNACube = Instantiate(mainNASprite);
                newNACube.transform.SetParent(parentTransform);
                var script = newNACube.AddComponent<NACubeInitScript>();
                newNACube.SetActive(false);
                cubesNA.Add(newNACube, script);
                currentNACubes.Enqueue(newNACube);

                

            }

            for (int i = 0; i < cubesCount; i++)
            {
                Vector3 NACubePosition = new Vector3(cubeNAPositionX / 100, cubeNAHeight, depthNA);
                cubeNAPositionX = cubeNAPositionX + step;

                currentNAcube = currentNACubes.Dequeue();
                var script = cubesNA[currentNAcube];
                script.InitNACube(NACubesInformation[i]);

                currentNAcube.name = ("NA Cube") + i;
                currentNAcube.transform.localScale = new Vector3(transform.localScale.x * NAСubesSize, transform.localScale.y * NAСubesSize, transform.localScale.z * NAСubesSize);
                currentNAcube.transform.position = NACubePosition;

                currentNAcube.SetActive(true);

                var NACubeRB = currentNAcube.AddComponent<Rigidbody>();
                NACubeRB.isKinematic = true;



                m_NACubes.Add(currentNAcube);

            }

            m_helperChildRoom.InitNACubesArray(m_NACubes.ToArray());
        }

        private void CubeOnPlaceCounter()
        {
            counter++;
            
            if (cubesCount == counter)
            {

                //ToyHouse.Storage.ProgressStorage.LevelComplete(ToyHouse.Storage.ProgressStorage.Room.Childrenroom);

                //windowManager.ShowLevelCompletePopup();
                //windowManager.gameObject.SetActive(true);
                levelCleaner?.Invoke();
            }

        }

        
        private void DespawnCube(object cube)
        {
            m_CubesInGame.Remove(cube as GameObject);

            m_helperChildRoom.InitCubesArray(m_CubesInGame.ToArray());
        }
        
        private void MatchController_DespawnNACubeEvent(object naCube)
        {
            m_NACubes.Remove(naCube as GameObject);

            m_helperChildRoom.InitNACubesArray(m_NACubes.ToArray());
            m_helperChildRoom.ChangeHelperTarget();
        }

    }
}



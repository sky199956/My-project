using System.Collections.Generic;
using UnityEngine;

namespace ChildRoom
{
    public class LevelController: MonoBehaviour
    {

        [SerializeField] private List<GameObject> levelPrefabs;
        //[SerializeField] private HappyWindow m_windowManager;
        [SerializeField] private HelperChildRoom m_helperChildRoom;
        
        private int progress;
        private GameObject level;

        private void Start()
        {
            
            LevelStart();
        }

/*        public override void InitLevel()
        {
           
            base.InitLevel();
            
            progress = ToyHouse.Storage.ProgressStorage.GetLevel(ToyHouse.Storage.ProgressStorage.Room.Childrenroom);


            
        }*/

        public void LevelStart()
        {

            //if(level == null)
            {
                if (progress == 0)
                {
                    progress++;
                }
                level = Instantiate(levelPrefabs[progress - 1]);
                level.transform.SetParent(gameObject.transform);
                //level.GetComponent<SpawnObjects3D>().windowManager = m_windowManager;
                level.GetComponent<SpawnObjects3D>().m_helperChildRoom = m_helperChildRoom;

                //m_helperChildRoom.TutorialActivation(progress-1);

                if (!level.TryGetComponent<SpawnObjects3D>(out var spawnObjects3D))
                {
                    Debug.Log("Missing \"spawnOblect3D\" component");
                }
                spawnObjects3D.levelCleaner += DeleteLevel; 
                
                m_helperChildRoom.HelperSetOn();
            }

        }

        public void DeleteLevel()
        {
            var offSequence = GetComponentInChildren<PostersMovement>();
            offSequence.OffSequince();
            m_helperChildRoom.HelperSetOff();
            Destroy(level);
            progress++;
            LevelStart();
        }

    } 

}

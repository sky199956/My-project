using UnityEngine;

namespace ChildRoom
{
    public class NACubeInitScript : MonoBehaviour
    {
        private NACubeInfo dataNACube;


        public void InitNACube(NACubeInfo _dataNACube)
        {
            dataNACube = _dataNACube;
            GetComponent<SpriteRenderer>().sprite = dataNACube.NACubeSprite; 
        }
        public int id_NA_Cube
        {
            get { return dataNACube.IdNACube; }
            protected set { }
        }


    }
}



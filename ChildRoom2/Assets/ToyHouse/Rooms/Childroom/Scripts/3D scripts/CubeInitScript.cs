using UnityEngine;

namespace ChildRoom
{
    public class CubeInitScript : MonoBehaviour
    {

        private CubeInfo dataCubes;


        public void InitCube(CubeInfo _dataCubes)
        {
            dataCubes = _dataCubes;
            GetComponent<Renderer>().material = dataCubes.CubeMaterial;

        }

        public int ID_Cube
        {
            get { return dataCubes.IdCube; }
            protected set { }
        }


    }
}



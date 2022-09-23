using UnityEngine;

namespace ChildRoom
{
    [CreateAssetMenu(fileName = "CubeInfo", menuName = "Cubes/newCube")]

    public class CubeInfo : ScriptableObject

    {
        [Tooltip("Unic cube id")]
        [SerializeField] private int idCube;
        public int IdCube
        {
            get { return idCube; }
            protected set { }
        }

        [Tooltip("Cube Texture")]
        [SerializeField] private Material cubeMaterial;
        public Material CubeMaterial
        {
            get { return cubeMaterial; }
            protected set { }
        }


    }
}



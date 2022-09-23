using UnityEngine;

namespace ChildRoom
{
    [CreateAssetMenu(fileName = "NotActiveCubeInfo", menuName = "Cubes/newNACube")]

    public class NACubeInfo : ScriptableObject
    {
        [Tooltip("Unic id not active cube")]
        [SerializeField] private int idNACube;
        public int IdNACube
        {
            get { return idNACube; }
            protected set { }
        }


        
        [Tooltip("Sprite not active cube")]
        [SerializeField] private Sprite naCubeSprite;
        public Sprite NACubeSprite
        {
            get { return naCubeSprite; }
            protected set { }
        }
        

    }
}



using UnityEngine;

namespace ToyHouse.General
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_Background;

        void Awake()
        {
            Application.targetFrameRate = 60;
            var ppu = m_Background.sprite.pixelsPerUnit;

            float defaultCameraSize = m_Background.bounds.size.y / 2;            

            float widthUnits = (float) Screen.width / ppu; 
            float heightUnits = (float) Screen.height / ppu;

            float ratio = widthUnits / heightUnits;
            float newCameraSize = defaultCameraSize / ratio;

            gameObject.GetComponent<Camera>().orthographicSize = newCameraSize;
        }
    }
}

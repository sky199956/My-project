using UnityEngine;
using UnityEngine.UI;

namespace ToyHouse.General.Tools
{
    public class ShowFPS : MonoBehaviour
    {
        [SerializeField] Text m_FPSField;
        private float m_DeltaTime;

        void Update()
        {
            m_DeltaTime += (Time.deltaTime - m_DeltaTime) * 0.1f;
            float fps = 1.0f / m_DeltaTime;
            m_FPSField.text = Mathf.Ceil(fps).ToString();
        }
    }
}
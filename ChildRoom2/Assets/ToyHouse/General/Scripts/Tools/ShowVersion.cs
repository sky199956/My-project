using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class ShowVersion : MonoBehaviour
{
    [SerializeField] private Text m_VersionField;
    [SerializeField] private int m_BuildNum;
    void Update()
    {
        var build = PlayerSettings.iOS.buildNumber;
        if (build == null || build == "")
        {
            build = m_BuildNum.ToString();
        }
        if (m_VersionField != null)
        {
            m_VersionField.text = $"{Application.version} ({build})";
        }
    }
}
#endif

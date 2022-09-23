using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MuteButton : MonoBehaviour
{
    public Sprite RelevantIcon 
    {
        get 
        {
            if (!IsAudioEnabled)
            {
                return m_DisableSprite;
            }
            else
            {
                return m_EnableSprite;
            }
        }
    }
    public bool IsAudioEnabled
    {
        get
        {
            switch (m_Mixer.name)
            {
                case "Music":
                    return m_IsMusicEnabled;
                case "Effects":
                    return m_IsEffectsEnabled;
                case "Master":
                    return m_IsMasterEnabled;
                default:
                    return m_IsMasterEnabled;
            }
        }

        private set 
        {
            switch (m_Mixer.name)
            {
                case "Music":
                    m_IsMusicEnabled = value;
                    break;
                case "Effects":
                    m_IsEffectsEnabled = value;
                    break;
                case "Master":
                    m_IsMasterEnabled = value;
                    //m_IsEffectsEnabled = value;
                    //m_IsMusicEnabled = value;
                    break;
                default:
                    m_IsMasterEnabled = value;
                    break;
            }
        }
    }
    public string VolumeProperty
    {
        get
        {
            switch (m_Mixer.name)
            {
                case "Music":
                    return "MusicVolume";
                case "Effects":
                    return "EffectsVolume";
                case "Master":
                    return "MasterVolume";
                default:
                    return "MasterVolume";
            }
        }
    }

    [SerializeField] private Sprite m_EnableSprite;
    [SerializeField] private Sprite m_DisableSprite;
    [SerializeField] private AudioMixerGroup m_Mixer;

    static private bool m_IsMasterEnabled = true;
    static private bool m_IsEffectsEnabled = true;
    static private bool m_IsMusicEnabled = true;

    private Image m_Image;

    public void SwitchAudio()
    {
        IsAudioEnabled = !IsAudioEnabled;
        SetRelevantState();
    }

    private void Start()
    {
        m_Image = GetComponent<Image>();
        SetRelevantState();
    }
    /*
    private void OnEnable()
    {
        SetRelevantState();
    }*/

    private void SetRelevantState()
    {
        m_Image.sprite = RelevantIcon;

        float volumeValue = IsAudioEnabled ? 0 : -80;
        m_Mixer.audioMixer.SetFloat(VolumeProperty, volumeValue);
    }
}
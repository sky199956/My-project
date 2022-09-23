using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CheckInternetCoonection : MonoBehaviour
{
    [SerializeField] private GameObject m_InternetConnectionPopup;
    [SerializeField] private GameObject m_AgeVerefication;
    [SerializeField] private GameObject m_Subscription;
    private const string m_UrlToWebRequest = "https://game-validator.dkotrack.com/api/receipt";

    public void CheckInternetConnection()
    {
        StartCoroutine(CheckInternet());
    }

    public IEnumerator CheckInternet()
    {
        UnityWebRequest request = new UnityWebRequest(m_UrlToWebRequest);

        request.SendWebRequest();

        yield return new WaitUntil(() => request.isDone);

        var res = request.result;
        if (res == UnityWebRequest.Result.ProtocolError || res == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log("Internet is ok");
            m_AgeVerefication.SetActive(true);
            yield break;
        }
        else
        {
            Debug.Log("Internet is NO ok");
            m_InternetConnectionPopup.SetActive(true);
            m_Subscription.SetActive(false);
            yield break;
        }
    }
}

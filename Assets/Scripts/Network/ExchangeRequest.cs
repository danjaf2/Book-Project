using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExchangeRequest : MonoBehaviour
{
    private string url = "url";


    public void GetBooks()
    {
        StartCoroutine(MakeRequest());
    }

    IEnumerator MakeRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Received" + request.downloadHandler.text);
            var data = JsonConvert.DeserializeObject<List<DataBook>>(request.downloadHandler.text);
            
        }
    }
}

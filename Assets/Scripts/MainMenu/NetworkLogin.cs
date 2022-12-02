using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkLogin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckLogin(string user, string pass)
    {
        loginInfo r = new loginInfo();
        r.username = user;
        r.password = pass;
        string data = JsonConvert.SerializeObject(r);
        StartCoroutine(SendLogin("http://localhost:3000/api/auth/signin", data));
    }

    IEnumerator SendLogin(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");

            var data = JsonConvert.DeserializeObject<idResponce>(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                Login.userID = data.id;
                SceneManager.LoadScene(1);
            }
            else
            {
                print("Invalid Input");
            }
            
        }
        Debug.Log("Status Code: " + request.responseCode);
    }
}

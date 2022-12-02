using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public static int userID=-1;

   
    // Start is called before the first frame update
    public void buttonPressed()
    {
        userID = -1;
        print(email.text);
        print(password.text);
        NetworkLogin nl = this.gameObject.GetComponent<NetworkLogin>();
        nl.CheckLogin(email.text, password.text);

        
        
    }
}

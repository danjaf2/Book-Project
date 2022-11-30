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

    //Temp values
    private string emailC = "user@user.ca";
    private string passwordC = "password";
    // Start is called before the first frame update
    public void buttonPressed()
    {
        print(email.text);
        print(password.text);

        if (email.text == emailC && password.text == passwordC)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            print("Invalid Input");
        }
    }
}

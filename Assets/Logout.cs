using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    // Start is called before the first frame update
   public void logout()
    {
        Login.userID = -1;
        SceneManager.LoadScene(0);
    }
}

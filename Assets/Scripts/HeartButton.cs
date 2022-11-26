using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent clickEvent = new UnityEvent();
    public GameObject button;

    void Start()
    {
        button = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

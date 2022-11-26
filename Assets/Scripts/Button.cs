using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
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
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Button"&& hit.collider.gameObject==button)
                {
                    clickEvent.Invoke();
                    print("Here");
                }

            }
        }
    }
}

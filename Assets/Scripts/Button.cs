using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent clickEvent = new UnityEvent();
    public GameObject button;
    public Color c;
    public SpriteRenderer rend;

    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        c = rend.color;
        button = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Button"&& hit.collider.gameObject==button)
                {
                    clickEvent.Invoke();
                    StartCoroutine(colorChange());
                }

            }
        }
    }

    IEnumerator colorChange()
    {
        this.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().color = c;
}
}

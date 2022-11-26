﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity;


    public BookDisplay menu;
    public GameObject previousHovered;


    private void Start()
    {
        
       
    }

    void Update ()
    {
        float mouseX = Input.GetAxis ("Mouse X");
        float mouseY = Input.GetAxis ("Mouse Y");
        
        // Left-click or middle-click to drag camera around
        if(Input.GetMouseButton(0) || Input.GetMouseButton(2))
        {
            transform.Translate(new Vector3(-mouseX, 0.0f, -mouseY) * mouseSensitivity, Space.World);
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Book")
                {
                    print(hit.collider.gameObject.GetComponent<Book>());
                    menu.Display(hit.collider.gameObject.GetComponent<Book>());
                }
                else
                {
                    menu.Hide();
                }
            }
        }
        RaycastHit hovered;
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hovered))
        {
            if (hovered.collider.tag == "Book")
            {
                if (previousHovered == null)
                {
                    print("Here");
                    previousHovered = hovered.collider.gameObject;
                    previousHovered.GetComponent<Book>().heart.SetActive(true);
                }else if (previousHovered != null&&hovered.collider.gameObject != previousHovered)
                {
                    previousHovered.GetComponent<Book>().heart.SetActive(false);
                    previousHovered = hovered.collider.gameObject;
                    previousHovered.GetComponent<Book>().heart.SetActive(true);
                }
            }
            else
            {
                if (previousHovered != null)
                {
                    previousHovered.GetComponent<Book>().heart.SetActive(false);
                    previousHovered = null;
                }  
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (previousHovered != null)
            {
                previousHovered.GetComponent<Book>().Favorite();
            }
            
        }

    }
}

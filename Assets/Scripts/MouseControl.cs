using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity;


    public BookDisplay menu;
    public static GameObject previousHovered;


    private void Start()
    {
        
       
    }

    void Update ()
    {
        float mouseX = Input.GetAxis ("Mouse X");
        float mouseY = Input.GetAxis ("Mouse Y");
        if(Input.mouseScrollDelta.y != 0)
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + Input.mouseScrollDelta.y * 5f);


        if (Camera.main.transform.position.z > 0.21)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0.21f);
        }
        if (Camera.main.transform.position.z < -21.51)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -21.51f);
        }
        if (Camera.main.transform.position.x > 3.5)
        {
            Camera.main.transform.position = new Vector3(3.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Camera.main.transform.position.x < -3.5)
        {
            Camera.main.transform.position = new Vector3(-3.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

        // Left-click or middle-click to drag camera around
        /*if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
        {
            transform.Translate(new Vector3(-mouseX, 0.0f, -mouseY) * mouseSensitivity, Space.World);
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 20f))
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
        if (Physics.Raycast(ray2, out hovered, 20f))
        {
            if (hovered.collider.tag == "Book"&&hovered.transform.position.z != 3.37f)
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

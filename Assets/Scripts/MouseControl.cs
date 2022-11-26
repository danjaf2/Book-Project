using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity;


    public Text title;
    public Text genre;
    public RawImage cover;
    public GameObject previousHovered;


    private void Start()
    {
        title = GameObject.Find("Title").GetComponent<Text>();
        cover = GameObject.Find("Cover").GetComponent<RawImage>();
        genre = GameObject.Find("Genre").GetComponent<Text>();
        title.transform.parent.gameObject.SetActive(false);
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
                    title.transform.parent.gameObject.SetActive(true);
                    title.text = "Title:" + hit.collider.gameObject.GetComponent<Book>().name;
                    cover.texture = hit.collider.gameObject.GetComponent<Book>().image;
                    genre.text = "Genre:" + hit.collider.gameObject.GetComponent<Book>().genre.ToString();
                }
                else
                {
                    title.transform.parent.gameObject.SetActive(false);
                }
            }
        }
        RaycastHit hovered;
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hovered))
        {
            if (hovered.collider.tag == "Book"|| hovered.collider.tag == "HeartButton")
            {
                if (previousHovered == null)
                {
                    print("Here");
                    previousHovered = hovered.collider.gameObject;
                    previousHovered.GetComponent<Book>().heart.SetActive(true);
                }else if (hovered.collider.gameObject != previousHovered)
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public BookObject book;
    [System.NonSerialized]
    public string name;
    public enum Genre
    {
        Horror, Comedy, Fantasy, Adventure, Lovecraft, Dark
    }
    Color[] colors = { Color.green, Color.red, Color.white, Color.blue, Color.yellow, Color.black };
    [System.NonSerialized]
    public Genre genre;
    [System.NonSerialized]
    private Material matImage;
    [System.NonSerialized]
    public Texture2D image;
    public bool favorite;
    public GameObject heart;
    public ShelfManager manager;

    private float targetHeight =1f;
    // Start is called before the first frame update
    void Start()
    {
        name = book.name;
        genre = book.genre;
        matImage = book.matImage;
        image = book.image;
        manager = GameObject.FindObjectOfType<ShelfManager>();
        this.GetComponent<Renderer>().material.color = colors[(int)genre];
        this.transform.GetChild(0).GetComponent<Renderer>().material=matImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (favorite)
        {
            heart.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            heart.GetComponent<SpriteRenderer>().color = Color.black;
        }
        
    }
    Genre GetGenre()
    {
        return genre;
    }
    public void Favorite()
    {
        if (!favorite)
        {
            favorite = true;
            manager.addBookToFav(this);
        }
        else
        {
            favorite = false;
            manager.addBookToFav(this);
        }
    }
}

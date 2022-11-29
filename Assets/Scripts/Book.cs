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
    public Sprite image;
    public bool favorite;
    public GameObject heart;
    public ShelfManager manager;
    public int ID;

    private float targetHeight =1f;
    // Start is called before the first frame update
    void Start()
    {
        setBook();
        heart = this.transform.GetChild(0).GetChild(0).gameObject;
        heart.SetActive(false);
        manager = GameObject.FindObjectOfType<ShelfManager>();
        
    }

    public void setBook()
    {
        name = book.name;
        genre = book.genre;
        var rect = new Rect(0, 0, book.image.width, book.image.height);
        var sprite = Sprite.Create(book.image, rect, new Vector2(0.5f, 0.5f));
        image = sprite;
        this.GetComponent<Renderer>().material.color = colors[(int)genre];
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = image;
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
            Book copy = Instantiate(this);
            copy.favorite = true;
            copy.ID = this.ID;
            manager.addBookToFav(copy);
        }
        else
        {
            manager.addBookToFav(this);
            
        }
    }
}

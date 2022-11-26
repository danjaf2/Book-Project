using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookDisplay : MonoBehaviour
{

    public Text title;
    public Text genre;
    public RawImage cover;
    public Book currentBook;
    public ShelfManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<ShelfManager>();
        title = GameObject.Find("Title").GetComponent<Text>();
        cover = GameObject.Find("Cover").GetComponent<RawImage>();
        genre = GameObject.Find("Genre").GetComponent<Text>();
        title.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(Book book)
    {
        currentBook = book;
        title.transform.parent.gameObject.SetActive(true);
        title.text = "Title:" + book.name;
        cover.texture = book.image;
        genre.text = "Genre:" + book.genre.ToString();
    }
    public void Hide()
    {
        currentBook = null;
        title.transform.parent.gameObject.SetActive(false);
    }

    public void AddSelectedToReading()
    {
        manager.addBookToReading(currentBook);
    }
    public void AddSelectedToToRead()
    {
        manager.addBookToToRead(currentBook);
    }
    public void AddSelectedToRead()
    {
        manager.addBookToRead(currentBook);
    }
}

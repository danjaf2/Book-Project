using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookDisplay : MonoBehaviour
{

    public TMP_Text title;
    public TMP_Text genre;
    public TMP_Text author;
    public RawImage cover;
    public Book currentBook;
    public ShelfManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<ShelfManager>();
        title = GameObject.Find("Title").GetComponent<TMP_Text>();
        cover = GameObject.Find("Cover").GetComponent<RawImage>();
        genre = GameObject.Find("Genre").GetComponent<TMP_Text>();
        author = GameObject.Find("Author").GetComponent<TMP_Text>();
        title.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }


    public void Display(Book book)
    {
        currentBook = book;
        title.transform.parent.gameObject.SetActive(true);
        title.text = "Title:" + book.name;
        author.text = "Author:" + book.author;
        cover.texture = textureFromSprite(book.image);
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

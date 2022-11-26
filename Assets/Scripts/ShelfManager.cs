using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public List<Book> allBooks;
    public Shelf recShelf;
    public Shelf favShelf;
    public Shelf readingShelf;
    public Shelf toReadShelf;
    public Shelf readShelf;
    // Start is called before the first frame update
    void Start()
    {
        recShelf = GameObject.Find("RecShelf").GetComponent<Shelf>();
        favShelf = GameObject.Find("FavShelf").GetComponent<Shelf>();
        readingShelf = GameObject.Find("ReadingShelf").GetComponent<Shelf>();
        toReadShelf = GameObject.Find("ToReadShelf").GetComponent<Shelf>();
        readShelf = GameObject.Find("ReadShelf").GetComponent<Shelf>();
        var list = FindObjectsOfType<Book>();
        for (int i = 0; i < list.Length; i++)
        {
            allBooks.Add(list[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Book book in allBooks)
        {
            if (!favShelf.books.Contains(book))
            {
                Shelf currentShelf = findCurrentShelf(book);
                if (currentShelf == null)
                {
                    recShelf.books.Add(book);
                }
            }
        }
    }

    public Shelf findCurrentShelf(Book book)
    {
        if (recShelf.books.Contains(book))
        {
            return recShelf;
        } else if (favShelf.books.Contains(book))
        {
            return favShelf;
        }
        else if (readingShelf.books.Contains(book))
        {
            return readingShelf;
        }
        else if (toReadShelf.books.Contains(book))
        {
            return toReadShelf;
        }
        else if (readShelf.books.Contains(book))
        {
            return readShelf;
        }
        return null;
    }
    public void addBookToFav(Book book)
    {
        Shelf currentShelf= findCurrentShelf(book);
        if (currentShelf!=favShelf)
        {
            currentShelf.currentIndex = 0;
            currentShelf.books.Remove(book);
            favShelf.books.Add(book);
        }
        else
        {
            favShelf.books.Remove(book);
            favShelf.currentIndex = 0;
        }
    }

    public void addBookToReading(Book book)
    {
        Shelf currentShelf = findCurrentShelf(book);
        if (currentShelf != readingShelf)
        {
            currentShelf.currentIndex = 0;
            currentShelf.books.Remove(book);
            print("Added");
            readingShelf.books.Add(book);
        }
        else
        {
            readingShelf.books.Remove(book);
            readingShelf.currentIndex = 0;
        }
    }

    public void addBookToToRead(Book book)
    {
        Shelf currentShelf = findCurrentShelf(book);
        if (currentShelf != toReadShelf)
        {
            currentShelf.currentIndex = 0;
            currentShelf.books.Remove(book);
            toReadShelf.books.Add(book);
        }
        else
        {
            toReadShelf.books.Remove(book);
            toReadShelf.currentIndex = 0;
        }
    }

    public void addBookToRead(Book book)
    {
        Shelf currentShelf = findCurrentShelf(book);
        if (currentShelf != readShelf)
        {
            currentShelf.currentIndex = 0;
            currentShelf.books.Remove(book);
            readShelf.books.Add(book);
        }
        else
        {
            readShelf.books.Remove(book);
            readShelf.currentIndex = 0;
        }
    }
}

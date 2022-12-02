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
    public ExchangeRequest request;

    public Book prefab;
    // Start is called before the first frame update
    void Start()
    {
        recShelf = GameObject.Find("RecShelf").GetComponent<Shelf>();
        favShelf = GameObject.Find("FavShelf").GetComponent<Shelf>();
        readingShelf = GameObject.Find("ReadingShelf").GetComponent<Shelf>();
        toReadShelf = GameObject.Find("ToReadShelf").GetComponent<Shelf>();
        readShelf = GameObject.Find("ReadShelf").GetComponent<Shelf>();
        request = GameObject.FindObjectOfType<ExchangeRequest>();        

        
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

    // Update is called once per frame
    void Update()
    {
        /*
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
        */


        
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
        bool found = false;
        for (int i = 0; i < favShelf.books.Count; i++)
        {
            if (book.ID == favShelf.books[i].ID)
            {
                found = true;
                Book b = favShelf.books[i];
                b.favorite = false;
                favShelf.books.Remove(b);
                
                //request.FavoriteChange(book);
                b.transform.position = new Vector3(1000000f, 0f, 0f);
            }
        }
        if (found)
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                if (book.ID == allBooks[i].ID)
                {
                    allBooks[i].favorite = false;
                }
            }
        }
        if (found == false)
        {
            favShelf.books.Add(book);
        }
        /*Shelf currentShelf= findCurrentShelf(book);
        if (currentShelf!=favShelf && currentShelf!=null)
        {
            currentShelf.currentIndex = 0;
            currentShelf.books.Remove(book);
            favShelf.books.Add(book);
        }else if (currentShelf == null)
        {
            favShelf.books.Add(book);
        }
        else
        {

            favShelf.books.Remove(book);
            favShelf.currentIndex = 0;
        }*/
    }

    public void addBookToReading(Book book)
    {
        Shelf currentShelf = findCurrentShelf(book);
        if (currentShelf == recShelf)
        {
            request.ShelfRecommendedChange(book, 2);
        }
        if (currentShelf == favShelf)
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                if (book.ID == allBooks[i].ID)
                {
                    print("HEY");
                    if (book != allBooks[i])
                    {
                       Shelf shelf = findCurrentShelf(allBooks[i]);
                        print(shelf.gameObject.name);
                        shelf.currentIndex = 0;
                        shelf.books.Remove(allBooks[i]);
                        print("HERE");
                        readingShelf.books.Add(allBooks[i]);
                        request.ShelfChange(allBooks[i], 2);
                        return;
                    }
                }
            }
        }
        if (currentShelf != readingShelf)
        {
            if (currentShelf != null)
            {
                currentShelf.currentIndex = 0;
                currentShelf.books.Remove(book);
            }
            print("Added");
            readingShelf.books.Add(book);
            request.ShelfChange(book, 2);
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
        if (currentShelf == recShelf)
        {
            request.ShelfRecommendedChange(book, 1);
        }
        if (currentShelf == favShelf)
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                if (book.ID == allBooks[i].ID)
                {
                    print("HEY");
                    if (book != allBooks[i])
                    {
                        Shelf shelf = findCurrentShelf(allBooks[i]);
                        print(shelf.gameObject.name);
                        shelf.currentIndex = 0;
                        shelf.books.Remove(allBooks[i]);
                        print("HERE");
                        toReadShelf.books.Add(allBooks[i]);
                        request.ShelfChange(allBooks[i], 1);
                        return;
                    }
                }
            }
        }
        if (currentShelf != toReadShelf)
        {
            if (currentShelf != null)
            {
                currentShelf.currentIndex = 0;
                currentShelf.books.Remove(book);
            }
            toReadShelf.books.Add(book);
            request.ShelfChange(book, 1);
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
        if (currentShelf == recShelf)
        {
            request.ShelfRecommendedChange(book, 3);
        }
        if (currentShelf == favShelf)
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                if (book.ID == allBooks[i].ID)
                {
                    print("HEY");
                    if (book != allBooks[i])
                    {
                        Shelf shelf = findCurrentShelf(allBooks[i]);
                        print(shelf.gameObject.name);
                        shelf.currentIndex = 0;
                        shelf.books.Remove(allBooks[i]);
                        print("HERE");
                        readShelf.books.Add(allBooks[i]);
                        request.ShelfChange(allBooks[i], 3);
                        return;
                    }
                }
            }
        }
        if (currentShelf != readShelf)
        {
            if (currentShelf != null)
            {
                currentShelf.currentIndex = 0;
                currentShelf.books.Remove(book);
            }
            readShelf.books.Add(book);
            request.ShelfChange(book, 3);
        }
        else
        {
            readShelf.books.Remove(book);
            readShelf.currentIndex = 0;
        }
    }
}

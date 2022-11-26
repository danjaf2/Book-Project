using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shelf : MonoBehaviour
{
    int currentIndex = 0;
    int maxBooksInShelfVisible = 6;
    public List<Book> books;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Right()
    {
        currentIndex = (currentIndex + 1) % books.Count;
    }
    public void Left()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = books.Count - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex=(currentIndex+1)%books.Count;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = books.Count - 1;
            }

        }
        sortByGenre();

        //sortByGenre();
        int counter = 0;
        int booksInserted = 0;
        for (int i = currentIndex; booksInserted < books.Count; i = (i + 1) % books.Count)
        {
            if (counter < maxBooksInShelfVisible)
            {
                books[i].transform.position = new Vector3(this.transform.position.x + 2.5f + 3.6f * booksInserted, 0.3f, this.transform.position.z);
                counter++;
            }
            else
            {
                books[i].transform.position = new Vector3(this.transform.position.x + 2.5f + 3.6f * booksInserted, -10f, this.transform.position.z);
            }
            booksInserted++;
        }

    }

   public void sortByGenre()
    {
        List<Book> SortedList = books.OrderBy(o => (int)o.genre).ToList();
        books = SortedList;
    }
}

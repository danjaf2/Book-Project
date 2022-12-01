using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Book;

[CreateAssetMenu(fileName ="New Book", menuName = "Book")]
public class BookObject : ScriptableObject
{
    public string name;
    public string author;
    public Genre genre;
    public Texture2D image;
}

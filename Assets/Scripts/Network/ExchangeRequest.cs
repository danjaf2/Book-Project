using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Book;

public class ExchangeRequest : MonoBehaviour
{
    private string url = "url";
    public Book prefab;

    public void GetBooks()
    {
        StartCoroutine(MakeRequest());
    }

    IEnumerator MakeRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Received" + request.downloadHandler.text);
            var data = JsonConvert.DeserializeObject<List<DataBook>>(request.downloadHandler.text);
            ShelfManager manager = GameObject.FindObjectOfType<ShelfManager>();


            for (int i = 0; i < data.Count; i++)
            {
                BookObject bookData = BookObject.CreateInstance("BookObject") as BookObject;
                var texture = Resources.Load<Texture2D>(data[i].cover);
                bookData.image = texture;
                bookData.name = data[i].title;
                bookData.author = data[i].author;
                bool valid = System.Enum.TryParse(data[i].genre, out Genre result);
                bookData.genre = result;

                Book b = Instantiate(prefab);
                b.book = bookData;
                b.setBook();
            }
            /*
            Book temp = Instantiate(prefab);
            BookObject obj = BookObject.CreateInstance("BookObject") as BookObject;
            obj.genre = Book.Genre.Children;
            obj.name = "Jerry";
            var texture = Resources.Load<Texture2D>("BookCovers/Cover3");
            obj.image = texture;
            obj.author = "Test";

            temp.book = obj;
            */

            
        }
    }
}

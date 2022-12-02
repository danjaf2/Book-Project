using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Book;

public class ExchangeRequest : MonoBehaviour
{
    private string url = "http://localhost:3000/api/books";
    public Book prefab;

    public void GetBooks()
    {
        StartCoroutine(MakeRequest());
    }


    public void SendRecommended(string[] array)
    {
        RecData r = new RecData();
        r.genres = array;
        r.userId = Login.userID;
        string data = JsonConvert.SerializeObject(r);
        StartCoroutine(Post("http://localhost:3000/api/books/recommendations", data));

    }

    

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            //StartCoroutine(GetRec());
            var data = JsonConvert.DeserializeObject<List<DataBook>>(request.downloadHandler.text);
            ShelfManager manager = GameObject.FindObjectOfType<ShelfManager>();
            manager.recShelf.books.Clear();
            for (int j = 0; j < manager.allBooks.Count; j++)
            {
                manager.allBooks[j].transform.position= new Vector3(1000000f, 0f, 0f);
            }
                for (int i = 0; i < data.Count; i++)
                {
                for (int j = 0; j < manager.allBooks.Count; j++)
                {
                    if (manager.allBooks[j].ID == data[i].id)
                    {
                        manager.recShelf.books.Add(manager.allBooks[j]);
                    }
                }
            }
        }
        Debug.Log("Status Code: " + request.responseCode);
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

            print(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                BookObject bookData = BookObject.CreateInstance("BookObject") as BookObject;
                data[i].cover = data[i].cover.Split('.')[0];
                var texture = Resources.Load<Texture2D>(data[i].cover);
                bookData.image = texture;
                bookData.id = data[i].id;
                bookData.name = data[i].title;
                bookData.author = data[i].author;
                bool valid = System.Enum.TryParse(data[i].genre, out Genre result);
                bookData.genre = result;

                Book b = Instantiate(prefab);
                b.book = bookData;
                b.userBookShelfID = -1;
                b.setBook();
                manager.allBooks.Add(b);
            }
            StartCoroutine(SetBookShelves());
        }

    }

    IEnumerator SetBookShelves()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/api/UserAllBooks/"+Login.userID.ToString());
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Received" + request.downloadHandler.text);
            var data = JsonConvert.DeserializeObject<List<DataBookInstance>>(request.downloadHandler.text);
            ShelfManager manager = GameObject.FindObjectOfType<ShelfManager>();

            print(data.Count);
            foreach(DataBookInstance inst in data)
            {
                for (int j = 0; j < manager.allBooks.Count; j++)
                {
                    if(inst.bookId== manager.allBooks[j].ID)
                    {
                        manager.allBooks[j].userBookShelfID = inst.id;
                        //To Read
                        if (inst.shelfId == 1)
                        {
                            manager.addBookToToRead(manager.allBooks[j]);
                        }else if (inst.shelfId == 2) //Reading
                        {
                            manager.addBookToReading(manager.allBooks[j]);
                        }
                        else //Read
                        {
                            manager.addBookToRead(manager.allBooks[j]);
                        }
                        if (inst.favorited==1)
                        {
                            manager.allBooks[j].FavoriteLoad();
                        }
                    }
                }
            }
            
        }
    }

    public void ShelfChange(Book book, int shelfID)
    {
        string url = "http://localhost:3000/api/moveBook/"+book.userBookShelfID.ToString() + "/" + shelfID;
        StartCoroutine(PutShelfUpdate(url));
    }

    public void FavoriteChange(Book book)
    {
        string url = "http://localhost:3000/api/toggleFavorite/" + book.userBookShelfID.ToString();
        StartCoroutine(PutShelfUpdate(url));
    }

    public void ShelfRecommendedChange(Book book, int shelfID)
    {
        string url = string.Format("http://localhost:3000/api/addBook/{0}/{1}?bookId={2}&favorited={3}",shelfID, Login.userID, book.ID, book.favorite.ToString().ToLower());
        StartCoroutine(PostShelfRecUpdate(url));
    }

    IEnumerator PostShelfRecUpdate(string url)
    {
        var request = new UnityWebRequest(url, "POST");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        Debug.Log("Status Code: " + request.responseCode);
    }

    IEnumerator PutShelfUpdate(string url)
    {
        var request = new UnityWebRequest(url, "PUT");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        Debug.Log("Status Code: " + request.responseCode);
    }

}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Book;

public class AddGenresToList : MonoBehaviour
{
    public GameObject itemTemplate;
    public GameObject content;
    public List<Toggle> toggleList;

    int amountOfGenres = 6;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfGenres; i++)
        {
            var copy = Instantiate(itemTemplate);
            copy.transform.parent= content.transform;
            copy.GetComponentInChildren<Text>().text = ((Genre)i).ToString();
            toggleList.Add(copy.GetComponent<Toggle>());
            toggleList[i].isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

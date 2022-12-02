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
    public ExchangeRequest request;

    int amountOfGenres = 10;
    // Start is called before the first frame update
    void Start()
    {
        request = GameObject.FindObjectOfType<ExchangeRequest>();
        for (int i = 0; i < amountOfGenres; i++)
        {
            var copy = Instantiate(itemTemplate);
            copy.transform.parent= content.transform;
            copy.GetComponentInChildren<Text>().text = ((Genre)i).ToString();
            copy.GetComponentInChildren<Text>().color = Color.white;
            toggleList.Add(copy.GetComponent<Toggle>());
            toggleList[i].isOn = false;
        }

        request.RecSelectGet(toggleList);
    }

    // Update is called once per frame
    void Update()
    {
        int count=0;
        for(int i=0;i< toggleList.Count;i++)
        {
            if (toggleList[i].isOn)
            {
                count++;
                print(((Genre)i).ToString());
            }
        }

        if (count >= 3)
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                if (!toggleList[i].isOn)
                {
                    toggleList[i].interactable = false;
                }
            }
        }
        if (count < 3)
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                toggleList[i].interactable = true;
            }
        }
    }


    public void Submit()
    {
        string []list= new string[3];
        int index = 0;
        //Send data
        for (int i = 0; i < toggleList.Count; i++)
        {
            if (toggleList[i].isOn)
            {
                list[index] = ((Genre)i).ToString();
                index++;
            }
        }
        if (index == 3 || index == 0)
        {
            request.RecSelectChange(list);
        }
    }
}

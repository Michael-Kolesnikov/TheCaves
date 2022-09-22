using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIPanel;
    void Start()
    {
        UIPanel.SetActive(false);
    }   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(isOpened) UIPanel.SetActive(false);
            else UIPanel.SetActive(true);



            isOpened = !isOpened;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invmanager : MonoBehaviour
{
    public List<ItemsScriptableobject> items;
    [SerializeField]
    private Transform invui;
    public List<Image> slots;

    public void Start()
    {
        for (int i = 0; i < invui.childCount; i++)
        {
            slots.Add(invui.GetChild(i).GetChild(0).GetComponent<Image>());
        }
    }
}

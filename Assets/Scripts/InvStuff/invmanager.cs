using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class invmanager : MonoBehaviour
{
    [Header("SlotChangeUi")]
    [SerializeField]
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField]
    EventSystem m_EventSystem;
    [Header("Transforms")]
    [SerializeField]
    private Transform invui;
    [SerializeField]
    private Transform OpenAndCloseInvUi;
    [SerializeField]
    private Transform WeaponHolder;
    [SerializeField]
    private CharController Player;

    private GameObject HoldTempSlot = null;
    private bool GetTempSlotOnce = false;

    [Header("Lists")]
    public List<Image> slots;
    public List<bool> items;
    public void Start()
    {
        for (int i = 0; i < invui.GetChild(1).childCount; i++)
        {
            slots.Add(invui.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>());
        }
        invui.gameObject.SetActive(false);

        // for(int i = 0; i < WeaponHolder.childCount; i++)
        // {
        //     SwayWeapon.Add(WeaponHolder.GetChild(i)
        // }
    }


    private void Update()
    {
    // Weapon Swap ------------------------------------------------------
    #region uglycode
        if (Input.GetKeyDown(KeyCode.Alpha1) && invui.gameObject.activeSelf == false && items.Count >= 1)
        {
            for (int i = 0; i < WeaponHolder.childCount; i++)
            {
                WeaponHolder.GetChild(i).gameObject.SetActive(false);

            }
            WeaponHolder.GetChild(0).gameObject.SetActive(true);
            WeaponHolder.GetComponentInChildren<Sway>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && invui.gameObject.activeSelf == false && items.Count >= 2)
        {
            for (int i = 0; i < WeaponHolder.childCount; i++)
            {
                WeaponHolder.GetChild(i).gameObject.SetActive(false);
            }
            WeaponHolder.GetChild(1).gameObject.SetActive(true);
            WeaponHolder.GetComponentInChildren<Sway>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && invui.gameObject.activeSelf == false && items.Count >= 3)
        {
            for (int i = 0; i < WeaponHolder.childCount; i++)
            {
                WeaponHolder.GetChild(i).gameObject.SetActive(false);
            }
            WeaponHolder.GetChild(2).gameObject.SetActive(true);
            WeaponHolder.GetComponentInChildren<Sway>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && invui.gameObject.activeSelf == false && items.Count >= 4)
        {
            for (int i = 0; i < WeaponHolder.childCount; i++)
            {
                WeaponHolder.GetChild(i).gameObject.SetActive(false);
            }
            WeaponHolder.GetChild(3).gameObject.SetActive(true);
            WeaponHolder.GetComponentInChildren<Sway>().enabled = true;
        }
        #endregion
    // Open Inv --------------------------------------------------------
    #region OpenInv
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (invui.gameObject.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Player.enabled = true;
                StartCoroutine(OpeninvOrClose());
                

                WeaponHolder.GetComponentInChildren<Sway>().enabled = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Player.enabled = false;

                WeaponHolder.GetComponentInChildren<Sway>().enabled = false;
                 
                StartCoroutine(OpeninvOrClose());
                
            }

        }
    }
    private IEnumerator OpeninvOrClose()
    {
        float timelerp = 0;
        if(invui.gameObject.activeSelf == true)
        {
            while(timelerp <= 0.3f)
            {
                timelerp += Time.deltaTime * 0.7f;
                invui.position = Vector3.Lerp(invui.position, OpenAndCloseInvUi.GetChild(0).position, timelerp / 1);
                yield return new WaitForFixedUpdate();
            }
            
            invui.gameObject.SetActive(false);
        }
        else
        {
            invui.gameObject.SetActive(true);
            while (timelerp <= 0.3f)
            {
                timelerp += Time.deltaTime * 0.7f;
                invui.position = Vector3.Lerp(invui.position, OpenAndCloseInvUi.position, timelerp / 1);
                yield return new WaitForFixedUpdate();
            }
        }
    }
   
    #endregion
    #region DragUi
    public void DragUi()
    {
        StartCoroutine(DragUiNumerator());
    }
    private IEnumerator DragUiNumerator()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position

        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        while (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2))
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("itemSlot"))
                {
                    

                    if (GetTempSlotOnce == false)
                    {
                        for (int i = 0; i < slots.Count; i++)
                        {
                            slots.ToArray()[i].tag = "Untagged";
                        }
                        HoldTempSlot = result.gameObject;
                        //HoldTempWeapon = HoldTempSlot.transform.parent
                        result.gameObject.tag = "itemSlot";
                        HoldTempSlot.transform.position = result.gameObject.transform.position;
                    }
                    GetTempSlotOnce = true;
                    result.gameObject.transform.position = Input.mousePosition;
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        break;
                    }
                }

            }
            yield return new WaitForFixedUpdate();
        }

    }
    #endregion
    #region StopDraging
    public void EndDragUi()
    {
        StopCoroutine(DragUiNumerator());
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("ItemSlotHolder") && WeaponHolder.childCount > 1 && result.gameObject.transform.GetSiblingIndex() <= WeaponHolder.childCount)
            {

                WeaponHolder.GetChild(result.gameObject.transform.GetSiblingIndex()).GetChild(0).SetParent(WeaponHolder.GetChild(HoldTempSlot.transform.parent.GetSiblingIndex()));
                HoldTempSlot.transform.position = result.gameObject.transform.position;
                WeaponHolder.GetChild(HoldTempSlot.transform.parent.GetSiblingIndex()).GetChild(0).SetParent(WeaponHolder.GetChild(result.gameObject.transform.GetSiblingIndex()));

                //WeaponHolder.GetChild(result.gameObject.transform.GetSiblingIndex()).SetSiblingIndex(HoldTempSlot.transform.parent.GetSiblingIndex());

                invui.GetChild(1).GetChild(result.gameObject.transform.GetSiblingIndex()).GetChild(0).transform.position = HoldTempSlot.transform.parent.position;
                invui.GetChild(1).GetChild(result.gameObject.transform.GetSiblingIndex()).GetChild(0).SetParent(HoldTempSlot.transform.parent);
                HoldTempSlot.transform.SetParent(invui.GetChild(1).GetChild(result.gameObject.transform.GetSiblingIndex()));


            }
            else
            {
                HoldTempSlot.transform.position = HoldTempSlot.transform.parent.position;
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            slots.ToArray()[i].tag = "itemSlot";
        }
        GetTempSlotOnce = false;
        // HoldTempSlot.GetComponent<Image>().raycastTarget = true;
    }
    #endregion
}

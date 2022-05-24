using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class ItemPickUp : MonoBehaviour
{
    [Header("Inv And Scriptableobject")]
    [SerializeField]
    private ItemsScriptableobject Item;
    [SerializeField]
    private invmanager inv;
    [Header("Transforms")]
    [SerializeField]
    private Transform UiPickUp;
    [SerializeField]
    private Transform Player;
    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI Text;

    private float LerpTime = 0;

    private bool IsDisableOrNot;

    private Vector3 UiPickUpNormalSize = new Vector3(1, 1, 1);
    private Vector3 UiPickUpHideSize = new Vector3(0, 0, 0);
    //private void OnMouseEnter()
    //{
    //    if (Vector3.Distance(transform.position, Player.position) <= 5)
    //    {
    //        Text.text = Item.ItemName;
    //        IsDisableOrNot = true;
    //        StartCoroutine(OnHover());
    //    }
    //
    //}
    private void OnMouseOver()
    {
        if( Vector3.Distance(transform.position, Player.position) >= 5 && IsDisableOrNot == true)
        {
            
            IsDisableOrNot = false;
            StartCoroutine(OffHover());
        }
        if(Vector3.Distance(transform.position, Player.position) <= 5 && IsDisableOrNot == false)
        {
            Text.text = Item.ItemName;
            IsDisableOrNot = true;
            StartCoroutine(OnHover());
        }
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, Player.position) <= 5)
        {
            inv.items.Add(Item);
            Destroy(gameObject);
            UiPickUp.localScale = UiPickUpHideSize;

            for (int i = 0; i < inv.slots.Count; i++)
            {
                if (inv.slots.ToArray()[i].enabled == false)
                {

                    inv.slots.ToArray()[i].enabled = true;
                    inv.slots.ToArray()[i].sprite = Item.ItemIcon;
                    break;
                }

            }
        }
    }
   private void OnMouseExit()
   {
      
       IsDisableOrNot = false;
       StartCoroutine(OffHover());
   }

    private IEnumerator OffHover()
    {
        LerpTime = 0;
        while (LerpTime < 0.3f)
        {
            LerpTime += 0.3f * Time.deltaTime;
            UiPickUp.localScale = Vector3.Lerp(UiPickUp.localScale, UiPickUpHideSize, LerpTime / 1);
            Debug.Log("a");
            if (IsDisableOrNot == true)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
       
    }
    private IEnumerator OnHover()
    {
        LerpTime = 0;
        while (LerpTime < 0.3f)
        {
            LerpTime += 0.3f * Time.deltaTime;
            UiPickUp.localScale = Vector3.Lerp(UiPickUp.localScale, UiPickUpNormalSize, LerpTime / 1);
            if (IsDisableOrNot == false)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

}

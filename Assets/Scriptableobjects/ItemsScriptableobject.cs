using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class ItemsScriptableobject : ScriptableObject
{

    public string ItemName;



    public Sprite ItemIcon;

    public int damage = 5;

    public bool CanAttack = true;
}

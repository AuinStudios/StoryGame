using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Change The LightBulb")]
    [SerializeField]
    private Material LightBulbs;
    [Header("Enable The Lights")]
    [SerializeField]
    private GameObject LightSource;
    [Header("stopTheScript")]
    [SerializeField]
    private LightSwitch DestoryScript;
    [Header("OpenElevator")]
    [SerializeField]
    private Animator ElevatorOpen;

    private Quaternion LightSwitchRot;
    // Start is called before the first frame update
    void Start()
    {
        LightSwitchRot = Quaternion.Euler(-90, transform.localRotation.y, transform.localRotation.z);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LightBulbs.SetFloat("_emisson", 2);
            LightSource.SetActive(true);
            transform.localRotation = LightSwitchRot;
            ElevatorOpen.SetBool("isopened", true);
            Destroy(DestoryScript);
        }
    }
}

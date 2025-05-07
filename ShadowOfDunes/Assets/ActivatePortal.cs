using System;
using TMPro;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    [SerializeField] private PortalGate_Controller portalGateScript;
    [SerializeField] private TextMeshProUGUI collisionText;

    public String numberOfKeys;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag =="Player" && collisionText.text == numberOfKeys){
            portalGateScript.F_TogglePortalGate(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag =="Player"){
            portalGateScript.F_TogglePortalGate(false);
        }
    }
}

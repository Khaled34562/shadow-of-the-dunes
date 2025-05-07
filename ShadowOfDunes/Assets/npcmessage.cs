using UnityEngine;
using UnityEngine.UI;

public class npcmessage : MonoBehaviour
{
   [SerializeField] private RawImage rawImage; // Reference to the RawImage component

    private void Start()
    {
        // Deactivate RawImage at start
        if (rawImage != null)
        {
            rawImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activate RawImage when entering trigger
        if (rawImage != null)
        {
            rawImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Deactivate RawImage when exiting trigger
        if (rawImage != null)
        {
            rawImage.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionTextUpdater : MonoBehaviour
{
    // Singleton instance
    public static CollisionTextUpdater Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI collisionText; // Assign TextMeshProUGUI in Inspector
     [SerializeField] private Runestone_Controller runestoneScript;
    private int collisionCount = 0;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }

        // Ensure the Sphere Collider is set as a trigger
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            sphereCollider.isTrigger = true; // Set to trigger for OnTriggerEnter
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            collisionCount++;
            runestoneScript.ToggleRuneStone(true);
            UpdateTextAcrossScenes();
        }
    }

    private void UpdateTextAcrossScenes()
    {
        // Update text in current scene
        if (collisionText != null)
        {
            collisionText.text = "" + collisionCount;
        }

        // Find and update TextMeshProUGUI in all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach (GameObject obj in rootObjects)
            {
                TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
                foreach (TextMeshProUGUI text in texts)
                {
                    if (text.CompareTag("activate")) // Tag your TMP Text with "CollisionText"
                    {
                        text.text = "" + collisionCount;
                    }
                }
            }
        }
    }

    // Call this to set the text reference dynamically (e.g., when a new scene loads)
    public void SetTextReference(TextMeshProUGUI newText)
    {
        collisionText = newText;
        UpdateTextAcrossScenes(); // Update immediately
    }

    // Handle scene loading to ensure text updates
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the TextMeshProUGUI in the new scene
        GameObject textObject = GameObject.FindWithTag("activate");
        if (textObject != null)
        {
            collisionText = textObject.GetComponent<TextMeshProUGUI>();
            UpdateTextAcrossScenes();
        }
    }
}
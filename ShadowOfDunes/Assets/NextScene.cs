using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 2.0f;

    [SerializeField] private TextMeshProUGUI collisionText;

     public string sceneToLoad = "Level1";

     public string numberOfKeys;

    public void FadeOut(){
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration){
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration){
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime /duration);
            yield return null;
        }
        cg.alpha = end;
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2.5f);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }

    // Make sure the collider is set to Is Trigger in the Unity Inspector
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (or any specific tag)
        if (other.CompareTag("Player") && collisionText.text == numberOfKeys)
        {
            FadeOut();
            StartCoroutine(LoadSceneAfterDelay());
        }
    }
}

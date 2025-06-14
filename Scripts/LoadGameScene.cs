using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public GameObject fadeCanvasPrefab;
    public void LoadScene()
    {
        StartCoroutine(FadeAndLoadScene());
    }
    private IEnumerator FadeAndLoadScene()
    {
        GameObject fadeinstance = Instantiate(fadeCanvasPrefab);
        Animator animator = fadeinstance.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }
}

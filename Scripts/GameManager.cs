using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject fadeCanvasprefab;
    private float startTime;
    private bool isPlaying = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        isPlaying = true;
        Debug.Log("�Q�[���J�n");
        StartCoroutine(Fadein());
    }
    private IEnumerator Fadein()
    {
        GameObject fadeinstance = Instantiate(fadeCanvasprefab);
        Animator animator = fadeinstance.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FadeIn");
        }
        yield return new WaitForSeconds(2f);
    }
    public float StopTime()
    {
        isPlaying = false;
        float elapsed = Time.time - startTime;
        Debug.Log($"�Q�[���N���A!�o�ߎ���:{elapsed:F2}�b");
        return elapsed;
    }
}

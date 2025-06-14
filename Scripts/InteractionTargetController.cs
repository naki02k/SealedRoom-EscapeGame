using System.Collections;
using UnityEngine;

public class InteractionTargetController : MonoBehaviour
{
    public Outline outline;
    public GameObject fadeCanvasPrefab;
    public GameObject interactionUI; // キーパッドでもパズルでも共通
    public Camera mainCamera;
    public GameObject mainCameraGazePointer;
    public Camera puzzleCamera;
    public Transform cameraTargetPoint; // カメラが移動する先（個別）

    [HideInInspector] public bool isSolved = false;
    [SerializeField] private FirstPersonMovement movement;

    private GameObject fadeInstance;

    private void Awake()
    {
        if (outline == null) outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    private void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

    public void OnHoverEnter()
    {
        if (!isSolved && outline != null) outline.enabled = true;
    }

    public void OnHoverExit()
    {
        if (!isSolved && outline != null) outline.enabled = false;
    }

    public void OnClick()
    {
        if (!isSolved)
        {
            StartCoroutine(SwitchToInteractionView());
        }
    }

    private IEnumerator SwitchToInteractionView()
    {
        if (fadeCanvasPrefab != null)
        {
            fadeInstance = Instantiate(fadeCanvasPrefab);
            var animator = fadeInstance.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("FadeOut");
                yield return new WaitForSeconds(2f);

                mainCamera.gameObject.SetActive(false);
                puzzleCamera.gameObject.SetActive(true);
                mainCameraGazePointer.SetActive(false);
                puzzleCamera.transform.position = cameraTargetPoint.position;
                puzzleCamera.transform.rotation = cameraTargetPoint.rotation;

                SwitchAudioListener(mainCamera, puzzleCamera);

                if (interactionUI != null)
                    interactionUI.SetActive(true);

                animator.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1f);
            }
        }

        movement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMainView()
    {
        StartCoroutine(SwitchBackToMainCamera());
    }

    private IEnumerator SwitchBackToMainCamera()
    {
        if (fadeCanvasPrefab != null)
        {
            fadeInstance = Instantiate(fadeCanvasPrefab);
            var animator = fadeInstance.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("FadeOut");
                yield return new WaitForSeconds(2f);

                mainCamera.gameObject.SetActive(true);
                puzzleCamera.gameObject.SetActive(false);
                mainCameraGazePointer.SetActive(true);
                

                SwitchAudioListener(puzzleCamera, mainCamera);

                if (interactionUI != null)
                    interactionUI.SetActive(false);

                animator.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1f);
            }
        }

        movement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SwitchAudioListener(Camera from, Camera to)
    {
        var fromListener = from.GetComponent<AudioListener>();
        var toListener = to.GetComponent<AudioListener>();

        if (fromListener != null) fromListener.enabled = false;
        if (toListener != null) toListener.enabled = true;
    }
}

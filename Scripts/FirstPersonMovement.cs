using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform cameraTransform;
    public float bobFrequency = 8f;
    public float bobAmplitude = 0.05f;
    private Vector3 originalcameraPosition;
    private float bobTimer = 0f;
    private Vector3 currentMoveInput;

    public AudioClip[] FoodStepClips;
    public float stepInterval = 0.5f;
    private float stepTimer;

    private AudioSource audioSource;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        if (cameraTransform != null)
        {
            originalcameraPosition = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        
        // 接地判定
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 軽く接地するように
        }

        // 移動入力
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        currentMoveInput = new Vector3(x, 0, z);
        
        Vector3 move = transform.right * x + transform.forward * z;
        if (controller.enabled)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }


        // 重力適用
        velocity.y += gravity * Time.deltaTime;
        if (controller.enabled)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        HandleHeadBob(move);
    }
    void FixedUpdate()
    {
        //移動入力が一定以上 & 接地しているかを判定（歩行中かどうか）
        bool isMoving = currentMoveInput.magnitude > 0.1f && controller.isGrounded;
        if (isMoving)
        {
            //足音のインターバルを減算
            stepTimer -= Time.fixedDeltaTime;
            //インターバルがゼロになったら足音を再生
            if (stepTimer <= 0f)
            {
                PlayFootStep();
                stepTimer = stepInterval;//タイマーをリセット
            }
        }
        else
        {
            stepTimer = 0f; // 止まった時はすぐリセット
        }
    }
    void PlayFootStep()
    {
        if (FoodStepClips.Length == 0) return;
        // ランダムに足音を選んで再生（自然さを演出）
        int index = Random.Range(0, FoodStepClips.Length);
        audioSource.PlayOneShot(FoodStepClips[index]);
    }
    void HandleHeadBob(Vector3 move)
    {
        //カメラが設定されていない場合は何もしない
        if (cameraTransform == null) return;
        //接地していて、かつ移動中の場合のみ視点揺れを適用
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            //時間に応じて揺れのタイマーを進める
            bobTimer += Time.deltaTime * bobFrequency;
            //サインはを使って揺れのタイマーを進める
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            // 元のカメラ位置に上下方向の揺れを加える
            cameraTransform.localPosition = originalcameraPosition + new Vector3(0f, bobOffset, 0f);
        }
        else
        {
            //移動していないときはタイマーをリセット
            bobTimer = 0f;
            //カメラ位置をもとの位置にスムーズに戻す
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                originalcameraPosition, 
                Time.deltaTime * 5f);//補間速度
        }
    }
}

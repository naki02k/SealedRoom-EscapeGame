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
        
        // �ڒn����
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �y���ڒn����悤��
        }

        // �ړ�����
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        currentMoveInput = new Vector3(x, 0, z);
        
        Vector3 move = transform.right * x + transform.forward * z;
        if (controller.enabled)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }


        // �d�͓K�p
        velocity.y += gravity * Time.deltaTime;
        if (controller.enabled)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        HandleHeadBob(move);
    }
    void FixedUpdate()
    {
        //�ړ����͂����ȏ� & �ڒn���Ă��邩�𔻒�i���s�����ǂ����j
        bool isMoving = currentMoveInput.magnitude > 0.1f && controller.isGrounded;
        if (isMoving)
        {
            //�����̃C���^�[�o�������Z
            stepTimer -= Time.fixedDeltaTime;
            //�C���^�[�o�����[���ɂȂ����瑫�����Đ�
            if (stepTimer <= 0f)
            {
                PlayFootStep();
                stepTimer = stepInterval;//�^�C�}�[�����Z�b�g
            }
        }
        else
        {
            stepTimer = 0f; // �~�܂������͂������Z�b�g
        }
    }
    void PlayFootStep()
    {
        if (FoodStepClips.Length == 0) return;
        // �����_���ɑ�����I��ōĐ��i���R�������o�j
        int index = Random.Range(0, FoodStepClips.Length);
        audioSource.PlayOneShot(FoodStepClips[index]);
    }
    void HandleHeadBob(Vector3 move)
    {
        //�J�������ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (cameraTransform == null) return;
        //�ڒn���Ă��āA���ړ����̏ꍇ�̂ݎ��_�h���K�p
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            //���Ԃɉ����ėh��̃^�C�}�[��i�߂�
            bobTimer += Time.deltaTime * bobFrequency;
            //�T�C���͂��g���ėh��̃^�C�}�[��i�߂�
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            // ���̃J�����ʒu�ɏ㉺�����̗h���������
            cameraTransform.localPosition = originalcameraPosition + new Vector3(0f, bobOffset, 0f);
        }
        else
        {
            //�ړ����Ă��Ȃ��Ƃ��̓^�C�}�[�����Z�b�g
            bobTimer = 0f;
            //�J�����ʒu�����Ƃ̈ʒu�ɃX���[�Y�ɖ߂�
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                originalcameraPosition, 
                Time.deltaTime * 5f);//��ԑ��x
        }
    }
}

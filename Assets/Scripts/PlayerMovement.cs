using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float velocidadeCorrida = 8f;
    [SerializeField] private float forcaPulo = 5f;
    [SerializeField] private float gravidade = -9.81f;

    [Header("Câmera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float sensibilidadeMouse = 2f;
    [SerializeField] private float limiteVerticalCamera = 80f;

    private CharacterController controller;
    private Vector3 velocidadeAtual;
    private float rotacaoVertical = 0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovimentarCamera();
        Movimentar();
    }

    void MovimentarCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse;

        // Gira o corpo do player no eixo horizontal
        transform.Rotate(Vector3.up * mouseX);

        // Gira a câmera no eixo vertical, com limite pra não virar de cabeça pra baixo
        rotacaoVertical -= mouseY;
        rotacaoVertical = Mathf.Clamp(rotacaoVertical, -limiteVerticalCamera, limiteVerticalCamera);
        cameraTransform.localRotation = Quaternion.Euler(rotacaoVertical, 0f, 0f);
    }

    void Movimentar()
    {
        bool noChao = controller.isGrounded;

        if (noChao && velocidadeAtual.y < 0)
        {
            velocidadeAtual.y = -2f; // pequena força pra "colar" no chão
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * h + transform.forward * v;
        float velAtual = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidade;

        controller.Move(direcao * velAtual * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && noChao)
        {
            velocidadeAtual.y = Mathf.Sqrt(forcaPulo * -2f * gravidade);
        }

        velocidadeAtual.y += gravidade * Time.deltaTime;
        controller.Move(velocidadeAtual * Time.deltaTime);
    }
}
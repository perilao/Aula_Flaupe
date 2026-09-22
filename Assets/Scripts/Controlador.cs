using UnityEngine;

public class Controlador : MonoBehaviour, IActivatable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string nomeEstado = "Open";

    private bool open = false;

    void Update()
    {
        // Mantém o controle manual pela tecla E, se ainda quiser testar
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetOpen(!open);
        }
    }

    public interface IActivatable
    {
        void Activate();
        void Deactivate();
    }

    public void Activate()
    {
        SetOpen(true);
    }

    public void Deactivate()
    {
        SetOpen(false);
    }

    private void SetOpen(bool novoEstado)
    {
        if (open == novoEstado) return; // evita repetir Play toda hora enquanto a placa estiver pressionada

        open = novoEstado;
        Debug.Log(open ? "Porta abrindo" : "Porta fechando");
        animator.SetBool("open", open);
    }
}
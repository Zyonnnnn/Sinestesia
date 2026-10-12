using UnityEngine;

public class FreeState : BaseState
{
    LighterBehaviour lighter;
    private StateMachine stateMachine;

    public override void OnStart(GameObject gameObject, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        lighter = gameObject.GetComponent<LighterBehaviour>();

        // Garantir que o objeto f�sico volte a colidir e a ser afetado pela gravidade
        var rb = lighter.gameObject.GetComponent<Rigidbody>();
        var col = lighter.gameObject.GetComponent<Collider>();

        if (col != null)
            col.enabled = true;

        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero; // resetar velocidade antes do impulso
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    public override void OnTick()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}

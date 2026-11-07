using UnityEngine;

public interface IPlayerMovement : IPlayerComponent
{

}

[RequireComponent(typeof(Rigidbody))]
public class PlayerFPSMovementComponent : MonoBehaviour, IPlayerMovement
{
    Player player;
    Rigidbody rb;

    [SerializeField]
    float speedMultuplayer = 10;

    public IPlayerComponent PlayerComponent => this;

    public void Init(Player player)
    {
        this.player = player;

        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "you fucked up (player dont have Rigidbody)");
    }

    void FixedUpdate()
    {
        if (player.InputReader.MoveDelta == Vector2.zero)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 moveForce = (player.PlayerBody.Orientation.forward * player.InputReader.MoveDelta.y + player.PlayerBody.Orientation.right * player.InputReader.MoveDelta.x) * speedMultuplayer;
        rb.AddForce(moveForce);

        //TODO: ADD speed limiter
    }
}

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
    [SerializeField]
    float walkSpeedLimit = 2;
    [SerializeField]
    float sprintSpeedLimit = 5;

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

        float limiter = walkSpeedLimit;

        if (player.InputReader.IsSprinting) limiter = sprintSpeedLimit;

        Vector3 velocity = rb.linearVelocity;
        Vector3 flatVelocity = new Vector3(velocity.x, 0, velocity.z);

        if(flatVelocity.magnitude > limiter)
        {
            Vector3 limitedFlatVelocity = flatVelocity.normalized * limiter;
            Vector3 limitedVelocity = new Vector3(limitedFlatVelocity.x, velocity.y, limitedFlatVelocity.z);

            rb.linearVelocity = limitedVelocity;
        }

        Debug.Log(velocity.magnitude);
    }
}

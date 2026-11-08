using UnityEngine;

public interface IPlayerMovement : IPlayerComponent
{

}

[RequireComponent(typeof(Rigidbody))]
public class PlayerFPSMovementComponent : MonoBehaviour, IPlayerMovement
{
    Player player;
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField]
    float speedMultuplayer = 10.0f;
    [SerializeField]
    float walkSpeedLimit = 2.0f;
    [SerializeField]
    float sprintSpeedLimit = 5.0f;

    [Header("Camera")]

    [SerializeField]
    float sensitivityX = 0.2f;
    [SerializeField]
    float sensitivityY = 0.2f;
    [SerializeField]
    float maxXAngle = 90;
    [SerializeField]
    float minXAngle = -90;

    public IPlayerComponent PlayerComponent => this;

    float cameraX;
    float cameraY;

    public void Init(Player player)
    {
        this.player = player;

        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "you fucked up (player dont have Rigidbody)");
    }
  
    void FixedUpdate()
    {
        #region Camera

        cameraX -= player.InputReader.CursorDelta.y * sensitivityY;
        cameraY += player.InputReader.CursorDelta.x * sensitivityX;

        cameraX = Mathf.Clamp(cameraX, minXAngle, maxXAngle);

        player.PlayerBody.CameraTransform.localRotation = Quaternion.Euler(cameraX, cameraY, 0);


        #endregion

        #region Movement
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
        #endregion
    }
}

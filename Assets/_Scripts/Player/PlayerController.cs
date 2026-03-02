using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	Vector2 movement;
	Rigidbody playerRb;

	[SerializeField] int moveSpeed = 2;

	[SerializeField] float xClamp = 3f;
	[SerializeField] float zClamp = 3f;

	private void Awake()
	{
		playerRb = GetComponent<Rigidbody>();
	}
	public void Move(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
		Debug.Log(movement);
	}

	private void FixedUpdate()
	{
		MovePlayer();

	}

	private void MovePlayer()
	{
		Vector3 currentPos = transform.position;
		Vector3 movementDir = new Vector3(movement.x, 0f, movement.y);
		Vector3 newPos = currentPos + movementDir * (moveSpeed * Time.fixedDeltaTime);

		newPos.x = Mathf.Clamp(newPos.x, -xClamp, xClamp);
		newPos.z = Mathf.Clamp(newPos.z, -zClamp, zClamp);
		playerRb.MovePosition(newPos);
	}
}

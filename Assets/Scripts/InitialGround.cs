using UnityEngine;

public class InitialGround : MonoBehaviour
{
    [SerializeField] private Vector3 snapPos;

    private void Update()
    {
        transform.Translate(MinotaurMovement.singleton.currentSpeed * Time.deltaTime * Vector2.left);

        if (transform.position.x <= snapPos.x)
        {
            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}

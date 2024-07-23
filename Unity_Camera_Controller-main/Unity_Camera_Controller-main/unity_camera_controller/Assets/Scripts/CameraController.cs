using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minY = 65f;
    public float maxY = 540f;
    public float inertiaDuration = 0.5f;
    public float moveSpeed = 0.65f;
    public float deceleration = 0.95f;

    private Vector3 dragOrigin;
    private Vector3 velocity;
    private float timeSinceLastDrag;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector2)Input.GetTouch(0).position;
            dragOrigin = Camera.main.ScreenToWorldPoint(inputPosition);
            velocity = Vector3.zero; //reset speed 
            timeSinceLastDrag = 0f; //reduce time
        }

        //move
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 inputPosition = Input.GetMouseButton(0) ? Input.mousePosition : (Vector2)Input.GetTouch(0).position;
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(inputPosition);
            pos.y += difference.y * moveSpeed;
            velocity = difference / Time.deltaTime * moveSpeed;
            dragOrigin = Camera.main.ScreenToWorldPoint(inputPosition);
            timeSinceLastDrag = 0f; //reduce time
        }
        else if (timeSinceLastDrag < inertiaDuration)
        {
            //momentum effect
            pos.y += velocity.y * Time.deltaTime;
            velocity *= deceleration; //reduce speed
            timeSinceLastDrag += Time.deltaTime;
        }
        else
        {
            velocity = Vector3.zero; //reset
        }

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}

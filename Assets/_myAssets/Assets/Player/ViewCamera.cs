using UnityEngine;
[ExecuteAlways]

public class ViewCamera : MonoBehaviour
{
    [SerializeField] private Transform pitchTransform;
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float armLengeth = 7;
    [SerializeField] public float cameraTurnSpeed = 30f;

    private Transform _parentTransform;

    public Camera GetViewCamera()
    {
        return viewCamera;
    }

    Vector3 GetViewRightDir()
    {
        return viewCamera.transform.right;
    }

    Vector3 GetViewUpDir()
    {
        return Vector3.Cross(GetViewRightDir(), Vector3.up);
    }

    public Vector3 InputToWorldDir(Vector2 input)
    {
        return GetViewRightDir() * input.x + GetViewUpDir() * input.y;
    }

    public void SetFollowParent(Transform parentTransform)
    {
        _parentTransform = parentTransform;
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * cameraTurnSpeed);

    }
    void Update()
    {
        viewCamera.transform.position = pitchTransform.position - viewCamera.transform.forward * armLengeth;
    }

    private void LateUpdate()
    {
        if (_parentTransform != null)
        {
            transform.position = _parentTransform.position;
        }
    }
}

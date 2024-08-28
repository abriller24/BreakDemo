using UnityEngine;
[ExecuteAlways]

public class ViewCamera : MonoBehaviour
{
    [SerializeField] private Transform pitchTransform;
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float armLengeth = 7;

    public Transform _parentTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        viewCamera.transform.position = pitchTransform.position - viewCamera.transform.forward * armLengeth;
    }
    public void SetFollowParent(Transform parentTransform)
    {
        _parentTransform = parentTransform;
    }

    private void LateUpdate()
    {
        if (_parentTransform != null)
        {
            transform.position = _parentTransform.position;
        }
    }
}

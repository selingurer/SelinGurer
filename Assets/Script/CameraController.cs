
using Base;
using UnityEngine;

public class CameraController : MyObject
{
    private static CameraController instance;
    public static CameraController Instance { get { return instance; } }
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;
    float z = 0.0f;

    public bool camTransformChanged;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && camTransformChanged)
        {
            hit_position = Input.mousePosition;
            camera_position = transform.position;

        }
        if (Input.GetMouseButton(0) && camTransformChanged)
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
        }
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    void LeftMouseDrag()
    {

        current_position.z = hit_position.z = camera_position.y;

        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

        direction = direction * -1;

        Vector3 position = camera_position + direction;

        transform.position = new Vector3(Mathf.Clamp(position.x,4.5f,UserManager.Instance.GridCount/PanelHome.Instance.line*2), Mathf.Clamp(position.y, -PanelHome.Instance.line, -2f), position.z);
    }
}

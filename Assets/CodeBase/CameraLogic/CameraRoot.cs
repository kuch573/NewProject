using UnityEngine;

public class CameraRoot : MonoBehaviour
{

    [SerializeField] private Transform target;
    private Vector3 offset;
    private float mouse_sens = 1f;
    private float view = 1.5f;
    public Camera cam_holder;
    private float _rotationY, _rotationX;

    void Start()
    {
        
        var transform1 = transform;
        var eulerAngles = transform1.eulerAngles;
        _rotationY = eulerAngles.y;
        _rotationX = eulerAngles.x;
        offset = target.position - transform1.position;
    }

    void LookAtTarget()
    {
        
        Quaternion rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
        transform.position = target.position - (rotation * offset * view);
        transform.LookAt(target);
    }

    void Update()
    {
        if (Input.GetButtonDown("View"))
        {
            if (view == 0.75f)
            {
                view = 1.5f;
            }
            else
            {
                view = view * 0.5f;
            }
        }

        _rotationX += Input.GetAxis("Mouse X") * mouse_sens;
            _rotationY -= Input.GetAxis("Mouse Y") * mouse_sens;

            LookAtTarget();
    }
    
}

using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Speed = 10f;
    public Transform Target;			// target to follow (cam is fixed to following this around till it is NULL)
    public bool FollowTarget = false;	// follow the target? (only if target is not NULL)
    public bool AllowInput = true;		// the cam wont read keyinput if set to false
    public Transform CamTr;
    public Vector2 MinXz;
    public Vector2 MaxXz;
    private Transform _tr;

    public delegate void CamMaunallyMoved();
    public CamMaunallyMoved OnCamManuallyMoved = null;

    private bool _moved;// helper

    public bool AllowLimitMove = false;
    public bool AllowSpeed = false;

    void Start()
    {
        _tr = transform;
        if (Target && FollowTarget) _tr.position = Target.position;
    }

    void Update()
    {
        if (!Input.anyKey || !AllowInput) return;
        _moved = false;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) { _moved = true; Translate(Vector3.forward * Time.deltaTime * Speed); }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { _moved = true; Translate(Vector3.back * Time.deltaTime * Speed); }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { _moved = true; Translate(Vector3.left * Time.deltaTime * Speed); }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { _moved = true; Translate(Vector3.right * Time.deltaTime * Speed); }

        if (OnCamManuallyMoved != null && _moved)
        {
            var pos = _tr.position;
            if (pos.x < MinXz.x) pos.x = MinXz.x;
            if (pos.x > MaxXz.x) pos.x = MaxXz.x;
            if (pos.z < MinXz.y) pos.z = MinXz.y;
            if (pos.z > MaxXz.y) pos.z = MaxXz.y;
            _tr.position = pos;

            OnCamManuallyMoved(); // call callback
        }
    }

    void LateUpdate()
    {
        if (!Target || !FollowTarget) return;
        var difference = Target.position - _tr.position;
        _tr.position = AllowSpeed ? Vector3.Slerp(_tr.position, Target.position, Speed * Time.deltaTime * Mathf.Clamp(difference.magnitude, 0f, 2f)) : Vector3.Slerp(_tr.position, Target.position, Time.deltaTime * Mathf.Clamp(difference.magnitude, 0f, 2f));

        if (!AllowLimitMove) return;

        var pos = _tr.position;
        if (pos.x < MinXz.x) pos.x = MinXz.x;
        if (pos.x > MaxXz.x) pos.x = MaxXz.x;
        if (pos.z < MinXz.y) pos.z = MinXz.y;
        if (pos.z > MaxXz.y) pos.z = MaxXz.y;
        _tr.position = pos;
    }

    private void Translate(Vector3 pos)
    {
        FollowTarget = false; // stop follow mode if manually moved

        // if SHIFT is held, move at double speed
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) pos *= 2.5f;

        // apply
        var r = CamTr.eulerAngles;
        r.x = 0; _tr.position += Quaternion.Euler(r) * pos;
    }

    public void Follow(bool doFollowCurrentTarget)
    {
        FollowTarget = doFollowCurrentTarget;
    }

    public void Follow(Transform t)
    {
        Target = t;
        FollowTarget = true;
    }

    // ====================================================================================================================
}

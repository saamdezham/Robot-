using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 startPos_cylinder;
    public Transform transform_cylinder;

    private void Awake() {
        startPos_cylinder = transform_cylinder.position;
    }


    // Update is called once per frame
    void Update()
    {
        MoveLeftRight();
        MoveForwardBack();
        MoveRotate();
    }

    void MoveLeftRight(){
        Vector3 vec_left = Vector3.zero;
        vec_left.x = Input.GetAxis("Horizontal");
        Vector3 v = new Vector3(vec_left.x, 0.0f, 0.0f) * Time.deltaTime * 5.0f;
        transform_cylinder.Translate(v, Space.Self); 
    }

    void MoveForwardBack(){
        Vector3 vec_forward = Vector3.zero;
        vec_forward.z = Input.GetAxis("Vertical");
        Vector3 v = new Vector3(0.0f, 0.0f, vec_forward.z) * Time.deltaTime * 5.0f;
        transform_cylinder.Translate(v, Space.Self); 
    }

    void MoveRotate(){
        Vector3 vec_rotate = Vector3.zero;
        vec_rotate.y = Input.GetAxis("Rotate");
        Vector3 v = new Vector3(0.0f, vec_rotate.y, 0.0f) * Time.deltaTime * 5.0f;
        transform_cylinder.Rotate(v, Space.Self); 
    }
}

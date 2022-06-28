using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _speed = 0.5f;

    private float _leftBound = -5f;
    private float _rightBound = 5f;
    private float _rearBound = -8f;
    private float _forwardBound = 0f;

    private Transform _transform;
    private Touch _touch;


    private void Start() => _transform = GetComponent<Transform>();

    private void LateUpdate()
    {
        if (Input.touchCount == 2)
        {          
             _touch = Input.GetTouch(0);

             if (_touch.phase == TouchPhase.Moved)
                  Move();                    
        }
    }

    private void Move() 
    {
        _transform.Translate
            (new Vector3(-_touch.deltaPosition.x, 0, -_touch.deltaPosition.y) * _speed * Time.deltaTime, Space.World);

        if (_transform.position.x < _leftBound)
           RestrictMovement(_leftBound, _transform.position.z);

        if (_transform.position.x > _rightBound)
            RestrictMovement(_rightBound, _transform.position.z);

        if (_transform.position.z < _rearBound)
            RestrictMovement(_transform.position.x, _rearBound);

        if (_transform.position.z > _forwardBound)
            RestrictMovement(_transform.position.x, _forwardBound);
    } 

    private void RestrictMovement(float xPos, float zPos) =>
        _transform.position = new Vector3(xPos, _transform.position.y, zPos);
}

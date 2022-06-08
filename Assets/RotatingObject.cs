using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private Transform _loadingImageTransform;
    [SerializeField] private Vector3 _rotationAngle;

    private void Update()
    {
        _loadingImageTransform.localRotation *= Quaternion.Euler(_rotationAngle * Time.deltaTime);
    }
}

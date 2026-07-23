using UnityEngine;

public class CameraFollow_Script : MonoBehaviour
{
    [SerializeField] Transform Cameras;
    [SerializeField] Transform targetOBJ;

    [Range(0f, 1f)]
    [SerializeField] float Suavidade;

    void FixedUpdate()
    {
        if(targetOBJ != null)
        {
            Cameras.position = Vector3.Slerp(Cameras.position, targetOBJ.position, Suavidade);
        }
    }
}

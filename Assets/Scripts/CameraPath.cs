using UnityEngine.CustomComponents;
using UnityEngine;

public class CameraPath : Model
{
    [SerializeField]
    private Vector3 center;
    [SerializeField]
    private float size;
    [SerializeField]
    private bool isTesting;
    #region Properties
    public Vector3 Center { get => center + transform.position; }
    public float Size { get => size; }
    #endregion
    public new virtual CameraPathPackage Pack()
    {
        CameraPathPackage result = new CameraPathPackage(poolName, gameObject.name, transform.position, transform.rotation, center, size);
        return result;
    }
    public virtual void Unpack(CameraPathPackage package, ObjectPool pool)
    {
        transform.position = package.Position;
        transform.rotation = package.Rotation;
        name = package.Name;

        center = package.Center;
        size = package.Size;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(isTesting)
        {
            Camera.main.transform.position = new Vector3(Center.x, Center.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize = size;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)Center, new Vector3(Camera.main.aspect * size * 2, size * 2));
    }
#endif
}

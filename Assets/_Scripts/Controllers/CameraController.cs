using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance { get { return instance; } }

    private Camera mainCamera;

    [SerializeField] private float[] bounds = new float[4]; //Left, Right, Bottom, Top

    private GameObject target;

    private PixelPerfectCamera ppCamera;
    private int zoomAmountX = 64;
    private int zoomAmountY = 36;
    private float width
    {
        get { return mainCamera.orthographicSize * mainCamera.aspect * 2; }
    }
    private float height
    {
        get { return mainCamera.orthographicSize * 2; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        ppCamera = GetComponent<PixelPerfectCamera>();
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ZoomIn();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ZoomOut();
        }*/
    }
    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.transform.position.ToVector2().ToVector3(transform.position.z);
        }

        BoundsUpdate();
    }
    private void ZoomIn()
    {
        ppCamera.refResolutionX -= zoomAmountX;
        ppCamera.refResolutionY -= zoomAmountY;
    }
    private void ZoomOut()
    {
        ppCamera.refResolutionX += zoomAmountX;
        ppCamera.refResolutionY += zoomAmountY;
    }
    private void BoundsUpdate()
    {
        if (transform.position.x - width / 2 < bounds[0])
        {
            transform.SetPosX(bounds[0] + width / 2);
        }
        else if (transform.position.x + width / 2 > bounds[1])
        {
            transform.SetPosX(bounds[1] - width / 2);
        }
        if (transform.position.y - height / 2 < bounds[2])
        {
            transform.SetPosY(bounds[2] + height / 2);
        }
        else if (transform.position.y + height / 2 > bounds[3])
        {
            transform.SetPosY(bounds[3] - height / 2);
        }
    }
    public void SetBounds(float[] newBounds)
    {
        bounds = newBounds;
    }
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}

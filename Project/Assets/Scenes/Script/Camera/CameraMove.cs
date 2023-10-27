using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float xDefaultRotation = 5f;
    public float xRoation;


    public float yCameraPosition;
    private float yRotation;
    private float cameraMoveFrontBackSpeed;

    public Transform playerTransform;
    private float distanceFromPlayer = 10f;
    private float xRotationDuration = 10f;
    //private float rotationSpeed = 100f;

    private float maxAlpha = 1f;
    private float alphavalue = 0.3f;

    public float DistanceFromPlayer
    {
        get { return distanceFromPlayer; }
        set { distanceFromPlayer = value; }
    }

    public float CameraMoveSpeed
    {
        get { return cameraMoveFrontBackSpeed; }
        set { cameraMoveFrontBackSpeed = value; }
    }
    public float YCameraPosition
    {
        set { yCameraPosition = value; }
    }
    public float XRotation
    {
        get; set;
    }
    private void Awake()
    {
        distanceFromPlayer = OnGameData.instance.CameraDistance;
    }
    private void FixedUpdate()
    {
        yRotation = playerTransform.eulerAngles.y;
        if (xRoation > xRotationDuration)
            xRoation = xRotationDuration;
        else if (xRoation < -xRotationDuration)
            xRoation = -xRotationDuration;

        transform.rotation = Quaternion.Euler(xDefaultRotation + xRoation, yRotation, 0);

        BlurringObjects();
    }
    public void SyncWithPlayer(Vector3 playerDirection)
    {
        Vector3 offset = -playerTransform.forward * distanceFromPlayer;
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.y = yCameraPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraMoveFrontBackSpeed * Time.deltaTime);

    }
    private Dictionary<Renderer, float> originalAlphas = new Dictionary<Renderer, float>();
    private void BlurringObjects()
    {
        var test = playerTransform.position;
        test.y += 0.25f;
        Vector3 toPlayer = test - transform.position;
        RaycastHit[] hits;
        
        hits = Physics.RaycastAll(transform.position, toPlayer.normalized, toPlayer.magnitude);
        HashSet<Renderer> hitThisFrame = new HashSet<Renderer>();

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            hitThisFrame.Add(rend);

            if (rend)
            {
                if (!originalAlphas.ContainsKey(rend))
                {
                    originalAlphas[rend] = rend.material.color.a;
                }

                Material mat = rend.material;
                Color color = mat.color;
                color.a = alphavalue;
                mat.color = color;
            }
        }

        foreach (var rend in originalAlphas.Keys.ToList())
        {
            if (!hitThisFrame.Contains(rend))
            {
                Material mat = rend.material;
                Color color = mat.color;
                color.a = originalAlphas[rend];
                mat.color = color;
                originalAlphas.Remove(rend);
            }
        }
    }
}


//void LateUpdate()
//{
//    Vector3 direction = (playerTransform.position - transform.position).normalized;
//    RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
//                        1 << LayerMask.NameToLayer("EnvironmentObject"));

//    for (int i = 0; i < hits.Length; i++)
//    {
//        TransObjectTest[] obj = hits[i].transform.GetComponentsInChildren<TransObjectTest>();

//        for (int j = 0; j < obj.Length; j++)
//        {
//            obj[j]?.BecomeTransparent();
//        }
//    }
//}
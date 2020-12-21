using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    private ARSessionOrigin arOrigin;
    public GameObject placeablePrefab;
    
    public GameObject Pointer; //yeni

  
    private GameObject spawnedObject;
    private List<GameObject> placedPrefabList = new List<GameObject>();
    
    [SerializeField]
    private int max = 0;
    private int placedPrefabCount;

    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()//yeni
    {
       

    }

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase==TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
   
    void Update()
    {
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (placedPrefabCount < max)
            {
                SpawnPrefab(hitPose);
            }

        }
    }
    public void SetPrefabType(GameObject prefabType)
    {
        placeablePrefab = prefabType;
    }

    public void DestroyGameObject()//yeni
    {
        Destroy(spawnedObject);
    }
    private void SpawnPrefab(Pose hitPose)
    {
        spawnedObject = Instantiate(placeablePrefab, hitPose.position, hitPose.rotation);
        placedPrefabList.Add(spawnedObject);
        placedPrefabCount++;
        
    }
}
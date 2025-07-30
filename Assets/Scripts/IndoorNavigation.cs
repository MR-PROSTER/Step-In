// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.AI;

// public class IndoorNavigation : MonoBehaviour
// {
//     [SerializeField] private Transform player;
//     [SerializeField] private ARTrackedImageManager trackedImageManager;
//     [SerializeField] private GameObject trackedImagePrefab;

//     private NavMeshPath navMeshPath;
//     private LineRenderer lineRenderer;

//     void Start()
//     {
//         navMeshPath = new NavMeshPath();
//         lineRenderer = GetComponent<LineRenderer>();
//         Screen.sleepTimeout = SleepTimeout.NeverSleep;
//     }

//     void OnEnable()
//     {
//         trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
//     }

//     void OnDisable()
//     {
//         trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
//     }

//     void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
//     {
//         foreach (var trackedImage in eventArgs.added)
//         {
//             // Handle new QR code detection
//             UpdateTrackingPosition(trackedImage);
//         }
//     }

//     void UpdateTrackingPosition(ARTrackedImage trackedImage)
//     {
//         // Basic positioning logic - will be expanded
//         transform.position = trackedImage.transform.position;
//         transform.rotation = trackedImage.transform.rotation;
//     }
// }



// Open AI Code

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.AI;

public class IndoorNavigation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject trackedImagePrefab;
    [SerializeField] private Transform destinationTarget;
    [SerializeField] private Transform navigationTarget;

    private NavMeshPath navMeshPath;
    private LineRenderer lineRenderer;

    void Start()
    {
        navMeshPath = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateTrackingPosition(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateTrackingPosition(trackedImage);
        }
    }

    void UpdateTrackingPosition(ARTrackedImage trackedImage)
    {
        navigationTarget.position = trackedImage.transform.position;
        navigationTarget.rotation = trackedImage.transform.rotation;
    }

    void Update()
    {
        if (player == null || navigationTarget == null) return;

        if (NavMesh.CalculatePath(player.position, navigationTarget.position, NavMesh.AllAreas, navMeshPath))
        {
            lineRenderer.positionCount = navMeshPath.corners.Length;
            lineRenderer.SetPositions(navMeshPath.corners);
        }
        else
        {
            Debug.LogError("Failed to calculate path...");
        }
    }
}
}

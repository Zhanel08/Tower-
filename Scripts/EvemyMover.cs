using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EvemyMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private List<Node> path = new List<Node>();
    private WaitForEndOfFrame _pathWaitTime;
    private Enemy _enemy;
    private GridManager gridManager;
    private Pathfinder pathfinder;

    private void Awake()
    {
        _pathWaitTime = new WaitForEndOfFrame();
        _enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath();
        StartCoroutine(FollowPath());
    }


    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartingCoordinates);
    }

    private void RecalculatePath()
    {
        path.Clear();
        
        path = pathfinder.GetNewPath();
    }

    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {

        for (int index = 1; index < path.Count; index++)
        {
            var waypoint = path[index];
            Vector3 startPos = transform.position;
            Vector3 finalPos = gridManager.GetPositionFromCoordinates(waypoint.coordinates);
            float travelPersent = 0f;

            transform.LookAt(finalPos);
            while (travelPersent < 1f)
            {
                travelPersent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, finalPos, travelPersent);
                yield return _pathWaitTime;
            }
        }

        FinishPath();
    }
}

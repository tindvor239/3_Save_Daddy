using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    [SerializeField]
    private List<EnemyController> enemies = new List<EnemyController>();
    private List<Transform> passedDestinations = new List<Transform>();

    [SerializeField]
    protected GameState gameState;

    private CharacterController player;
    private static Vector3 from, todir;
    #region Properties
    public List<Transform> Destinations { get => destinations; }
    public List<Transform> PassedDestinations { get => passedDestinations; }
    public List<EnemyController> Enemies { get => enemies; }
    #endregion
    protected override void Awake()
    {
        #region Singleton
        base.Awake();
        #endregion
    }
    private void Start()
    {
        player = GameController.PlayerController;
    }
    // Update is called once per frame
    private void Update()
    {
        Debug.DrawRay(from, todir);
    }
    #region Raycasting
    public static GameObject RayCastObject(Vector3 fromPosition, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public static GameObject RayCastObject(Vector3 fromPosition, Vector3 direction, LayerMask ignoreLayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction, Mathf.Infinity, ignoreLayer);
        if (hit.collider != null)
        {
            Debug.Log("Hit");
            return hit.collider.gameObject;
        }
        return null;
    }
    public static GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = GameController.GetDirectionVector(fromPosition, toPosition);
        GameObject destinateObject = RayCastObject(fromPosition, direction);
        return destinateObject;
    }
    public static GameObject RayCastToObject(Vector3 fromPosition, Vector3 toPosition, LayerMask ignoreLayer)
    {
        Vector3 direction = GameController.GetDirectionVector(fromPosition, toPosition);
        from = fromPosition;
        todir = direction;
        GameObject destinateObject = RayCastObject(fromPosition, direction, ignoreLayer);
        return destinateObject;
    }
    #endregion
    #region Linecasting
    public static bool IsBlocked(Vector3 fromPosition, Vector3 toPosition)
    {
        RaycastHit2D hit = Physics2D.Linecast(fromPosition, toPosition);
        if (hit.collider != null)
        {
            if (hit.collider.transform.position == toPosition)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    public static bool IsBlocked(Vector3 fromPosition, Vector3 toPosition, int layerIndex)
    {
        RaycastHit2D hit = Physics2D.Linecast(fromPosition, toPosition, layerIndex);
        if (hit.collider != null)
        {
            if(hit.collider.transform.position == toPosition)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    #endregion
    #region Path Handle
    public bool CheckPlayerInPath()
    {
        foreach(Transform transform in destinations)
        {
            if(transform.GetComponent<CharacterController>())
            {
                return true;
            }
        }
        return false;
    }
    public static int GetDestinationIndex(Transform transform)
    {
        for (int i = 0; i < Instance.destinations.Count; i++)
        {
            if (Instance.destinations[i] == transform)
            {
                return i;
            }
        }
        return -1;
    }
    public static int GetNextDestinationIndex(CharacterController controller)
    {
        int index = GetDestinationIndex(controller.transform);
        controller.GetComponent<Collider2D>().enabled = false;
        if (index != -1 && index + 1 < Instance.destinations.Count)
        {
            for (int i = index + 1; i < Instance.destinations.Count; i++)
            {
                bool isBlocked = IsBlocked(Instance.destinations[index].position, Instance.destinations[i].position);
                if (!isBlocked)
                {
                    controller.GetComponent<Collider2D>().enabled = true;
                    return i;
                }
            }
            controller.GetComponent<Collider2D>().enabled = true;
            return -1;
        }
        else
        {
            controller.GetComponent<Collider2D>().enabled = true;
            return 0;
        }
    }
    public static EnemyController GetClosestEnemy()
    {
        EnemyController closestEnemy = null;
        if(Instance.enemies.Count != 0 && Instance.enemies[0] != null)
        {
            float firstDistance = Vector2.Distance(Instance.player.transform.position, Instance.enemies[0].transform.position);
            foreach(EnemyController enemy in Instance.enemies)
            {
                float distance = Vector2.Distance(Instance.player.transform.position, enemy.transform.position);
                if (distance <= firstDistance)
                {
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }
    #endregion
    #region State Handle
    public static bool isWin()
    {
        if(Instance.destinations[Instance.destinations.Count - 1].GetComponent<CharacterController>() != null)
        {
            Instance.gameState = Win();
            return true;
        }
        return false;
    }
    private static GameState Win()
    {
        //TO DO: Show win menu.
        return GameState.win;
    }
    #endregion
    public enum GameState {start, play, pause, win, gameover }
}

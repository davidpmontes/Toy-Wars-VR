using System.Collections.Generic;
using UnityEngine;

public enum PATHS
{
    PATROL1
}

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }
    private Dictionary<PATHS, List<Vector3>> paths;


    [SerializeField] private GameObject[] wp = default;

    private void Awake()
    {
        Instance = this;
        paths = new Dictionary<PATHS, List<Vector3>>();

        paths.Add(PATHS.PATROL1, new List<Vector3>(new Vector3[] { wp[0].transform.position, wp[1].transform.position, wp[2].transform.position }));
    }

    public List<Vector3> GetPath(PATHS value)
    {
        return paths[value];
    }
}

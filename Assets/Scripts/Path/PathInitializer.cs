using UnityEngine;

public class PathInitializer : MonoBehaviour
{
    [SerializeField] GameObject gameObjectWithPathSource;
    [SerializeField] MoveByPath moveByPath;

    void Start()
    {
        if (moveByPath != null) 
        {
            if(gameObjectWithPathSource.TryGetComponent<IPathSource>(out var pathSource))
                moveByPath.SetPath(pathSource.GetPath());
        }
    }
}

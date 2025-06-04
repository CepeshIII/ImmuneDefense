using UnityEngine;

public class PathInitializer : MonoBehaviour
{
    [SerializeField] GameObject gameObjectWithPathSource;

    void Start()
    {
        if (TryGetComponent<IMoveByPath>(out var moveByPath))
        {
            if (moveByPath != null) 
            {
                if(gameObjectWithPathSource.TryGetComponent<IPathSource>(out var pathSource))
                    moveByPath.SetPath(pathSource.GetPath());
            }
        }
    }
}

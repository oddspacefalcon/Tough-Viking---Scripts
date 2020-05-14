using UnityEngine;

public class FromWorldPointTextPositioner : IFloatingTextPositioner
{
    private readonly Camera _camera;
    private readonly Vector3 _worldPosition;
    private readonly float _speed;
    private float _timeToLive;

    private float _yOffset; 


    // konstruktorn nedan parametrarn nedan tas in en position från leveln, antalet sekunder texten skall visas, samt speeden som texten ska åka upp
    public FromWorldPointTextPositioner(Camera camera, Vector3 worlPosition, float timeToLive, float speed)
    {
        
        _camera = camera;
        _worldPosition = worlPosition;
        _timeToLive = timeToLive;
        _speed = speed;
    }

    public bool GetPosition(ref Vector2 position, GUIContent content, Vector2 size)
    {
        // var frame drar vi bort time.deltatime från timetoLive och om detta blir mindre än 0 indikerar det till vår OnGUI metod att dags att destroy
        if ((_timeToLive -= Time.deltaTime) <= 0)
            return false;

        var screenPosition = _camera.WorldToScreenPoint(_worldPosition);
        position.x = screenPosition.x - (size.x / 2); 
        position.y = Screen.height - screenPosition.y - _yOffset;

        _yOffset += Time.deltaTime * _speed;
        return true;

    }


}



using UnityEngine;
// scrollningseffekt i bakgrunden 
public class BackgroundParallax : MonoBehaviour {

    public Transform[] Backgrounds;
    public float ParallaxScale;
    public float ParallaxReductionFactor;
    public float Smoothing;

    private Vector3 _lastPosition;

    public void Start()
    {
        _lastPosition = transform.position;
    }

    public void Update()
    {
        var parallax = (_lastPosition.x - transform.position.x) * ParallaxScale;

        // for loop för att få index variabeln för att få reda på hur mycket parallax effekten ska minskas beroende på hur långt bak i arrayen vi är.
        for (var i = 0; i < Backgrounds.Length; i++)
        {
            var backgroundTargetPostion = Backgrounds[i].position.x - parallax * (i * ParallaxReductionFactor + 1);
            Backgrounds[i].position = Vector3.Lerp(
                Backgrounds[i].position, // from parameter
               new Vector3(backgroundTargetPostion, Backgrounds[i].position.y, Backgrounds[i].position.z), // to 
               Smoothing * Time.deltaTime);
        }

        _lastPosition = transform.position;

    }
}

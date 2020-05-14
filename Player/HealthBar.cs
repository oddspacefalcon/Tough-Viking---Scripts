using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public Player Player; 
    public Transform ForegroundSprite; // foreground spriten ska bli mindre och mindre för att visa healthbaren 
    public SpriteRenderer ForegroundRenderer; // kunna ändra färgen på healthbaren när man tapper liv
    public Color MaxHealthColor = new Color(255 /255f, 63/255f, 63 / 255f); // konverterar rgb colorspace till ett colorspace som unity förstår
    public Color MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

    public void Update()
    {
        var healthPercent = Player.Health / (float) Player.MaxHealth; //float division resulterar i värde mellan 0-1
        ForegroundSprite.localScale = new Vector3(healthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, healthPercent);

    }
}

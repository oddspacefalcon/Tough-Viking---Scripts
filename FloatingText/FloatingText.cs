using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    private static readonly GUISkin Skin = Resources.Load<GUISkin>("GameSkin"); // vill kunna adera en ny stil genom att lägga till i GameSkin's customstyle
    
    public static FloatingText Show(string text, string style, IFloatingTextPositioner positioner) // sätter in text osv som ska visas
    {
        var go = new GameObject("Floating Text"); // Show metoden börjar intantiera ett nytt gameobject
        var floatingText = go.AddComponent<FloatingText>(); // intantierar denna komponenten på go objektet
        floatingText.Style = Skin.GetStyle(style);
        floatingText._positioner = positioner;
        floatingText._content = new GUIContent(text);
        return floatingText;

    }

    private GUIContent _content;
    private IFloatingTextPositioner _positioner;

    public string Text { get { return _content.text; } set { _content.text = value; } }
    public GUIStyle Style { get; set; }


    // var vill vi positionera vår text denna frame? a) returnera positionen vart vi vill ha texten. b) return false och indikera att vi ej vill ha floating texten aktiv
    public void OnGUI()
    {
        int _size = 5;

        var position = new Vector2();
        var contentSize = Style.CalcSize(_content); // storleken på content ( i detta fall texten) 
        if (!_positioner.GetPosition(ref position, _content, contentSize)) // få positionen men om positionen returnerar falskt förstör texten annars kan vi använda det i en GUI:label position (nedan)
        {
            Destroy(gameObject);
            return;
        }

        GUI.Label(new Rect(position.x, position.y, _size * contentSize.x, _size * contentSize.y), _content, Style);
        
    }

}

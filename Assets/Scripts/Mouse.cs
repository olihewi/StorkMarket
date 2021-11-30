using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mouse : MonoBehaviour
{
    public static Mouse INSTANCE;
    public Sprite openSprite;
    public Sprite grabbingSprite;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    void Start()
    {
        INSTANCE = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        Release();
    }

    public void Grab()
    {
        spriteRenderer.sprite = grabbingSprite;
    }

    public void Release()
    {
        spriteRenderer.sprite = openSprite;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x,transform.position.y,0.0F);
        Vector3 screenPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        bool mouseOffScreen = screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1;
        Cursor.visible = !mouseOffScreen;
    }
}

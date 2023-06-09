using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootItem : MonoBehaviour
{
    private buff attachedBuff;

    public Sprite spriteSpeed;
    public Sprite spriteMaxSpeed;
    public Sprite spriteJump;
    public Sprite spriteDash;
    public Sprite spriteDamage;
    public Sprite spriteAttackSpeed;
    public Sprite spriteHealth;

    void Start()
    {
        var playerSprite = GetComponent<SpriteRenderer>();
        switch (attachedBuff.type)
        {
            case buffTypes.speed:
                playerSprite.sprite = spriteSpeed;
                break;
            case buffTypes.maxSpeed:
                playerSprite.sprite = spriteMaxSpeed;
                break;
            case buffTypes.jump:
                playerSprite.sprite = spriteJump;
                break;
            case buffTypes.dash:
                playerSprite.sprite = spriteDash;
                break;
            case buffTypes.damage:
                playerSprite.sprite = spriteDamage;
                break;
            case buffTypes.attackSpeed:
                playerSprite.sprite = spriteAttackSpeed;
                break;
            case buffTypes.health:
                playerSprite.sprite = spriteHealth;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            var player = other.gameObject.GetComponent<playerController>();
            player.addBuff(attachedBuff);
            Destroy(this.gameObject);
        }
    }
}

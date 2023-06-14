using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootItem : MonoBehaviour
{
    [SerializeField]
    private buff attachedBuff;

    [SerializeField]
    public Sprite spriteSpeed;
    [SerializeField]
    public Sprite spriteMaxSpeed;
    [SerializeField]
    public Sprite spriteJump;
    [SerializeField]
    public Sprite spriteDash;
    [SerializeField]
    public Sprite spriteDamage;
    [SerializeField]
    public Sprite spriteAttackSpeed;
    [SerializeField]
    public Sprite spriteHealth;

    public void StartAndAttach(buff buffToAttach)
    {
        attachedBuff = buffToAttach;

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

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,1) * 1f, 1f), ForceMode2D.Impulse);

        StartCoroutine("destroyAfterTime", 10f);
    }

    IEnumerator destroyAfterTime(float time){
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerPickup"){
            Debug.Log("Player picked up a buff of: " + attachedBuff.type.ToString());
            var player = other.transform.parent.parent.gameObject.GetComponent<playerController>();
            player.addBuff(attachedBuff);
            Destroy(this.gameObject);
        }
    }
}

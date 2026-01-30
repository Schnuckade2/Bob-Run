using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialAbility : MonoBehaviour
{
    public PlayerScript player;
    public LogicScript logic;
    public ParticleSystem abilityParticles;

    public float abilityForce = 4f;
    public float fallForce = -5f;
    public float cooldownTime = 5f;
  
    public bool isOnCooldown = false;
    public bool isAbilityRunning = false;

    public void TryActivateAbility()
    {
        if (!isOnCooldown && !isAbilityRunning && !logic.isPaused)
        {
            StartCoroutine(ActivateAbility());
        }
    }



    private void Update()
    {
        
        
        
    }


    IEnumerator ActivateAbility()
    {
        isAbilityRunning = true;
        isOnCooldown = true;

        // Hochspringen
        player.rb.linearVelocity = new Vector2(5, abilityForce);

        // Warte 1 Sekunde
        yield return new WaitForSeconds(1f);

        // Fallbewegung
        // player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, fallForce);
        if (abilityParticles != null)
        {
            abilityParticles.Play();
        }

        // Warten bis Spieler landet
        yield return new WaitUntil(() => player.isGrounded);

        // Partikel abspielen
        

        // Cooldown
        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
        isAbilityRunning = false;
    }

    // Optional für externe Abfragen
    public bool IsOnCooldown()
    {
        return isOnCooldown || isAbilityRunning;
    }
}

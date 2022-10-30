using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayAudioClips(int _index)
    {
        switch (_index)
        {
            case 0:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.normalPalletAC);
                break;
            case 1:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.powerPalletAC);
                break;
            case 2:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.enemyKilledPlayerAC);
                break;
            case 3:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.playerKilledEnemyAC);
                break;
            case 4:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.walkingPac);
                break;
            case 5:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.enemyScaredState);
                break;
            case 6:
                Manager.instance.audioSource.PlayOneShot(Manager.instance.enemyNormalState);
                break;

        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {

        print("SCORE HANDLER" + collider2D.name);
        if (collider2D.CompareTag("NormalPellet"))
        {
            Manager.instance.NormalPallet();
            //CheckVictory();
            Destroy(collider2D.gameObject);
            PlayAudioClips(0);
        }

        else if (collider2D.CompareTag("cherry"))
        {
            Manager.instance.Cherry();
            //CheckVictory();
            Destroy(collider2D.gameObject);
            PlayAudioClips(0);
        }

        else if (collider2D.CompareTag("PowerPellet"))
        {
            Manager.instance.PowerPalletAvailed();
            Destroy(collider2D.gameObject);
            PlayAudioClips(1);
        }

        else if (collider2D.CompareTag("Enemy"))
        {
            if (Manager.instance.enemiesCanDoDamage)
            {
                print(" REINSTI PLAYER");
                Manager.instance.CheckDefeatOrReinstantiate();
                Destroy(this.gameObject);
            }
            else
            {
                Manager.instance.ReinstantiateEnemy(collider2D.gameObject);
                PlayAudioClips(3);
            }
        }
    }


}

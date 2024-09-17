using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class RockBehaviour : MonoBehaviour
    {
        private float minRespawnTimer;
        private float maxRespawnTimer;
        private int minDrops;
        private int maxDrops;
        private GameObject dropItem;
        public IEnumerator RockBreakEvent(GameObject rock)
        {
             //5. deactivate rock when broken.
             rock.SetActive(false);
            //6. if broken, replace the rock after timer
            float respawnTime = Random.Range(minRespawnTimer, maxRespawnTimer);
            yield return new WaitForSeconds(respawnTime);
            rock.SetActive(true);
        }

        private void SpawnLoot()
        {
            int dropCount = Random.Range(minDrops, maxDrops);

            for (int i = 0; i < dropCount; i++)
            {
                Instantiate(dropItem, transform.position, Quaternion.identity);
            }
        }
    }
}
using Diplom.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diplom.Spawners.Player
{
    public class PlayerSpawner : BaseSpawner
    {

        public void GetHero(GameObject prefab)
        {
            _prefab = prefab;
            
        }
        public void OnRespawn(GameObject player, PlayerBattleComponent battleStats)
        {
            StartCoroutine(Respawn(player, battleStats));
        }
        private IEnumerator Respawn(GameObject player, PlayerBattleComponent battleStats)
        {
            yield return new WaitForSeconds(5f);
            player.transform.position = _spawnPoint[_spawnPoint.Length-1].position;
            player.SetActive(true);

            battleStats.OnRespawn();
        }
    }
}
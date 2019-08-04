using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Darkness m_darkness;

    private void Start()
    {
        m_darkness = FindObjectOfType<Darkness>();
    }

    /// <summary>
    /// use this to "kill" the player!
    /// </summary>
    public void Die()
    {
        SpawnManager spawnManager = SpawnManager.GetInstance();

        // respawn at the relevant spawn point
        Transform spawnPoint = spawnManager.GetSpawnPoint();
        transform.position = spawnPoint.position;

        // allow player to re-place their checkpoint
        spawnManager.SetCanPlaceFlag(true);

        // do other reset stuffs here, such as darkness reset
        m_darkness.ResetDarkness(spawnPoint);

		SpawnManager.GetInstance().SetCanPlaceFlag(true);
	}
}
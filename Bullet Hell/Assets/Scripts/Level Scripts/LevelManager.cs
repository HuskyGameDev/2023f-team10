using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] int wavesUntilBoss = 3;
    [SerializeField] GameObject[] bosses;
    [SerializeField] GameObject bossSpawnPoint;

    [SerializeField] GameObject[] backgrounds;
    [SerializeField] ParticleSystem warpBackground;

    public enum LevelTypes { WORLD1 = 0, WORLD2 = 1, WORLD3 = 2 };

    public LevelTypes currentLevel = LevelTypes.WORLD1;
    public int wavesCompleted = 0;

    private void Start()
    {
        waveManager = GetComponent<WaveManager>();

        // Start the first wave
        Invoke(nameof(checkWave), 1f); // Delay the first wave by 1 second

    }

    /* this is for testing features not needed in the build
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            changeLevel((LevelTypes) ((int)(++currentLevel)%3));
        }
    }
    */

    //spawn and keep track of waves
    public void checkWave()
    {
        //if there are still enemies left don't do anything
        if (!checkForEnemies())
        {
            switch (currentLevel)
            {
                case LevelTypes.WORLD1:
                    if (wavesCompleted == wavesUntilBoss)
                    {
                        wavesCompleted = 0;
                        bossLevel();
                    }
                    else
                        waveManager.spawnWave(1);

                    break;

                case LevelTypes.WORLD2:
                    if (wavesCompleted == wavesUntilBoss)
                    {
                        wavesCompleted = 0;
                        bossLevel();
                    }
                    else
                        waveManager.spawnWave(2);

                    break;

                case LevelTypes.WORLD3:
                    if (wavesCompleted == wavesUntilBoss)
                    {
                        wavesCompleted = 0;
                        bossLevel();
                    }
                    else
                        waveManager.spawnWave(3);

                    break;
            }
        }
        //wait a second to see if the enemies are all gone
        else Invoke("checkWave", 1);
    }

    //handles boss levels
    private void bossLevel()
    {
        AudioManager.bossMusic();
        Instantiate(bosses[(int)currentLevel], bossSpawnPoint.transform);
    }

    public void onBossDefeat()
    {
        AudioManager.normalMusic();

        //if boss is defeated switch level
        changeLevel((LevelTypes)((int)(++currentLevel) % 3));
    }

    //change background with warp transition
    public void changeLevel(LevelTypes newLevel)
    {
        CancelInvoke();

        //update current level for the rest of the script
        currentLevel = newLevel;

        // Start the particle system effect
        startWarpEffect();

        // Update background for the new level
        Invoke(nameof(UpdateBackground), 3f);
        Invoke(nameof(checkWave), 8f);

    }

    //Update the backgroun after the warp is in full effect
    private void UpdateBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == (int)currentLevel);
        }
    }

    private void startWarpEffect()
    {
        // Start or restart the particle system
        if (warpBackground.isPlaying)
        {
            warpBackground.Stop();
        }
        warpBackground.Play();
    }

    //returns true if there are enemies present or false if there are none
    private bool checkForEnemies()
    {
        GameObject enemy = null;

        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
            return true;
        else
            return false;
    }
}

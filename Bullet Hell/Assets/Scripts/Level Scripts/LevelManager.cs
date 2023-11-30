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
        Invoke(nameof(checkWave), 3f); // Delay the first wave by 3 seconds

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            changeLevel((LevelTypes) ((int)(++currentLevel)%3));

        }
    }

    //spawn and keep track of waves
    public void checkWave()
    {
        switch(currentLevel)
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

    //handles boss levels
    private void bossLevel()
    {
        //spawn boss
        Instantiate(bosses[(int) currentLevel], bossSpawnPoint.transform);
    }

    public void onBossDefeat()
    {
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

}

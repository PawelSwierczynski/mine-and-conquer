using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Token { get; set; }
    public int LumberjackLevel { get; set; }
    public int StonequarryLevel { get; set; }
    public int GoldmineLevel { get; set; }
    public int BarracksLevel { get; set; }
    public int Swordsmans { get; set; }
    public int WallLevel { get; set; }
    public int WatchtowerLevel { get; set; }
    public int WoodCount { get; set; }
    public int StoneCount { get; set; }
    public int GoldCount { get; set; }
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateResources(string resourceCountsJson)
    {
        ResourceCounts resourceCounts = JsonUtility.FromJson<ResourceCounts>(resourceCountsJson.Substring(1, resourceCountsJson.Length - 2));

        WoodCount = resourceCounts.WoodCount;
        StoneCount = resourceCounts.StoneCount;
        GoldCount = resourceCounts.GoldCount;
    }

    public void PlaySound(int soundIdentifier)
    {
        audioSource.PlayOneShot(audioClips[soundIdentifier], 0.5f);
    }
}
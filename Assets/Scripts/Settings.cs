using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings", order = 0)]
public class Settings : ScriptableObject {
    [SerializeField] public string appIdentifier;
    [SerializeField] public string connectionHost;
    [SerializeField] public int connectionPort;
    [SerializeField] public string apiUri;
}
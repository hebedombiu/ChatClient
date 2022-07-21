using UnityEngine;

namespace LoadingIcon {

public class LoadingIconView : MonoBehaviour {
    [SerializeField] private Transform icon;
    [SerializeField] private float speed;

    private void Update() {
        icon.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}

}
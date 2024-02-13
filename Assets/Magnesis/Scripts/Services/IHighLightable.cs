using UnityEngine;

public interface IHighLightable
{
    public Vector3 Position { get; }
    void HoverEnable();
    void HoverDisable();
    void HighLightEnable();
    void HighLightDisable();
}
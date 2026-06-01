using UnityEngine.UI;

public class EmptyImage : Graphic
{
    public override void SetAllDirty() { }
    public override void SetVerticesDirty() { }
    public override void SetMaterialDirty() { }
    protected override void OnPopulateMesh(VertexHelper vh) { vh.Clear(); }
}
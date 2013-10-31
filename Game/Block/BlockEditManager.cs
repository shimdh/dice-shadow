using UnityEngine;

[ExecuteInEditMode]
public class BlockEditManager : MonoBehaviour
{
    public DataCenter.BlockState blockState; // 블럭의 상태;
    public BlockTextureManager blockTextureManager; // 블럭의 택스쳐를 참조할 매니저;
    private Block block; // 블럭;

    private void Awake()
    {
        block = gameObject.GetComponent<Block>();
    }

    // Use this for initialization
    private void Start()
    {
        block.BlockState = blockState;
    }

#if UNITY_EDITOR
    // Update is called once per frame
    private void Update()
    {
        if (blockTextureManager == null) return;
        if (blockTextureManager.BlockTextures.Length <= (int) blockState) return;
        renderer.material.mainTexture = blockTextureManager.BlockTextures[(int) blockState];
        block.BlockState = blockState;
    }
#endif
}
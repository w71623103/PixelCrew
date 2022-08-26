/*
 * IT'S A README FILE ON ITEM USE FOR PIXEL CREW
 * 
 * 理论上来说，你不需要修改任何脚本的名称、位置、内容
 * 
 * 如果你需要制作一个可以拾取的物品，那么请首先在Scene中Create New Empty，创建一个空对象
 * 然后为其添加Sprite Renderer(使它显示）、Collider2D(形状随意，尽量符合物品形状即可)然后勾选isTrigger
 * 最后，为其添加PickUpItem脚本。
 * 
 * 添加好之后，在Items文件夹里右键Create Item，创建一个物品组件，然后将右侧Inspector里面的内容填充完全(Description目前还没用到 不填也不会有bug)
 * 修改完之后，将这个组件拖到PickUpItem脚本对应的空格里，再为脚本提供物品自己的SpriteRenderer即可使用
 * 
 * 拾取的信息会在Debug.Log中显示
 * 
 * 或者你可以直接复制我制作好的Prefab
 * 使用时修改Item组件和物品名即可。
 * 
 */

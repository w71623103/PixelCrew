/*
 * Prefab使用说明
 * 
 * 1.添加玩家 直接在Scene中添加PlayerSet即可，这是一个包含了Blaze和Camera的Set
 * 
 * 2.地形 直接使用里面的方块就行 墙上方需要加一片地板 Platform是单向平台
 * 
 * 3.NPC 一个小石头 目前没功能
 * 
 * 4.潜行区域 StealthArea 按I切换潜行
 * 
 * 5.物品 Coin是按F才能拾取的Prefab； LifePoint是碰撞自动拾取的Prefab
 * 
 * 6.按K 松开时会发射子弹 目前没有蓄力
 * 
 * 7.Portal 按F交互 需要在Portal的里面加入通向的Scene名称
 * 
 * 8.关于相机边界 可以通过修改PlayerSet -> CameraPref -> Border -> Polygon Collider2D的形状来修改相机边界
 * 
 * 9.Scenes -> PrefabsLayout 里有目前的所有Prefab
 * 
 * 10.关于打上文字 需要添加Canvas Prefab 以及 NPC Prefab 然后将canvas的Render Mode调成ScreenSpace-Camera 调整DialogBox到合适的大小和位置 
 * 然后将DialogBox拖入NPC脚本中第一个空 Text拖入第二个空 第三个空写入你需要的文字
 * 使用时在NPC面前按F即可显示文字。
 */
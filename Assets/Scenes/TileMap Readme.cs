/*
 * Tilemap使用说明
 * 1.创建一个Scene
 * 2.将PlayerSet拖入其中 删掉原有的MainCamera
 * 3.创建一个2D Object -> Tilemap -> Rectangular
 * 4.你会发现出现了Grid，在Grid下面创建两个对象 一个BackGround 一个ForeGround
 * 5.给BackGround直接添加Tilemap组件以及Tilemap Renderer组件 
 *      将Tilemap Renderer中的Sorting Layer调为Background
 * 6.继续在grid中创建Tilemap 并将其放在ForeGround名下 其下所有Tilemap的Sorting Layer都是ForeGround
 * (Shadow可以为Shadow 影响不大）
 * 7.创建多个Tilemap是出于碰撞管理的考虑 所以你需要在Ground、Platform、Shadow最上方修改对应的Layer
 * 
 * 8.Ground：我们不再区分地面和墙体 因为不能爬墙 所以统一为Ground 你需要给Ground添加Tilemap Collider 2D 勾选Used By Composite
 *  再添加一个Composite Collider 2D，并将Rigidbody 2D调整为Static即可
 * 
 * 9.Platform：你需要先画出你的平台 然后有几个平台添加几个Box Collider 2D，将它们收束到你的平台大小。
 * 为每一个Collider勾选Used By Effector 然后添加Platform Effector组件，并将其Collider Mask调为PlayerCol（有且只有PlayerCol）
 * 
 * 10.Shadow：即StealthArea 所以你需要给这个Tilemap调整为StealthArea层 然后如同平台一样 为阴影添加Polygon Collider 为每一个Collider勾选Is Trigger即可
 * 
 * 11.如果你的Blaze无法正常显示 ，检查Blaze的Sprite Renderer的Sorting Layer是否调整为ForeGround 以及你的Tilemap的Sorting Layer是否正确。
 * 
 * 12.搭关卡愉快！
 */
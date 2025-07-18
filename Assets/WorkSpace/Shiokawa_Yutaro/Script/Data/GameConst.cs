public class GameConst
{
    // マップ関連
    public static readonly int MAP_SQUARE_HEIGHT_COUNT = 32;
    public static readonly int MAP_SQUARE_WIDTH_COUNT = 32;
    public static int MAP_SQUARE_COUNT { get { return MAP_SQUARE_HEIGHT_COUNT * MAP_SQUARE_WIDTH_COUNT; } }
    public static readonly int AREA_DEVIDE_COUNT = 8;
    // 最小部屋サイズ
    public static readonly int MIN_ROOM_SIZE = 3;

    // フロア関連
    // 1フロア内のキャラクターの最大数
    public static readonly int FLOOR_ENEMY_MAX = 15;

    // アクション関連
    // 通常攻撃のアクションID
    public static readonly int NORMAL_ATTACK_ACTION_ID = 0;

    //アイテム関連
    public static readonly string ITEM_SPRITE_FILE_NAME = "Design/Sprites/Item/itemIcons";
}

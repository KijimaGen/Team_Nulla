public class GameConst
{
    // �}�b�v�֘A
    public static readonly int MAP_SQUARE_HEIGHT_COUNT = 32;
    public static readonly int MAP_SQUARE_WIDTH_COUNT = 32;
    public static int MAP_SQUARE_COUNT { get { return MAP_SQUARE_HEIGHT_COUNT * MAP_SQUARE_WIDTH_COUNT; } }
    public static readonly int AREA_DEVIDE_COUNT = 8;
    // �ŏ������T�C�Y
    public static readonly int MIN_ROOM_SIZE = 3;

    // �t���A�֘A
    // 1�t���A���̃L�����N�^�[�̍ő吔
    public static readonly int FLOOR_ENEMY_MAX = 15;

    // �A�N�V�����֘A
    // �ʏ�U���̃A�N�V����ID
    public static readonly int NORMAL_ATTACK_ACTION_ID = 0;

    //�A�C�e���֘A
    public static readonly string ITEM_SPRITE_FILE_NAME = "Design/Sprites/Item/itemIcons";
}

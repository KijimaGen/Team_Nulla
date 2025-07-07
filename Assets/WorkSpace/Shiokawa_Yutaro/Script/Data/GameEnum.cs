/**
 * @file GameEnum.cs
 * @brief �񋓑̒�`
 * @author yao
 * @date 2025/4/10
 */

public enum eGamePart
{
    Invalid = -1,
    Standby,    // �����p�[�g
    Title,      // �^�C�g���p�[�g
    MainGame,   // ���C���p�[�g
    Ending,     // �G���f�B���O�p�[�g
    Max,
}

/// <summary>
/// �}�X�̒n�`
/// </summary>
public enum eTerrain
{
    Invalid = -1,
    Passage,    // �ʘH
    Room,       // ����
    Wall,       // ��
    Stair,      // �K�i
    Max,
}

/// <summary>
/// 4����
/// </summary>
public enum eDirectionFour
{
    Invalid = -1,
    Up,
    Right,
    Down,
    Left,
    Max,
}

/// <summary>
/// 8����
/// </summary>
public enum eDirectionEight
{
    Invalid = -1,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft,
    Max,
}

/// <summary>
/// �t���A�̏I���v��
/// </summary>
public enum eFloorEndReason
{
    Invalid = -1,   // �t���A�͏I�����Ă��Ȃ�
    Dead,           // �v���C���[���S
    Stair,          // �K�i�ňړ�
}

/// �_���W�����I���v��
public enum eDungeonEndReason
{
    Invalid = -1,    // �I�����Ă��Ȃ�
    Dead,           // �v���C���[���S
    Clear,          // �N���A
}

/// �L�����̃A�j���[�V�����̎��
public enum eCharacterAnimation
{
    Invalid = -1,   // �s���l
    Wait,
    Walk,
    Attack,
    Damage,
    Max
}

public enum eItemCategory
{
    Potion,
    Food,
    Wand,
    Scroll,
    Bag,
    Throwing,
    Weapon,
    Armor,

    Max
}

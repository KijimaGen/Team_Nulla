using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class AttackStrategy
{
    public abstract UniTask Execute(CharacterBase character);
}

public class GoingAttack : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("Ú‹ßUŒ‚");
    }
}

public class LongRangeAttack : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("‰“‹——£UŒ‚");
    }
}

public class CounterAttack : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("ƒJƒEƒ“ƒ^[UŒ‚");
    }
}

/// <summary>
/// Œã‚ë‚É‰ñ”ğ‚µ‚ÄŠÔ‡‚¢‚ğæ‚é
/// </summary>
/// <returns></returns>
public class TakeDistanceState : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("Œã‚ë‰ñ”ğ");
    }
}

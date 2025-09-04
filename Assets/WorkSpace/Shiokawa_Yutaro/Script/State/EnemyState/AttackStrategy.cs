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
        await character.GoingAttack();
    }
}

public class LongRangeAttack : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("‰“‹——£UŒ‚");
        await character.LongRangeAttack();
    }
}

public class CounterAttack : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("ƒJƒEƒ“ƒ^[UŒ‚");
        await character.CounterAttack();
    }
}

/// <summary>
/// Œã‚ë‚É‰ñ”ğ‚µ‚ÄŠÔ‡‚¢‚ğæ‚é
/// </summary>
/// <returns></returns>
public class TakeDistance : AttackStrategy
{
    public override async UniTask Execute(CharacterBase character)
    {
        Debug.Log("Œã‚ë‰ñ”ğ");
        await character.TakeDistance();
    }
}

using Cysharp.Threading.Tasks;

//interface ってのがあるらしい

public abstract class StateBase<T> where T : CharacterBase
{
    /// <summary>
    /// ステートに入る
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public abstract UniTask Enter(T character);
    /// <summary>
    /// ステートをじっこうする
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public abstract UniTask Execute(T character);
    /// <summary>
    /// ステートから出る
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public abstract UniTask Exit(T character);
}

public interface IInteractable
{
    InteractionOption InteractionOption { get; set; }
    void Interact();
}

public enum InteractionOption
{
    None,
    Option1,
    Option2
}
public interface IExplodable
{
    void BombExplode();
}


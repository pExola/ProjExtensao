public interface IInteractable
{
    // Texto que aparece (ex: "E) Coletar Poção")
    string GetPrompt();

    // Ação que acontece ao interagir
    void Interact();
}
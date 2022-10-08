namespace PoPoy.Client.Shared.PopupChat
{
    public partial class PopupChat
    {
        private bool showChat = false;

        private void OpenChat()
        {
            showChat = true;
            StateHasChanged();
        }
        private void CloseChat()
        {
            showChat = false;
            StateHasChanged();
        }
    }
}

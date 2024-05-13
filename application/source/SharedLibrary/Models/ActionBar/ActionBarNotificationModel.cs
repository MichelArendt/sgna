namespace SharedLibrary.Models.ActionBar
{
    public class ActionBarNotificationModel
    {
        public ActionBarNotificationModel(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}

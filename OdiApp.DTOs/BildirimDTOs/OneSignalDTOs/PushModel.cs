namespace OdiApp.DTOs.BildirimDTOs.OneSignalDTOs
{
    public class PushModel
    {
        public PushHeadings headings { get; set; } = new PushHeadings();
        public PushSubtitle subtitle { get; set; } = new PushSubtitle();
        public PushContents contents { get; set; } = new PushContents();
        public IncludeAliases include_aliases { get; set; } = new IncludeAliases();
        public string target_channel { get; set; }
        public string name { get; set; }
        //public List<PushButton> buttons { get; set; } = new List<PushButton>();
        public string ios_badgeType { get; set; }
        public int ios_badgeCount { get; set; }
        public string ios_sound { get; set; }
        public string chrome_web_icon { get; set; }
        public string chrome_web_image { get; set; }
        public string chrome_icon { get; set; }
        public IosAttachments ios_attachments { get; set; } = new IosAttachments();
        public string big_picture { get; set; }
        public string app_id { get; set; }
        public PushData data { get; set; } = new PushData();
    }
    public class PushButton
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string icon_type { get; set; }
    }

    public class PushContents
    {
        public string en { get; set; }
        public string tr { get; set; }
    }

    public class PushData
    {
        public string notType { get; set; }
        public int bildirimTipi { get; set; }
        public string avatar { get; set; }
        public string displayName { get; set; }
    }

    public class PushHeadings
    {
        public string en { get; set; }
        public string tr { get; set; }
    }

    public class IncludeAliases
    {
        public List<string> external_id { get; set; }
    }

    public class IosAttachments
    {
        public string image1 { get; set; }
    }
    public class PushSubtitle
    {
        public string en { get; set; }
        public string tr { get; set; }
    }
}
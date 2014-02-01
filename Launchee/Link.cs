using System.Collections.Generic;
using System.Data.Odbc;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using Newtonsoft.Json.Linq;

namespace Launchee
{
    class Link
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string Arguments { get; set; }

        public bool IsDefault { get; set; }

        public Link()
        {
            IsDefault = false;
        }

        public JumpListLink ToJumpListLink()
        {
            JumpListLink link = new JumpListLink(Path, Title);

            if (!string.IsNullOrWhiteSpace(Icon))
            {
                link.IconReference = new IconReference(Icon);
            }

            if (!string.IsNullOrWhiteSpace(Arguments))
            {
                link.Arguments = Arguments;
            }

            return link;
        }

        public static Link FromAnonymousObject(JObject obj)
        {
            Link link = obj.ToObject<Link>();
           
            return link;
        }
    }
}

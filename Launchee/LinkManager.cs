using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Launchee
{
    class LinkManager
    {

        internal IEnumerable<Link> LoadLinksFromFile(string filePath)
        {
            if(!File.Exists(filePath))
                return null;

            List<Link> links = new List<Link>();

            string fileContents = File.ReadAllText(filePath);

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContents);

            if (json.ContainsKey("default"))
            {
                Link defaultLink = Link.FromAnonymousObject(json["default"] as JObject);
                defaultLink.IsDefault = true;
                links.Add(defaultLink);
            }

            if (json.ContainsKey("links"))
            {
                var jsonLinks = json["links"] as IEnumerable<object>;
                foreach (var jsonLink in jsonLinks)
                {
                    Link link = Link.FromAnonymousObject(jsonLink as JObject);
                    links.Add(link);
                }
            }


            return links;
        }
    }
}

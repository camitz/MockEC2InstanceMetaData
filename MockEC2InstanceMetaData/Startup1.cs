using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;

[assembly: OwinStartup(typeof(MockEC2InstanceMetaData.Startup1))]

namespace MockEC2InstanceMetaData
{
    public class Startup1
    {
        private string _path;

        public void Configuration(IAppBuilder app)
        {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
            while (!Directory.GetFiles(_path).Select(Path.GetFileName).Contains("metadata.json"))
                _path += "\\..";

            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";

                using (var file = new StreamReader(_path + "\\metadata.json"))
                {
                    var o = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                    var s = o.SelectToken("");

                    string ret;
                    try
                    {
                        s = o.SelectToken(context.Request.Uri.PathAndQuery.Replace("/", "."));
                    }
                    catch (JsonException)
                    {
                        ret = string.Join("\n", s.Select(t => t.Path.Split(".".ToCharArray()).Last()));
                        return context.Response.WriteAsync(ret);
                    }

                    if (s.ToString().Contains("{") )
                        ret = string.Join("\n",
                            s.Select(t => t.Path.Split(".".ToCharArray()).Last() + (t.ToString().Contains("{") ? "/" : "")));
                    else
                    {
                        ret = (s as JValue).ToString();
                    }

                    return context.Response.WriteAsync(ret);
                }
            });
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ATTime.Startup1))]

namespace ATTime
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

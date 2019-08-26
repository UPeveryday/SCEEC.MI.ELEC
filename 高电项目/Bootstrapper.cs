using System;
using Stylet;
using StyletIoC;
using 高电项目.Pages;
using 高电项目.ViewModels;

namespace 高电项目
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder) 
        {
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}

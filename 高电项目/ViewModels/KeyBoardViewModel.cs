using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高电项目.ViewModels
{
    public class KeyBoardViewModel : Screen
    {
        public void Cw()
        {
            Console.WriteLine("hello");
            高电项目.Views.KeyBoardView kv = new Views.KeyBoardView();
            kv.ShowDialog();
        }
    }
}

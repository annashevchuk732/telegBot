using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Bot
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            MyBot bot = new MyBot();
            InitializeComponent();
        }
    }
}

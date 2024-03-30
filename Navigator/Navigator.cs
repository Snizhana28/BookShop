﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF.App3.Navigator
{
    public class Navigator
    {
        public class NavigatorObject
        {
            public static MainWindow? pageSwitcher;

            public static void Switch(UserControl newPage)
            {
                pageSwitcher?.Navigate(newPage);
            }

            public static void Switch(UserControl newPage, object state)
            {
                pageSwitcher?.Navigate(newPage, state);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView {
    /// <summary>
    /// На основе http://studassistent.ru/charp/menyu-v-konsolnom-prilozhenii-c
    /// </summary>
    class ConsoleMenu {
        string[] menuItems;
        int counter = 0;
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public ConsoleMenu(string[] menuItems) {
            this.menuItems = menuItems;
        }

        public int PrintMenu() {
            ConsoleKeyInfo key;
            do {
                Console.Clear();
                Console.WriteLine(Header1);
                Console.WriteLine();
                Console.WriteLine(Header2);
                Console.WriteLine();
                for (int i = 0; i < menuItems.Length; i++) {
                    if (counter == i) {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    } else {
                        Console.WriteLine(menuItems[i]);
                    }

                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow) {
                    counter--;
                    if (counter == -1) counter = menuItems.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow) {
                    counter++;
                    if (counter == menuItems.Length) counter = 0;
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return counter;
        }
    }
}

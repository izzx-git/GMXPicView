using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{//Конвертер картинок в формат экрана GMX 640*200*2 цвета на байт из 16
    class Program
    {
        static void Main(string[] args)
        {
            string file = ""; //@"c:\zx48k";  //путь 
            if (args.Length == 0) return; //выход, не передали имя файла
            file = args[0]; //получим имя
            System.Console.WriteLine(file);
            //Console.ReadLine();
            string file_in = file+".cel";  //путь 
            string file_outP = file+"-p.C";  //путь пиксели
            string file_outA = file+"-a.C";  //путь атрибуты
            string file_out = file+".C";  //путь выходной файл
            string[] mostP = new string[8]; //часто встречающиеся цвета пикселя
            string[] mostA = new string[8]; //часто встречающиеся цвета фона
            
            //string readbuf=""; //буфер для чтения пикселя 4 байта
            byte[] byte_outP = new byte[1];
            byte[] byte_outA = new byte[1];
            byte[] byte_in=new byte[3]; //входной буфер

            FileStream FS_in = new FileStream(file_in, FileMode.Open); //открываем входной файл
            FileStream FS_outP = new FileStream(file_outP, FileMode.Create); //создаём выходной файл пикселей
            FileStream FS_outA = new FileStream(file_outA, FileMode.Create); //создаём выходной файл атрибутов
            FileStream FS_out = new FileStream(file_out, FileMode.Create); //создаём выходной файл

            for (int i = 0; i < 32; i ++) //пропускаем первые 32 байта заголовка
            {
                FS_in.Read(byte_in, 0, 1);     
            }

            for (int i = 0; i < 16000; i ++) //всего картинка 640*200=16000 точек по 4 байта на точку
            {

                Array.Clear(mostP,0,8); //очистим массивы
                Array.Clear(mostA, 0, 8);

                FS_in.Read(byte_in, 0, 3); //читаем три байта    
                int bite07 = detColor(byte_in);//определяем цвет
                bool bite07b = getWB(bite07); //приводим его в ч.б
                if (bite07b)  mostP[7] = bite07.ToString(); //запомним цвет пикселя
                else mostA[7] = bite07.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite06 = detColor(byte_in);
                bool bite06b = getWB(bite06);
                if (bite06b) mostP[6] = bite06.ToString(); //запомним цвет пикселя
                else mostA[6] = bite06.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite05 = detColor(byte_in);
                bool bite05b = getWB(bite05);
                if (bite05b) mostP[5] = bite05.ToString(); //запомним цвет пикселя
                else mostA[5] = bite05.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite04 = detColor(byte_in);
                bool bite04b = getWB(bite04);
                if (bite04b) mostP[4] = bite04.ToString(); //запомним цвет пикселя
                else mostA[4] = bite04.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite03 = detColor(byte_in);
                bool bite03b = getWB(bite03);
                if (bite03b) mostP[3] = bite03.ToString(); //запомним цвет пикселя
                else mostA[3] = bite03.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite02 = detColor(byte_in);
                bool bite02b = getWB(bite02);
                if (bite02b) mostP[2] = bite02.ToString(); //запомним цвет пикселя
                else mostA[2] = bite02.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite01 = detColor(byte_in);
                bool bite01b = getWB(bite01);
                if (bite01b) mostP[1] = bite01.ToString(); //запомним цвет пикселя
                else mostA[1] = bite01.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт

                FS_in.Read(byte_in, 0, 3); //читаем три байта
                int bite00 = detColor(byte_in);
                bool bite00b = getWB(bite00);
                if (bite00b) mostP[0] = bite00.ToString(); //запомним цвет пикселя
                else mostA[0] = bite00.ToString(); //запомним цвет фона
                FS_in.Read(byte_in, 0, 1); //пропустим четвёртый байт
                //Посчитаем выходной байт
                byte_outP[0] = Convert.ToByte(Convert.ToInt16(bite07b) * 128 + Convert.ToInt16(bite06b) * 64 + Convert.ToInt16(bite05b) * 32 + Convert.ToInt16(bite04b) * 16 + Convert.ToInt16(bite03b) * 8 + Convert.ToInt16(bite02b) * 4 + Convert.ToInt16(bite01b) * 2 + Convert.ToInt16(bite00b));
                FS_outP.Write (byte_outP,0,1); //запишем в файл
                //определим выходной байт цвета
                var resultP = mostP.Where(x => !string.IsNullOrWhiteSpace(x)); //убираем пустые ячейки
                var resultA = mostA.Where(x => !string.IsNullOrWhiteSpace(x));
                int byteAP = 0; //цвет пикселя
                int byteAF = 0;  //цвет фона
                int bright = 0; //повышенная яркость
                if (resultP.Count() > 0)
                {
                    var mostPP = resultP.GroupBy(x => x).OrderByDescending(x => x.Count()).First(); //какой часто встречается?
                    byteAP = Convert.ToInt16(mostPP.Key);
                    if (byteAP > 7)
                    {
                        byteAP = byteAP - 8; //в 8 цветов
                        //bright = 1;
                    }
                }
                if (resultA.Count() > 0)
                {
                    var mostAA = resultA.GroupBy(x => x).OrderByDescending(x => x.Count()).First(); //какой часто встречается?
                    byteAF = Convert.ToInt16(mostAA.Key);
                    if (byteAF > 7)
                    {
                        byteAF = byteAF - 8; //в 8 цветов
                        //bright = 1;
                    }
                }
                //посчитаем выходной байт цвета
                byte_outA[0] = Convert.ToByte(bright*64+byteAF*8+byteAP); //цвет в формат ZX
                FS_outA.Write(byte_outA, 0, 1); //запишем в файл
            }
            //Теперь сделаем файл атрибутов белое на чёрном
            //byte_outA[0] = System.Convert.ToByte('\x07'); ; //цвет
            //for (int i = 0; i < 16000; i++)
            //{
            //    FS_outA.Write(byte_outA, 0, 1);
            //}

            //в конце объединим в один файл пиксели и атрибуты
            FS_outP.Position = 0; //в начало файла пикселей
            for (int i = 0; i < 16000; i++) 
            {
                FS_outP.Read(byte_in, 0, 1); //переносим по байту
                FS_out.Write(byte_in, 0, 1);
            }
            //в конец добавим 384 пустых байт для ровного счёта в 63 сектора по 256
            byte_in[0] = Convert.ToByte(0);
            for (int i = 0; i < 384; i++)
            {
                FS_out.Write(byte_in, 0, 1);
            }
            FS_outA.Position = 0; //в начало файла атрибутов
            for (int i = 0; i < 16000; i++)
            {
                FS_outA.Read(byte_in, 0, 1); //переносим по байту
                FS_out.Write(byte_in, 0, 1);
            }
            //в конец добавим 384 пустых байт для ровного счёта в 63 сектора по 256
            byte_in[0] = Convert.ToByte(0);
            for (int i = 0; i < 384; i++)
            {
                FS_out.Write(byte_in, 0, 1);
            }


            //закрываем файлы
            FS_in.Close();
            FS_out.Close();
            FS_outP.Close();
            FS_outA.Close();

        }
        static int detColor (byte[] pixel_in )
        {//преобразует цвет RGB из 3 байт в цвет 4 бита 0-15
            string[] colorsRGB = new string[]// объявляем текстовый массив и перечисляем цвета
                 {  "000000", 
                    "c00000",
                    "0000c0",
                    "c000c0",
                    "00c000",                  
                    "c0c000",
                    "00c0c0",
                    "c0c0c0",
                    "000000",
                    "ff0000",
                    "0000ff",
                    "ff00ff",
                    "00ff00",
                    "ffff00",
                    "00ffff",
                    "ffffff"
                };
            int color = 0;
            string readbuf = "";
            readbuf = BitConverter.ToString(pixel_in); //конвертим в строку
            readbuf = readbuf.Remove(2, 1); //убрать лишние символы
            readbuf = readbuf.Remove(4, 1);
            readbuf = readbuf.ToLower(); //в нижний регистр
            for (int i = 0; i < 16; i++)
            {
                if (colorsRGB[i] == readbuf)
                {
                    color = i;
                }
            }
            if (color == 8) color = 0; //чёрный 8 = чёрный 0
            return color;
        }
        static bool getWB(int color_in)
        {//превращает цвет 0-15 в 1 или 0, то есть ч.б
            //цвета 0-3 и 8-11 считаются фон 
            //цвета 4-7 и 12-15 считаются пиксель
            bool color_out;
            if (color_in >= 0 && color_in <=3 || color_in >= 8 && color_in <= 11)
            {
                color_out=false;
            }
            else
            {
                color_out=true;
            }


            return color_out;
        }

    }
}

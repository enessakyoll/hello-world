using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Numerics;
using System.Diagnostics;

namespace Prolab1
{
    public partial class Hashing : System.Web.UI.Page
    {
        public static int data = 0;
        public static int column = 2;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //1-Random Öğrenci Numarası ve Adı oluşturma kısmı
        #region
        public string[] studentName()
        {
            string random_name = "";
            data = Int32.Parse(TextBoxNumber.Text.ToString());
            string harfler = "abcdefghijklmnoprstuvyz";
            Random names = new Random();
            string[] name_array = new string[data];
            for (int i = 0; i < data; i++)
            {
                random_name = "";
                for (int j = 0; j < 10; j++)
                {
                    random_name += harfler[names.Next(harfler.Length)];
                }
                if (!name_array.Contains(random_name))
                {
                    name_array[i] = random_name;
                }
                else i--;
            }
            return name_array;
        }

        public string[] studentNumber()
        {
            char ch = '0';
            string random_number = "";
            data = Int32.Parse(TextBoxNumber.Text.ToString());
            string number = "1234567890";
            Random numbers = new Random();
            string[] number_array = new string[data];
            for (int i = 0; i < data; i++)
            {
                random_number = "";
                for (int j = 0; j < 9; j++)
                {
                    ch = number[numbers.Next(number.Length)];
                    if (j == 0)
                    {
                        if (ch != '0')
                        {
                            random_number += ch;
                        }
                        else j--;
                    }
                    else random_number += ch;
                }
                if (!number_array.Contains(random_number))
                {
                    number_array[i] = random_number.ToString();
                }
                else i--;
            }
            return number_array;
        }



        protected void ButtonRandomStudent_Click(object sender, EventArgs e)
        {
            string name = "";
            string number = "";
            data = Int32.Parse(TextBoxNumber.Text.ToString());
            string[] default_student = new string[data];
            string[] created_numbers = new string[data];
            string[] created_names = new string[data];

            created_numbers = studentNumber();
            created_names = studentName();

            for (int i = 0; i < data; i++)
            {
                name = "";
                number = "";

                name = created_numbers[i].ToString();
                number = created_names[i].ToString();
                default_student[i] = name + " " + number;
            }
            string filename = "Lineer";
            try
            {
                ExportTxt(default_student, filename);
                LabelCreateResult1.Text = "**Dosyalar başarılı bir şekilde kaydedildi.";
            }
            catch (Exception ex)
            {
                LabelCreateResult1.Text = "!!!Dosya Oluşturulurken Hata." + ex.InnerException.Message.ToString();
            }
        }
        #endregion

        //.txt Dosyası Oluşturma Fonksiyonu
        public void ExportTxt(string[] array, string filename)
        {
            int rows = array.Length;
            string fileLoc = @"D:\DersNotlari\yaz okulu\Prolab1\Prolab1\Files\" + filename + ".txt";//tek backslash ile ifade edebilmek için @ kullandık.
            if (!File.Exists(fileLoc))
            {
                using (var sw = new StreamWriter(fileLoc))
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (i != rows - 1)
                        {
                            sw.Write(array[i] + "\n");
                        }
                        else sw.Write(array[i]);
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                File.Delete(fileLoc);
                using (var sw = new StreamWriter(fileLoc))
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (i != rows - 1)
                        {
                            sw.Write(array[i] + "\n");
                        }
                        else sw.Write(array[i]);
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        //2-Bolen Kalan ve Kare Ortası Dosyalarının Oluşturulması
        #region
        public void CreateBolenKalan(List<string> file) //Bolen Kalan Fonksiyonu
        {
            int arrayIndex = 0;
            int modValue = file.Count;
            long fileValue = 0; //Lineer dosyadan okunan her bir satırdaki öğrenci num. tutan değişken
            int firstIndex = 0; //Mod alındıktan sonra bulunan ilk değer
            int fileSize = file.Count;
            string[] bolenKalanArray = new string[file.Count];
            string[] sep;
            if (TextBoxAyirmaDegeri.Text != "")
            {
                sep = new string[] { TextBoxAyirmaDegeri.ToString() };
            }
            else
            {
                sep = new string[] { " " };
            }

            for (int i = 0; i < file.Count; i++)
            {
                string[] lines = file[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
                fileValue = long.Parse(lines[0].ToString());
                arrayIndex = (int)(fileValue % modValue);
                firstIndex = arrayIndex;
                if (bolenKalanArray[arrayIndex] == null)
                {
                    bolenKalanArray[arrayIndex] = lines[0] + " " + lines[1];
                }
                else //lineer search
                {
                    fileSize = file.Count;
                    for (int j = arrayIndex; j < fileSize; j++)
                    {
                        arrayIndex = arrayIndex + 1;
                        if (arrayIndex < file.Count)//Dosyanın sonuna kadar taranıp taranmadığının kontrolü
                        {
                            if (arrayIndex != firstIndex)//Dizinin tekrar en başından başladıktan sonra ilk index'e gelip gelmediğinin kontrolü
                            {
                                if (bolenKalanArray[arrayIndex] == null)
                                {
                                    bolenKalanArray[arrayIndex] = lines[0] + " " + lines[1];
                                    break;
                                }
                            }
                            else
                            {
                                LabelCreateResult2.Text += "!!!Bolen Kalan Dizisi Dolu. ";
                            }
                        }
                        else
                        {
                            j = -1;
                            arrayIndex = -1;
                            fileSize = firstIndex;
                        }
                    }
                }
            }
            ExportTxt(bolenKalanArray, "BolenKalan");
        }

        
        

        public int arrayLength(int sayi)//Dinamik olarak Kare Ortası dosyasının oluşturulabilmesi için öğrenci sayısının kaç basamak girildiğini hesaplar.
        {
            int length = 0;
            while (sayi > 0)
            {
                length++;
                sayi = sayi / 10;
            };
            return length;
        }

        public void CreateKareOrtasi(List<string> file)//Kare Ortası Fonksiyonu
        {
            int arrayIndex = 0;
            int substringValue = arrayLength(file.Count);
            int arraySize = Convert.ToInt32(Math.Pow(10.0, Convert.ToDouble(substringValue)));
            string midSquareValue = "";
            long fileValue = 0;
            int firstIndex = 0;
            int fileSize = file.Count;
            string[] kareOrtasiArray = new string[arraySize];
            string[] sep;
            if (TextBoxAyirmaDegeri.Text != "")
            {
                sep = new string[] { TextBoxAyirmaDegeri.ToString() };

            }
            else
            {
                sep = new string[] { " " };
            }

            for (int i = 0; i < file.Count; i++)
            {
                string[] lines = file[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
                fileValue = long.Parse(lines[0].ToString());
                BigInteger x = fileValue * fileValue;
                string temp = x.ToString();
                midSquareValue = x.ToString().Substring(temp.Length - substringValue - 6, substringValue);
                arrayIndex = Int32.Parse(midSquareValue);
                firstIndex = arrayIndex;
                if (kareOrtasiArray[arrayIndex] == null)
                {
                    kareOrtasiArray[arrayIndex] = lines[0] + " " + lines[1];
                }
                else //lineer search
                {
                    fileSize = arraySize;
                    for (int j = arrayIndex; j < fileSize; j++)
                    {
                        arrayIndex = arrayIndex + 1;
                        if (arrayIndex < arraySize)
                        {
                            if (arrayIndex != firstIndex)
                            {
                                if (kareOrtasiArray[arrayIndex] == null)
                                {
                                    kareOrtasiArray[arrayIndex] = lines[0] + " " + lines[1];
                                    break;
                                }
                            }
                            else
                            {
                                LabelCreateResult2.Text += "!!!Kare Ortası Dizisi Dolu. ";
                            }
                        }
                        else
                        {
                            j = -1;
                            arrayIndex = -1;
                            fileSize = firstIndex;
                        }
                    }
                }
            }

            ExportTxt(kareOrtasiArray, "kareOrtasi");
        }

        protected void ButtonBolenKalan_Click(object sender, EventArgs e)
        {
            List<string> fileLine = new List<string>();
            if (FileUpload1.HasFile == true)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                StreamReader reader = new StreamReader(FileUpload1.FileContent);
                do
                {
                    fileLine.Add(reader.ReadLine());
                } while (reader.Peek() != -1);
                reader.Close();
            }
            try
            {
                CreateBolenKalan(fileLine);
                CreateKareOrtasi(fileLine);
                LabelCreateResult2.Text = "**Dosyalar başarılı bir şekilde kaydedildi.";
            }
            catch (Exception ex)
            {
                LabelCreateResult2.Text = "!!!Dosyalar Oluşturulurken Hata." + ex.InnerException.Message.ToString();
            }

        }
        #endregion

        //3-Arama ve Sürelerin Hesabı
        #region
        //3-Öğrenci numarası bulma sürelerini hesaplama fonksiyonu
        public Stopwatch calculateSearchTime(List<string> fileGelen, string ogrNumber, int choosen)
        {
            Stopwatch watch = new Stopwatch();
            switch (choosen)
            {
                case 1: //Lineer Hesaplama
                    int found = 0;
                    string[] sepLineer;
                    if (TextBoxAyirmaDegeri.Text != "")
                    {
                        sepLineer = new string[] { TextBoxAyirmaDegeri.ToString() };
                    }
                    else
                    {
                        sepLineer = new string[] { " " };
                    }
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < fileGelen.Count; i++)
                    {
                        string[] linesLineer = fileGelen[i].Split(sepLineer, StringSplitOptions.RemoveEmptyEntries);
                        if (linesLineer[0].ToString() == ogrNumber)
                        {
                            found = i + 1;
                            break;
                        }
                    }
                    watch.Stop();
                    LabelLineer.Text = "*Lineer arama sonucunda " + ogrNumber + " nolu öğrenci " + found + ". sırada ";
                    break;

                case 2: //Bolen-Kalan Hesaplama
                    int fileSizeBolen = fileGelen.Count;
                    int arrayIndexBolen = Int32.Parse(ogrNumber) % fileSizeBolen;
                    int modValue = fileGelen.Count;
                    int firstIndexBolen = arrayIndexBolen;
                    string[] sepBolen;
                    if (TextBoxAyirmaDegeri.Text != "")
                    {
                        sepBolen = new string[] { TextBoxAyirmaDegeri.ToString() };
                    }
                    else
                    {
                        sepBolen = new string[] { " " };
                    }
                    /*timer Start*/
                    watch = Stopwatch.StartNew();
                    string[] lines = fileGelen[arrayIndexBolen].Split(sepBolen, StringSplitOptions.RemoveEmptyEntries);
                    if (lines[0] == null)
                    {
                        LabelBolenKalan.Text = "Bölen Kalan-İlk İndex Noktasında Kayıt Yok!";
                        break;
                    }
                    else if (lines[0].ToString() == ogrNumber)
                    {
                        LabelBolenKalan.Text = "*Bölen Kalan arama sonucunda " + ogrNumber + " nolu öğrenci " + (arrayIndexBolen + 1) + ". sırada ";
                        break;
                    }
                    else
                    {
                        fileSizeBolen = fileGelen.Count;
                        for (int j = arrayIndexBolen + 1; j < fileSizeBolen; j++)
                        {
                            arrayIndexBolen = arrayIndexBolen + 1;
                            if (arrayIndexBolen < fileGelen.Count)
                            {
                                if (arrayIndexBolen != firstIndexBolen)
                                {
                                    string[] lines2 = fileGelen[j].Split(sepBolen, StringSplitOptions.RemoveEmptyEntries);
                                    if (lines2[0].ToString() == ogrNumber)
                                    {
                                        LabelBolenKalan.Text = "*Bölen Kalan arama sonucunda " + ogrNumber + " nolu öğrenci " + (j + 1) + ". sırada ";
                                        break;
                                    }
                                }
                                else
                                {
                                    LabelBolenKalan.Text = "Bölen Kalan-Lineer Arama Sonucu: Kayıt Yok!";
                                    break;
                                }
                            }
                            else
                            {
                                j = -1;
                                arrayIndexBolen = -1;
                                fileSizeBolen = firstIndexBolen;
                            }
                        }
                    }
                    watch.Stop();
                    break;
                case 3: //Kare Ortası Hesaplama
                    int arrayIndexKare = 0;
                    int substringValue = arrayLength(fileGelen.Count);
                    string midSquareValue = "";
                    long fileValue = long.Parse(ogrNumber);
                    BigInteger x = fileValue * fileValue;
                    string temp = x.ToString();
                    midSquareValue = x.ToString().Substring(temp.Length - substringValue - 6, substringValue);
                    arrayIndexKare = Int32.Parse(midSquareValue);

                    int firstIndexKare = arrayIndexKare;
                    int fileSize = fileGelen.Count;

                    string[] sepKare;
                    if (TextBoxAyirmaDegeri.Text != "")
                    {
                        sepKare = new string[] { TextBoxAyirmaDegeri.ToString() };
                    }
                    else
                    {
                        sepKare = new string[] { " " };
                    }
                    /*timer Start*/
                    watch = Stopwatch.StartNew();
                    string[] linesKare = fileGelen[arrayIndexKare].Split(sepKare, StringSplitOptions.RemoveEmptyEntries);
                    if (linesKare[0] == null)
                    {
                        LabelKareOrtasi.Text = "Kare Ortası-İlk İndex Noktasında Kayıt Yok!";
                        break;
                    }
                    else if (linesKare[0].ToString() == ogrNumber)
                    {
                        LabelKareOrtasi.Text = "*Kare Ortası arama sonucunda " + ogrNumber + " nolu öğrenci " + (arrayIndexKare + 1) + ". sırada ";
                        break;
                    }
                    else
                    {
                        fileSize = fileGelen.Count;
                        for (int j = arrayIndexKare + 1; j < fileSize; j++)
                        {
                            arrayIndexKare = arrayIndexKare + 1;
                            if (arrayIndexKare < fileGelen.Count)
                            {
                                if (arrayIndexKare != firstIndexKare)
                                {
                                    string[] lines4 = fileGelen[j].Split(sepKare, StringSplitOptions.RemoveEmptyEntries);
                                    if (lines4[0].ToString() == ogrNumber)
                                    {
                                        LabelKareOrtasi.Text = "*Kare Ortası arama sonucunda " + ogrNumber + " nolu öğrenci " + (j + 1) + ". sırada ";
                                        break;
                                    }
                                }
                                else
                                {
                                    LabelKareOrtasi.Text = "Kare Ortası-Lineer Arama Sonucu: Kayıt Yok!";
                                    break;
                                }
                            }
                            else
                            {
                                j = -1;
                                arrayIndexKare = -1;
                                fileSize = firstIndexKare;
                            }
                        }
                    }
                    watch.Stop();
                    break;

                default: break;
            }

            return watch;
        }


        //Dosya Okuma Fonksiyonu
        public List<string> ReadFile(string path)
        {
            List<string> fileLine = new List<string>();
            StreamReader reader = new StreamReader(path);
            do
            {
                fileLine.Add(reader.ReadLine());
            } while (reader.Peek() != -1);
            reader.Close();
            return fileLine;
        }
        protected void ButtonCalc_Click(object sender, EventArgs e)
        {
            //Lineer Süre Hesabı
            string filePath = Server.MapPath("~/Files");
            List<string> fileLineer = new List<string>(ReadFile(filePath + "//Lineer.txt"));
            var elapsedMsLineer = calculateSearchTime(fileLineer, TextBoxNumberValue.Text.ToString(), 1).Elapsed;
            LabelLineer.Text += elapsedMsLineer.ToString() + " ms sürede bulunmuştur.";
            LabelLineer.Visible = true;


            //Bölen Kalan Süre Hesabı
            List<string> fileBolenKalan = new List<string>(ReadFile(filePath + "//BolenKalan.txt"));
            var elapsedMsBolenKalan = calculateSearchTime(fileBolenKalan, TextBoxNumberValue.Text.ToString(), 2).Elapsed;
            LabelBolenKalan.Text += elapsedMsBolenKalan.ToString() + " ms sürede bulunmuştur.";
            LabelBolenKalan.Visible = true;


            //Kare Ortası Süre Hesabı
            List<string> fileKareOrtasi = new List<string>(ReadFile(filePath + "//KareOrtasi.txt"));
            var elapsedMsKareOrtasi = calculateSearchTime(fileKareOrtasi, TextBoxNumberValue.Text.ToString(), 3).Elapsed;
            LabelKareOrtasi.Text += elapsedMsKareOrtasi.ToString() + " ms sürede bulunmuştur.";
            LabelKareOrtasi.Visible = true;


            //En iyi ve en kötü yöntem 
            int min = 0; int max = 0;
            int lineer = 0, bolen = 0, kare = 0;
            lineer = Int32.Parse(elapsedMsLineer.Ticks.ToString());
            bolen = Int32.Parse(elapsedMsBolenKalan.Ticks.ToString());
            kare = Int32.Parse(elapsedMsKareOrtasi.Ticks.ToString());
            string[] result = { "Lineer " + lineer, "Bölen Kalan " + bolen, "Kare Ortası " + kare };
            min = lineer;
            max = lineer;
            if (bolen < min) min = bolen;
            if (kare < min) min = kare;
            if (bolen > max) max = bolen;
            if (kare > max) max = kare;
            foreach (var item in result)
            {
                if (item.Contains(min.ToString())) LabelSiralama.Text += "**En hızlı yöntem: " + item.ToString() + "ms ";
                else if (item.Contains(max.ToString())) LabelSiralama.Text += "**En yavaş yöntem: " + item.ToString() + "ms ";
            }
        }
        #endregion
    }
}




//public int ModValue(int sayi)
//{
//    int length=0;
//    while (sayi > 0)
//    {
//        length++;
//        sayi = sayi / 10;
//    };
//    return length;
//}



/*
 if (dt.Rows.Count > 0)
            {
                string filename = name + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dt.Columns[0].MaxLength = 400;
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
                Response.Charset = "windows-1254"; //ISO-8859-13 ISO-8859-9  windows-1254
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                string header = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title></title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1254\" />\n<style>\n</style>\n</head>\n<body>\n";
                this.EnableViewState = false;
                Response.Write(header + tw.ToString());
                Response.End();
            }
*/

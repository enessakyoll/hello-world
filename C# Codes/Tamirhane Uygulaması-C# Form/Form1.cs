using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Prolab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Tamirhane
        {
            public string durum { get; set; }
            public string usta { get; set; }
            public DateTime tarih { get; set; }
        }
        public class Kaporta : Tamirhane
        {
            public string degisenParca { get; set; }
            public int parcaFiyati { get; set; }
            public int iscilikFiyati { get; set; }
            public string bolum { get; set; } = "Kaporta";
        }

        public class Motor : Tamirhane
        {
            public string degisenParca { get; set; }
            public int parcaFiyati { get; set; }
            public int iscilikFiyati { get; set; }
            public string bolum { get; set; } = "Motor";
        }

        public class Elektrik : Tamirhane
        {
            public string degisenParca { get; set; }
            public int parcaFiyati { get; set; }
            public int iscilikFiyati { get; set; }
            public string bolum { get; set; } = "Elektrik";
        }

        public class Lastik : Tamirhane
        {
            public string degisenParca { get; set; }
            public int parcaFiyati { get; set; }
            public int iscilikFiyati { get; set; }
            public string bolum { get; set; } = "Lastik";
        }

        public class AracKaporta : Kaporta
        {
            public string plaka { get; set; }
            public string marka { get; set; }
            public string model { get; set; }
            public int yil { get; set; }

            private Kaporta kaporta;
            public AracKaporta(Kaporta kaporta)
            {
                this.kaporta = kaporta;
            }
            public AracKaporta() { }
            // Constructor:
            //public AracKaporta(string plaka, string marka, string model, int yil)
            //{
            //    AracKaporta.plaka = plaka;
            //    AracKaporta.marka = marka;
            //    AracKaporta.model = model;
            //    AracKaporta.yil = yil;
            //}

        }

        public class AracMotor : Motor
        {
            public string plaka { get; set; }
            public string marka { get; set; }
            public string model { get; set; }
            public int yil { get; set; }

            private Motor motor;
            public AracMotor(Motor motor)
            {
                this.motor = motor;
            }
            public AracMotor() { }
            // Constructor:
            //public AracMotor(string plaka, string marka, string model, int yil)
            //{
            //    AracMotor.plaka = plaka;
            //    AracMotor.marka = marka;
            //    AracMotor.model = model;
            //    AracMotor.yil = yil;
            //}

        }

        public class AracElektrik : Elektrik
        {
            public string plaka { get; set; }
            public string marka { get; set; }
            public string model { get; set; }
            public int yil { get; set; }

            private Elektrik elektrik;
            public AracElektrik(Elektrik elektrik)
            {
                this.elektrik = elektrik;
            }
            public AracElektrik() { }
            // Constructor:
            //public AracElektrik(string plaka, string marka, string model, int yil)
            //{
            //    AracElektrik.plaka = plaka;
            //    AracElektrik.marka = marka;
            //    AracElektrik.model = model;
            //    AracElektrik.yil = yil;
            //}
        }

        public class AracLastik : Lastik
        {
            public string plaka { get; set; }
            public string marka { get; set; }
            public string model { get; set; }
            public int yil { get; set; }

            private Lastik lastik;
            public AracLastik(Lastik lastik)
            {
                this.lastik = lastik;
            }
            public AracLastik() { }
            // Constructor:
            //public AracLastik(string plaka, string marka, string model, int yil)
            //{
            //    AracLastik.plaka = plaka;
            //    AracLastik.marka = marka;
            //    AracLastik.model = model;
            //    AracLastik.yil = yil;
            //}
        }

        static Object global = new Object(); //Global Nesne

        public static string dateformat = "";
        public static bool selected_index = true;

        private void buttonAra_Click(object sender, EventArgs e)
        {
            dateformat = "d.MM.yyyy HH:mm:ss";
            DataTable dt = new DataTable();
            dt.Columns.Add("aracPlaka");
            dt.Columns.Add("aracMarka");
            dt.Columns.Add("aracModel");
            dt.Columns.Add("aracYil");
            dt.Columns.Add("tarih");
            dt.Columns.Add("usta");
            dt.Columns.Add("tamirhaneBolum");
            dt.Columns.Add("tamirhaneDurum");
            dt.Columns.Add("bolumParca");
            dt.Columns.Add("bolumParcaFiyat");
            dt.Columns.Add("bolumIscilik");
            try
            {
                int count = 0;
                for (int i = 0; i < obje.Length; i++)
                {
                    if (obje[i] != null)
                    {
                        count++;
                    }
                    else break;
                }
                for (int i = 0; i < count; i++)
                {
                    Type t = obje[i].GetType();
                    string type = t.Name.ToString();
                    if (type == "AracKaporta")
                    {
                        AracKaporta kaporta;
                        kaporta = (AracKaporta)obje[i];
                        dt.Rows.Add();
                        dt.Rows[i]["aracPlaka"] = kaporta.plaka.ToString();
                        dt.Rows[i]["aracMarka"] = kaporta.marka;
                        dt.Rows[i]["aracModel"] = kaporta.model;
                        dt.Rows[i]["aracYil"] = kaporta.yil;
                        dt.Rows[i]["tarih"] = kaporta.tarih;
                        dt.Rows[i]["usta"] = kaporta.usta;
                        dt.Rows[i]["tamirhaneBolum"] = kaporta.bolum;
                        dt.Rows[i]["bolumParca"] = kaporta.degisenParca;
                        dt.Rows[i]["bolumParcaFiyat"] = kaporta.parcaFiyati;
                        dt.Rows[i]["bolumIscilik"] = kaporta.iscilikFiyati;
                        dt.Rows[i]["tamirhaneDurum"] = kaporta.durum;

                    }
                    else if (type == "AracMotor")
                    {
                        AracMotor motor;
                        motor = (AracMotor)obje[i];
                        dt.Rows.Add();
                        dt.Rows[i]["aracPlaka"] = motor.plaka.ToString();
                        dt.Rows[i]["aracMarka"] = motor.marka;
                        dt.Rows[i]["aracModel"] = motor.model;
                        dt.Rows[i]["aracYil"] = motor.yil;
                        dt.Rows[i]["tarih"] = motor.tarih;
                        dt.Rows[i]["usta"] = motor.usta;
                        dt.Rows[i]["tamirhaneBolum"] = motor.bolum;
                        dt.Rows[i]["bolumParca"] = motor.degisenParca;
                        dt.Rows[i]["bolumParcaFiyat"] = motor.parcaFiyati;
                        dt.Rows[i]["bolumIscilik"] = motor.iscilikFiyati;
                        dt.Rows[i]["tamirhaneDurum"] = motor.durum;
                    }
                    else if (type == "AracElektrik")
                    {
                        AracElektrik elektrik;
                        elektrik = (AracElektrik)obje[i];
                        dt.Rows.Add();
                        dt.Rows[i]["aracPlaka"] = elektrik.plaka.ToString();
                        dt.Rows[i]["aracMarka"] = elektrik.marka;
                        dt.Rows[i]["aracModel"] = elektrik.model;
                        dt.Rows[i]["aracYil"] = elektrik.yil;
                        dt.Rows[i]["tarih"] = elektrik.tarih;
                        dt.Rows[i]["usta"] = elektrik.usta;
                        dt.Rows[i]["tamirhaneBolum"] = elektrik.bolum;
                        dt.Rows[i]["bolumParca"] = elektrik.degisenParca;
                        dt.Rows[i]["bolumParcaFiyat"] = elektrik.parcaFiyati;
                        dt.Rows[i]["bolumIscilik"] = elektrik.iscilikFiyati;
                        dt.Rows[i]["tamirhaneDurum"] = elektrik.durum;
                    }
                    else if (type == "AracLastik")
                    {
                        AracLastik lastik;
                        lastik = (AracLastik)obje[i];
                        dt.Rows.Add();
                        dt.Rows[i]["aracPlaka"] = lastik.plaka.ToString();
                        dt.Rows[i]["aracMarka"] = lastik.marka;
                        dt.Rows[i]["aracModel"] = lastik.model;
                        dt.Rows[i]["aracYil"] = lastik.yil;
                        dt.Rows[i]["tarih"] = lastik.tarih;
                        dt.Rows[i]["usta"] = lastik.usta;
                        dt.Rows[i]["tamirhaneBolum"] = lastik.bolum;
                        dt.Rows[i]["bolumParca"] = lastik.degisenParca;
                        dt.Rows[i]["bolumParcaFiyat"] = lastik.parcaFiyati;
                        dt.Rows[i]["bolumIscilik"] = lastik.iscilikFiyati;
                        dt.Rows[i]["tamirhaneDurum"] = lastik.durum;
                    }
                    else MessageBox.Show("Obje Boş!");
                    dataGridViewKayitlar.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void buttonAraVT_Click(object sender, EventArgs e)
        {
            selected_index = false;
            dateformat = "dd.MM.yyyy HH:mm:ss";
            try
            {
                
                string plaka = textBoxArama.Text.ToString();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                string selectCommand = "select * from tamir where aracPlaka='" + plaka + "' order by tarih desc;";
                String connectionString = "Data Source=ABRA;Initial Catalog=otoTamirDB;Integrated Security=True";
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                dataGridViewKayitlar.DataSource = table;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        private void dataGridViewKayitlar_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewKayitlar.SelectedRows)
            {
                string plaka = row.Cells[0].Value.ToString();
                string tarih = row.Cells[4].Value.ToString();
                DateTime date = DateTime.ParseExact(tarih, dateformat, CultureInfo.InvariantCulture);
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                DataTable dt = new DataTable();


                //if (selected_index==false)
                //{
                //    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //    string selectCommand = "select * from tamir where aracPlaka='" + plaka + "' and tarih='" + formattedDate + "' order by tarih desc;";
                //    String connectionString = "Data Source=ABRA;Initial Catalog=otoTamirDB;Integrated Security=True";
                //    dataAdapter = new SqlDataAdapter(selectCommand, connectionString);
                //    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                //    dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //    dataAdapter.Fill(dt); 
                //}
                //else
                //{
                //    dt.Columns.Add("aracPlaka");
                //    dt.Columns.Add("aracMarka");
                //    dt.Columns.Add("aracModel");
                //    dt.Columns.Add("aracYil");
                //    dt.Columns.Add("tarih");
                //    dt.Columns.Add("usta");
                //    dt.Columns.Add("tamirhaneBolum");
                //    dt.Columns.Add("tamirhaneDurum");
                //    dt.Columns.Add("bolumParca");
                //    dt.Columns.Add("bolumParcaFiyat");
                //    dt.Columns.Add("bolumIscilik");
                //}
                //dataGridViewKayitlar.DataSource = dt;
                if (row.Cells.Count!=11)
                {
                    textBoxPlaka.Text = row.Cells[0].Value.ToString();
                    textBoxMarka.Text = row.Cells[1].Value.ToString();
                    textBoxModel.Text = row.Cells[2].Value.ToString();
                    textBoxYil.Text = row.Cells[3].Value.ToString();
                    string tarihVT = row.Cells[4].Value.ToString();
                    DateTime dateVT = DateTime.ParseExact(tarihVT, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    dateTimePicker1.Value = new DateTime(dateVT.Year, dateVT.Month, dateVT.Day, dateVT.Hour, dateVT.Minute, dateVT.Second);
                    comboBoxUsta.Text = row.Cells[5].Value.ToString();
                    comboBoxBolum.Text = row.Cells[6].Value.ToString();
                    textBoxDegisenParca.Text = row.Cells[7].Value.ToString();
                    textBoxParcaFiyat.Text = row.Cells[8].Value.ToString();
                    textBoxIscilik.Text = row.Cells[9].Value.ToString();
                }
                else
                {
                    textBoxPlaka.Text = row.Cells[0].Value.ToString();
                    textBoxMarka.Text = row.Cells[1].Value.ToString();
                    textBoxModel.Text = row.Cells[2].Value.ToString();
                    textBoxYil.Text = row.Cells[3].Value.ToString();
                    string tarihVT = row.Cells[4].Value.ToString();
                    DateTime dateVT = DateTime.ParseExact(tarihVT, dateformat, CultureInfo.InvariantCulture);
                    dateTimePicker1.Value = new DateTime(dateVT.Year, dateVT.Month, dateVT.Day, dateVT.Hour, dateVT.Minute, dateVT.Second);
                    comboBoxUsta.Text = row.Cells[5].Value.ToString();
                    comboBoxBolum.Text = row.Cells[6].Value.ToString();
                    comboBoxDurum.Text = row.Cells[7].Value.ToString();
                    textBoxDegisenParca.Text = row.Cells[8].Value.ToString();
                    textBoxParcaFiyat.Text = row.Cells[9].Value.ToString();
                    textBoxIscilik.Text = row.Cells[10].Value.ToString();
                    
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Bolum Doldur
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            string selectCommand = "select * from bolum";
            String connectionString = "Data Source=ABRA;Initial Catalog=otoTamirDB;Integrated Security=True";
            dataAdapter = new SqlDataAdapter(selectCommand, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            comboBoxBolum.DataSource = table;
            comboBoxBolum.DisplayMember = table.Columns["BolumTitle"].ToString();
            comboBoxBolum.ValueMember = table.Columns["BolumValue"].ToString();

            //Usta Doldur
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter();
            DataTable table2 = new DataTable();
            selectCommand = "select * from usta";
            dataAdapter2 = new SqlDataAdapter(selectCommand, connectionString);
            SqlCommandBuilder commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            table2.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter2.Fill(table2);
            comboBoxUsta.DataSource = table2;
            comboBoxUsta.DisplayMember = table2.Columns["AdıSoyadı"].ToString();
            comboBoxUsta.ValueMember = table2.Columns["AdıSoyadı"].ToString();

            //Durum Doldur
            SqlDataAdapter dataAdapter3 = new SqlDataAdapter();
            DataTable table3 = new DataTable();
            selectCommand = "select * from durum";
            dataAdapter3 = new SqlDataAdapter(selectCommand, connectionString);
            SqlCommandBuilder commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            table3.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter3.Fill(table3);
            comboBoxDurum.DataSource = table3;
            comboBoxDurum.DisplayMember = table3.Columns["Durum"].ToString();
            comboBoxDurum.ValueMember = table3.Columns["Durum"].ToString();
            //labelDurum.Text = comboBoxBolum.SelectedValue.ToString();
            
        }
        public static Object[] obje = new object[20];
        private void buttonKayıtOlustur_Click(object sender, EventArgs e)
        {
            string choosen = comboBoxBolum.Text;
            try
            {
                switch (choosen)
                {
                    case "Kaporta":
                        AracKaporta kaporta = new AracKaporta();
                        kaporta.plaka = textBoxPlaka.Text.ToUpper();
                        kaporta.marka = textBoxMarka.Text.ToUpper();
                        kaporta.model = textBoxModel.Text.ToUpper();
                        kaporta.yil = Int32.Parse(textBoxYil.Text);
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
                        kaporta.tarih = dateTimePicker1.Value;
                        kaporta.usta = comboBoxUsta.Text;
                        kaporta.degisenParca = textBoxDegisenParca.Text;
                        kaporta.parcaFiyati = Int32.Parse(textBoxParcaFiyat.Text);
                        kaporta.iscilikFiyati = Int32.Parse(textBoxIscilik.Text);
                        kaporta.durum = comboBoxDurum.Text;
                        for (int i = 0; i < obje.Length; i++)
                        {
                            if (obje[i] == null)
                            {
                                obje[i] = kaporta; break;
                            }
                            else if (obje[i] != null)
                            {
                                Type t = obje[i].GetType();
                                string type = t.Name.ToString();
                                if (type == "AracKaporta")
                                {
                                    AracKaporta kapTemp;
                                    kapTemp = (AracKaporta)obje[i];
                                    if (kapTemp.plaka == kaporta.plaka)
                                    {
                                        obje[i] = kaporta; break;
                                    }
                                }
                            }
                            else if (i == obje.Length - 1) MessageBox.Show("Nesne dizisi dolu!");
                        }
                        break;
                    case "Motor":
                        AracMotor motor = new AracMotor();
                        motor.plaka = textBoxPlaka.Text.ToUpper();
                        motor.marka = textBoxMarka.Text.ToUpper();
                        motor.model = textBoxModel.Text.ToUpper();
                        motor.yil = Int32.Parse(textBoxYil.Text);
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
                        motor.tarih = dateTimePicker1.Value;
                        motor.usta = comboBoxUsta.Text;
                        motor.degisenParca = textBoxDegisenParca.Text;
                        motor.parcaFiyati = Int32.Parse(textBoxParcaFiyat.Text);
                        motor.iscilikFiyati = Int32.Parse(textBoxIscilik.Text);
                        motor.durum = comboBoxDurum.Text;
                        for (int i = 0; i < obje.Length; i++)
                        {
                            if (obje[i] == null)
                            {
                                obje[i] = motor; break;
                            }
                            else if (obje[i] != null)
                            {
                                Type t = obje[i].GetType();
                                string type = t.Name.ToString();
                                if (type == "AracMotor")
                                {
                                    AracMotor motorTemp;
                                    motorTemp = (AracMotor)obje[i];
                                    if (motorTemp.plaka == motor.plaka)
                                    {
                                        obje[i] = motor; break;
                                    }
                                }
                            }
                            else if (i == obje.Length - 1) MessageBox.Show("Nesne dizisi dolu!");
                        }
                        break;

                    case "Elektrik":
                        AracElektrik elektrik = new AracElektrik();
                        elektrik.plaka = textBoxPlaka.Text.ToUpper();
                        elektrik.marka = textBoxMarka.Text.ToUpper();
                        elektrik.model = textBoxModel.Text.ToUpper();
                        elektrik.yil = Int32.Parse(textBoxYil.Text);
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
                        elektrik.tarih = dateTimePicker1.Value;
                        elektrik.usta = comboBoxUsta.Text;
                        elektrik.degisenParca = textBoxDegisenParca.Text;
                        elektrik.parcaFiyati = Int32.Parse(textBoxParcaFiyat.Text);
                        elektrik.iscilikFiyati = Int32.Parse(textBoxIscilik.Text);
                        elektrik.durum = comboBoxDurum.Text;
                        for (int i = 0; i < obje.Length; i++)
                        {

                            if (obje[i] == null)
                            {
                                obje[i] = elektrik; break;
                            }
                            else if (obje[i] != null)
                            {
                                Type t = obje[i].GetType();
                                string type = t.Name.ToString();
                                if (type == "AracElektrik")
                                {
                                    AracElektrik elekTemp;
                                    elekTemp = (AracElektrik)obje[i];
                                    if (elekTemp.plaka == elektrik.plaka)
                                    {
                                        obje[i] = elektrik; break;
                                    }
                                }
                            }
                            else if (i == obje.Length - 1) MessageBox.Show("Nesne dizisi dolu!");
                        }
                        break;
                    case "Lastik":
                        AracLastik lastik = new AracLastik();
                        lastik.plaka = textBoxPlaka.Text.ToUpper();
                        lastik.marka = textBoxMarka.Text.ToUpper();
                        lastik.model = textBoxModel.Text.ToUpper();
                        lastik.yil = Int32.Parse(textBoxYil.Text);
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
                        lastik.tarih = dateTimePicker1.Value;
                        lastik.usta = comboBoxUsta.Text;
                        lastik.degisenParca = textBoxDegisenParca.Text;
                        lastik.parcaFiyati = Int32.Parse(textBoxParcaFiyat.Text);
                        lastik.iscilikFiyati = Int32.Parse(textBoxIscilik.Text);
                        lastik.durum = comboBoxDurum.Text;
                        for (int i = 0; i < obje.Length; i++)
                        {

                            if (obje[i] == null)
                            {
                                obje[i] = lastik; break;
                            }
                            else if (obje[i] != null)
                            {
                                Type t = obje[i].GetType();
                                string type = t.Name.ToString();
                                if (type == "AracLastik")
                                {
                                    AracLastik lastikTemp;
                                    lastikTemp = (AracLastik)obje[i];
                                    if (lastikTemp.plaka == lastik.plaka)
                                    {
                                        obje[i] = lastik; break;
                                    }
                                }
                            }
                            else if (i == obje.Length - 1) MessageBox.Show("Nesne dizisi dolu!");
                        }
                        break;
                }
                labelNesneAlert.Text = "*Başarılı";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata!! Exception:" + ex.InnerException.Message.ToString());
            }
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string insertPlaka = textBoxPlaka.Text.ToUpper();
                string query = "INSERT INTO tamir(aracPlaka,aracMarka,aracModel,aracYil,tarih,usta,tamirhaneBolum,bolumParca,bolumParcaFiyat,bolumIscilik,odenenPara) VALUES (@aracPlaka,@aracMarka,@aracModel,@aracYil,@tarih,@usta,@tamirhaneBolum,@bolumParca,@bolumParcaFiyat,@bolumIscilik,@odenenPara)";
                SqlConnection con = new SqlConnection("Data Source=ABRA;Initial Catalog=otoTamirDB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(query, con);
                //baglanti.Open();

                if (obje[0] != null)
                {
                    for (int i = 0; i < obje.Length; i++)
                    {
                        Type t = obje[i].GetType();
                        string type = t.Name.ToString();
                        if (type == "AracKaporta")
                        {
                            AracKaporta kaporta;
                            kaporta = (AracKaporta)obje[i];
                            if (kaporta.plaka == insertPlaka)
                            {
                                cmd.Parameters.AddWithValue("@aracPlaka", kaporta.plaka);
                                cmd.Parameters.AddWithValue("@aracMarka", kaporta.marka);
                                cmd.Parameters.AddWithValue("@aracModel", kaporta.model);
                                cmd.Parameters.AddWithValue("@aracYil", kaporta.yil);
                                cmd.Parameters.AddWithValue("@tarih", kaporta.tarih);
                                cmd.Parameters.AddWithValue("@usta", kaporta.usta);
                                cmd.Parameters.AddWithValue("@tamirhaneBolum", kaporta.bolum);
                                cmd.Parameters.AddWithValue("@bolumParca", kaporta.degisenParca);
                                cmd.Parameters.AddWithValue("@bolumParcaFiyat", kaporta.parcaFiyati);
                                cmd.Parameters.AddWithValue("@bolumIscilik", kaporta.iscilikFiyati);
                                cmd.Parameters.AddWithValue("@odenenPara", kaporta.parcaFiyati + kaporta.iscilikFiyati);
                                obje[i] = null;
                                break;
                            }
                        }
                        if (type == "AracMotor")
                        {
                            AracMotor motor;
                            motor = (AracMotor)obje[i];
                            if (motor.plaka == insertPlaka)
                            {
                                cmd.Parameters.AddWithValue("@aracPlaka", motor.plaka);
                                cmd.Parameters.AddWithValue("@aracMarka", motor.marka);
                                cmd.Parameters.AddWithValue("@aracModel", motor.model);
                                cmd.Parameters.AddWithValue("@aracYil", motor.yil);
                                cmd.Parameters.AddWithValue("@tarih", motor.tarih);
                                cmd.Parameters.AddWithValue("@usta", motor.usta);
                                cmd.Parameters.AddWithValue("@tamirhaneBolum", motor.bolum);
                                cmd.Parameters.AddWithValue("@bolumParca", motor.degisenParca);
                                cmd.Parameters.AddWithValue("@bolumParcaFiyat", motor.parcaFiyati);
                                cmd.Parameters.AddWithValue("@bolumIscilik", motor.iscilikFiyati);
                                cmd.Parameters.AddWithValue("@odenenPara", motor.parcaFiyati + motor.iscilikFiyati);
                                obje[i] = null;
                                break;
                            }
                        }
                        if (type == "AracElektrik")
                        {
                            AracElektrik elektrik;
                            elektrik = (AracElektrik)obje[i];
                            if (elektrik.plaka == insertPlaka)
                            {
                                cmd.Parameters.AddWithValue("@aracPlaka", elektrik.plaka);
                                cmd.Parameters.AddWithValue("@aracMarka", elektrik.marka);
                                cmd.Parameters.AddWithValue("@aracModel", elektrik.model);
                                cmd.Parameters.AddWithValue("@aracYil", elektrik.yil);
                                cmd.Parameters.AddWithValue("@tarih", elektrik.tarih);
                                cmd.Parameters.AddWithValue("@usta", elektrik.usta);
                                cmd.Parameters.AddWithValue("@tamirhaneBolum", elektrik.bolum);
                                cmd.Parameters.AddWithValue("@bolumParca", elektrik.degisenParca);
                                cmd.Parameters.AddWithValue("@bolumParcaFiyat", elektrik.parcaFiyati);
                                cmd.Parameters.AddWithValue("@bolumIscilik", elektrik.iscilikFiyati);
                                cmd.Parameters.AddWithValue("@odenenPara", elektrik.parcaFiyati + elektrik.iscilikFiyati);
                                obje[i] = null;
                                break;
                            }
                        }
                        if (type == "AracLastik")
                        {
                            AracLastik lastik;
                            lastik = (AracLastik)obje[i];
                            if (lastik.plaka == insertPlaka)
                            {
                                cmd.Parameters.AddWithValue("@aracPlaka", lastik.plaka);
                                cmd.Parameters.AddWithValue("@aracMarka", lastik.marka);
                                cmd.Parameters.AddWithValue("@aracModel", lastik.model);
                                cmd.Parameters.AddWithValue("@aracYil", lastik.yil);
                                cmd.Parameters.AddWithValue("@tarih", lastik.tarih);
                                cmd.Parameters.AddWithValue("@usta", lastik.usta);
                                cmd.Parameters.AddWithValue("@tamirhaneBolum", lastik.bolum);
                                cmd.Parameters.AddWithValue("@bolumParca", lastik.degisenParca);
                                cmd.Parameters.AddWithValue("@bolumParcaFiyat", lastik.parcaFiyati);
                                cmd.Parameters.AddWithValue("@bolumIscilik", lastik.iscilikFiyati);
                                cmd.Parameters.AddWithValue("@odenenPara", lastik.parcaFiyati + lastik.iscilikFiyati);
                                obje[i] = null;
                                break;
                            }
                        }
                    }
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    // Sorgu çalıştırılır:
                    cmd.ExecuteNonQuery();
                    // Bağlantı kapatılır:
                    con.Close();
                    labelDBAlert.Text = "Başarıyla kaydedildi.";
                }
                else MessageBox.Show("Obje Boş!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanına kayıtta hata. Exception: " + ex.InnerException.Message.ToString());
            }
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            textBoxPlaka.Text = "";
            textBoxMarka.Text = "";
            textBoxModel.Text = "";
            textBoxYil.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            comboBoxUsta.SelectedIndex = 0;
            comboBoxBolum.SelectedIndex = 0;
            textBoxDegisenParca.Text = "";
            textBoxParcaFiyat.Text = "";
            textBoxIscilik.Text = "";
            comboBoxDurum.SelectedIndex = 0;
            labelNesneAlert.Text = "";
            labelDBAlert.Text = "";
        }







        //connectionString = "Data Source=ABRA;Initial Catalog=otoTamirDB;Integrated Security=True"
        //string value = textBoxPlaka.Text;
        //AracElektrik arac = new AracElektrik(textBoxPlaka.Text.ToString(), textBoxMarka.Text.ToString(), textBoxModel.Text.ToString(), Int32.Parse(textBoxYil.Text.ToString()));
        //obje = arac;
        //AracElektrik arac2;
        //arac2 = (AracElektrik)obje;
        //Object[] deger = new object[20];
        //deger[0] = arac;
        //Type t = obje.GetType();
        //string x = t.Name.ToString();
    }


}


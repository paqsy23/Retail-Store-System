using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace PROYEK_SDP
{
    public partial class Pegawai : Form
    {
        OracleConnection conn = new OracleConnection(" user id=admin1;password=admin;");
        public Pegawai()
        {
            InitializeComponent();
            conn.Open();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Pegawai_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            //this.Width = 1000;
            label1.Text = "Nama       :";
            label2.Text = "Password   :";
            label3.Text = "Jabatan    :";
            label4.Text = "Alamat     :";
            label5.Text = "Nomor tlpn :";

            label11.Text = "ID_Pegawai :";

            button2.Location = new Point(button2.Location.X, button1.Location.Y);
            button3.Location = new Point(button3.Location.X, button1.Location.Y);
            button4.Location = new Point(button4.Location.X, button1.Location.Y);

            tampilPegawai();
        }

        private void tampilPegawai()
        {
            OracleCommand cmd = new OracleCommand("select * from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            textBox11.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            comboBox1.Enabled = true;
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select id_pegawai from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex != -1 && textBox4.Text != "" && textBox5.Text != "" && textBox5.Text.Length > 7)
            {
                int counter = 1;
                String id = comboBox1.SelectedItem.ToString().Substring(0, 3);
                id = id.ToUpper();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["id_pegawai"].ToString().Contains(id))
                    {
                        counter = Int32.Parse(row["id_pegawai"].ToString().Substring(3)) + 1;
                    }
                }
                String nama = textBox1.Text;
                id = id + counter.ToString().PadLeft(3, '0');
                Boolean cek_nomor = true;
                try
                {
                    Int32.Parse(textBox5.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nomor Telpn hanya boleh angka");
                    cek_nomor = false;
                }
                if (cek_nomor == true)
                {
                    try
                    {
                        OracleCommand command = new OracleCommand();
                        command.Connection = conn;
                        command.CommandText = "insert into pegawai(id_pegawai, nama_pegawai, jabatan, alamat_pegawai, password, nomor_telp) values(:id_pegawai, :nama_pegawai, :jabatan, :alamat_pegawai, :password, :nomor_telp)";
                        command.Parameters.Add("id_pegawai", id);
                        command.Parameters.Add("nama_pegawai", nama);
                        command.Parameters.Add("jabatan", comboBox1.SelectedItem.ToString());
                        command.Parameters.Add("alamat_pegawai", textBox4.Text);
                        command.Parameters.Add("password", textBox2.Text);
                        command.Parameters.Add("nomor_telp", textBox5.Text);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            tampilPegawai();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean cek_nomor = true;
            try
            {
                Int32.Parse(textBox5.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Nomor Telpn hanya boleh angka");
                cek_nomor = false;
            }

            if (cek_nomor == true)
            {
                String nama = textBox1.Text;
                OracleCommand command = new OracleCommand();
                command.Connection = conn;
                String update = "update pegawai set nama_pegawai = : nama_pegawai, alamat_pegawai = :alamat_pegawai, password = :password, nomor_telp = :nomor_telp where id_pegawai = '" + textBox11.Text + "'";
                command.CommandText = update;
                command.Parameters.Add("nama_pegawai", nama);
                command.Parameters.Add("alamat_pegawai", textBox4.Text);
                command.Parameters.Add("password", textBox2.Text);
                command.Parameters.Add("nomor_telp", textBox5.Text);

                command.ExecuteNonQuery();
            }
            tampilPegawai();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            String delete = "delete from pegawai where id_pegawai = '" + textBox11.Text + "'";
            cmd.CommandText = delete;
            cmd.ExecuteNonQuery();
            tampilPegawai();
            button1.Enabled = !false;
            button2.Enabled = !true;
            button3.Enabled = !true;
            comboBox1.Enabled = !false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString())
                {
                    comboBox1.SelectedIndex = i;
                }
            }
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            comboBox1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tampilPegawai();
        }
    }
}

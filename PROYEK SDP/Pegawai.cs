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
            label1.Text = "Nama       :";
            label2.Text = "Password   :";
            label3.Text = "Jabatan    :";
            label4.Text = "Alamat     :";
            label5.Text = "Nomor tlpn :";

            label11.Text = "ID_Pegawai :";
            label6.Text = "Nama       :";
            label7.Text = "Password   :";
            label8.Text = "Jabatan    :";
            label9.Text = "Alamat     :";
            label10.Text = "Nomor tlpn :";

            tampilPegawai();
        }

        private void tampilPegawai()
        {
            OracleCommand cmd = new OracleCommand("select * from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select id_pegawai from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        if (textBox4.Text != "")
                        {
                            if (textBox5.Text != "" && textBox5.Text.Length>10)
                            {
                                int counter = 1;
                                String id = textBox3.Text.Substring(0, 3);
                                id = id.ToUpper();
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["id_pegawai"].ToString().Contains(id))
                                    {
                                        counter++;
                                    }
                                }
                                String nama=textBox1.Text;
                                //if (nama.Contains("'"))
                                //{
                                //    nama = textBox1.Text.Substring(0, textBox1.Text.IndexOf("'")) + "'" + textBox1.Text.Substring(textBox1.Text.IndexOf("'"));
                                //}
                                //MessageBox.Show(nama);
                                id = id + counter;
                                //String insert = "insert into pegawai values(lpad('" + id + "',3,'0'), '" + nama + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + textBox5.Text + "')";
                                //cmd.CommandText = insert;
                                try
                                {
                                    OracleCommand command = new OracleCommand();
                                    command.Connection = conn;
                                    command.CommandText = "insert into pegawai(id_pegawai, nama_pegawai, jabatan, alamat_pegawai, password, nomor_telp) values(:id_pegawai, :nama_pegawai, :jabatan, :alamat_pegawai, :password, :nomor_telp)";
                                    command.Parameters.Add("id_pegawai", id);
                                    command.Parameters.Add("nama_pegawai", nama);
                                    command.Parameters.Add("jabatan", textBox3.Text);
                                    command.Parameters.Add("alamat_pegawai", textBox4.Text);
                                    command.Parameters.Add("password", textBox2.Text);
                                    command.Parameters.Add("nomor_telp", textBox5.Text);
                                    
                                    
                                    //command.Parameters.Add()
                                    MessageBox.Show(command.Parameters[0].Value.ToString());
                                    command.ExecuteNonQuery();
                                    tampilPegawai();
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show(ex.Message.ToString());
                                }
                                
                            }
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand("select id_pegawai from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            String update = "update pegawai set nama_pegawai = '" + textBox6.Text + "', alamat_pegawai = '" + textBox9.Text + "', password = '" + textBox7.Text + "', nomor_telp = '" + textBox10.Text + "' where id_pegawai = '"+textBox11.Text+"'";
            cmd.CommandText = update;
            cmd.ExecuteNonQuery();
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
            MessageBox.Show(cmd.CommandText);
        }
    }
}

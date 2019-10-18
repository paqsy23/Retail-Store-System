using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        OracleConnection conn = new OracleConnection(" user id=n217116624;password=217116624;");
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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
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
                            if (textBox5.Text != "" && textBox5.Text.Length>8)
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
                                
                                String insert = "insert into pegawai values('"+id+"'||lpad('" + counter + "',3,'0'), '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + textBox5.Text + "')";
                                cmd.CommandText = insert;
                                cmd.ExecuteNonQuery();
                                tampilPegawai();
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
                OracleCommand cmd = new OracleCommand("select id_pegawai from pegawai", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                String delete = "delete from pegawai where id_pegawai = '" + textBox11.Text + "'";
                cmd.CommandText = delete;
                cmd.ExecuteNonQuery();
                tampilPegawai();
                MessageBox.Show(cmd.CommandText);
        }
    }
}

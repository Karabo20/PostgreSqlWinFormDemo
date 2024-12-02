using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSqlWinFormDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        string connString = "Server=localhost; Database=DataCamp_Courses; Port=5432; User Id=postgres; Password=123456";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Records> entries = new List<Records>();
            using (NpgsqlConnection connect = new NpgsqlConnection(connString))
            {
                connect.Open();

                using (NpgsqlCommand query = new NpgsqlCommand($"select * from Learning where instructor = @Instructor", connect))
                {
                    query.Parameters.AddWithValue("@Instructor", "Al Sweigart");
                    using (NpgsqlDataReader reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Records record = new Records
                            {
                                ID = reader.GetInt32(0),
                                Course = reader.GetString(1),
                                Instructor = reader.GetString(2),
                                Duration = reader.GetInt32(3)
                            };

                            entries.Add(record);
                        }
                    } dataGridView1.DataSource = entries;   
                       
                }

            }

        }
    }
}

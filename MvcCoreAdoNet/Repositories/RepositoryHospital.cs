using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Hospital> GetHospitales()
        {
            List<Hospital> hospitales = new List<Hospital>();

            string query = "select * from HOSPITAL";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = query;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            while (reader.Read()) {
                Hospital hospital = new Hospital();
                hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                hospital.Nombre = this.reader["NOMBRE"].ToString();
                hospital.Direccion = this.reader["DIRECCION"].ToString();
                hospital.Telefono = this.reader["TELEFONO"].ToString();
                hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
                hospitales.Add(hospital);
            }

            this.reader.Close();
            this.cn.Close();
            return hospitales;
        }

        public Hospital FindHospital(int idHospital)
        {
            int id = idHospital;
            string query = "select * from HOSPITAL where HOSPITAL_COD = @idhospital";
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = query;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            Hospital hospital = new Hospital();
            hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = this.reader["NOMBRE"].ToString();
            hospital.Direccion = this.reader["DIRECCION"].ToString();
            hospital.Telefono = this.reader["TELEFONO"].ToString();
            hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());

            this.cn.Close();
            this.reader.Close();
            this.com.Parameters.Clear();

            return hospital;
        }

        public void CreateHospital(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            string query = "insert into HOSPITAL values (@idHospital, @nombre, @direccion, @telefono, @camas)";
            this.cn.Open();
            this.com.Parameters.AddWithValue("@idHospital", idHospital);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = query;
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdateHospital(int idHospital, string nombre, string direccion, string telefono, int camas) {

            string query = "update HOSPITAL set NOMBRE=@nombre , DIRECCION=@direccion, TELEFONO=@telefono, NUM_CAMA=@camas" +
                " where HOSPITAL_COD=@idhospital";
            this.com.CommandText = query;
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = System.Data.CommandType.Text;
            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteHospital(int idHospital)
        {
            string query = "delete from HOSPITAL where HOSPITAL_COD=@idhospital";
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandText = query;
            this.com.CommandType = System.Data.CommandType.Text;
            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}

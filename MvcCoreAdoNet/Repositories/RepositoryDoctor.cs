using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryDoctor
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryDoctor()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            List<Doctor> doctores = new List<Doctor>();
            string query = "select * from DOCTOR";
            this.com.CommandText = query;
            this.com.CommandType = System.Data.CommandType.Text;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();

                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(string especialidad)
        {
            List<Doctor> doctores = new List<Doctor>();
            string query = "select * from DOCTOR where ESPECIALIDAD=@especialidad";
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = query;
            await this.cn.OpenAsync();
            this.reader = this.com.ExecuteReader();
            while (await this.reader.ReadAsync())
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();

                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }


        public async Task<List<string>> GetEspecialidadesAsync()
        {
            string query = "select distinct ESPECIALIDAD from DOCTOR";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = query;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<string> especialidades = new List<string>();
            while (await this.reader.ReadAsync()) 
            {
                especialidades.Add(this.reader["ESPECIALIDAD"].ToString());
            }

            await this.cn.CloseAsync();
            await this.reader.CloseAsync();
            return especialidades;

        }


    }


}

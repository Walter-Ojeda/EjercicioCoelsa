using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Implementaciones
{
    public class DatosContacts : Datos, IDatosContacts
    {
        private static readonly string[] FirstNames = new[]
        {
            "Mauricio", "Gabriel", "Guillermo", "Matias", "German", "Noelia", "Paulo", "Cristiano", "Filipe", "Samuel"
        };

        private static readonly string[] LastNames = new[]
       {
            "Perez", "Thomsons", "Martinez", "Balcas", "Gimenez", "Perez"
        };

        private static readonly string[] Companyes = new[]
        {
            "Twitter", "Ford", "Mercado Libre", "Techint", "Google", "Facebook"
        };

        //Ejecuto stored procedure sp_contacts_delete_by_id
        public void Delete(int id)
        {
            SqlConnection connection = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_delete_by_id", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }
        }

        //Ejecuto stored procedure sp_contacts_get_all
        public List<Contact> GetAll()
        {
            SqlConnection connection = null;

            List<Contact> contacts = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_get_all", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        contacts = new List<Contact>();
                        foreach (DataRow row in dt.Rows)
                        {
                            contacts.Add(MapDataRow(row));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }

            return contacts;
        }



        //Ejecuto stored procedure sp_contacts_get_by_id
        public Contact GetById(int id)
        {
            SqlConnection connection = null;

            Contact contact = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_get_by_id", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        contact = MapDataRow(dt.Rows[0]);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }

            return contact;
        }

        //Ejecuto stored procedure sp_contacts_get_by_filter
        public List<Contact> GetListByFilter(Filtro<Contact> filtro, out int totalRows)
        {
            SqlConnection connection = null;
            List<Contact> contacts = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_get_by_filter", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", filtro.Map.FirstName);
                command.Parameters.AddWithValue("@LastName", filtro.Map.LastName);
                command.Parameters.AddWithValue("@Company", filtro.Map.Company);
                command.Parameters.AddWithValue("@Email", filtro.Map.Email);
                command.Parameters.AddWithValue("@PhoneNumber", filtro.Map.PhoneNumber);
                command.Parameters.AddWithValue("@PageIndex", filtro.PageIndex);
                command.Parameters.AddWithValue("@PageSize", filtro.PageSize);
                command.Parameters.Add("@TotalRows", SqlDbType.Int).Direction = ParameterDirection.Output;

                //SqlParameter parameterTotalRows = new SqlParameter();
                //parameterTotalRows.ParameterName = "@TotalRows";
                //parameterTotalRows.DbType = DbType.Int32;
                //parameterTotalRows.Direction = ParameterDirection.Output;

                //command.Parameters.Add(parameterTotalRows);                

                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        contacts = new List<Contact>();
                        foreach (DataRow row in dt.Rows)
                        {
                            contacts.Add(MapDataRow(row));
                        }
                    }
                }

                totalRows = (int)command.Parameters["@TotalRows"].Value;

            }
            catch (Exception ex)
            {
                totalRows = 0;
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }

            return contacts;
        }

        //Ejecuto stored procedure sp_contacts_insert
        public int Insert(Contact entidad)
        {
            int newContactId = 0;
            SqlConnection connection = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_insert", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", entidad.FirstName);
                command.Parameters.AddWithValue("@LastName", entidad.LastName);
                command.Parameters.AddWithValue("@Company", entidad.Company);
                command.Parameters.AddWithValue("@Email", entidad.Email);
                command.Parameters.AddWithValue("@PhoneNumber", entidad.PhoneNumber);
                newContactId = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }

            return newContactId;
        }

        //Ejecuto stored procedure sp_contacts_update
        public Contact Update(Contact entidad)
        {
            SqlConnection connection = null;
            Contact contactUpdated = null;
            try
            {
                this.connection.OpenConnection();
                connection = this.connection.GetConnection();
                SqlCommand command = new SqlCommand("sp_contacts_update", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", entidad.Id);
                command.Parameters.AddWithValue("@FirstName", entidad.FirstName);
                command.Parameters.AddWithValue("@LastName", entidad.LastName);
                command.Parameters.AddWithValue("@Company", entidad.Company);
                command.Parameters.AddWithValue("@Email", entidad.Email);
                command.Parameters.AddWithValue("@PhoneNumber", entidad.PhoneNumber);

                int rowsUpdated = command.ExecuteNonQuery();

                if (rowsUpdated == 1)
                    contactUpdated = entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.CloseConnection();
            }

            return contactUpdated;
        }

        private Contact MapDataRow(DataRow row)
        {
            Contact contact = new Contact();
            contact.Id = int.Parse(row["Id"].ToString());
            contact.FirstName = row["FirstName"].ToString();
            contact.LastName = row["LastName"].ToString();
            contact.Company = row["Company"].ToString();
            contact.PhoneNumber = row["PhoneNumber"].ToString();
            contact.Email = row["Email"].ToString();

            return contact;
        }
    }
}

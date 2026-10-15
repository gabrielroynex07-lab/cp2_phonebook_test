using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace RegistrationNew_224102915
{
    public class ContactRepository
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["RegistrationDb"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'RegistrationDb' not found in App.config.");
        }

        private static object DbValue(string value)
        {
            return value == null ? (object)DBNull.Value : value;
        }

        public List<Contact> GetAll()
        {
            var contacts = new List<Contact>();

            const string sql =
                "SELECT ContactId, UserId, FirstName, LastName, PhoneNumber, Email, Address, Notes, " +
                "CreatedAt, UpdatedAt, Category " +
                "FROM dbo.Contacts WHERE UserId = @UserId ORDER BY LastName, FirstName";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", Session.UserId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            ContactId = (int)reader["ContactId"],
                            UserId = (int)reader["UserId"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Email = reader["Email"] as string,
                            Address = reader["Address"] as string,
                            Notes = reader["Notes"] as string,
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = reader["UpdatedAt"] as DateTime?,
                            Category = reader["Category"] as string,
                        });
                    }
                }
            }

            return contacts;
        }

        public void Insert(Contact c)
        {
            const string sql =
                "INSERT INTO dbo.Contacts (UserId, FirstName, LastName, PhoneNumber, Email, Address, Notes, Category) " +
                "OUTPUT INSERTED.ContactId " +
                "VALUES (@UserId, @FirstName, @LastName, @PhoneNumber, @Email, @Address, @Notes, @Category )";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", c.UserId);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", c.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", DbValue(c.Email));
                cmd.Parameters.AddWithValue("@Address", DbValue(c.Address));
                cmd.Parameters.AddWithValue("@Notes", DbValue(c.Notes));
                cmd.Parameters.AddWithValue("@Category", DbValue(c.Category));
                conn.Open();
                c.ContactId = (int)cmd.ExecuteScalar();
            }
        }

        public int Update(Contact c)
        {
            const string sql =
                "UPDATE dbo.Contacts SET FirstName = @FirstName, LastName = @LastName, " +
                "PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address, Notes = @Notes, " +
                "Category = @Category, UpdatedAt = SYSUTCDATETIME() " +
                "WHERE ContactId = @ContactId AND UserId = @UserId";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                cmd.Parameters.AddWithValue("@UserId", Session.UserId);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", c.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", DbValue(c.Email));
                cmd.Parameters.AddWithValue("@Address", DbValue(c.Address));
                cmd.Parameters.AddWithValue("@Notes", DbValue(c.Notes));
                cmd.Parameters.AddWithValue("@Category", DbValue(c.Category));
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int contactId)
        {
            const string sql =
                "DELETE FROM dbo.Contacts WHERE ContactId = @ContactId AND UserId = @UserId";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ContactId", contactId);
                cmd.Parameters.AddWithValue("@UserId", Session.UserId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
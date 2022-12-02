using DAL.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MemberRepository : RepositoryBase<int, Member>, IMember
    {
        public MemberRepository(IDbConnection connection) : base(connection)
        {
        }

        public override int Add(Member entity)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO MEMBER (Email, Pseudo , Pwd_Hash, FirstName, LastName , Birthdate) OUTPUT [inserted].[Member_id] VALUES (@Email, @Pseudo, @Psw, @FirstName, @LastName, @BirthDate)";
            NewMethod(entity.Email, "@Email", cmd);
            NewMethod(entity.Pseudo, "@Pseudo", cmd);
            NewMethod(entity.hashPsw, "@Psw", cmd);
            NewMethod(entity.FirstName, "@FirstName", cmd);
            NewMethod(entity.LastName, "@LastName", cmd);
            NewMethod(entity.BirthDate, "@BirthDate", cmd);
 

            _connection.Open();
            int id = (int)cmd.ExecuteScalar();
            _connection.Close();
            return id;

        }

        public override bool Delete(int id)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM MEMBER WHERE Member_id=@Id";
            NewMethod(id, "@Id", cmd);
            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result == 1;
        }

        private static void NewMethod(object? value, string name, IDbCommand cmd)
        {
            IDbDataParameter DeleteParam = cmd.CreateParameter();
            DeleteParam.ParameterName = name;
            DeleteParam.Value = value ?? DBNull.Value;

            cmd.Parameters.Add(DeleteParam);
        }

        public override IEnumerable<Member> GetAll()
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member";
            _connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader);
                }
            }
            _connection.Close();
        }

        public override Member GetById(int id)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member WHERE Member_id= @Id";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@Id";
            IdParam.Value = id;
            command.Parameters.Add(IdParam);
            _connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Mapper(reader);
                }
                else
                {
                    return null;
                }
            }
            _connection.Close();
        }

        public Member? getByIdentifiant(string identifiant)
        {
            return getByIdentifiant(identifiant, identifiant);
        }
        public Member? getByIdentifiant(string pseudo, string email)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member WHERE Pseudo= @identifiant OR Email= @email";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@identifiant";
            IdParam.Value = pseudo;
            command.Parameters.Add(IdParam);

            IDbDataParameter emailParam = command.CreateParameter();
            emailParam.ParameterName = "@email";
            emailParam.Value = email;
            command.Parameters.Add(emailParam);
            _connection.Open();
            Member member = null;
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    member = Mapper(reader);
                }

            }
           
            _connection.Close();
            return member;
        }

        public Member? GetByEmail(string email)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member WHERE Email= @email";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@email";
            IdParam.Value = email;
            command.Parameters.Add(IdParam);
            _connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Mapper(reader);
                }
                else
                {
                    return null;
                }
            }
            _connection.Close();
        }

        public override bool Update(Member entity)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Member SET Email=@Email, Pseudo = @Pseudo, FirstName = @FirstName, LastName = @LastName , BirthDate=@BirthDate WHERE Member_Id= @id";


            NewMethod(entity.Email, "@Email", cmd);
            NewMethod(entity.Pseudo, "@Pseudo", cmd);
            NewMethod(entity.FirstName, "@FirstName", cmd);
            NewMethod(entity.LastName, "@LastName", cmd);
            NewMethod(entity.BirthDate, "@BirthDate", cmd);
            NewMethod(entity.MemberId, "@id", cmd);
            _connection.Open();
            int result =cmd.ExecuteNonQuery();
            _connection.Close();

            return result==1;
        }

        protected override Member Mapper(IDataRecord record)
        {
            return new Member()
            {
                MemberId = (int)record["Member_ID"],
                Pseudo = (string)record["Pseudo"],
                Email = (string)record["Email"],
                FirstName= (string)record["FirstName"],
                LastName= (string)record["LastName"],
                BirthDate= (DateTime)record["BirthDate"]
            };
        }

        public string? GetHashPwd(string identifiant)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Pwd_Hash  FROM Member WHERE Pseudo= @identifiant OR Email=@identifiant";

            IDbDataParameter pseudoParam = command.CreateParameter();
            pseudoParam.ParameterName = "@identifiant";
            pseudoParam.Value = identifiant;
            command.Parameters.Add(pseudoParam);

            _connection.Open();

            string? hashPaw = (string?)command.ExecuteScalar();

            _connection.Close();
            return hashPaw;
        }

        public void UpdateProfile(Member data, int id)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Member SET Email=@Email, Pseudo = @Pseudo, FirstName = @FirstName, LastName = @LastName , BirthDate=@BirthDate ,Pwd_Hash=@Psw  WHERE Member_Id= @id";


            NewMethod(data.Email, "@Email", cmd);
            NewMethod(data.Pseudo, "@Pseudo", cmd);
            NewMethod(data.FirstName, "@FirstName", cmd);
            NewMethod(data.LastName, "@LastName", cmd);
            NewMethod(data.BirthDate, "@BirthDate", cmd);
            NewMethod(data.hashPsw, "@Psw", cmd);
            NewMethod(id, "@id", cmd);
            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public Member? getByIdentifiantAdmin(string identifiant)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member WHERE (Pseudo= @identifiant OR Email= @identifiant) AND (isAdmin=1)";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@identifiant";
            IdParam.Value = identifiant;
            command.Parameters.Add(IdParam);

     
            _connection.Open();
            Member member = null;
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    member = Mapper(reader);
                }

            }

            _connection.Close();
            return member;
        }

        public IEnumerable<Member> getListeCompte()
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Member WHERE isAdmin=0 ";
            _connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader);
                }
            }
            _connection.Close();
        }

        public void DeleteAdmin(int ide)
        {
           

            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Member WHERE Member_ID=@Id";
            NewMethod(ide, "@Id", cmd);

            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}

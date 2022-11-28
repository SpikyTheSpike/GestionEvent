using DAL.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EvenetRepository : RepositoryBase<int, Evenement> , IEvenet
    {
        public EvenetRepository(IDbConnection connection) : base(connection)
        {
        }

        public override int Add(Evenement entity)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                "INSERT INTO Event (Member_Id , Nom, Description , DateDebut, DateFin, Photo , LimitePersonne) " +
                "OUTPUT [inserted].[Event_Id] " +
                "VALUES (@Member_Id, @Nom, @Description, @DateDebut, @DateFin, @Photo, @LimitePersonne)";
            NewMethod(entity.Member_Id, "@Member_Id", cmd);
            NewMethod(entity.Nom, "@Nom", cmd);
            NewMethod(entity.Description, "@Description", cmd);
            NewMethod(entity.DateDebut, "@DateDebut", cmd);
            NewMethod(entity.DateFin, "@DateFin", cmd);
            NewMethod(entity.Photo, "@Photo", cmd);
            NewMethod(entity.LimitPlace, "@LimitePersonne", cmd);


            _connection.Open();
            int id = (int)cmd.ExecuteScalar();
            _connection.Close();
            return id;
        }

        public override bool Delete(int id)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Event WHERE Event_Id=@Id";
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

        public override IEnumerable<Evenement> GetAll()
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Event";
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

        public override Evenement? GetById(int id)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Event WHERE Event_Id= @id";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@id";
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

        public override bool Update(Evenement entity)
        {
            throw new NotImplementedException();
        }

        protected override Evenement Mapper(IDataRecord record)
        {
            return new Evenement()
            {
                Event_Id = (int)record["Event_Id"],
                Member_Id = (int)record["Member_Id"],
                Nom = (string)record["Nom"],
                Description = (string)record["Description"],
                DateDebut = (DateTime)record["DateDebut"],
                DateFin = (DateTime)record["DateFin"],
                Photo = (string)record["Photo"],
                LimitPlace = (int)record["LimitePersonne"],
                CreateAt = (DateTime)record["CreateAt"], 
                LastModif = (DateTime)record["ModifiedAt"]
            };
        }

        public Evenement? getById(int id)
        {
            return GetById(id);
        }
    }
}

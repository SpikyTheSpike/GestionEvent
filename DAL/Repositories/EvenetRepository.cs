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

        public  bool Delete(int id,int member_id)
        {

            IDbCommand selec = _connection.CreateCommand();
            selec.CommandType = CommandType.Text;
            selec.CommandText = "SELECT COUNT(*) FROM Event WHERE Event_Id=@Id AND Member_Id=@MId";
             NewMethod(id, "@Id", selec);
            NewMethod(member_id, "@MId", selec);
            _connection.Open();
            int binks = (int)selec.ExecuteScalar();
            _connection.Close();

            if (binks > 0)
            {
                IDbCommand command = _connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE  FROM InscriptionEvent WHERE Event_Id=@lId";
                NewMethod(id, "@lId", command);
                _connection.Open();
                int result1 = command.ExecuteNonQuery();
                _connection.Close();
            }

            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Event WHERE Event_Id=@Id AND Member_Id=@MId";
            NewMethod(id, "@Id", cmd);
            NewMethod(member_id, "@MId", cmd);
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
            command.CommandText = "SELECT * FROM Event  WHERE  isCancel<>1";
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

        public  IEnumerable<Evenement> GetFutur()
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Event WHERE DateDebut > GETDATE() AND isCancel<>1";
            
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

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Cancel(int eventId, int memberId)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Event " +
                "SET isCancel=1 " +
                " WHERE  Event_Id=@Id AND Member_Id=@MId";
            NewMethod(eventId, "@Id", cmd);
            NewMethod(memberId, "@MId", cmd);
            
            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();

            return result;
        }

        public int UnCancel(int eventId, int memberId)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Event " +
                "SET isCancel=0 " +
                " WHERE  Event_Id=@Id AND Member_Id=@MId";
            NewMethod(eventId, "@Id", cmd);
            NewMethod(memberId, "@MId", cmd);

            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();

            return result;
        }

        public void UpdateEvent(Evenement data, int id)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Event SET Nom=@Nom, Description = @Description, DateDebut = @DateDebut, DateFin = @DateFin , Photo=@Photo ,LimitePersonne=@LimitPlace, ModifiedAt= GETDATE() WHERE Event_Id= @EventId AND Member_Id= @id";


            NewMethod(data.Nom, "@Nom", cmd);
            NewMethod(data.Description, "@Description", cmd);
            NewMethod(data.DateDebut, "@DateDebut", cmd);
            NewMethod(data.DateFin, "@DateFin", cmd);
            NewMethod(data.Photo, "@Photo", cmd);
            NewMethod(data.LimitPlace, "@LimitPlace", cmd);
            NewMethod(id, "@id", cmd);
            NewMethod(data.Event_Id, "@EventId", cmd);
            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}

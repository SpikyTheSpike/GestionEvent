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
    public class InscriptionRepository : RepositoryBase<int, Inscription>, IInscription
    {
        public InscriptionRepository(IDbConnection connection) : base(connection)
        {
        }

        public override int Add(Inscription entity)
        {
            IDbCommand selec = _connection.CreateCommand();
            selec.CommandType = CommandType.Text;
            selec.CommandText = "SELECT SUM(NombreParticipant) FROM InscriptionEvent WHERE Event_Id=@Id GROUP BY EVENT_ID ";
            NewMethod(entity.Event_Id, "@Id", selec);
            _connection.Open();
            int? nombrePlacePrie = (int?)selec.ExecuteScalar();
            _connection.Close();

            if (nombrePlacePrie is null)
                nombrePlacePrie = 0;

            IDbCommand selec1 = _connection.CreateCommand();
            selec1.CommandType = CommandType.Text;
            selec1.CommandText = "SELECT LimitePersonne FROM Event WHERE Event_Id=@Event_Id";
            NewMethod(entity.Event_Id, "@Event_Id", selec1);
            _connection.Open();
            int maxPlace = (int)selec1.ExecuteScalar();
            _connection.Close();

            if (nombrePlacePrie + entity.NombrePlace <= maxPlace)
            {
                IDbCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO InscriptionEvent ( NombreParticipant , Member_Id, Event_Id, Remarque) OUTPUT [inserted].[Inscription_Id] VALUES (@NombreParticipant, @Member_Id, @Event_Id, @Remarque)";
                NewMethod(entity.NombrePlace, "@NombreParticipant", cmd);
                NewMethod(entity.Member_Id, "@Member_Id", cmd);
                NewMethod(entity.Event_Id, "@Event_Id", cmd);
                NewMethod(entity.Remarque, "@Remarque", cmd);

                _connection.Open();
                int id = (int)cmd.ExecuteScalar();
                _connection.Close();
                return id;
            }
            else
            {
                throw new Exception();
            }
        }
        private static void NewMethod(object? value, string name, IDbCommand cmd)
        {
            IDbDataParameter DeleteParam = cmd.CreateParameter();
            DeleteParam.ParameterName = name;
            DeleteParam.Value = value ?? DBNull.Value;

            cmd.Parameters.Add(DeleteParam);
        }

        public override bool Delete(int id)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM InscriptionEvent WHERE Inscription_Id=@Id";
            NewMethod(id, "@Id", cmd);
            _connection.Open();
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result == 1;
        }

        public override IEnumerable<Inscription> GetAll()
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM InscriptionEvent";
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

        public override Inscription? GetById(int id)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM InscriptionEvent WHERE Inscription_Id= @Id";
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

        public IEnumerable<Inscription> getInscriptionByEvent(int Event_Id, int Member_Id)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;


            command.CommandText = "SELECT ie.* FROM InscriptionEvent ie JOIN Event e ON ie.Event_Id = e.Event_Id WHERE e.Event_Id=@Id  AND e.Member_Id=@MId";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@Id";
            IdParam.Value = Event_Id;
            command.Parameters.Add(IdParam);
            IDbDataParameter IdParam2 = command.CreateParameter();
            IdParam2.ParameterName = "@MId";
            IdParam2.Value = Member_Id;
            command.Parameters.Add(IdParam2);
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

        public IEnumerable<Inscription> getMyInscription(int Member_Id)
        {
            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;

        
            command.CommandText = "SELECT ie.* " +
                                   "FROM Event e " +
                                   "JOIN InscriptionEvent  ie  on ie.Event_Id=e.Event_Id  " +
                                   "WHERE ie.Member_Id= @Id";
            IDbDataParameter IdParam = command.CreateParameter();
            IdParam.ParameterName = "@Id";
            IdParam.Value = Member_Id;
            command.Parameters.Add(IdParam);
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

        public override bool Update(Inscription entity)
        {
            throw new NotImplementedException();
        }

        protected override Inscription Mapper(IDataRecord record)
        {
            return new Inscription(){
                Inscription_Id= (int)record["Inscription_Id"],
                Event_Id= (int)record["Event_Id"],
                Member_Id= (int)record["Member_Id"],
                Remarque= (string)record["Remarque"],
                NombrePlace= (int)record["NombreParticipant"]
            };
        }

       
    }
}

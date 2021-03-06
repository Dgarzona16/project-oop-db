using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using project.CLASS;

namespace project.CONNECT_SQL
{
    //
    //Interfaz
    //
    public interface SQLReaderWriter
    {
        DataTable consult(string a, string b);
        DataTable GetData(int id);
        bool Delete(int id);
    }
    //
    //Evento
    //
    public class EventSQL : accessSQL, SQLReaderWriter
    {
        public EventSQL() : base()
        {
        }
        public DataTable getAreas()
        {
            DataTable ds = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Nombre FROM AREA";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
            }
            return ds;
        }
        public int getId(NewEvent EventData)
        {
            int id;
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT Id FROM EVENTO WHERE Titulo = '{EventData.Titulo}' AND FechaHora_Inicio = CONVERT(datetime,'{EventData.FechaHora_Inicio}') AND FechaHora_Fin = CONVERT(datetime,'{EventData.FechaHora_Fin}') AND CantidadParticipantes = {EventData.CantidadParticipantes} AND Imagen = '{EventData.Imagen}' AND Id_Area = {EventData.Id_Area}";
                    command.CommandType = CommandType.Text;
                    id = (int)command.ExecuteScalar();
                }
            }
            return id;
        }
        public DataTable consult(string word, string colunm)
        {
            DataTable dt = new DataTable();

            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (colunm == "Nombre de Evento")
                    {
                        command.CommandText = $"SELECT EVENTO.Id, EVENTO.Titulo, EVENTO.FechaHora_Inicio, EVENTO.FechaHora_Fin, EVENTO.CantidadParticipantes, AREA.Nombre AS 'Area' FROM EVENTO INNER JOIN AREA ON EVENTO.Id_Area = AREA.Id WHERE Titulo LIKE '{word}%'";
                    }
                    if (colunm == "Area")
                    {
                        command.CommandText = $"SELECT EVENTO.Id, EVENTO.Titulo, EVENTO.FechaHora_Inicio, EVENTO.FechaHora_Fin, EVENTO.CantidadParticipantes, AREA.Nombre AS 'Area' FROM EVENTO INNER JOIN AREA ON EVENTO.Id_Area = AREA.Id WHERE AREA.Nombre LIKE '{word}%'";
                    }

                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool Insert(NewEvent EventData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO EVENTO (Titulo, Imagen, FechaHora_Inicio, FechaHora_Fin, CantidadParticipantes, Id_Area) VALUES ('{EventData.Titulo}','{EventData.Imagen}', CONVERT(datetime,'{EventData.FechaHora_Inicio}'), CONVERT(datetime,'{EventData.FechaHora_Fin}'), {EventData.CantidadParticipantes}, {EventData.Id_Area})";
                    command.CommandType = CommandType.Text;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void InsertObjetivo(int id, string objetivo)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO OBJETIVO (Id_Evento, Objectivo) VALUES ({id}, '{objetivo}')";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetData(int id)
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT E.Titulo, E.Imagen, E.FechaHora_Inicio, E.FechaHora_Fin,E.CantidadParticipantes, A.Nombre AS 'Area', P.Numero AS 'Piso' FROM EVENTO AS E INNER JOIN AREA AS A ON E.Id_Area = A.Id INNER JOIN PISO AS P ON A.Id_Piso = P.Id WHERE E.Id = {id}";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public bool Delete(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM OBJETIVO WHERE Id_Evento = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM EVENTO WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    //
    //Coleccion
    //
    public class CollectionSQL : accessSQL, SQLReaderWriter
    {
        public CollectionSQL() : base()
        {
        }
        public DataTable getAreas()
        {
            DataTable ds = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Nombre FROM AREA";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
            }
            return ds;
        }
        DataTable SQLReaderWriter.consult(string word, string colunm)
        {
            DataTable dt = new DataTable();

            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (colunm == "Nombre de coleccion")
                    {
                        command.CommandText = $"SELECT COLECCION.Id, COLECCION.Nombre, COLECCION.Tipo, COLECCION.Genero, AREA.Nombre AS 'Area' FROM COLECCION INNER JOIN AREA ON COLECCION.Id_Area = AREA.Id WHERE COLECCION.Nombre LIKE '{word}%'";
                    }
                    if (colunm == "Tipo de coleccion")
                    {
                        command.CommandText = $"SELECT COLECCION.Id, COLECCION.Nombre, COLECCION.Tipo, COLECCION.Genero, AREA.Nombre AS 'Area' FROM COLECCION INNER JOIN AREA ON COLECCION.Id_Area = AREA.Id WHERE COLECCION.Tipo LIKE '{word}%'";
                    }
                    if (colunm == "Genero")
                    {
                        command.CommandText = $"SELECT COLECCION.Id, COLECCION.Nombre, COLECCION.Tipo, COLECCION.Genero, AREA.Nombre AS 'Area' FROM COLECCION INNER JOIN AREA ON COLECCION.Id_Area = AREA.Id WHERE COLECCION.Genero LIKE '{word}%'";
                    }

                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool Insert(NewCollection CollectionData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO COLECCION (Nombre, Tipo, Genero, Id_Area) VALUES ('{CollectionData.Nombre}', '{CollectionData.Tipo}', '{CollectionData.Genero}', '{CollectionData.Id_Area}')";
                    command.CommandType = CommandType.Text;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public DataTable GetData(int id)
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT C.Nombre, C.Tipo, C.Genero, A.Nombre AS 'Area',P.Numero AS 'Piso' FROM COLECCION AS C INNER JOIN AREA AS A ON C.Id_Area = A.Id INNER JOIN PISO AS P ON A.Id_Piso = P.Id WHERE C.Id = {id}";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool Delete(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM COLECCION WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    //
    //Material
    //
    public class MaterialSQL : accessSQL, SQLReaderWriter
    {
        public MaterialSQL() : base()
        {
        }
        public DataTable getFormato()
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Tipo FROM FORMATO";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public DataTable getColecciones()
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Nombre FROM COLECCION";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public int getId(NewMaterial MaterialData)
        {
            int id = 0;
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT Id FROM MATERIAL WHERE Nombre = '{MaterialData.Nombre}' AND Id_Coleccion = '{MaterialData.Id_Coleccion}' AND Id_Formato = '{MaterialData.Id_Formato}'";
                    command.CommandType = CommandType.Text;
                    id = (int)command.ExecuteScalar();
                }
            }
            return id;
        }
        public DataTable consult(string word, string colunm)
        {
            DataTable dt = new DataTable();

            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (colunm == "Nombre del material")
                    {
                        command.CommandText = $"SELECT MATERIAL.Id, MATERIAL.Nombre, MATERIAL.Autor, MATERIAL.Idioma, MATERIAL.Editorial, COLECCION.Nombre AS 'Coleccion' FROM MATERIAL INNER JOIN COLECCION ON MATERIAL.Id_Coleccion = COLECCION.Id WHERE MATERIAL.Nombre LIKE '{word}%'";
                    }
                    if (colunm == "Autor")
                    {
                        command.CommandText = $"SELECT MATERIAL.Id, MATERIAL.Nombre, MATERIAL.Autor, MATERIAL.Idioma, MATERIAL.Editorial, COLECCION.Nombre AS 'Coleccion' FROM MATERIAL INNER JOIN COLECCION ON MATERIAL.Id_Coleccion = COLECCION.Id WHERE MATERIAL.Autor LIKE '{word}%'";
                    }
                    if (colunm == "Formato")
                    {
                        command.CommandText = $"SELECT MATERIAL.Id, MATERIAL.Nombre, MATERIAL.Autor, MATERIAL.Idioma, MATERIAL.Editorial, COLECCION.Nombre AS 'Coleccion' FROM MATERIAL INNER JOIN COLECCION ON MATERIAL.Id_Coleccion = COLECCION.Id INNER JOIN FORMATO ON MATERIAL.Id_Formato = FORMATO.Id WHERE FORMATO.Tipo LIKE '{word}%'";
                    }
                    if (colunm == "Palabras clave")
                    {
                        command.CommandText = $"SELECT MATERIAL.Id, MATERIAL.Nombre, MATERIAL.Autor, MATERIAL.Idioma, MATERIAL.Editorial, COLECCION.Nombre AS 'Coleccion' FROM MATERIAL INNER JOIN COLECCION ON MATERIAL.Id_Coleccion = COLECCION.Id INNER JOIN PALABRAS ON PALABRAS.Id_Material = MATERIAL.Id WHERE PALABRAS.Palabra LIKE '{word}%'";
                    }

                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool Insert(NewMaterial materialData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO MATERIAL (Nombre, Portada, Autor, Idioma, Editorial, Fecha_Publicacion,Id_Formato, Id_Coleccion) VALUES ('{materialData.Nombre}', '{materialData.Portada}', '{materialData.Autor}', '{materialData.Idioma}', '{materialData.Editorial}', CONVERT(date,'{materialData.FechaPublicacion}'), '{materialData.Id_Formato}', '{materialData.Id_Coleccion}')";
                    command.CommandType = CommandType.Text;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void InsertEtiqueta(int id, string etiqueta)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO ETIQUETA (Id_Material, Codigo) VALUES ('{id}', '{etiqueta}')";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertPalabra(int id, string palabra)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO PALABRAS (Id_Material, Palabra) VALUES ('{id}', '{palabra}')";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetData(int id)
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT M.Nombre, M.Portada, M.Autor, M.Idioma, M.Editorial, M.Fecha_Publicacion, F.Tipo AS 'Formato', C.Nombre AS 'Coleccion' FROM MATERIAL AS M INNER JOIN FORMATO AS F ON M.Id_Formato = F.Id INNER JOIN COLECCION AS C ON M.Id_Coleccion = C.Id WHERE M.Id = {id}";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public bool Delete(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM PRESTAMO WHERE Id_Material = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM RESERVA WHERE Id_Material = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM ETIQUETA WHERE Id_Material = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM PALABRAS WHERE Id_Material = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM MATERIAL WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    //
    //Reserva
    //
    public class ReserveSQL : accessSQL, SQLReaderWriter
    {
        public ReserveSQL() : base()
        {
        }
        public DataTable consult(string word, string colunm)
        {
            DataTable dt = new DataTable();

            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (colunm == "Reserva / Usuario")
                    {
                        command.CommandText = $"SELECT RESERVA.Id, USUARIO.Nombre AS 'Usuario', RESERVA.FechaHora_Reserva AS 'Fecha_Reserva', RESERVA.FechaHora_Prestamo AS 'Fecha_Prestamo', RESERVA.FechaHora_Devolucion AS 'Fecha_Devolucion', MATERIAL.Nombre AS 'Objeto_Prestado'FROM RESERVA INNER JOIN USUARIO ON RESERVA.Id_Usuario = USUARIO.Id INNER JOIN MATERIAL ON RESERVA.Id_Material = MATERIAL.Id WHERE USUARIO.Nombre LIKE '{word}%'";
                    }
                    if (colunm == "Prestamo / Usuario")
                    {
                        command.CommandText = $"SELECT PRESTAMO.Id, USUARIO.Nombre AS 'Usuario', PRESTAMO.FechaHora_Prestamo AS 'Fecha Prestamo', PRESTAMO.FechaHora_Devolucion AS 'Fecha devolucion', MATERIAL.Nombre AS 'Objeto prestado'FROM PRESTAMO INNER JOIN USUARIO ON PRESTAMO.Id_Usuario = USUARIO.Id INNER JOIN MATERIAL ON PRESTAMO.Id_Material = MATERIAL.Id WHERE USUARIO.Nombre LIKE '{word}%'";
                    }

                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool validBorrow(int idMaterial, string fechaA, string fechaB)
        {
            bool existent = true;
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT id FROM PRESTAMO WHERE Id_Material = {idMaterial} AND FechaHora_Prestamo BETWEEN CAST('{fechaA}' AS DATETIME) AND CAST('{fechaB}' AS DATETIME)";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return false;
                    }
                }
            }
            return existent;
        }
        public bool InsertPrestamo(NewReservation ReserveData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO PRESTAMO (Id_Usuario, Id_Material, FechaHora_Prestamo, FechaHora_Devolucion) VALUES ('{ReserveData.Id_Usuario}', '{ReserveData.Id_Material}', CONVERT(datetime,'{ReserveData.FechaHora_Prestamo}'), CONVERT(datetime,'{ReserveData.FechaHora_Devolucion}'))";
                    command.CommandType = CommandType.Text;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool InsertReserva(NewReservation ReserveData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO RESERVA (Id_Usuario, Id_Material, FechaHora_Reserva, FechaHora_Prestamo, FechaHora_Devolucion) VALUES ({ReserveData.Id_Usuario}, {ReserveData.Id_Material},CONVERT(datetime,'{ReserveData.FechaHora_Reserva}'), CONVERT(datetime,'{ReserveData.FechaHora_Prestamo}'), CONVERT(datetime,'{ReserveData.FechaHora_Devolucion}'))";
                    command.CommandType = CommandType.Text;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public DataTable GetData(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM PRESTAMO WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteR(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM Reserva WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    //
    //Usuario
    //
    public class UserSQL : accessSQL, SQLReaderWriter
    {
        public UserSQL() : base()
        {
        }
        public DataTable consult(string word, string colunm)
        {
            DataTable dt = new DataTable();

            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (colunm == "Nombre")
                    {
                        command.CommandText = $"SELECT Id, Nombre, Direccion, Institucion, Telefono, Correo, Ocupacion FROM USUARIO WHERE Nombre LIKE '{word}%'";
                    }
                    if (colunm == "Ocupacion")
                    {
                        command.CommandText = $"SELECT Id, Nombre, Direccion, Institucion, Telefono, Correo, Ocupacion FROM USUARIO WHERE Nombre LIKE '{word}%'";
                    }

                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetData(int id)
        {
            DataTable dt = new DataTable();
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT Id, Nombre, Direccion, Institucion, Telefono, Correo, Ocupacion, Fotografia FROM USUARIO WHERE Id = {id}";
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public bool Insert(NewUser UserData)
        {
            using (var connection = getConnection())
            {
                connection.Open();
                try
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"INSERT INTO USUARIO (Nombre, Direccion, Institucion, Telefono, Correo, Fotografia, Ocupacion) VALUES ('{UserData.Nombre}', '{UserData.Direccion}', '{UserData.Institucion}', '{UserData.Telefono}', '{UserData.Correo}', '{UserData.Fotografia}', '{UserData.Ocupacion}')";
                        command.CommandType = CommandType.Text;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (var connection = getConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM PRESTAMO WHERE Id_Usuario = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM RESERVA WHERE Id_Usuario = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"DELETE FROM USUARIO WHERE Id = {id}";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
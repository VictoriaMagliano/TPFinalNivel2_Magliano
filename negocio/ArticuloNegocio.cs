using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data.SqlClient;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

           

            try
            {

                datos.setearConsulta("Select A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio From ARTICULOS A, CATEGORIAS C, MARCAS M Where M.Id =A.IdMarca and C.Id=A.IdCategoria");
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Articulo aux= new Articulo();
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    // aux.IdMarca = (int)lector["IdMarca"];
                    //aux.IdCategoria = (int)lector["IdCategoria"];
                    aux.Marca=new Marca();
                    aux.Marca.Descripcion = (String)datos.Lector["Marca"];
                    aux.Categoria =new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    
                    if(!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }


        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl,Precio) values ('" + nuevo.Codigo + "','" + nuevo.Nombre + "','" + nuevo.Descripcion + "',@idMarca, @idCategoria,@imagenUrl + nuevo.Precio + ")");
                datos.setearConsulta("Insert into ARTICULOS (Codigo,Nombre,Descripcion,Precio,IdMarca, IdCategoria, ImagenUrl) values ('" + nuevo.Codigo + "','" + nuevo.Nombre + "','" + nuevo.Descripcion + "'," + nuevo.Precio + ",@IdMarca,@IdCategoria, @imagenUrl)");
                datos.setearParametros("@idMarca", nuevo.Marca.Id);
                datos.setearParametros("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@imagenUrl", nuevo.ImagenUrl);
                datos.ejecutarAccion();         
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}


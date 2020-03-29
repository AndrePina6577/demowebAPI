using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demowebAPI.Controllers
{
    public class CategoriasController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: api/Categorias
        public List<Categoria> Get()
        {
            var lista = from Categoria in dc.Categorias select Categoria;

            return lista.ToList();
        }

        // GET: api/Categorias/AC
        [Route("api/Categorias/{sigla}")]
        public IHttpActionResult Get(string sigla)
        {
            var categoria = dc.Categorias.SingleOrDefault(c => c.Sigla == sigla);

            if (categoria != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, categoria));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não localizada"));
        }

        // POST: api/Categorias
        public IHttpActionResult Post([FromBody]Categoria novaCategoria)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novaCategoria.Sigla);

            if (categoria != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict,
                    $"Já existe uma categoria com a sigla {novaCategoria.Sigla}"));
            }

            dc.Categorias.InsertOnSubmit(novaCategoria);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

        // PUT: api/Categorias/5
        public IHttpActionResult Put([FromBody]Categoria editCategoria)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == editCategoria.Sigla);

            if (categoria != null)
            {
                categoria.Sigla = editCategoria.Sigla;
                categoria.Categoria1 = editCategoria.Categoria1;

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não localizada"));
        }

        // DELETE: api/Categorias/CM
        [Route("api/Categorias/{sigla}")]
        public IHttpActionResult Delete(string sigla)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == sigla);
        
            if (categoria != null)
            {
                dc.Categorias.DeleteOnSubmit(categoria);

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não localizada"));
        }
    }
}

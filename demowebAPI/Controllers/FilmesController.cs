using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demowebAPI.Controllers
{
    public class FilmesController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: api/Filmes
        public List<Filme> Get()
        {
            var lista = from filme in dc.Filmes orderby filme.Titulo select filme;

            return lista.ToList();
        }

        // GET: api/Filmes/5
        public IHttpActionResult Get(int id)
        {
            var filme = dc.Filmes.SingleOrDefault(f => f.Id == id);

            if (filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Filme não encontrado"));
        }

        // POST: api/Filmes
        public IHttpActionResult Post([FromBody]Filme novoFilme)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == novoFilme.Id);

            if (filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"Filme com id {novoFilme.Id} já se encontra existeste."));
            }

            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novoFilme.Categoria);

            if (categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe ainda essa categoria. Vá a categorias primeiro."));
            }

            dc.Filmes.InsertOnSubmit(novoFilme);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
        }

        // PUT: api/Filmes/5
        public IHttpActionResult PUT([FromBody]Filme editFilme)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == editFilme.Id);

            if (filme == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Filme não existente"));
            }

            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == editFilme.Categoria);

            if (categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não existente"));
            }

            filme.Titulo = editFilme.Titulo;
            filme.Categoria = editFilme.Categoria;

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
        }

        // DELETE: api/Filmes/5
        public IHttpActionResult Delete(int id)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme != null)
            {
                dc.Filmes.DeleteOnSubmit(filme);

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Filme não existente"));
        }
    }
}

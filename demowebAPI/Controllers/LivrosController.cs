using demowebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demowebAPI.Controllers
{
    public class LivrosController : ApiController
    {
        // GET: api/Livros
        public List<Livro> Get()
        {
            return Biblioteca.Livros;
        }

        // GET: api/Livros/5
        public IHttpActionResult Get(int id)
        {
            Livro livro = Biblioteca.Livros.FirstOrDefault(x => x.Id == id);

            if (livro != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, livro));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não localizado"));
        }

        // POST: api/Livros
        public IHttpActionResult Post([FromBody]Livro obj)
        {
            Livro livro = Biblioteca.Livros.FirstOrDefault(x => x.Id == obj.Id);

            if (livro != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, $"Já existe um livro registado com o Id {obj.Id}"));
            }

            Biblioteca.Livros.Add(obj);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

        // PUT: api/Livros/5
        public IHttpActionResult Put([FromBody]Livro obj)
        {
            Livro livro = Biblioteca.Livros.FirstOrDefault(x => x.Id == obj.Id);

            if (livro != null)
            {
                livro.Titulo = obj.Titulo;
                livro.Autor = obj.Autor;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Livro alterado com sucesso"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não localizado"));

        }

        // DELETE: api/Livros/5
        public IHttpActionResult Delete(int id)
        {
            Livro livro = Biblioteca.Livros.FirstOrDefault(x => x.Id == id);

            if (livro != null)
            {
                Biblioteca.Livros.Remove(livro);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Livro apagado com sucesso"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Livro não localizado"));
        }
    }
}

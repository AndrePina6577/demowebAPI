using demowebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demowebAPI.Controllers
{
    public class EmpregadosController : ApiController
    {
        private List<Empregado> Funcionarios;

        public EmpregadosController()
        {
            Funcionarios = new List<Empregado>
            {
                new Empregado { Id = 1, Nome = "André", Apelido = "Pina" },
                new Empregado { Id = 2, Nome = "Rodrigo", Apelido = "Pina" },
                new Empregado { Id = 3, Nome = "Ângela", Apelido = "Oliveira" }
            };
        }

        // GET: api/Empregado
        public List<Empregado> Get()
        {
            return Funcionarios;
        }

        // GET: api/Empregado/5
        /// <summary>
        /// Dados completos do empregado
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>retorna empregado</returns>
        public Empregado Get(int id)
        {
            return Funcionarios.FirstOrDefault(x => x.Id == id);
        }


        //GET: api/Empregados/GetNomes
        /// <summary>
        /// Nome próprio de todos os empregados
        /// </summary>
        /// <returns>lista com os nomes de todos os empregados</returns>
        [Route("api/Empregados/GetNomes")]
        public List<string> GetNomes()
        {
            List<string> output = new List<string>();

            foreach (var f in Funcionarios)
            {
                output.Add(f.Nome);
            }

            return output;
        }

        // POST: api/Empregados
        /// <summary>
        /// Registo de novo empregado
        /// </summary>
        /// <param name="novoEmp">Empregado</param>
        public void Post([FromBody]Empregado novoEmp)
        {
            Funcionarios.Add(novoEmp);
        }

        // PUT: api/Empregados/5
        /*public void Put(int id, [FromBody]string value)
        {
        }*/

        // DELETE: api/Empregado/5
        public void Delete(int id)
        {
            Funcionarios.Remove(Funcionarios.Find(x => x.Id == id));
        }
    }
}

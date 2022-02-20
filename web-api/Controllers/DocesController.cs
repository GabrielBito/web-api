using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace web_api.Controllers
{
    public class DocesController : ApiController
    {
        // GET: api/Doces
        public IHttpActionResult Get()
        {
            List<Models.Doce> doces = RepositoriesEntity.Doce.getAll();
            return Ok(doces);
        }

        public IHttpActionResult Get(string descricao)
        {
            List<Models.Doce> doces = RepositoriesEntity.Doce.getByDescricao(descricao);
            return Ok(doces);
        }

        // GET: api/Doces/5
        public IHttpActionResult Get(int id)
        {
            Models.Doce doce = RepositoriesEntity.Doce.getById(id);
            return Ok(doce);
        }

        // POST: api/Doces
        public IHttpActionResult Post([FromBody]Models.Doce doce)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                RepositoriesEntity.Doce.save(doce);
                return Ok(doce);
            }
            catch (Exception ex)
            {
                Utils.Log.gravar(ex);
                return InternalServerError();
            }
        }

        // PUT: api/Doces/5
        public IHttpActionResult Put(int id, [FromBody]Models.Doce doce)
        {
            try
            {
                doce.Id = id;
                RepositoriesEntity.Doce.update(doce);
                return Ok();
            }
            catch (Exception ex)
            {
                Utils.Log.gravar(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Doces/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                RepositoriesEntity.Doce.deleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Utils.Log.gravar(ex);
                return InternalServerError();
            }
        }
    }
}

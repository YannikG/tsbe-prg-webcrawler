using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public ActionResult<List<ArticleEntity>> GetAll()
        {
            var result = _articleRepository.GetAll();

            return Ok(result);
        }
    }
}

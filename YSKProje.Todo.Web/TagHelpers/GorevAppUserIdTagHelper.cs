using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.Todo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.Todo.Web.TagHelpers
{
    [HtmlTargetElement("getirGorevAppUserId")]
    public class GorevAppUserIdTagHelper : TagHelper
    {
        private readonly IGorevService _gorevService;

        public GorevAppUserIdTagHelper(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public int AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Gorev> gorevler = _gorevService.GetirileAppUserId(AppUserId);
            int tamamlananlarGorevSayisi = gorevler.Where(I => I.Durum).Count();
            int ustundecalistigiGorevSayisi = gorevler.Where(I => I.Durum == false).Count();

            string htmlString = $"<strong>Tamamladığı görev sayısı : </strong> {tamamlananlarGorevSayisi} <br/> <strong>Üstünde çalıştığı görev sayısı : </strong>{ustundecalistigiGorevSayisi}";

            output.Content.SetHtmlContent(htmlString);

        }
    }
}

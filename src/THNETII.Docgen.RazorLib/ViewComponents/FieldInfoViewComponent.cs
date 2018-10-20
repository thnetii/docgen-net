using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace THNETII.Docgen.RazorLib.ViewComponents
{
    public class FieldInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FieldInfo fieldInfo) =>
            View(fieldInfo);
    }
}

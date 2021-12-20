using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

namespace WebManagement.Extensions
{
    public static class HtmlExtensionCustom
    {
        public static IHtmlString EmailInput(this HtmlHelper html, string name, string value)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("type", "email");
            tagBuilder.Attributes.Add("value", value);
            return new HtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString EnumDescriptionDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var type = typeof(TEnum);

            var enums = MultiPack.EnumLib.GetAllEnumsAndDescriptions(type);

            //IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items = enums.Select(item => new SelectListItem
            {
                Text = item.Value,
                Value = item.Key.ToString(),
                Selected = item.Key.Equals(metadata.Model)
            });

            var itemsSelect = new SelectList(items, "Value", "Text", metadata.Model);

            return htmlHelper.DropDownListFor(expression, itemsSelect, htmlAttributes);
        }

        public static MvcHtmlString DataEntityDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<Data.ViewModels.Shared._BaseEntityViewModel> entities, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IEnumerable<SelectListItem> items = entities.Select(item => new SelectListItem
            {
                Text = item.ToString(),
                Value = item.Id.ToString(),
                Selected = item.Id.Equals(metadata.Model)
            });

            var itemsSelect = new SelectList(items, "Value", "Text", metadata.Model);

            return htmlHelper.DropDownListFor(expression, itemsSelect, "", htmlAttributes);
        }

    }
}
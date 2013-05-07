using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace Grana.Portugal.ViewHelpers
{
    public static class FormHelpers
    {
        public static MvcHtmlString DropDownListMine(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<select id=\"{0}\" name=\"{0}\">", name);

            foreach (SelectListItem listItem in selectList)
            {
                builder.AppendFormat("<option value=\"{0}\"", listItem.Value);

                if (listItem.Selected)
                    builder.Append(" selected=\"selected\"");

                builder.Append(">");
                builder.Append(listItem.Text);
                builder.Append("</option>");
            }

            builder.Append("</select>");

            return new MvcHtmlString(builder.ToString());
        }

       

        public static MvcHtmlString DropDownList<TModel>(this HtmlHelper helper, string name, IEnumerable<TModel> modelList, Func<TModel, string> textFunc, Func<TModel, string> valFunc, string selectedVal, bool addEmptyItem) where TModel : class
        {
            return DropDownList(helper, name, modelList, textFunc, valFunc, (model, currentVal) => currentVal == selectedVal, addEmptyItem);
        }

        public static MvcHtmlString DropDownList<TModel>(this HtmlHelper helper, string name, IEnumerable<TModel> modelList, Func<TModel, string> textFunc, Func<TModel, string> valFunc, Func<TModel, string, bool> isSelectedFunc, bool addEmptyItem) where TModel : class
        {

            List<SelectListItem> result = new List<SelectListItem>();

            if (addEmptyItem)
            {
                SelectListItem emptyItem = new SelectListItem();
                emptyItem.Text = "";
                emptyItem.Value = "";
                emptyItem.Selected = isSelectedFunc(null, "");
                result.Add(emptyItem);
            }

            foreach (TModel item in modelList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Text = textFunc(item);
                selectItem.Value = valFunc(item);
                selectItem.Selected = isSelectedFunc(item, selectItem.Value);
                result.Add(selectItem);
            }

            return DropDownListMine(helper, name, result);

        }

        public static MvcHtmlString LabelForMine<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            string propName = (property.Body as MemberExpression).Member.Name;
            return LabelForMine(helper, property, propName);
        }

        public static MvcHtmlString LabelForMine<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, string labelText)
        {
            string expressionText = ExpressionHelper.GetExpressionText(property);

            StringBuilder builder = new StringBuilder();
            bool isFirst = true;

            foreach (char c in labelText)
            {
                if ((char.IsUpper(c) || char.IsDigit(c)) && !isFirst)
                    builder.Append(" ");

                builder.Append(c);

                isFirst = false;
            }

            labelText = builder.ToString();

            string labelForId = expressionText.Replace('[', '_').Replace(']', '_').Replace('.', '_');
            return new MvcHtmlString(string.Format("<label for=\"{0}\">{1}</label>", labelForId, labelText));
        }

        public static MvcHtmlString RadioButtonAndLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, string radioButtonValue, string selectedVal)
        {
            return RadioButtonAndLabelFor(helper, property, radioButtonValue, radioButtonValue, selectedVal);
        }

        public static MvcHtmlString RadioButtonAndLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, string radioButtonValue, string labelText, string selectedVal)
        {
            string expressionText = ExpressionHelper.GetExpressionText(property);
            string radioButtonGroup = expressionText.Replace('[', '_').Replace(']', '_').Replace('.', '_');

            string radioId = radioButtonGroup + "_" + radioButtonValue.Replace(" ", "");
            string labelHtml = string.Format("<label for=\"{0}\">{1}</label>", radioId, labelText);

            string setSelectedHtml = string.Empty;


            if (radioButtonValue == selectedVal)
                setSelectedHtml = "checked=\"checked\"";

            string radioButtonHtml = string.Format(@"<input type=""radio"" id=""{0}"" name=""{1}"" value=""{2}""{3} />", radioId, radioButtonGroup, radioButtonValue, setSelectedHtml);

            return new MvcHtmlString(radioButtonHtml + labelHtml);
        }

        public static MvcHtmlString CheckboxMine<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, string checkboxValue, string labelText, string selectedVal)
        {
            string expressionText = ExpressionHelper.GetExpressionText(property);
            string checkboxBoxGroup = expressionText.Replace('[', '_').Replace(']', '_').Replace('.', '_');

            string chkId = checkboxBoxGroup + "_" + checkboxValue.Replace(" ", "");
            string labelHtml = string.Format("<label for=\"{0}\">{1}</label>", chkId, labelText);

            string setSelectedHtml = string.Empty;


            if (checkboxValue == selectedVal)
                setSelectedHtml = "checked=\"checked\"";

            string checkBoxHtml = string.Format(@"<input type=""checkbox"" id=""{0}"" name=""{1}"" value=""{2}""{3} />", chkId, checkboxBoxGroup, checkboxValue, setSelectedHtml);

            return new MvcHtmlString(checkBoxHtml + labelHtml);
        }

        public static MvcHtmlString CheckboxAndLabelFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> property, string labelText, object chkHtmlAttributes = null)
        {
            MvcHtmlString label = helper.LabelFor(property, labelText);
            MvcHtmlString checkbox = helper.CheckBoxFor(property, chkHtmlAttributes);

            return new MvcHtmlString(checkbox.ToHtmlString() + label.ToHtmlString());
        }

        public static MvcHtmlString TextBoxAndValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            return TextBoxAndValidationFor(helper, property, null);
        }

        public static MvcHtmlString TextBoxAndValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property, object textBoxProperties)
        {
            MvcHtmlString textString = helper.TextBoxFor(property, textBoxProperties);

            //Reduce it if indexing into a List to just be the name instead
            string propName = (property.Body as MemberExpression).Member.Name;
            MvcHtmlString validationString = helper.ValidationMessage(propName);

            string validationHtml = validationString != null ? validationString.ToHtmlString() : "";
            return new MvcHtmlString(textString.ToHtmlString() + validationHtml);
        }

        public static MvcHtmlString PhoneLink(this HtmlHelper helper, string number)
        {
            if (string.IsNullOrEmpty(number))
                return new MvcHtmlString("");

            number = number.Replace(" ", "");

            if (number.StartsWith("0"))
                number = number.Substring(1);

            return new MvcHtmlString(string.Format("<a href=\"callto:+44{0}\">Call</a>", number));
        }

    }
}
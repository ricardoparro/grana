
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grana.Portugal.ViewHelpers
{
    public class ViewSelectItemFactory 
    {
        public static List<SelectListItem> AddAllInNumericRange(int fromInclusive, int toInclusive, int selectedVal, int interval, bool addEmpty)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            if (addEmpty)
                items.Add(new SelectListItem());

            for (int i = fromInclusive; i <= toInclusive; i = i + interval)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();

                item.Selected = i == selectedVal;

                items.Add(item);
            }

            return items;
        }

        public static  List<SelectListItem> AddMaritalStatus(string selectedVal)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Solteiro(a)", Value = "Single", Selected = selectedVal == "Single" });
            items.Add(new SelectListItem { Text = "Casado(a)", Value = "Married", Selected = selectedVal == "Married" });
            items.Add(new SelectListItem { Text = "Divorciado(a)", Value = "Divorced", Selected = selectedVal == "Divorced" });
            items.Add(new SelectListItem { Text = "Viúvo(a)", Value = "Widower", Selected = selectedVal == "Widower"});
            items.Add(new SelectListItem { Text = "Vive junto", Value = "LivingTogether", Selected = selectedVal == "LivingTogether" });
            items.Add(new SelectListItem { Text = "Separado(a)", Value = "Separated", Selected = selectedVal == "Separated" });
            items.Add(new SelectListItem { Text = "Outro", Value = "Other", Selected = selectedVal == "Other" });

            return items;
        }

        public static List<SelectListItem> AddEmploymentStatus(string selectedVal)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Full Time", Value = "FullTime", Selected = selectedVal == "FullTime" });
            items.Add(new SelectListItem { Text = "Part time", Value = "PartTime", Selected = selectedVal == "PartTime" });
            items.Add(new SelectListItem { Text = "Temporario", Value = "Temporary", Selected = selectedVal == "Temporary" });
            items.Add(new SelectListItem { Text = "Freelance", Value = "SelfEmployed", Selected = selectedVal == "SelfEmployed" });
            items.Add(new SelectListItem { Text = "Estudante", Value = "Student", Selected = selectedVal == "Student" });
            items.Add(new SelectListItem { Text = "Domestico(a)", Value = "HomeMaker", Selected = selectedVal == "HomeMaker" });
            items.Add(new SelectListItem { Text = "Reformado(a)", Value = "Retired", Selected = selectedVal == "Retired" });
            items.Add(new SelectListItem { Text = "Desempregado(a) sem benefícios", Value = "Unemployed", Selected = selectedVal == "Unemployed" });
            items.Add(new SelectListItem { Text = "Desempregado(a) com benefícios", Value = "OnBeneficts", Selected = selectedVal == "OnBeneficts" });

            return items;

        }

        public static List<SelectListItem> AddIndustry(string selectedVal)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Alimentos e Bebidas", Value = "FoodBeverages", Selected = selectedVal == "FoodBeverages" });
            items.Add(new SelectListItem { Text = "Animais", Value = "PetsAnimals", Selected = selectedVal == "PetsAnimals" });
            items.Add(new SelectListItem { Text = "Aulas e Cursos", Value = "ClassesCourses", Selected = selectedVal == "ClassesCourses" });
            items.Add(new SelectListItem { Text = "Automotivos", Value = "Automotive", Selected = selectedVal == "Automotive" });
            items.Add(new SelectListItem { Text = "Casa e Decoração", Value = "HomeDecoration", Selected = selectedVal == "HomeDecoration" });
            items.Add(new SelectListItem { Text = "Consultoria", Value = "Consultancy", Selected = selectedVal == "Consultancy" });
            items.Add(new SelectListItem { Text = "Contabilidade", Value = "Accounting", Selected = selectedVal == "Accounting" });
            items.Add(new SelectListItem { Text = "Distribuidores", Value = "Logistics", Selected = selectedVal == "Logistics" });
            items.Add(new SelectListItem { Text = "Eletroeletrônicos", Value = "Electronics", Selected = selectedVal == "Electronics" });
            items.Add(new SelectListItem { Text = "Eventos", Value = "Events", Selected = selectedVal == "Events" });
            items.Add(new SelectListItem { Text = "Financeiro", Value = "Finance", Selected = selectedVal == "Finance" });
            items.Add(new SelectListItem { Text = "Gráficas", Value = "PrintDesign", Selected = selectedVal == "PrintDesign" });
            items.Add(new SelectListItem { Text = "Imóveis", Value = "RealEstate", Selected = selectedVal == "RealEstate" });
            items.Add(new SelectListItem { Text = "Indústrias", Value = "Manufacturing", Selected = selectedVal == "Manufacturing" });
            items.Add(new SelectListItem { Text = "Informática", Value = "ComputerServices", Selected = selectedVal == "ComputerServices" });
            items.Add(new SelectListItem { Text = "Jurídico", Value = "LegalServices", Selected = selectedVal == "LegalServices" });
            items.Add(new SelectListItem { Text = "Materiais", Value = "Retail", Selected = selectedVal == "Retail" });
            items.Add(new SelectListItem { Text = "Moda e Beleza", Value = "FashionBeauty", Selected = selectedVal == "FashionBeauty" });
            items.Add(new SelectListItem { Text = "Musica", Value = "MusicIndustry", Selected = selectedVal == "MusicIndustry" });
            items.Add(new SelectListItem { Text = "Publicidade", Value = "Advertisement", Selected = selectedVal == "Advertisement" });
            items.Add(new SelectListItem { Text = "Religião", Value = "Religion", Selected = selectedVal == "Religion" });
            items.Add(new SelectListItem { Text = "Representantes", Value = "Representatives", Selected = selectedVal == "Representatives" });
            items.Add(new SelectListItem { Text = "Restaurantes", Value = "Restaurants", Selected = selectedVal == "Restaurants" });
            items.Add(new SelectListItem { Text = "Saúde", Value = "HealthCare", Selected = selectedVal == "HealthCare" });
            items.Add(new SelectListItem { Text = "Segurança", Value = "PublicSafety", Selected = selectedVal == "PublicSafety" });
            items.Add(new SelectListItem { Text = "Terceirizados", Value = "Outsourcing", Selected = selectedVal == "Outsourcing" });
            items.Add(new SelectListItem { Text = "Turismo e Lazer", Value = "LeisureTurism", Selected = selectedVal == "LeisureTurism" });

            return items;

        }

        public static List<SelectListItem> AddIncomeFrequency(string selectedVal)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Mensalmente", Value = "Monthly", Selected = selectedVal == "Monthly" });
            items.Add(new SelectListItem { Text = "Duas vezes ao mês", Value = "TwiceMonthly", Selected = selectedVal == "TwiceMonthly" });
            items.Add(new SelectListItem { Text = "Semanalmente", Value = "Weekly", Selected = selectedVal == "Weekly" });
            items.Add(new SelectListItem { Text = "Ultimo dia do mês", Value = "LastDayOfMonth", Selected = selectedVal == "LastDayOfMonth" });
            items.Add(new SelectListItem { Text = "Ultima sexta-feira do mês", Value = "LastDayOfMonth", Selected = selectedVal == "LastDayOfMonth" });

            return items;
        }

    }
}

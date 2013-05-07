using Grana.Model.EnumModel;

namespace Grana.Model
{
    partial class Application
    {
        public ApplicationStatuses ApplicationStatusAsEnum
        {
            get { return EnumExtension.Parse<ApplicationStatuses>(AppStatus).Value; }
            set { AppStatus = value.ToString(); }
        }

        public static string GetPortugueseNameOfStatus(ApplicationStatuses status)
        {
            switch (status)
            {
                case ApplicationStatuses.Approved:
                    return "Aprovado";

                case ApplicationStatuses.Declined:
                    return "Rejeitado";

                case ApplicationStatuses.New:
                    return "Nova Entrada";

                case ApplicationStatuses.Undecided:
                    return "Pendente";
            }

            return "";
        }
    }
}

using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Web.Extension
{
    /// <summary>
    /// Статический класс предоставляющий методы расширения для отображения данных в виде Json
    /// </summary>
    public static class JsonViews
    {
        /// <summary>
        /// Метод расширения для отображения города
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static object CityView(this City city)
        {
            if (city != null)
            {
                return new
                {
                    city.Id,
                    city.Name
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения професии
        /// </summary>
        /// <param name="prof"></param>
        /// <returns></returns>
        public static object ProfView(this ProfessionalArea prof)
        {
            if (prof != null)
            {
                return new
                {
                    prof.Id,
                    prof.Name
                };
            }
            return null;
        }
    }
}

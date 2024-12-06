namespace WarspearOnlineApi.Enums.BaseRecordDB
{
    /// <summary>
    /// Дропы.
    /// </summary>
    public class DropEnum
    {
        public static readonly string vyaz_krit_dd = "Крит вяз дд";
        public static readonly string vyaz_krit_hil = "Крит вяз маг";
        public static readonly string inj_fiz = "Крит инж физ";
        public static readonly string inj_mag = "Крит инж маг";
        public static readonly string ork = "орк";
        public static readonly string sprut = "спрут";
        public static readonly string jija_db = "жижа дб";
        public static readonly string jija_bb = "жижа бб";
        public static readonly string demon = "демон";

        /// <summary>
        /// Получить картинку.
        /// </summary>
        /// <param name="name">Название дропа (результат nameof).</param>
        /// <returns>Картинка.</returns>
        public static string GetImage(string name)
        {
            var image = name switch
            {
                nameof(vyaz_krit_dd) => Properties.Resources.vyaz_krit_dd,
                nameof(vyaz_krit_hil) => Properties.Resources.vyaz_krit_hil,
                nameof(inj_fiz) => Properties.Resources.inj_fiz,
                nameof(inj_mag) => Properties.Resources.inj_mag,
                nameof(ork) => Properties.Resources.ork,
                nameof(sprut) => Properties.Resources.sprut,
                nameof(jija_db) => Properties.Resources.jija_db,
                nameof(jija_bb) => Properties.Resources.jija_bb,
                nameof(demon) => Properties.Resources.demon,
                _ => throw new Exception("Такого дропа нет"),
            };

            return Convert.ToBase64String(image);
        }
    }
}

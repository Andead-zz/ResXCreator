namespace ResXCreator
{
    /// <summary>
    ///     Ресурс
    /// </summary>
    public class StringResource
    {
        /// <summary>
        ///     Без параметров
        /// </summary>
        public StringResource()
        {
        }

        /// <summary>
        ///     С параметрами
        /// </summary>
        /// <param name="name">Имя ресурса</param>
        /// <param name="value">Значение ресурса</param>
        public StringResource(string name, string value)
        {
            ResourceName = name;
            Value = value;
        }

        /// <summary>
        ///     Имя - идентефикатор
        /// </summary>
        public string ResourceName { set; get; }

        /// <summary>
        ///     Значение
        /// </summary>
        public string Value { set; get; }

        /// <summary>
        ///     Примечание
        /// </summary>
        public string Comment { set; get; }

        /// <summary>
        ///     Номер ресурса
        /// </summary>
        public int ID { set; get; }
    }
}
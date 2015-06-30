namespace KPK_Translate
{
    /// <summary>
    ///     Результаты операции
    /// </summary>
    public enum OperationResults
    {
        /// <summary>
        ///     Все ресурсы заменены
        /// </summary>
        TranslateOk,

        /// <summary>
        ///     Некоторые ресурсы не заменены
        /// </summary>
        SomeResourceNotTranslated,

        /// <summary>
        ///     Ни один ресурс не заменён
        /// </summary>
        AllIsNotTranslated,

        /// <summary>
        ///     Не был найден файл ресурсов
        /// </summary>
        FileNotFound
    }
}